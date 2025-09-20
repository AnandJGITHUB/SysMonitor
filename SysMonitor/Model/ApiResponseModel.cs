using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysMonitor.Model
{
    /// <summary>
    /// Model for converting system usage data into json
    /// </summary>
    public class ApiResponseModel
    {
        // property names match the requested payload
        
        public string IsDataValid { get; set; }//Species if data is valid or not and gives description if any . YES if data is valid and No if data is not valid
        public DateTime dateTime { get; set; }//Date time at which system usage details were collected
        public float cpu { get; set; }//CPU usage %
        public float ram_used { get; set; } //RAM used 
        public float disk_used { get; set; } //Disk space used



    }
}
