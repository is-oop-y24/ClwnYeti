using System;
using System.Collections.Generic;
using Backups.Classes;
using Backups.Interfaces;

namespace BackupsExtra.Entities
{
    public class Deleter : ISolverWhatToDoWithPoints
    {
        public RestorePoint Do(RestorePoint lastChosenPoint, IEnumerable<RestorePoint> otherPoints, ILogger logger, IArchiver archiver)
        {
            foreach (RestorePoint restorePoint in otherPoints)
            {
                try
                {
                    restorePoint.Delete(logger);
                }
                catch (Exception e)
                {
                    logger.Log(e.Message);
                    throw e;
                }

                logger.Log("Restore point was deleted");
            }

            return lastChosenPoint;
        }
    }
}