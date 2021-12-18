using Backups.Classes;

namespace BackupsExtra.Service
{
    public class RestorationManager
    {
        public void RestoreFilesFromRestorePoint(RestorePoint restorePoint)
        {
            restorePoint.GetRepository().Restore(null);
        }

        public void RestoreFilesFromRestorePoint(RestorePoint restorePoint, string newPath)
        {
            restorePoint.GetRepository().Restore(newPath);
        }
    }
}