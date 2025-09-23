using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysMonitor.Services
{
    /// <summary>
    /// API plugin abstraction for Handling post data
    /// </summary>
    public interface IAPIPlugin
    {


        public  void PostData<T>(string endPoint, T apiResponse);
    }
}
