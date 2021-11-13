using System;
using System.Collections.Generic;
using System.Linq;

namespace Backups.Classes
{
    public class RestorePoint
    {
        private List<Storage> _storages;
        public RestorePoint(int id, List<Storage> storages)
        {
            Id = id;
            Date = DateTime.Today;
            _storages = storages;
        }

        public RestorePoint(int id, List<Storage> storages, DateTime dateTime)
        {
            Id = id;
            Date = dateTime;
            _storages = storages;
        }

        public int Id { get; }
        private DateTime Date { get; }
    }
}
