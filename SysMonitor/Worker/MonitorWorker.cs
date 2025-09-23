using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SysMonitor.Helpers;
using SysMonitor.Model;
using SysMonitor.Services;

namespace SysMonitor.Worker
{
    /// <summary>
    /// This is Main Place of Our program. Service keeps monitoring system details and logs it on console. 
    /// Also posts data to API end point.
    /// </summary>
    public class MonitorWorker : BackgroundService
    {
        IMonitorPlugin _monitorPlugin;//
       
        private readonly Configurations _configClass;
        private readonly FileWritePlugin _fileWritePlugin;

        private readonly IAPIPlugin _handleApiRequest;
        public MonitorWorker(IMonitorPlugin monitorPlugin, IOptions<Configurations> configClass, FileWritePlugin fileWritePlugin, IAPIPlugin handleApiRequest)
        {
            _monitorPlugin = monitorPlugin;
              _configClass = configClass.Value;
            _fileWritePlugin = fileWritePlugin;
            _handleApiRequest = handleApiRequest;
        }
        /// <summary>
        /// Excutes asynchronouly. after every DelayMilliseconds ms specified in configuration file until cancellation requested.
        /// Gets the current system usage details
        /// Writes data to file and prints on console and also post data on the API end Point.
        /// </summary>
        /// <param name="stoppingToken">Token for stopping on pressing Ctrl+C or Kill</param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //  _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            HelperClass.ConsolePrintSuccess("*********************SYSTEM MONITORING STARTED*****************");

            while (!stoppingToken.IsCancellationRequested)// If cancllattion not requeted continue
            {
                ApiResponseModel apiResponse = new ApiResponseModel();//Model to send data in JSON format
                SysUsageDetails sysDetails = null;
                try
                {
                    sysDetails = _monitorPlugin.GetSystemDetails(_configClass.DriveDisk);//Gets current System details

                    if (sysDetails != null)
                    {
                        apiResponse.IsDataValid = "Yes, CPU usage in %, RAM and Disk space are in MB";//If system data got successfully set it to yes.
                        apiResponse.dateTime = DateTime.Now;
                        apiResponse.ram_used = sysDetails.RAM_usage;
                        apiResponse.cpu = (float)Math.Round(sysDetails.CPU_usage, 2);
                        apiResponse.disk_used = sysDetails.Disk_usage;
                        HelperClass.ConsolePrintSuccess(sysDetails?.ToString());

                    }
                    else
                    {
                        apiResponse.IsDataValid = $"No";//If system data does not get successfully set it to NO sending exception string as well which helpful in debugging.
                        apiResponse.dateTime = DateTime.Now;
                        HelperClass.ConsolePrintError("Error in reading System Usage");

                    }
                   

                }
                catch (Exception ex)
                {
                    HelperClass.ConsolePrintError(ex.Message);

                    apiResponse.IsDataValid = $"No, Exception Ocuured {ex.Message} ";//If system data does not get successfully set it to NO sending exception string as well which helpful in debugging.
                    apiResponse.dateTime = DateTime.Now;
                }


                try
                {
                    _fileWritePlugin.AppendJsonObjectAsync(_configClass.FilePath, apiResponse); //Writing data to json file

                }
                catch (Exception ex)
                {
                    HelperClass.ConsolePrintError(ex.Message);
                }


                try
                {
                    _handleApiRequest.PostData(_configClass.APIendPoint, apiResponse); // Sending data to API end point
                }
                catch (Exception ex)
                {
                    HelperClass.ConsolePrintError(ex.Message);
                }

               

                await Task.Delay(_configClass.DelayMilliseconds, stoppingToken); //Delay for DelayMilliseconds ms given in appsettigs.json
            }
            HelperClass.ConsolePrintError("*********************SYSTEM MONITORING COLSED*****************");
        }
    }
}
