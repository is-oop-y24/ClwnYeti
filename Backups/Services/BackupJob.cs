using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Classes;
using Backups.Interfaces;

namespace Backups.Services
{
    public class BackupJob : IBackupJob
    {
        private readonly List<JobObject> _currentJobObjects;
        private readonly List<RestorePoint> _restorePoints;
        private IArchiver _archiver;
        private string _directory = "C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\Storages";
        public BackupJob(IArchiver algorithmOfStorage, List<JobObject> jobObjects)
        {
            Id = Guid.NewGuid();
            _archiver = algorithmOfStorage;
            _restorePoints = new List<RestorePoint>();
            _currentJobObjects = jobObjects;
        }

        public Guid Id { get; }

        public RestorePoint MakeRestorePoint()
        {
            _restorePoints.Add(new RestorePoint(_restorePoints.Count, _archiver.Store(_directory, _currentJobObjects)));
            return _restorePoints[^1];
        }

        public RestorePoint MakeRestorePoint(DateTime dateTime)
        {
            _restorePoints.Add(new RestorePoint(_restorePoints.Count, _archiver.Store(_directory, _currentJobObjects), dateTime));
            return _restorePoints[^1];
        }

        public JobObject Add(string pathToFile)
        {
            _currentJobObjects.Add(new JobObject(pathToFile));
            return _currentJobObjects[^1];
        }

        public void Delete(string pathToFile)
        {
            foreach (JobObject j in _currentJobObjects.Where(j => j.Path == pathToFile))
            {
                _currentJobObjects.Remove(j);
            }
        }
    }
}