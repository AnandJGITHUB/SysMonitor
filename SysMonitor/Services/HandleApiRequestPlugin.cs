using SysMonitor.Helpers;
using System.Text;
using System.Text.Json;

namespace SysMonitor.Services
{
    /// <summary>
    /// Plugin to Handle API request
    /// </summary>
    public class HandleApiRequestPlugin :IAPIPlugin
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
        public async void PostData<T>(string endPoint, T apiResponse)
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
                    HelperClass.ConsolePrintSuccess($"Posted: {json}");
                }

            }
            catch (Exception ex)
            {

                HelperClass.ConsolePrintError($"Error in API request {ex.Message}");



            }
          //  Console.ResetColor();
        }
    }
}
