using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Classes;
using Isu.Services;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuExtraService
    {
        private readonly IsuService _isuService;
        private readonly GsaService _gsaService;

        public IsuExtraService()
        {
            _gsaService = new GsaService();
            _isuService = new IsuService();
        }

        public Group AddIsuGroup(string name)
        {
            return _isuService.AddGroup(name);
        }

        public Group AddGsaGroup(string name)
        {
            return _gsaService.AddGsaGroup(name);
        }

        public void AddGsaGroupForStudent(Student student, Group group)
        {
            Group isuGroup = _isuService.GetGroup(group.Id);
            if (isuGroup.Name[0] == group.Name[0])
            {
                throw new IsuExtraException("Student's department is equal to department of GSA group");
            }

            if (student.CourseNumber == group.CourseNumber)
            {
                throw new IsuExtraException("Student ");
            }

            if (IsGroupHaveCollisionWithGsaGroup(isuGroup.Id, group.Id, false))
            {
                throw new IsuExtraException("Student's ISU group schedule have collision with schedule of this GSA group");
            }

            List<Group> gsaGroupsOfStudent = _gsaService.GetGsaGroupsOfStudent(student);
            if (gsaGroupsOfStudent[0] == null)
            {
                if (gsaGroupsOfStudent[1] == null)
                {
                    _gsaService.AddGsaGroupForStudent(student, group);
                }
                else
                {
                    if (IsGroupHaveCollisionWithGsaGroup(gsaGroupsOfStudent[1].Id, group.Id, true))
                    {
                        throw new IsuExtraException("Student's GSA group schedule have collision with schedule of this GSA group");
                    }

                    _gsaService.AddGsaGroupForStudent(student, group);
                }
            }
            else if (gsaGroupsOfStudent[1] == null)
            {
                if (IsGroupHaveCollisionWithGsaGroup(gsaGroupsOfStudent[0].Id, group.Id, true))
                {
                    throw new IsuExtraException("Student's GSA group schedule have collision with schedule of this GSA group");
                }

                _gsaService.AddGsaGroupForStudent(student, group);
            }
            else
            {
                throw new IsuExtraException("Student already had two GSA groups");
            }
        }

        public Lesson AddLessonForGsaGroup(Group group, TimeSpan startTime, DayOfWeek dayOfWeek, string cabinet, string nameOfTeacher)
        {
            if (IsGroupHaveCollisionWithLesson(startTime, dayOfWeek, group.Id.Value, true))
            {
                throw new IsuExtraException("Lesson have collision with group schedule");
            }

            return _gsaService.AddLesson(group, startTime, dayOfWeek, cabinet, nameOfTeacher);
        }

        public Lesson AddLessonForIsuGroup(Group group, TimeSpan startTime, DayOfWeek dayOfWeek, string cabinet, string nameOfTeacher)
        {
            if (IsGroupHaveCollisionWithLesson(startTime, dayOfWeek, group.Id.Value, false))
            {
                throw new IsuExtraException("Lesson have collision with group schedule");
            }

            return _isuService.AddLesson(group, startTime, dayOfWeek, cabinet, nameOfTeacher);
        }

        public Student AddStudent(Group group, string name)
        {
            Student student = _isuService.AddStudent(group, name);
            _gsaService.AddStudentIntoInfo(student.Id);
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

        public List<Student> FindStudentsOfIsuGroup(string groupName)
        {
            return _isuService.FindStudents(groupName);
        }

        public List<Student> FindStudentsOfGsaGroup(string groupName)
        {
            List<Id> idOfStudents = _gsaService.GetStudentsIdOfGsaGroup(_gsaService.GetGroup(groupName).Id);
            return idOfStudents.Select(i => _isuService.GetStudent(i.Value)).ToList();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _isuService.FindStudents(courseNumber);
        }

        public Group FindIsuGroup(string groupName)
        {
            return _isuService.FindGroup(groupName);
        }

        public Group FindGsaGroup(string groupName)
        {
            return _gsaService.FindGroup(groupName);
        }

        public List<Group> FindIsuGroups(CourseNumber courseNumber)
        {
            return _isuService.FindGroups(courseNumber);
        }

        public List<Group> FindGsaGroups(CourseNumber courseNumber)
        {
            return _gsaService.FindGroups(courseNumber);
        }

        public void ChangeStudentIsuGroup(Student student, Group newGroup)
        {
            _isuService.ChangeStudentGroup(student, newGroup);
        }

        public void DeleteStudentFromGsaGroup(Student student, Group group)
        {
            _gsaService.DeleteStudentFromGsaGroup(student.Id, group);
        }

        public List<Student> FindStudentsWithoutTwoGsaGroup(Group group)
        {
            return _isuService.FindStudents(group.Name).Where(i => _gsaService.IsStudentHaveTwoGsaGroups(i.Id)).ToList();
        }

        public List<Student> FindStudentsWithoutTwoGsaGroup(CourseNumber courseNumber)
        {
            return _isuService.FindStudents(courseNumber).Where(i => _gsaService.IsStudentHaveTwoGsaGroups(i.Id)).ToList();
        }

        public List<Student> FindStudentsWithoutTwoGsaGroup()
        {
            return _isuService.AllStudents().Where(i => _gsaService.IsStudentHaveTwoGsaGroups(i.Id)).ToList();
        }

        private bool IsGroupHaveCollisionWithLesson(TimeSpan startTime, DayOfWeek dayOfWeek, int groupId, bool isGroupGsa)
        {
            List<Lesson> lessons = isGroupGsa ? _gsaService.GetLessonsOfGroup(groupId) : _isuService.GetLessonsOfGroup(groupId);
            return lessons.Any(lessonOfGroup => lessonOfGroup.LessonHaveCollisionWithAnother(startTime, dayOfWeek));
        }

        private bool IsGroupHaveCollisionWithGsaGroup(Id groupId, Id gsaGroupId, bool isFirstGroupGsa)
        {
            List<Lesson> lessonsOfFirstGroup = isFirstGroupGsa ? _gsaService.GetLessonsOfGroup(groupId.Value) : _isuService.GetLessonsOfGroup(groupId.Value);
            return (from lessonOfFirstGroup in lessonsOfFirstGroup from lessonOfSecondGroup in _gsaService.GetLessonsOfGroup(gsaGroupId.Value) where Lesson.LessonsHaveCollision(lessonOfFirstGroup, lessonOfSecondGroup) select lessonOfFirstGroup).Any();
        }
    }
}