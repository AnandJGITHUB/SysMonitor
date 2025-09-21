using SysMonitor.Helpers;
using SysMonitor.Model;
using System.Diagnostics;
using System.Management;

namespace SysMonitor.Services
{

    /// <summary>
    /// System Monitor Plugin for Windows
    /// </summary>
    internal class WindowsMonitorPlugin : IMonitorPlugin
    {

        private PerformanceCounter cpuCounter;//For Monitoring CPU %
        private PerformanceCounter ramCounter; //For Monitoring RAM usage
        public WindowsMonitorPlugin()
        {
            cpuCounter =  new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            cpuCounter.NextValue();
          
        }

        /// <summary>
        /// Gets Current System Details on WINDOWS OS
        /// </summary>
        /// <param name="driveLetter"></param>
        /// <returns></returns>
        public SysUsageDetails GetSystemDetails(string driveLetter)
        {
            SysUsageDetails details = new SysUsageDetails();
            try
            {
                details.RAM_usage = GetUsedRamMb();
                details.Disk_usage = GetDiskUsage(driveLetter);
                details.CPU_usage = GetCurrentCpuUsage();

            }
            catch (Exception ex)
            {
                HelperClass.ConsolePrintError(ex.Message);
                details = null;
            }




            return details;
        }

        /// <summary>
        /// Gets Current CPU usage in percent
        /// </summary>
        /// <returns></returns>
        public float GetCurrentCpuUsage()
        {
            float cpuUsage = 0;
  
            try
            {

                cpuUsage = cpuCounter.NextValue();
               
            }
            catch (Exception ex)
            {
                HelperClass.ConsolePrintError(ex.Message);
                throw ex;
            }

            return cpuUsage;
        }
        /// <summary>
        /// Returns Available RAM in MB
        /// </summary>
        /// <returns></returns>
        public float GetAvailableRamMb()
        {
            float availableRam = 0;

            try
            {

                availableRam = ramCounter.NextValue();
                //Thread.Sleep(1000);
                //availableRam = ramCounter.NextValue();
            }
            catch (Exception ex)
            {
                HelperClass.ConsolePrintError(ex.Message);
                throw ex;
            }

            return availableRam;
        }

        /// <summary>
        /// ManagementObjectSearcher is used to get total RAM of Sytem
        /// </summary>
        /// <returns></returns>
        public float GetTotalRamMb()
        {
            float totalRam = 0;
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
                foreach (ManagementObject mo in mos.Get())
                {
                    long totaLPhysicalMemory = Convert.ToInt64(mo["TotalPhysicalMemory"]);
                    totalRam = HelperClass.ConvertBytesTOMB(totaLPhysicalMemory);
                }
            }
            catch (Exception ex)
            {
                HelperClass.ConsolePrintError(ex.Message);
                throw ex;
            }
            return totalRam;
        }
        /// <summary>
        /// Calculates and returns Used RAM in MB 
        /// </summary>
        /// <returns></returns>

        public float GetUsedRamMb()
        {
            try
            {
                return GetTotalRamMb() - GetAvailableRamMb();
            }catch(Exception ex)
            {
                HelperClass.ConsolePrintError(ex.Message);
                throw ex;
            

            }
           
        }

        /// <summary>
        /// Returns the used space in MB of Disk/Drive
        /// </summary>
        /// <param name="driveLetter">Pass the Dirve of letter of Concern Drive c/d </param>
        /// <returns></returns>
        public float GetDiskUsage(string driveLetter)
        {
            try
            {
                DriveInfo drive = new DriveInfo(driveLetter);
                if (drive.IsReady)
                {
                    long totalSize = drive.TotalSize;
                   
                    long freeSpace = drive.TotalFreeSpace;
                    long usedSpace = totalSize - freeSpace;

                    ;
                    return HelperClass.ConvertBytesTOMB(usedSpace);


                }
            }
            catch (Exception ex)
            {
                HelperClass.ConsolePrintError(ex.Message);
                throw ex;
            }



            return 0; // Drive not ready or invalid
        }
    }
}
