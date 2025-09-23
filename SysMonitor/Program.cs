// Program.cs (.NET 8)
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
        services.AddSingleton<IMonitorPlugin, WindowsMonitorPlugin>();//DI for IMonitorPlugin
        services.AddSingleton<IAPIPlugin, HandleApiRequestPlugin>();//Sigleton DI for Handling API request
        services.AddSingleton(new FileWritePlugin());//Sigleton DI for File write plugin   
        
        services.AddHostedService<MonitorWorker>();//Worker Service which keeps monitoring , Adding this as Hosted service
    })
    .Build();


using (host)
{

    await host.RunAsync(); //RUN in background, continues excecution until canceled.


}
