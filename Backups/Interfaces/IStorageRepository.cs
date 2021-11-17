using System.Collections.Generic;
using Backups.Classes;

namespace Backups.Interfaces
{
    public interface IStorageRepository
    {
        public Storage Add(List<JobObject> jobObjects, string restorePointDirectory, int restorePointNumber);
        public Storage Add(JobObject jobObject, string backupDirectory, int restorePointNumber);
        public abstract IStorageRepository Empty();
    }
}