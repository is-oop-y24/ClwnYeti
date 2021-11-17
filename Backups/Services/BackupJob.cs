using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Classes;
using Backups.Interfaces;
using Backups.Tools;

namespace Backups.Services
{
    public class BackupJob : IBackupJob
    {
        private readonly List<JobObject> _currentJobObjects;
        private readonly List<RestorePoint> _restorePoints;
        private IArchiver _archiver;
        private IStorageRepository _repository;
        private string _directoryForSave;

        public BackupJob(IArchiver algorithmOfStorage, IStorageRepository repository, string directory)
        {
            Id = Guid.NewGuid();
            _archiver = algorithmOfStorage;
            _restorePoints = new List<RestorePoint>();
            _currentJobObjects = new List<JobObject>();
            _repository = repository;
            _directoryForSave = directory + "/" + Id;
        }

        public Guid Id { get; }

        public RestorePoint MakeRestorePoint()
        {
            _restorePoints.Add(new RestorePoint(_restorePoints.Count, _archiver.Store(_repository.Empty(), _directoryForSave, _restorePoints.Count, _currentJobObjects)));
            return _restorePoints[^1];
        }

        public RestorePoint MakeRestorePoint(DateTime dateTime)
        {
            _restorePoints.Add(new RestorePoint(_restorePoints.Count, _archiver.Store(_repository.Empty(), _directoryForSave, _restorePoints.Count, _currentJobObjects), dateTime));
            return _restorePoints[^1];
        }

        public JobObject Add(string pathToFile)
        {
            if (_currentJobObjects.Any(j => j.Path == pathToFile))
            {
                throw new BackupsException("This file was already added");
            }

            _currentJobObjects.Add(new JobObject(pathToFile));
            return _currentJobObjects[^1];
        }

        public void Delete(string pathToFile)
        {
            foreach (JobObject j in _currentJobObjects.Where(j => j.Path == pathToFile))
            {
                _currentJobObjects.Remove(j);
                return;
            }

            throw new BackupsException("This file wasn't in backup");
        }
    }
}