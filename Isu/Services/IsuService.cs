using System.Collections.Generic;
using System.Linq;
using Isu.Classes;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly int _maxNumOfStudents;
        private readonly List<Student> _students;
        private readonly List<Group> _groups;

        public IsuService()
        {
            _maxNumOfStudents = 30;
            _students = new List<Student>();
            _groups = new List<Group>();
        }

        public Group AddGroup(string name)
        {
            if (Group.IsGroupNameValidForIsuGroup(name))
            {
                _groups.Add(new Group(name, _groups.Count, (CourseNumber)(name[2] - 48), _maxNumOfStudents));
                return _groups[^1];
            }
            else
            {
                throw new IsuException($"Group name \"{name}\" is invalid");
            }
        }

        public Student AddStudent(Group group, string name)
        {
            _students.Add(new Student(group.Id, name, _students.Count, group.CourseNumber));
            group.AddStudent();
            return _students[^1];
        }

        public Student GetStudent(int id)
        {
            foreach (Student i in _students.Where(i => i.Id == id))
            {
                return i;
            }

            throw new IsuException($"Couldn't find a student with id {id}");
        }

        public Student FindStudent(string name)
        {
            return _students.FirstOrDefault(i => i.Name == name);
        }

        public List<Student> FindStudents(string groupName)
        {
            Group group = null;
            foreach (Group j in _groups.Where(j => j.Name == groupName))
            {
                group = j;
            }

            if (group == null)
            {
                throw new IsuException($"There's no group with name {groupName}");
            }

            return _students.Where(i => i.GroupId == group.Id).ToList();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _students.Where(i => i.CourseNumber == courseNumber).ToList();
        }

        public Group FindGroup(string groupName)
        {
            return _groups.FirstOrDefault(i => i.Name == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups.Where(i => i.CourseNumber == courseNumber).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            foreach (Group j in _groups.Where(j => j.Id == student.GroupId))
            {
                j.DeleteStudent();
            }

            newGroup.AddStudent();
            student.GroupId = newGroup.Id;
        }
    }
}