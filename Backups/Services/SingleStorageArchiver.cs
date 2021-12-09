using System.Collections.Generic;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.Services
{
    public class SingleStorageArchiver : IArchiver
    {
        private readonly IStorageRepository _repository;

        public SingleStorageArchiver(IStorageRepository repository)
        {
            _repository = repository;
        }

        public IStorageRepository Store(string backupDirectory, int restorePointNumber, List<JobObject> jobObjects, ILogger logger)
        {
            IStorageRepository storageRepository = _repository.WithPath(backupDirectory, restorePointNumber);
            Storage storage = storageRepository.Add(jobObjects);
            logger.Log("Storage was created");
            logger.Log(storage.Info());
            return storageRepository;
        }
    }
}