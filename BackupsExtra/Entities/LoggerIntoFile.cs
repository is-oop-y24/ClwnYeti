using System.IO;
using System.Text;
using Backups.Interfaces;

namespace BackupsExtra.Entities
{
    public class LoggerIntoFile : ILogger
    {
        private readonly string _fileName;
        public LoggerIntoFile(string pathToFile)
        {
            _fileName = pathToFile;
        }

        public string Log(string logInfo)
        {
            File.WriteAllText(_fileName, logInfo + "\n", Encoding.Default);
            return logInfo;
        }
    }
}