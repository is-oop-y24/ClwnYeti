using System;
using Backups.Interfaces;

namespace Backups.Classes
{
    public class RestorePoint
    {
        private IStorageRepository _storages;
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
    }
}
