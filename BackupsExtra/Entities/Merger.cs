using System.Collections.Generic;
using System.Linq;
using Backups.Classes;
using Backups.Interfaces;
using Backups.Services;

namespace BackupsExtra.Entities
{
    public class Merger : ISolverWhatToDoWithPoints
    {
        public RestorePoint Do(RestorePoint lastChosenPoint, IEnumerable<RestorePoint> otherPoints, ILogger logger, IArchiver archiver)
        {
            foreach (RestorePoint restorePoint in otherPoints)
            {
                lastChosenPoint = Merge(lastChosenPoint, restorePoint, archiver, logger);
                logger.Log("Restore point was merged");
            }

            return lastChosenPoint;
        }

        private static RestorePoint Merge(RestorePoint first, RestorePoint second, IArchiver archiver, ILogger logger)
        {
            second.Delete(logger);
            if (archiver.GetType() == typeof(SingleStorageArchiver))
            {
                return first;
            }

            IEnumerable<JobObject> objectsOfFirst = first.GetJobObjects();
            IEnumerable<JobObject> objects = second.GetJobObjects().Where(o => !objectsOfFirst.Contains(o));
            return new RestorePoint(first.Number, archiver.StoreIntoCurrentRepository(first.GetRepository(), first.BackupPath, first.Number, objects.ToList(), logger), first.BackupPath, first.Date);
        }
    }
}