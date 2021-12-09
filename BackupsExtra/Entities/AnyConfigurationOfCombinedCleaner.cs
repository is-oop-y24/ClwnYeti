using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class AnyConfigurationOfCombinedCleaner : IConfigurationOfCombinedCleaner
    {
        public bool IsNeededToDelete(bool firstCheck, bool secondCheck)
        {
            return firstCheck || secondCheck;
        }
    }
}