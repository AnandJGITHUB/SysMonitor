using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysMonitor.Model
{ 
    /// <summary>
    /// Model for reading Configurations from config appsettings.json file
    /// </summary>
    public class Configurations
    {
       public int DelayMilliseconds {  get; set; }   
        
        public string APIendPoint { get; set; }    

        public string DriveDisk {  get; set; }  

        public string FilePath { get; set; }    


    }
}
