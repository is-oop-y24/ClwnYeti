using System.Collections.Generic;
using Backups.Classes;

namespace Backups.Interfaces
{
    public interface IArchiver
    {
        public IEnumerable<Storage> Store(string directory, List<JobObject> jobObjects);
    }
}