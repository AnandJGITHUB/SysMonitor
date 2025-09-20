// Program.cs (.NET 6+)
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SysMonitor.Model;
using SysMonitor.Services;
using SysMonitor.Worker;

var host = Host.CreateDefaultBuilder(args)
      .ConfigureAppConfiguration((context, config) =>
      {
          // Load appsettings.json
          config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
      })
    .ConfigureServices((context, services) =>
    {
        services.Configure<Configurations>(context.Configuration.GetSection("Config"));//Add configuration Service
        services.AddTransient<IMonitorPlugin, WindowsMonitorPlugin>();//DI for IMonitorPlugin
        services.AddSingleton(new FileWritePlugin());//Sigleton DI for File write plugin
        services.AddSingleton<HandleApiRequestPlugin>();//Single DI for Handling API request

        services.AddHostedService<Worker>();//Worker Service which keeps monitoring , Adding this as Hosted service
    })
    .Build();


using (host)
{

    await host.RunAsync(); //RUN in background, continues excecution until canceled.


}
