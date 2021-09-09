using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Classes
{
    public class Course
    {
        public Course(CourseNumber name)
        {
            CourseNumber = name;
            Groups = new List<Group>(100);
        }

        public CourseNumber CourseNumber { get; }
        public List<Group> Groups { get; }

        public void AddGroup(Group group)
        {
            if (IsGroupValidForCourse(group))
            {
                Groups.Add(group);
            }
            else
            {
                throw new IsuException($"Group name \"{group.Name}\" isn't match to course {CourseNumber}");
            }
        }

        public int Count()
        {
            int c = 0;
            foreach (Group i in Groups)
            {
                c += i.Count();
            }

            return c;
        }

        private bool IsGroupValidForCourse(Group group)
        {
            return group.Name[2] == CourseNumber.Name;
        }

        private bool IsCourseNumberValid(char name)
        {
            return name > '0' && name <= '4';
        }
    }
}