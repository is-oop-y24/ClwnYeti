using System;
using Backups.Classes;

namespace Backups.Interfaces
{
    public interface IBackupJob
    {
        public RestorePoint MakeRestorePoint();
        public RestorePoint MakeRestorePoint(DateTime dateTime);

        public void RestoreFilesFromRestorePoint(int numberOfPoint);
        public void RestoreFilesFromRestorePoint(int numberOfPoint, string newPath);

        public string SaveToJsonFile();

        public void CleanPoints(ICleanerPoints cleanerPoints, ISolverWhatToDoWithPoints solver);

        public JobObject Add(string pathToFile);

        public void Delete(string pathToFile);
    }
}