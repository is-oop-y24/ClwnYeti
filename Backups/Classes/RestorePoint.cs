using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Interfaces;

namespace Backups.Classes
{
    public class RestorePoint
    {
        private readonly IStorageRepository _storages;
        public RestorePoint(int number, IStorageRepository storages, string backupPath)
        {
            Number = number;
            Date = DateTime.Today;
            _storages = storages;
            BackupPath = backupPath;
        }

        public RestorePoint(int number, IStorageRepository storages, string backupPath, DateTime dateTime)
        {
            Number = number;
            Date = dateTime;
            BackupPath = backupPath;
            _storages = storages;
        }

        public int Number { get; }
        public DateTime Date { get; }
        public string BackupPath { get; }
        public IEnumerable<Storage> GetStorages()
        {
            return _storages.GetAllStorages();
        }

        public void Restore()
        {
            _storages.Restore();
        }

        public void Restore(string newPath)
        {
            _storages.Restore(newPath);
        }

        public IEnumerable<JobObject> GetJobObjects()
        {
            return GetStorages().SelectMany(s => s.GetJobObjects()).ToList();
        }

        public IStorageRepository GetRepository()
        {
            return _storages;
        }

        public void Delete(ILogger logger)
        {
            _storages.DeleteAll(logger);
        }

        public string Info()
        {
            return $"Restore point with number {Number} was created {Date} and has {_storages.Count()} storages";
        }
    }
}
