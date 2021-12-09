using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Classes;
using Backups.Interfaces;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class CleanerCombined : ICleanerPoints
    {
        private readonly IConfigurationOfCombinedCleaner _configuration;
        private readonly int _number;
        private readonly DateTime _date;
        public CleanerCombined(IConfigurationOfCombinedCleaner configuration, int number, DateTime date)
        {
            _configuration = configuration;
            _number = number;
            _date = date;
        }

        public IEnumerable<RestorePoint> Clean(IEnumerable<RestorePoint> points)
        {
            return points.Where(p => _configuration.IsNeededToDelete(p.Number <= _number, p.Date >= _date)).ToList();
        }
    }
}