using System.Collections.Generic;
using Backups.Classes;

namespace Backups.Interfaces
{
    public interface IStorageRepository
    {
        public Storage Add(List<JobObject> jobObjects, string directory);
        public Storage Add(JobObject jobObject, string directory);
        public abstract IStorageRepository Empty();
    }
}