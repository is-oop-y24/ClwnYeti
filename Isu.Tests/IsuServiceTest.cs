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
            Group g = _isuService.AddGroup("M3202");
            Student s = _isuService.AddStudent(g, "Миксаил Кузутов");
            Assert.IsTrue(s.Group != null && g.IsStudentInGroup(s));
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group g = _isuService.AddGroup("M3202");
                for (int i = 0; i < 31; i++)
                {
                    _isuService.AddStudent(g, "Миксаил Кузутов");
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group g = _isuService.AddGroup("M2202");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {   
                Group g1 = _isuService.AddGroup("M3202");
                Student s = _isuService.AddStudent(g1, "Миксаил Кузутов");
                Group g2 = _isuService.AddGroup("M3201");
                _isuService.ChangeStudentGroup(s, g2);
            });
        }
    }
}