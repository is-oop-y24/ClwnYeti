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
        private readonly IEnumerable<ICleanerPoints> _cleaners;
        public CleanerCombined(IConfigurationOfCombinedCleaner configuration, IEnumerable<ICleanerPoints> cleaners)
        {
            _configuration = configuration;
            _cleaners = cleaners;
        }

        public IEnumerable<RestorePoint> Clean(IEnumerable<RestorePoint> points)
        {
            return points.Where(IsNeededToClean);
        }

        public bool IsNeededToClean(RestorePoint point)
        {
            return _configuration.IsNeededToDelete(_cleaners.Select(c => c.IsNeededToClean(point)));
        }
    }
}