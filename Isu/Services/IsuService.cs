using System;
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
        private readonly List<Lesson> _isuGroupLessons;

        public IsuService()
        {
            _isuGroupLessons = new List<Lesson>();
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
                throw new IsuException($"Group name \"{name}\" is invalid for Isu Group");
            }
        }

        public Student AddStudent(Group group, string name)
        {
            if (!Group.IsGroupNameValidForIsuGroup(group.Name))
            {
                throw new IsuException($"Group isn't ISU group");
            }

            _students.Add(new Student(group.Id, name, new Id(_students.Count), group.CourseNumber));
            _groups[group.Id.Value] = _groups[group.Id.Value].AddStudent();
            return _students[^1];
        }

        public Student GetStudent(int id)
        {
            foreach (Student i in _students.Where(i => i.Id.Value == id))
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

        public Group GetGroup(Id groupId)
        {
            foreach (Group g in _groups.Where(g => g.Id.Value == groupId.Value))
            {
                return g;
            }

            throw new IsuException($"Gsa group with Id {groupId.Value} doesn't exist");
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups.Where(i => i.CourseNumber == courseNumber).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            Group group = null;
            foreach (Group j in _groups.Where(j => j.Id.Value == student.GroupId.Value))
            {
                group = j.DeleteStudent();
            }

            if (group == null)
            {
                throw new IsuException("Student doesn't have a group");
            }

            _groups[group.Id.Value] = group;
            _groups[newGroup.Id.Value] = _groups[newGroup.Id.Value].AddStudent();
            _students[student.Id.Value] = student.ChangeGroup(newGroup.Id);
        }

        public Lesson AddLesson(Group group, TimeSpan startTime, DayOfWeek dayOfWeek, string cabinet, string nameOfTeacher)
        {
            _isuGroupLessons.Add(new Lesson(startTime, group.Id, dayOfWeek, cabinet, nameOfTeacher));
            return _isuGroupLessons[^1];
        }

        public List<Lesson> GetLessonsOfGroup(int groupId)
        {
            return _isuGroupLessons.Where(l => l.GroupId.Value == groupId).ToList();
        }

        public List<Student> AllStudents()
        {
            return _students;
        }
}
}