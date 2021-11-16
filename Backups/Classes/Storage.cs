using System;
using System.Collections.Generic;

namespace Backups.Classes
{
    public class Storage
    {
        public Storage(Guid id, string pathToArchive, List<ArchivedFilePath> objects)
        {
            Id = id;
            PathToArchive = pathToArchive;
            Objects = objects;
        }

        public Guid Id { get; }
        public string PathToArchive { get; }
        public List<ArchivedFilePath> Objects { get; }
    }
}