using SysMonitor.Helpers;
using System.Text.Json;

namespace SysMonitor.Services
{
    /// <summary>
    /// Plugin for Writing System usage details to a json file
    /// </summary>
    public class FileWritePlugin
    {

        public FileWritePlugin()
        {


        }

        /// <summary>
        /// Convrets newObject to json and append new json object at end of file if file alredy exists.
        /// If does not exist theb creates the file and appends the new object.
        /// </summary>
        /// <typeparam name="T">Type of the objecy</typeparam>
        /// <param name="filePath">Fath at which data sould be written</param>
        /// <param name="newObject">Object which is to be written to File</param>
        /// <returns></returns>
        public async Task AppendJsonObjectAsync<T>(string filePath, T newObject)
        {
            try
            {
                List<T> items = new();

                if (File.Exists(filePath))
                {
                    string existingJson = await File.ReadAllTextAsync(filePath);
                    if (!string.IsNullOrWhiteSpace(existingJson))
                    {
                        items = JsonSerializer.Deserialize<List<T>>(existingJson) ?? new();
                    }
                }

                items.Add(newObject);

                var options = new JsonSerializerOptions { WriteIndented = true };
                string updatedJson = JsonSerializer.Serialize(items, options);

                await File.WriteAllTextAsync(filePath, updatedJson);
            }
            catch (Exception ex)
            {

                HelperClass.ConsolePrintError($"Error in writing Data to File {ex.Message}");
            }

        }
    }
}
