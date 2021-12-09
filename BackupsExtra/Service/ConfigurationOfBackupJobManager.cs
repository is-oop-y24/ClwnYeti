using System;
using System.IO;
using Backups.Services;
using BackupsExtra.Tools;
using Newtonsoft.Json;

namespace BackupsExtra.Service
{
    public class ConfigurationOfBackupJobManager
    {
        public BackupJob CreateBackupJobFromFile(string fileName)
        {
            try
            {
                return (BackupJob)JsonConvert.DeserializeObject(File.ReadAllText(fileName));
            }
            catch (Exception)
            {
                throw new BackupExtraException("Creation of backup job was failed");
            }
        }

        public void SaveBackupJobIntoFile(BackupJob backupJob)
        {
            File.WriteAllText(backupJob.Id.ToString() + ".json", JsonConvert.SerializeObject(backupJob));
        }
    }
}