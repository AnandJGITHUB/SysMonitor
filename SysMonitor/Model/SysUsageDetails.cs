using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return
                   $"CPU USAGE={CPU_usage:F2} % " +
                   $"RAM USAGE={RAM_usage} MB " +
                   $"Disk USAGE={Disk_usage} MB ";
        }
    }
}
