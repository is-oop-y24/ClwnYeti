using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.Services
{
    public class SingleStorageArchiver : IArchiver
    {
        public IEnumerable<Storage> Store(string directory, List<JobObject> jobObjects)
        {
            throw new System.NotImplementedException();
        }
    }
}