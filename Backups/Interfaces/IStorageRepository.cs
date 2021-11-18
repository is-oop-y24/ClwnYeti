using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Backups.Classes;

namespace Backups.Interfaces
{
    public interface IStorageRepository
    {
        public Storage Add(List<JobObject> jobObjects, string restorePointDirectory, int restorePointNumber);
        public Storage Add(JobObject jobObject, string backupDirectory, int restorePointNumber);
        public abstract IStorageRepository Empty();
        public Storage GetById(Guid id);
        public IEnumerable<Storage> GetAllStorages();
    }
}