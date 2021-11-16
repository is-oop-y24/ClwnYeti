using System.Collections.Generic;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.Services
{
    public class SingleStorageArchiver : IArchiver
    {
        public IStorageRepository Store(IStorageRepository repository, string directory, List<JobObject> jobObjects)
        {
            repository.Add(jobObjects, directory);
            return repository;
        }
    }
}