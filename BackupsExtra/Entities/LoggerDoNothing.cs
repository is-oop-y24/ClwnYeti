using Backups.Interfaces;

namespace BackupsExtra.Entities
{
    public class LoggerDoNothing : ILogger
    {
        public string Log(string logInfo)
        {
            return logInfo;
        }
    }
}