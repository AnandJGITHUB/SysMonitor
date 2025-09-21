namespace SysMonitor.Model
{
    public class SysUsageDetails
    {
        public float CPU_usage { set; get; } 

        public float RAM_usage { set; get; }

        public float Disk_usage { set; get; }

        /// <summary>
        /// overrides ToString method to return all the properties in a specief string format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"CPU used={CPU_usage:F2}% RAM used={RAM_usage:N0}MB Disk used={Disk_usage:N0}MB";
        }
    }
}
