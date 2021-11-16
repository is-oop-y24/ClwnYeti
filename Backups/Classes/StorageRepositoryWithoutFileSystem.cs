using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Interfaces;

namespace Backups.Classes
{
    public class StorageRepositoryWithoutFileSystem : IStorageRepository
    {
        private readonly List<Storage> _storages;
        public StorageRepositoryWithoutFileSystem(string directory)
        {
            _storages = new List<Storage>();
        }

        public Storage Add(List<JobObject> jobObjects, string directory)
        {
            var id = Guid.NewGuid();
            _storages.Add(new Storage(id, directory + "/" + id, jobObjects.Select(j => new ArchivedFilePath(j.Path)).ToList()));
            return _storages[^1];
        }
    }
}