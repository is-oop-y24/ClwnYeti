using System.Collections.Generic;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.Services
{
    public class SplitStoragesArchiver : IArchiver
    {
        public IStorageRepository Store(IStorageRepository repository, string backupDirectory, int restorePointNumber, List<JobObject> jobObjects)
        {
            foreach (JobObject j in jobObjects)
            {
                repository.Add(j, backupDirectory, restorePointNumber);
            }

            return repository;
        }
    }
}