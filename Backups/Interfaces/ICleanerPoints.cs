using System.Collections.Generic;
using Backups.Classes;

namespace Backups.Interfaces
{
    public interface ICleanerPoints
    {
        public IEnumerable<RestorePoint> Clean(IEnumerable<RestorePoint> points);
    }
}