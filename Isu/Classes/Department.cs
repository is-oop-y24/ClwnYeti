using System.Collections.Generic;
using System.Threading;

namespace Isu.Classes
{
    public class Department
    {
        public Department()
        {
            Courses = new List<Course>(4)
            {
                new Course(new CourseNumber('1')),
                new Course(new CourseNumber('2')),
                new Course(new CourseNumber('3')),
                new Course(new CourseNumber('4')),
            };
        }

        public List<Course> Courses { get; }
        public int NumOfStudents { get; }

        public int Count()
        {
            int c = 0;
            foreach (Course i in Courses)
            {
                c += i.Count();
            }

            return c;
        }
    }
}