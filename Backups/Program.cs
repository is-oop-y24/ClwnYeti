using System.IO;
using Backups.Classes;
using Backups.Interfaces;
using Backups.Services;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            FileStream firstFile = File.Create("C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\1.txt");
            firstFile.Close();
            FileStream secondFile = File.Create("C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\2.txt");
            secondFile.Close();
            FileStream thirdFile = File.Create("C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\3.txt");
            thirdFile.Close();
            FileStream fourthFile = File.Create("C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\4.txt");
            fourthFile.Close();
            var firstTest = new BackupJob(new SplitStoragesArchiver(), new StorageRepositoryWithFileSystem(), "C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\Backups");
            var secondTest = new BackupJob(new SingleStorageArchiver(), new StorageRepositoryWithFileSystem(), "C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\Backups");
            firstTest.Add(firstFile.Name);
            firstTest.Add(secondFile.Name);
            firstTest.MakeRestorePoint();
            firstTest.Delete(firstFile.Name);
            firstTest.MakeRestorePoint();
            secondTest.Add(thirdFile.Name);
            secondTest.Add(fourthFile.Name);
            secondTest.MakeRestorePoint();
            secondTest.Add(firstFile.Name);
            secondTest.MakeRestorePoint();
        }
    }
}
