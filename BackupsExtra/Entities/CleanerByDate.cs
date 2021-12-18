using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Classes;
using Backups.Interfaces;

namespace BackupsExtra.Entities
{
    public class CleanerByDate : ICleanerPoints
    {
        private readonly DateTime _date;

        public CleanerByDate(DateTime date)
        {
            _date = date;
        }

        public IEnumerable<RestorePoint> Clean(IEnumerable<RestorePoint> points)
        {
            return points.Where(IsNeededToClean);
        }

        public bool IsNeededToClean(RestorePoint point)
        {
            return point.Date > _date;
        }
    }
}