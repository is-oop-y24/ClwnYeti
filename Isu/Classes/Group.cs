using System.Text.RegularExpressions;
using Isu.Tools;

namespace Isu.Classes
{
    public class Group
    {
        private readonly int _maxNumOfStudents;
        public Group(string name, int id, CourseNumber courseNumber, int maxNumOfStudents)
        {
            Name = name;
            NumOfStudents = 0;
            Id = new Id(id);
            CourseNumber = courseNumber;
            _maxNumOfStudents = maxNumOfStudents;
        }

        private Group(Group other, int numOfStudents)
        {
            Name = other.Name;
            NumOfStudents = numOfStudents;
            Id = other.Id;
            CourseNumber = other.CourseNumber;
            _maxNumOfStudents = other._maxNumOfStudents;
        }

        public string Name { get; }
        public CourseNumber CourseNumber { get; }
        public Id Id { get; }
        private int NumOfStudents { get; }
        public static bool IsGroupNameValidForIsuGroup(string name)
        {
            const string groupNameForCheck = @"[A-Z]{1}[0-9]{1}[1-5]{1}[0-9]{2,3}";
            return Regex.IsMatch(name, groupNameForCheck);
        }

        public static bool IsGroupNameValidForGsaGroup(string name)
        {
            const string groupNameForCheck = @"[A-Z]{1}[0-9]{1}[1-5]{1}[0-9]{1}"; // Department, Stream, Course, Group
            return Regex.IsMatch(name, groupNameForCheck);
        }

        public Group AddStudent()
        {
            if (IsNumOfStudentsValid(NumOfStudents + 1))
            {
                return new Group(this, NumOfStudents + 1);
            }

            throw new IsuException($"Group \"{Name}\" is crowded");
        }

        public Group DeleteStudent()
        {
            if (IsNumOfStudentsValid(NumOfStudents - 1))
            {
                return new Group(this, NumOfStudents + 1);
            }

            throw new IsuException($"Group \"{Name}\" is empty");
        }

        private bool IsNumOfStudentsValid(int numOfStudents)
        {
            return numOfStudents <= _maxNumOfStudents && numOfStudents >= 0;
        }
    }
}