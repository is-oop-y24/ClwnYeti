using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Backups.Interfaces;
using Backups.Tools;

namespace Backups.Classes
{
    public class StorageRepositoryWithoutFileSystem : IStorageRepository
    {
        private readonly List<Storage> _storages;
        public StorageRepositoryWithoutFileSystem()
        {
            _storages = new List<Storage>();
        }

        public Storage Add(List<JobObject> jobObjects, string backupDirectory, int restorePointNumber)
        {
            var id = Guid.NewGuid();
            _storages.Add(new Storage(id, backupDirectory + "/" + restorePointNumber + "/" + id + ".zip", jobObjects.Select(j => new ArchivedFilePath(j.Path)).ToList()));
            return _storages[^1];
        }

        public Storage Add(JobObject jobObject, string backupDirectory, int restorePointNumber)
        {
            var id = Guid.NewGuid();
            _storages.Add(new Storage(id, backupDirectory + "/" + restorePointNumber + "/" + id + ".zip",  new ArchivedFilePath(jobObject.Path)));
            return _storages[^1];
        }

        IStorageRepository IStorageRepository.Empty()
        {
            return new StorageRepositoryWithoutFileSystem();
        }

        public Storage GetById(Guid id)
        {
            foreach (Storage s in _storages.Where(s => s.Id == id))
            {
                return s;
            }

            throw new BackupsException("There is no storages with this id");
        }

        public IEnumerable<Storage> GetAllStorages()
        {
            return _storages;
        }
    }
}