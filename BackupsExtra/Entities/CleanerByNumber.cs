using System.Collections.Generic;
using System.Linq;
using Backups.Classes;
using Backups.Interfaces;

namespace BackupsExtra.Entities
{
    public class CleanerByNumber : ICleanerPoints
    {
        private readonly int _number;

        public CleanerByNumber(int number)
        {
            _number = number;
        }

        public IEnumerable<RestorePoint> Clean(IEnumerable<RestorePoint> points)
        {
            return points.Where(p => p.Number < _number);
        }
    }
}