using System.Collections.Generic;

namespace BackupsExtra.Interfaces
{
    public interface IConfigurationOfCombinedCleaner
    {
        public bool IsNeededToDelete(IEnumerable<bool> checkers);
    }
}