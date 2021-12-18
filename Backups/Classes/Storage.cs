using System;
using System.Collections.Generic;
using System.Linq;

namespace Backups.Classes
{
    public class Storage
    {
        private readonly List<ArchivedFilePath> _objects;
        public Storage(Guid id, string pathToArchive, List<ArchivedFilePath> objects)
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

        public int Count()
        {
            return _objects.Count;
        }

        public string Info()
        {
            return $"Storage with id {Id} has path to archive {PathToArchive} and {_objects.Count} objects";
        }
    }
}