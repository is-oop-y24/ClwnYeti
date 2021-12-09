using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Classes;
using Backups.Interfaces;
using Backups.Tools;

namespace Backups.Services
{
    public class BackupJob : IBackupJob
    {
        private readonly List<JobObject> _currentJobObjects;
        private readonly ILogger _logger;
        private readonly IArchiver _archiver;
        private readonly string _directoryForSave;
        private List<RestorePoint> _restorePoints;

        public BackupJob(IArchiver algorithmOfStorage, string directory, ILogger logger)
        {
            Id = Guid.NewGuid();
            _archiver = algorithmOfStorage;
            _restorePoints = new List<RestorePoint>();
            _currentJobObjects = new List<JobObject>();
            _logger = logger;
            _directoryForSave = directory + Path.PathSeparator + Id;
        }

        public Guid Id { get; }

        public RestorePoint MakeRestorePoint()
        {
            try
            {
                _restorePoints.Add(new RestorePoint(_restorePoints.Count, _archiver.StoreIntoNewRepository(_directoryForSave, _restorePoints.Count, _currentJobObjects, _logger), _directoryForSave));
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                throw e;
            }

            _logger.Log("Restore point was created");
            _logger.Log(_restorePoints[^1].Info());
            return _restorePoints[^1];
        }

        public RestorePoint MakeRestorePoint(DateTime dateTime)
        {
            try
            {
                _restorePoints.Add(new RestorePoint(_restorePoints.Count, _archiver.StoreIntoNewRepository(_directoryForSave, _restorePoints.Count, _currentJobObjects, _logger), _directoryForSave,  dateTime));
            }
            catch (BackupsException e)
            {
                _logger.Log(e.Message);
                throw e;
            }

            _logger.Log("Restore point was created");
            _logger.Log(_restorePoints[^1].Info());
            return _restorePoints[^1];
        }

        public void RestoreFilesFromRestorePoint(int numberOfPoint)
        {
            foreach (RestorePoint r in _restorePoints.Where(r => r.Number == numberOfPoint))
            {
                try
                {
                    r.Restore();
                }
                catch (Exception e)
                {
                    _logger.Log(e.Message);
                    throw e;
                }

                _logger.Log("Files were restored from restore point");
                return;
            }

            _logger.Log("There is no restore point with this number");
            throw new BackupsException("There is no restore point with this number");
        }

        public void RestoreFilesFromRestorePoint(int numberOfPoint, string newPath)
        {
            foreach (RestorePoint r in _restorePoints.Where(r => r.Number == numberOfPoint))
            {
                try
                {
                    r.Restore(newPath);
                }
                catch (Exception e)
                {
                    _logger.Log(e.Message);
                    throw e;
                }

                _logger.Log("Files were restored from restore point");
                return;
            }

            _logger.Log("There is no restore point with this number");
            throw new BackupsException("There is no restore point with this number");
        }

        public void CleanPoints(ICleanerPoints cleanerPoints, ISolverWhatToDoWithPoints solver)
        {
            var restorePoints = cleanerPoints.Clean(_restorePoints).ToList();
            if (restorePoints.Count == 0)
            {
                _logger.Log("Cleaner deleted all points");
                throw new BackupsException("Cleaner deleted all points");
            }

            IEnumerable<RestorePoint> otherRestorePoints = _restorePoints.Where(restorePoint => !restorePoints.Contains(restorePoint)).ToList();
            try
            {
                restorePoints[^1] = solver.Do(restorePoints[^1], otherRestorePoints, _logger, _archiver);
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                throw e;
            }

            _restorePoints = restorePoints;
            _logger.Log("Restore points were cleaned");
        }

        public JobObject Add(string pathToFile)
        {
            if (_currentJobObjects.Any(j => j.Path == pathToFile))
            {
                _logger.Log("This file was already added");
                throw new BackupsException("This file was already added");
            }

            _currentJobObjects.Add(new JobObject(pathToFile));
            _logger.Log("Job object was added into scope");
            _logger.Log(_currentJobObjects[^1].Info());
            return _currentJobObjects[^1];
        }

        public void Delete(string pathToFile)
        {
            foreach (JobObject j in _currentJobObjects.Where(j => j.Path == pathToFile))
            {
                _currentJobObjects.Remove(j);
                _logger.Log("Job object was deleted from scope");
                return;
            }

            _logger.Log("This file wasn't in backup");
            throw new BackupsException("This file wasn't in backup");
        }

        public string SaveToJsonFile()
        {
            return string.Empty;
        }
    }
}