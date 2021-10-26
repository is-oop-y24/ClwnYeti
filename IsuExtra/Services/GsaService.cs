using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Classes;
using IsuExtra.Classes;
using IsuExtra.Tools;
using Group = Isu.Classes.Group;

namespace IsuExtra.Services
{
    public class GsaService
    {
        private readonly List<GsaInfoAboutStudent> _gsaInfo;
        private readonly List<Group> _gsaGroups;
        private readonly List<Lesson> _gsaGroupLessons;
        private readonly int _maxNumOfStudents;

        public GsaService()
        {
            _maxNumOfStudents = 30;
            _gsaGroupLessons = new List<Lesson>();
            _gsaInfo = new List<GsaInfoAboutStudent>();
            _gsaGroups = new List<Group>();
        }

        public Group AddGsaGroup(string name)
        {
            if (!Group.IsGroupNameValidForGsaGroup(name))
            {
                throw new IsuExtraException($"Group name \"{name}\" is invalid for gsa");
            }

            _gsaGroups.Add(new Group(name, _gsaGroups.Count, (CourseNumber)(name[2] - 48), _maxNumOfStudents));
            return _gsaGroups[^1];
        }

        public Group FindGroup(string groupName)
        {
            return _gsaGroups.FirstOrDefault(g => g.Name == groupName);
        }

        public Group GetGroup(string groupName)
        {
            foreach (Group g in _gsaGroups.Where(g => g.Name == groupName))
            {
                return g;
            }

            throw new IsuExtraException($"Gsa group with name {groupName} doesn't exist");
        }

        public List<Group> GetGsaGroupsOfStudent(Student student)
        {
            GsaInfoAboutStudent gsaInfoAboutStudent = GetGsaInfoOfStudent(student.Id);
            var gsaGroupsOfStudent = new List<Group>
            {
                FindGroup(gsaInfoAboutStudent.FirstGsaGroupId),
                FindGroup(gsaInfoAboutStudent.SecondGsaGroupId),
            };
            return gsaGroupsOfStudent;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _gsaGroups.Where(g => g.CourseNumber == courseNumber).ToList();
        }

        public List<Id> GetStudentsIdOfGsaGroup(Id groupId)
        {
            return (from g in _gsaInfo where g.FirstGsaGroupId == groupId || g.SecondGsaGroupId == groupId select g.StudentId).ToList();
        }

        public void AddGsaGroupForStudent(Student student, Group gsaGroup)
        {
            if (_gsaInfo[student.Id.Value].FirstGsaGroupId.Value == -1)
            {
                _gsaInfo[student.Id.Value] = new GsaInfoAboutStudent(gsaGroup.Id, _gsaInfo[student.Id.Value].SecondGsaGroupId, student.Id);
            }
            else if (_gsaInfo[student.Id.Value].SecondGsaGroupId.Value == -1)
            {
                _gsaInfo[student.Id.Value] = new GsaInfoAboutStudent(_gsaInfo[student.Id.Value].FirstGsaGroupId, gsaGroup.Id, student.Id);
            }
            else
            {
                throw new IsuExtraException("Student already had two GSA groups");
            }

            _gsaGroups[gsaGroup.Id.Value] = gsaGroup.AddStudent();
        }

        public Lesson AddLesson(Group group, TimeSpan startTime, DayOfWeek dayOfWeek, string cabinet, string nameOfTeacher)
        {
            _gsaGroupLessons.Add(new Lesson(startTime, group.Id, dayOfWeek, cabinet, nameOfTeacher));
            return _gsaGroupLessons[^1];
        }

        public List<Lesson> GetLessonsOfGroup(int groupId)
        {
            return _gsaGroupLessons.Where(l => l.GroupId.Value == groupId).ToList();
        }

        public GsaInfoAboutStudent AddStudentIntoInfo(Id studentId)
        {
            _gsaInfo.Add(new GsaInfoAboutStudent(new Id(-1), new Id(-1), studentId));
            return _gsaInfo[^1];
        }

        public bool IsStudentHaveTwoGsaGroups(Id studentId)
        {
            GsaInfoAboutStudent idOfGsaGroupOfStudents = GetGsaInfoOfStudent(studentId);
            return idOfGsaGroupOfStudents.FirstGsaGroupId.Value != -1 &&
                   idOfGsaGroupOfStudents.SecondGsaGroupId.Value != -1;
        }

        public void DeleteStudentFromGsaGroup(Id studentId, Group group)
        {
            if (!Group.IsGroupNameValidForGsaGroup(group.Name))
            {
                throw new IsuExtraException($"Gsa group with name {group.Name} doesn't exist");
            }

            GsaInfoAboutStudent gsaInfoAboutStudent = GetGsaInfoOfStudent(studentId);
            if (gsaInfoAboutStudent.FirstGsaGroupId == group.Id)
            {
                _gsaInfo[studentId.Value] =
                    new GsaInfoAboutStudent(new Id(-1), gsaInfoAboutStudent.SecondGsaGroupId, studentId);
            }
            else if (gsaInfoAboutStudent.SecondGsaGroupId == group.Id)
            {
                _gsaInfo[studentId.Value] =
                    new GsaInfoAboutStudent(gsaInfoAboutStudent.FirstGsaGroupId, new Id(-1), studentId);
            }
            else
            {
                throw new IsuExtraException("Student isn't in this gsa group");
            }
        }

        private Group FindGroup(Id groupId)
        {
            return _gsaGroups.FirstOrDefault(g => g.Id == groupId);
        }

        private GsaInfoAboutStudent GetGsaInfoOfStudent(Id studentId)
        {
            return _gsaInfo.FirstOrDefault(g => g.StudentId == studentId);
        }
    }
}