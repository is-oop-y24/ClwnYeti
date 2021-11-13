namespace Backups.Classes
{
    public class Storage
    {
        public Storage(string pathToArchive)
        {
            PathToArchive = pathToArchive;
        }

        public string PathToArchive { get; }
    }
}