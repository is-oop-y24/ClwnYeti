using System.Text.RegularExpressions;
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
            _firstBackupJob.Add(firstFileName);
            _firstBackupJob.Add(secondFileName);
            _firstBackupJob.MakeRestorePoint();
            _firstBackupJob.Delete(firstFileName);
            _firstBackupJob.Add(thirdFileName);
            _firstBackupJob.MakeRestorePoint();
            Assert.True(true);
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
            _secondBackupJob.Add(firstFileName);
            _secondBackupJob.Add(secondFileName);
            _secondBackupJob.MakeRestorePoint();
            _secondBackupJob.Delete(firstFileName);
            _secondBackupJob.Add(thirdFileName);
            _secondBackupJob.MakeRestorePoint();
            Assert.True(true);
        }
    }
}