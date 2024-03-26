using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.DataAccess
{
    public class LoggingService
    {
        private const string Filename = "BankingApp.log";

        public static void WriteToLog(string methodSource, string objectSource, string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(Filename, true)) // Append mode
                {
                    string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}|{methodSource}|{objectSource}|{message}";
                    writer.WriteLine(logMessage);
                    writer.Flush(); // Ensure data is written to the file
                }
            }
            catch (Exception ex)
            {
                // Handle any potential errors during logging to avoid losing error information
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}
