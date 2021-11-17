using Backups.Interfaces;
using Backups.Tools;

namespace Backups.Services
{
    public class BackupJobBuilder
    {
        private IArchiver _archiver = null;
        private IStorageRepository _repository = null;
        private string _directory = "- -";

        public void WithArchiver(IArchiver archiver)
        {
            _archiver = archiver;
        }

        public void WithStorageRepository(IStorageRepository repository)
        {
            _repository = repository;
        }

        public void WithDirectory(string directory)
        {
            _directory = directory;
        }

        public BackupJob Create()
        {
            if (_archiver == null)
            {
                throw new BackupsException("Archiver didn't chose");
            }

            if (_repository == null)
            {
                throw new BackupsException("A way how to store files didn't chose");
            }

            if (_directory == "- -")
            {
                throw new BackupsException("Directory didn't chose");
            }

            return new BackupJob(_archiver, _repository, _directory);
        }
    }
}