using SysMonitor.Model;

namespace SysMonitor.Services
{

    /// <summary>
    /// Abstraction for Monitoring system Details, Separate plugins for Window,Linux, MacOs etc. operating systems
    /// should be created by implementing this pluging 
    /// </summary>
    public interface IMonitorPlugin
    {
        /// <summary>
        /// This methos returns all details of system like CPU usgae, RAM usage etc. Encapsulated in single class 
        /// </summary>
        /// <param name="driveLetter">Disk storage c or d (Assuming there is only one partition on system)</param>
        /// <returns></returns>
        public SysUsageDetails GetSystemDetails(string driveLetter);
    }
}
