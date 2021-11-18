namespace Backups.Classes
{
    public class JobObject
    {
        public JobObject(string pathToFile)
        {
            Path = pathToFile;
        }

        public string Path { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((JobObject)obj);
        }

        public override int GetHashCode()
        {
            return Path != null ? Path.GetHashCode() : 0;
        }

        private bool Equals(JobObject other)
        {
            return Path == other.Path;
        }
    }
}