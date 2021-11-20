using System;
using Isu.Classes;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuExtraTest
    {
        private IsuExtraService _isuExtraService;

        [SetUp]
        public void Setup()
        {
            _isuExtraService = new IsuExtraService();
        }

        [Test]
        public void CreateGsaGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuExtraException>(() =>
            {            
                _isuExtraService.AddGsaGroup("M3302");
            });
        }
        
        
        [Test]
        public void AddStudentToGsaGroup_DepartmentsAreEqual()
        {
            Assert.Catch<IsuExtraException>(() =>
            {  
                Group isuGroup = _isuExtraService.AddIsuGroup("M3202");
                Student student = _isuExtraService.AddStudent(isuGroup, "Миксаил Кузутов");
                Group gsaGroup = _isuExtraService.AddGsaGroup("M322");
                _isuExtraService.AddGsaGroupForStudent(student, gsaGroup);
            });
        }
        
        [Test]
        public void AddStudentToGsaGroup_ItIsIsuGroup()
        {
            Assert.Catch<IsuExtraException>(() =>
            {  
                Group isuGroup = _isuExtraService.AddIsuGroup("M3202");
                Student student = _isuExtraService.AddStudent(isuGroup, "Миксаил Кузутов");
                _isuExtraService.AddGsaGroupForStudent(student, isuGroup);
            });
        }
        
        [Test]
        public void DeleteStudentFromGsaGroup_StudentIsNotInGroup()
        {
            Assert.Catch<IsuExtraException>(() => 
            {  
                Group isuGroup = _isuExtraService.AddIsuGroup("M3202");
                Student student = _isuExtraService.AddStudent(isuGroup, "Миксаил Кузутов");
                Group gsaGroup = _isuExtraService.AddGsaGroup("N322"); 
                _isuExtraService.DeleteStudentFromGsaGroup(student, gsaGroup);
            });
        }
        
        [Test]
        public void AddStudentToGsaGroup_StudentHasCollisionOfSchedules()
        {
            Assert.Catch<IsuExtraException>(() => 
            { 
                Group isuGroup = _isuExtraService.AddIsuGroup("M3202");
                Group gsaGroup = _isuExtraService.AddGsaGroup("N322"); 
                _isuExtraService.AddLessonForIsuGroup(isuGroup, new TimeSpan(1, 30, 0), DayOfWeek.Monday, "330",
                    "Владислав Повышев");
                 _isuExtraService.AddLessonForGsaGroup(gsaGroup, new TimeSpan(1, 30, 0), DayOfWeek.Monday, "330",
                    "Владислав Повышев");
                Student student = _isuExtraService.AddStudent(isuGroup, "Миксаил Кузутов");
                _isuExtraService.AddGsaGroupForStudent(student, gsaGroup);
            });
        }
        
        [Test]
        public void AddStudentToGsaGroup_CoursesAreNotEqual()
        {
            Assert.Catch<IsuExtraException>(() => 
            {  
                Group isuGroup = _isuExtraService.AddIsuGroup("M3202");
                Student student = _isuExtraService.AddStudent(isuGroup, "Миксаил Кузутов");
                Group gsaGroup = _isuExtraService.AddGsaGroup("N342");
                _isuExtraService.AddGsaGroupForStudent(student, gsaGroup);
            });
        }
        
        [Test]
        public void AddStudentToGsaGroup_StudentHasTwoGsaGroup()
        {
            Assert.Catch<IsuExtraException>(() => 
            {  
                Group isuGroup = _isuExtraService.AddIsuGroup("M3202");
                Student student = _isuExtraService.AddStudent(isuGroup, "Миксаил Кузутов");
                Group firstGsaGroup = _isuExtraService.AddGsaGroup("N322"); 
                Group secondGsaGroup = _isuExtraService.AddGsaGroup("R322"); 
                Group thirdGsaGroup = _isuExtraService.AddGsaGroup("F322"); 
                _isuExtraService.AddGsaGroupForStudent(student, firstGsaGroup);
                _isuExtraService.AddGsaGroupForStudent(student, secondGsaGroup);
                _isuExtraService.AddGsaGroupForStudent(student, thirdGsaGroup);
            });
        }
    }
}