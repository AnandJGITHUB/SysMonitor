namespace SysMonitor.Helpers
{
 /// <summary>
 // Contains Method related to conversion etc.
 /// </summary>
    public class HelperClass
    {
        /// <summary>
        /// Converts  bytes to Mega bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static float ConvertBytesTOMB(long bytes)
        {

            return (bytes / (1024 * 1024));
        }

        /// <summary>
        /// Converts bytes to Giga bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static float ConvertTOGB(long bytes)
        {
            return ConvertBytesTOMB(bytes) / 1024;// *

        }

        /// <summary>
        /// Prints msg on console with Green forground and time stamp for better visual
        /// </summary>
        /// <param name="message"></param>
        public static void ConsolePrintSuccess(string message)
        {

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" [{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}]: {message}");
            Console.ResetColor();
        }

        
        /// <summary>
        /// Prints msg on console with Red forground and time stamp for better visual
        /// </summary>
        /// <param name="message"></param>
        public static void ConsolePrintError(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" [{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}]: {message}");
            Console.ResetColor();
        }
    }
}
