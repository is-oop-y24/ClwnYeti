using Backups.Classes;
using Backups.Interfaces;
using Backups.Services;
using Backups.Tools;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Tests
    {
        private IBackupJob _firstBackupJob;
        private IBackupJob _secondBackupJob;

        [SetUp]
        public void Setup()
        {
            var backupJobBuilder = new BackupJobBuilder();
            backupJobBuilder.WithStorageRepository(new StorageRepositoryWithoutFileSystem());
            backupJobBuilder.WithDirectory("C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\Backup");
            backupJobBuilder.WithArchiver(new SplitStoragesArchiver());
            _firstBackupJob = backupJobBuilder.Create();
            backupJobBuilder.WithArchiver(new SingleStorageArchiver());
            _secondBackupJob = backupJobBuilder.Create();
        }

        [Test]
        public void WorkingOfSplitStoragesArchiver()
        {
            string firstFileName = "C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\1.txt";
            string secondFileName = "C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\2.txt";
            string thirdFileName = "C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\3.txt";
            JobObject firstJobObject = _firstBackupJob.Add(firstFileName);
            JobObject secondJobObject = _firstBackupJob.Add(secondFileName);
            RestorePoint firstPoint = _firstBackupJob.MakeRestorePoint();
            _firstBackupJob.Delete(firstFileName);
            JobObject thirdJobObject = _firstBackupJob.Add(thirdFileName);
            RestorePoint secondPoint = _firstBackupJob.MakeRestorePoint();
            CollectionAssert.Contains(firstPoint.GetJobObjects(), firstJobObject);
            CollectionAssert.Contains(firstPoint.GetJobObjects(), secondJobObject);
            CollectionAssert.Contains(secondPoint.GetJobObjects(), secondJobObject);
            CollectionAssert.Contains(secondPoint.GetJobObjects(), thirdJobObject);
            CollectionAssert.DoesNotContain(secondPoint.GetJobObjects(), firstJobObject);
        }

        [Test]
        public void TryingToAddFileThatAlreadyExistInBackup_ThrowException()
        {
            Assert.Catch<BackupsException>(() =>
            {
                string firstFileName = "C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\1.txt";
                _firstBackupJob.Add(firstFileName);
                _firstBackupJob.Add(firstFileName);
            });
        }

        [Test]
        public void DeleteFileThatIsNotInBackup_ThrowException()
        {
            Assert.Catch<BackupsException>(() =>
            {
                string firstFileName = "C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\1.txt";
                _firstBackupJob.Delete(firstFileName);
            });
        }

        [Test]
        public void WorkingOfSingleStoragesArchiver()
        {
            string firstFileName = "C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\1.txt";
            string secondFileName = "C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\2.txt";
            string thirdFileName = "C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\3.txt";
            JobObject firstJobObject = _secondBackupJob.Add(firstFileName);
            JobObject secondJobObject = _secondBackupJob.Add(secondFileName);
            RestorePoint firstPoint = _secondBackupJob.MakeRestorePoint();
            _secondBackupJob.Delete(firstFileName);
            JobObject thirdJobObject = _secondBackupJob.Add(thirdFileName);
            RestorePoint secondPoint = _secondBackupJob.MakeRestorePoint();
            CollectionAssert.Contains(firstPoint.GetJobObjects(), firstJobObject);
            CollectionAssert.Contains(firstPoint.GetJobObjects(), secondJobObject);
            CollectionAssert.Contains(secondPoint.GetJobObjects(), secondJobObject);
            CollectionAssert.Contains(secondPoint.GetJobObjects(), thirdJobObject);
            CollectionAssert.DoesNotContain(secondPoint.GetJobObjects(), firstJobObject);
        }
    }
}