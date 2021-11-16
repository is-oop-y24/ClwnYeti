namespace Backups.Classes
{
    public class JobObject
    {
        public JobObject(string pathToFile)
        {
            Path = pathToFile;
        }

        public string Path { get; }
    }
}