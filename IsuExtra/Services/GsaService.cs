using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Classes;
using Isu.Services;
using IsuExtra.Classes;
using IsuExtra.Tools;
using Group = Isu.Classes.Group;

namespace IsuExtra.Services
{
    public class GsaService
    {
        private readonly IsuService _isuService;
        private readonly List<(int, int)> _gsaInfo;
        private readonly List<Group> _gsaGroups;
        private readonly List<Lesson> _ordinaryLessons;
        private readonly List<Lesson> _lessonsOfGsa;
        private readonly int _maxNumOfStudents;

        public GsaService()
        {
            _maxNumOfStudents = 30;
            _lessonsOfGsa = new List<Lesson>();
            _ordinaryLessons = new List<Lesson>();
            _gsaInfo = new List<(int, int)>();
            _isuService = new IsuService();
            _gsaGroups = new List<Group>();
        }

        public Group AddGroup(string name)
        {
            return _isuService.AddGroup(name);
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

        public void AddGsaGroupForStudent(Student student, Group gsaGroup)
        {
            if (CheckCollisionOfLessons(student.GroupId, gsaGroup.Id, false))
            {
                throw new IsuExtraException("Student has a collision of lessons");
            }

            if (_gsaInfo[student.Id].Item1 == -1)
            {
                _gsaInfo[student.Id] = (gsaGroup.Id, -1);
            }
            else
            {
                if (CheckCollisionOfLessons(_gsaInfo[student.Id].Item1, gsaGroup.Id, true))
                {
                    throw new IsuExtraException("Student has a collision of lessons");
                }

                _gsaInfo[student.Id] = (_gsaInfo[student.Id].Item1, gsaGroup.Id);
            }
        }

        public Lesson AddLessonForOrdinaryGroup(Group group, TimeSpan startTime, DayOfWeek dayOfWeek)
        {
            if (IsGroupHaveCollisionWithLesson(startTime, dayOfWeek, group.Id, false))
            {
                throw new IsuExtraException("Lesson have collision with group schedule");
            }

            _ordinaryLessons.Add(new Lesson(startTime, group.Id, dayOfWeek));
            return _ordinaryLessons[^1];
        }

        public Lesson AddLessonForGsaGroup(Group group, TimeSpan startTime, DayOfWeek dayOfWeek)
        {
            if (IsGroupHaveCollisionWithLesson(startTime, dayOfWeek, group.Id, true))
            {
                throw new IsuExtraException("Lesson have collision with group schedule");
            }

            _lessonsOfGsa.Add(new Lesson(startTime, group.Id, dayOfWeek));
            return _lessonsOfGsa[^1];
        }

        public Student AddStudent(Group group, string name)
        {
            Student student = _isuService.AddStudent(group, name);
            _gsaInfo.Add((-1, -1));
            return student;
        }

        public Student GetStudent(int id)
        {
            return _isuService.GetStudent(id);
        }

        public Student FindStudent(string name)
        {
            return _isuService.FindStudent(name);
        }

        public List<Student> FindStudents(string groupName)
        {
            return _isuService.FindStudents(groupName);
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _isuService.FindStudents(courseNumber);
        }

        public Group FindGroup(string groupName)
        {
            return _isuService.FindGroup(groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _isuService.FindGroups(courseNumber);
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            _isuService.ChangeStudentGroup(student, newGroup);
        }

        private bool CheckCollisionOfLessons(int firstGroupId, int secondGroupId, bool isFirstGroupGsa)
        {
            List<Lesson> lessons = isFirstGroupGsa ? _lessonsOfGsa : _ordinaryLessons;

            foreach (Lesson lessonOfFirstGroup in lessons)
            {
                if (lessonOfFirstGroup.GroupId != firstGroupId) continue;
                foreach (Lesson lessonOfSecondGroup in _lessonsOfGsa.Where(lesson => lesson.GroupId == secondGroupId))
                {
                    if (lessonOfSecondGroup.DayOfWeek != lessonOfFirstGroup.DayOfWeek) continue;
                    if (lessonOfSecondGroup.StartTime + new TimeSpan(1, 30, 0) < lessonOfFirstGroup.StartTime
                            && lessonOfFirstGroup.StartTime >= lessonOfSecondGroup.StartTime)
                    {
                        return true;
                    }

                    if (lessonOfFirstGroup.StartTime + new TimeSpan(1, 30, 0) < lessonOfSecondGroup.StartTime
                            && lessonOfSecondGroup.StartTime >= lessonOfFirstGroup.StartTime)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsGroupHaveCollisionWithLesson(TimeSpan startTime, DayOfWeek dayOfWeek, int groupId, bool isGroupGsa)
        {
            List<Lesson> lessons = isGroupGsa ? _lessonsOfGsa : _ordinaryLessons;
            foreach (Lesson lessonOfGroup in lessons.Where(l => l.GroupId == groupId))
            {
                if (lessonOfGroup.DayOfWeek != dayOfWeek) continue;
                if (lessonOfGroup.StartTime + new TimeSpan(1, 30, 0) < startTime
                        && startTime >= lessonOfGroup.StartTime)
                {
                    return true;
                }

                if (startTime + new TimeSpan(1, 30, 0) < lessonOfGroup.StartTime
                        && lessonOfGroup.StartTime >= startTime)
                {
                        return true;
                }
            }

            return false;
        }
    }
}