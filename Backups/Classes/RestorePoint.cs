using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Dynamic;
using System.Linq;
using Backups.Interfaces;

namespace Backups.Classes
{
    public class RestorePoint
    {
        private readonly IStorageRepository _storages;
        public RestorePoint(int number, IStorageRepository storages)
        {
            Number = number;
            Date = DateTime.Today;
            _storages = storages;
        }

        public RestorePoint(int number, IStorageRepository storages, DateTime dateTime)
        {
            Number = number;
            Date = dateTime;
            _storages = storages;
        }

        public int Number { get; }
        public DateTime Date { get; }
        public IEnumerable<Storage> GetStorages()
        {
            return _storages.GetAllStorages();
        }

        public IEnumerable<ArchivedFilePath> GetFilePathsAndTheirNewNames()
        {
            return GetStorages().SelectMany(s => s.GetPathsOfFilesAndTheirNewNames()).ToList();
        }
    }
}
