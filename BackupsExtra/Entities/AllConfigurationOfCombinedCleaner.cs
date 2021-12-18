using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class AllConfigurationOfCombinedCleaner : IConfigurationOfCombinedCleaner
    {
        public bool IsNeededToDelete(IEnumerable<bool> checkers)
        {
            return checkers.All(checker => checker);
        }
    }
}