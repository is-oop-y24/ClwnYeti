using System;
using Backups.Interfaces;

namespace BackupsExtra.Entities
{
    public class LoggerIntoConsole : ILogger
    {
        public string Log(string logInfo)
        {
            Console.WriteLine(logInfo);
            return logInfo;
        }
    }
}