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
        private readonly IEnumerable<Storage> _storages;
        public RestorePoint(int number, IStorageRepository storages)
        {
            Number = number;
            Date = DateTime.Today;
            _storages = storages.GetAllStorages();
        }

        public RestorePoint(int number, IStorageRepository storages, DateTime dateTime)
        {
            Number = number;
            Date = dateTime;
            _storages = storages.GetAllStorages();
        }

        public int Number { get; }
        public DateTime Date { get; }
        public IEnumerable<Storage> GetStorages()
        {
            return _storages;
        }

        public IEnumerable<JobObject> GetJobObjects()
        {
            return GetStorages().SelectMany(s => s.GetJobObjects()).ToList();
        }
    }
}