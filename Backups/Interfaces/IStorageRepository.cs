using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Backups.Classes;

namespace Backups.Interfaces
{
    public interface IStorageRepository
    {
        public Storage AddStorage(List<JobObject> jobObjects);
        public Storage AddStorage(JobObject jobObject);
        public Storage UpdateLastStorage(List<JobObject> jobObjects);
        public Storage UpdateLastStorage(JobObject jobObject);
        public int Count();
        public Storage GetById(Guid id);
        public IEnumerable<Storage> GetAllStorages();
        public void Restore(string newPath);
        public void DeleteAll(ILogger logger);
        public IStorageRepository WithPath(string backupDirectory, int restorePointNumber);
    }
}