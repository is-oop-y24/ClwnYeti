using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class AnyConfigurationOfCombinedCleaner : IConfigurationOfCombinedCleaner
    {
        public bool IsNeededToDelete(IEnumerable<bool> checkers)
        {
            return checkers.Any(checker => checker);
        }
    }
}