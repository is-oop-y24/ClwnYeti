using System.Collections.Generic;
using Backups.Classes;

namespace Backups.Interfaces
{
    public interface ISolverWhatToDoWithPoints
    {
        public RestorePoint Do(RestorePoint lastChosenPoint, IEnumerable<RestorePoint> otherPoints, ILogger logger, IArchiver archiver);
    }
}