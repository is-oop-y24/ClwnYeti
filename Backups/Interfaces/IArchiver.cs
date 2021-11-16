using System.Collections.Generic;
using Backups.Classes;

namespace Backups.Interfaces
{
    public interface IArchiver
    {
        public IStorageRepository Store(IStorageRepository repository, string directory, List<JobObject> jobObjects);
    }
}