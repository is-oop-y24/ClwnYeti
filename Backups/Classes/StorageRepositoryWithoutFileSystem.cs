using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Interfaces;
using Backups.Tools;

namespace Backups.Classes
{
    public class StorageRepositoryWithoutFileSystem : IStorageRepository
    {
        private readonly List<Storage> _storages;
        private readonly string _pathToDirectory;

        public StorageRepositoryWithoutFileSystem()
        {
            _storages = new List<Storage>();
            _pathToDirectory = string.Empty;
        }

        private StorageRepositoryWithoutFileSystem(string backupDirectory, int restorePointNumber)
        {
            _storages = new List<Storage>();
            _pathToDirectory = backupDirectory + Path.PathSeparator + restorePointNumber;
        }

        public Storage Add(List<JobObject> jobObjects)
        {
            var id = Guid.NewGuid();
            _storages.Add(new Storage(id, _pathToDirectory + "/" + id + ".zip", jobObjects.Select(j => new ArchivedFilePath(j.Path)).ToList()));
            return _storages[^1];
        }

        public Storage Add(JobObject jobObject)
        {
            var id = Guid.NewGuid();
            _storages.Add(new Storage(id, _pathToDirectory + "/" + id + ".zip",  new ArchivedFilePath(jobObject.Path)));
            return _storages[^1];
        }

        public int Count()
        {
            return _storages.Count;
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

        public void Restore()
        {
        }

        public void Restore(string newPath)
        {
        }

        public void DeleteAll(ILogger logger)
        {
            for (int i = 0; i < _storages.Count; i++)
            {
                logger.Log("Storage was deleted");
            }

            _storages.Clear();
        }

        public IStorageRepository WithPath(string backupDirectory, int restorePointNumber)
        {
            return new StorageRepositoryWithoutFileSystem(backupDirectory, restorePointNumber);
        }
    }
}