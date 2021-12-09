using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class BothConfigurationOfCombinedCleaner : IConfigurationOfCombinedCleaner
    {
        public bool IsNeededToDelete(bool firstCheck, bool secondCheck)
        {
            return firstCheck && secondCheck;
        }
    }
}