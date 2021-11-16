using System;

namespace Backups.Classes
{
    public class ArchivedFilePath
    {
        public ArchivedFilePath(string filePath)
        {
            OldFilePath = filePath;
            Id = Guid.NewGuid();
        }

        public string OldFilePath { get; }
        public Guid Id { get; }
    }
}