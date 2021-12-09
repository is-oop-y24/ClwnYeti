namespace BackupsExtra.Interfaces
{
    public interface IConfigurationOfCombinedCleaner
    {
        public bool IsNeededToDelete(bool firstCheck, bool secondCheck);
    }
}