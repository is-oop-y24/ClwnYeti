using Backups.Interfaces;
using Backups.Tools;

namespace Backups.Services
{
    public class BackupJobBuilder
    {
        private IArchiver _archiver = null;
        private ILogger _logger = null;
        private string _directory = "- -";

        public void WithArchiver(IArchiver archiver)
        {
            _archiver = archiver;
        }

        public void WithDirectory(string directory)
        {
            _directory = directory;
        }

        public void WithLogger(ILogger logger)
        {
            _logger = logger;
        }

        public BackupJob Create()
        {
            if (_archiver == null)
            {
                throw new BackupsException("Archiver didn't chose");
            }

            if (_logger == null)
            {
                throw new BackupsException("A logger didn't chose");
            }

            if (_directory == "- -")
            {
                throw new BackupsException("Directory didn't chose");
            }

            return new BackupJob(_archiver, _directory, _logger);
        }
    }
}