using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysMonitor.Services
{
    public interface IAPIPlugin
    {
        public  void PostData<T>(string endPoint, T apiResponse);
    }
}
