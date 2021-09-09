using Isu.Tools;

namespace Isu.Classes
{
    public class CourseNumber
    {
        public CourseNumber(char name)
        {
            if (IsCourseNumberValid(name))
            {
                Name = name;
            }
            else
            {
                throw new IsuException($"Course can't have number \"{name}\"");
            }
        }

        public char Name { get; }

        private bool Equals(CourseNumber obj)
        {
            return Name == obj.Name;
        }

        private bool IsCourseNumberValid(char name)
        {
            return name > '0' && name <= '4';
        }
    }
}