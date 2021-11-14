using System;
using System.Collections.Generic;
using Backups.Classes;

namespace Backups.Interfaces
{
    public interface IBackupJob
    {
        public JobObject CreateJobObject(string fileName);
        public void ChangeJobObject(string fileName);
        public void ChangeJobObject(JobObject jobObject);
        public void DeleteJobObject(string fileName);
        public void DeleteJobObject(JobObject jobObject);
        public RestorePoint MakeRestorePoint();
        public RestorePoint MakeRestorePoint(DateTime dateTime);
        public List<RestorePoint> GetRestorePointsThatCreatedAfterDate(DateTime dateTime);
        public List<RestorePoint> GetLastCreatedNumOfRestorePoints(int num);
    }
}