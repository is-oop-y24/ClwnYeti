using System.Collections.Generic;
using System.Linq;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.Services
{
    public class SplitStoragesArchiver : IArchiver
    {
        private readonly IStorageRepository _repository;

        public SplitStoragesArchiver(IStorageRepository repository)
        {
            _repository = repository;
        }

        public IStorageRepository StoreIntoNewRepository(string backupDirectory, int restorePointNumber, List<JobObject> jobObjects, ILogger logger)
        {
            IStorageRepository storageRepository = _repository.WithPath(backupDirectory, restorePointNumber);
            foreach (Storage storage in jobObjects.Select(j => storageRepository.AddStorage(j)))
            {
                logger.Log("Storage was created");
                logger.Log(storage.Info());
            }

            return storageRepository;
        }

        public IStorageRepository StoreIntoCurrentRepository(IStorageRepository storageRepository, string backupDirectory, int restorePointNumber, List<JobObject> jobObjects, ILogger logger)
        {
            foreach (Storage storage in jobObjects.Select(storageRepository.AddStorage))
            {
                logger.Log("Storage was created");
                logger.Log(storage.Info());
            }

            return storageRepository;
        }
    }
}