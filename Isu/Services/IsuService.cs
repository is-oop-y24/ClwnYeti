using System;
using System.Collections.Generic;
using Isu.Classes;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private Department _dep;

        public IsuService()
        {
            _dep = new Department();
        }

        public Group AddGroup(string name)
        {
            return name[2] switch
            {
                '1' => new Group(name, _dep.Courses[0]),
                '2' => new Group(name, _dep.Courses[1]),
                '3' => new Group(name, _dep.Courses[2]),
                '4' => new Group(name, _dep.Courses[3]),
                _ => throw new IsuException($"Group name \"{name}\" is invalid")
            };
        }

        public Student AddStudent(Group group, string name)
        {
            return new Student(group, name, _dep.Count());
        }

        public Student GetStudent(int id)
        {
            foreach (Course i in _dep.Courses)
            {
                foreach (Group f in i.Groups)
                {
                    foreach (Student r in f.Students)
                    {
                        if (r.Id == id)
                        {
                            return r;
                        }
                    }
                }
            }

            return null;
        }

        public Student FindStudent(string name)
        {
            foreach (Course i in _dep.Courses)
            {
                foreach (Group f in i.Groups)
                {
                    foreach (Student r in f.Students)
                    {
                        if (r.Name == name)
                        {
                            return r;
                        }
                    }
                }
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            foreach (Course i in _dep.Courses)
            {
                foreach (Group f in i.Groups)
                {
                    if (f.Name == groupName)
                    {
                        return f.Students;
                    }
                }
            }

            return new List<Student>();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            foreach (Course i in _dep.Courses)
            {
                if (i.CourseNumber == courseNumber)
                {
                    var st = new List<Student>(i.Count());
                    foreach (Group f in i.Groups)
                    {
                        foreach (Student r in f.Students)
                        {
                            st.Add(r);
                        }
                    }

                    return st;
                }
            }

            return new List<Student>();
        }

        public Group FindGroup(string groupName)
        {
            foreach (Course i in _dep.Courses)
            {
                foreach (Group f in i.Groups)
                {
                    if (f.Name == groupName)
                    {
                        return f;
                    }
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            foreach (Course i in _dep.Courses)
            {
                if (i.CourseNumber == courseNumber)
                {
                    return i.Groups;
                }
            }

            return new List<Group>();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            student.Group = newGroup;
        }
    }
}