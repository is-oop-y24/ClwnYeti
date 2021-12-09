using System.Collections.Generic;
using Backups.Classes;

namespace Backups.Interfaces
{
    public interface IArchiver
    {
        public IStorageRepository Store(string backupDirectory, int restorePointNumber, List<JobObject> jobObjects, ILogger logger);
    }
}