using System;
using Backups.Interfaces;
using Microsoft.VisualBasic;

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