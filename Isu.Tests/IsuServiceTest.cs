using Isu.Classes;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3202");
            Student student = _isuService.AddStudent(group, "Миксаил Кузутов");
            Assert.IsTrue(student.GroupId == group.Id);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3202");
                for (int i = 0; i < 31; i++)
                {
                    _isuService.AddStudent(group, "Миксаил Кузутов");
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("52202");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group firstGroup = _isuService.AddGroup("M3202");
            Student student = _isuService.AddStudent(firstGroup, "Миксаил Кузутов");
            Group secondGroup = _isuService.AddGroup("M3201");
            _isuService.ChangeStudentGroup(student, secondGroup);
            Assert.True(student.GroupId == secondGroup.Id);
        }
    }
}