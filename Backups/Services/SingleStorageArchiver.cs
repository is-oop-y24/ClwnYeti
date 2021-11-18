using System.Collections.Generic;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.Services
{
    public class SingleStorageArchiver : IArchiver
    {
        public IStorageRepository Store(IStorageRepository repository, string backupDirectory, int restorePointNumber, List<JobObject> jobObjects)
        {
            repository.Add(jobObjects, backupDirectory, restorePointNumber);
            return repository;
        }
    }
}