using System.Collections.Generic;
using Backups.Classes;

namespace Backups.Interfaces
{
    public interface IArchiver
    {
        public IStorageRepository StoreIntoNewRepository(string backupDirectory, int restorePointNumber, List<JobObject> jobObjects, ILogger logger);
        public IStorageRepository StoreIntoCurrentRepository(IStorageRepository storageRepository, string backupDirectory, int restorePointNumber, List<JobObject> jobObjects, ILogger logger);
    }
}