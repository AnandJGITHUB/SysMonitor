using SysMonitor.Model;

namespace SysMonitor.Services
{
    /// <summary>
    /// Sample for Linux
    /// </summary>
    internal class LinuxMonitorPlugin : IMonitorPlugin
    {
        public SysUsageDetails GetSystemDetails(string driveLetter)
        {
          return  new SysUsageDetails();
        }
    }
}
