using SysMonitor.Helpers;
using SysMonitor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SysMonitor.Services
{
    /// <summary>
    /// Plugin to Handle API request
    /// </summary>
    public class HandleApiRequestPlugin
    {
        private readonly HttpClient _http; //http client to handle private because should not accessible outside of the class

        public HandleApiRequestPlugin()
        {
            _http = new HttpClient();//Initilization
        }

        /// <summary>
        /// Converts newObejct to json and post data to endPoint Url.
        /// </summary>
        /// <param name="endPoint">Url for Posting data</param>
        /// <param name="apiResponse">Data to be posted </param>
        public async void PostData(string endPoint, ApiResponseModel apiResponse)
        {
            try
            {
                var json = JsonSerializer.Serialize(apiResponse);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _http.PostAsync(endPoint, content);
                if (!response.IsSuccessStatusCode)
                {
                    HelperClass.ConsolePrintError($"POST failed: {(int)response.StatusCode} {response.ReasonPhrase}");
                }
                else
                {
                    HelperClass.ConsolePrintSuccess($"{DateTime.Now:O} Posted: {json}");
                }

            }
            catch (Exception ex)
            {

                HelperClass.ConsolePrintError($"Error in API request {ex.Message}");



            }
            Console.ResetColor();
        }
    }
}
