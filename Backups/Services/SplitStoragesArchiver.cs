using System.Collections.Generic;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.Services
{
    public class SplitStoragesArchiver : IArchiver
    {
        public IEnumerable<Storage> Store(string directory, List<JobObject> jobObjects)
        {
            throw new System.NotImplementedException();
        }
    }
}