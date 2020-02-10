using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kshte.Helpers
{
    public class ErrorLogger
    {
        public readonly string LogPath;

        public ErrorLogger(string logFileName = null)
        {
            if (string.IsNullOrWhiteSpace(logFileName))
            {
                logFileName = $"log@{DateTime.Now.ToString("dd.MM.yyyy H-mm-ss")}.txt";
            }

            if (string.IsNullOrWhiteSpace(Path.GetExtension(logFileName)))
            {
                logFileName = Path.ChangeExtension(logFileName, ".txt");
            }

            LogPath = Path.Combine(KshteSettings.Settings.LogFolderPath, logFileName);
        }

        public void LogException(Exception e)
        {
            string directoryPath = Path.GetDirectoryName(LogPath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (FileStream fs = new FileStream(LogPath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"Exception Time: {DateTime.Now} Full data: {e.ToString()}");
                }
            }
        }
    }
}
