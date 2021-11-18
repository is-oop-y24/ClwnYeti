using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Backups.Classes
{
    public class Storage
    {
        private readonly IEnumerable<ArchivedFilePath> _objects;
        public Storage(Guid id, string pathToArchive, IEnumerable<ArchivedFilePath> objects)
        {
            Id = id;
            PathToArchive = pathToArchive;
            _objects = objects;
        }

        public Storage(Guid id, string pathToArchive, ArchivedFilePath obj)
        {
            Id = id;
            PathToArchive = pathToArchive;
            _objects = new List<ArchivedFilePath> { obj };
        }

        public Guid Id { get; }
        public string PathToArchive { get; }

        public IEnumerable<JobObject> GetJobObjects()
        {
            return _objects.Select(afp => new JobObject(afp.OldFilePath)).ToList();
        }
    }
}