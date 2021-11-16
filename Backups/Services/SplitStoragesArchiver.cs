using System.Collections.Generic;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.Services
{
    public class SplitStoragesArchiver : IArchiver
    {
        public IStorageRepository Store(IStorageRepository repository, string directory, List<JobObject> jobObjects)
        {
            foreach (JobObject j in jobObjects)
            {
                repository.Add(j, directory);
            }

            return repository;
        }
    }
}