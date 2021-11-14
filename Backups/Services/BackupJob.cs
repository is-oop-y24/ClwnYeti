using System.Collections.Generic;
using System.IO;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.Services
{
    public class BackupJob
    {
        private readonly List<JobObject> _currentJobObjects;
        private readonly List<RestorePoint> _restorePoints;
        private IArchiver _archiver;
        public BackupJob(IArchiver algorithmOfStorage)
        {
            _archiver = algorithmOfStorage;
            _restorePoints = new List<RestorePoint>();
            _currentJobObjects = new List<JobObject>();
        }

        public JobObject CreateJobObject(FileStream fileStream)
        {
            _currentJobObjects.Add(new JobObject(fileStream.Name));
            return _currentJobObjects[^1];
        }

        public void ChangeJobObject(FileStream fileStream)
        {
        }
    }
}