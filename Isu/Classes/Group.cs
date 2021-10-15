using System.Text.RegularExpressions;
using Isu.Tools;

namespace Isu.Classes
{
    public class Group
    {
        private readonly int _maxNumOfStudents;
        public Group(string name, int id, CourseNumber courseNumber, int maxNumOfStudents)
        {
            if (IsNameCorrect(name))
            {
                Name = name;
                NumOfStudents = 0;
                Id = id;
                CourseNumber = courseNumber;
                _maxNumOfStudents = maxNumOfStudents;
            }
            else
            {
                throw new IsuException($"Group name \"{name}\" is invalid");
            }
        }

        public string Name { get; }
        public CourseNumber CourseNumber { get; }
        public int Id { get; }
        private int NumOfStudents { get; set; }

        public void AddStudent()
        {
            if (!IsGroupCrowded())
            {
                NumOfStudents += 1;
            }
            else
            {
                throw new IsuException($"Group \"{Name}\" is crowded");
            }
        }

        public void DeleteStudent()
        {
            if (NumOfStudents > 0)
            {
                NumOfStudents -= 1;
            }
            else
            {
                throw new IsuException($"Group \"{Name}\" is empty");
            }
        }

        private bool IsNameCorrect(string name)
        {
            const string groupNameForCheck = @"[A-Z]{1}[0-9]{1}[1-5]{1}[0-9]{2-3}";
            return Regex.IsMatch(name, groupNameForCheck);
        }

        private bool IsGroupCrowded()
        {
            return NumOfStudents >= _maxNumOfStudents;
        }
    }
}