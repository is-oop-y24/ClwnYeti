using System;
using Isu.Classes;
using Isu.Services;

namespace Isu
{
    internal class Program
    {
        private static void Main()
        {
            var isuService = new IsuService();
            Group firstGroup = isuService.AddGroup("M3202");
            Console.Write(firstGroup.Id.Value + "\n");
            Student student = isuService.AddStudent(firstGroup, "Миксаил Кузутов");
            Console.Write(isuService.FindStudent(student.Name).GroupId.Value + "\n");
            Group secondGroup = isuService.AddGroup("M3201");
            Console.Write(secondGroup.Id.Value + "\n");
            isuService.ChangeStudentGroup(student, secondGroup);
            Console.Write(isuService.FindStudent(student.Name).GroupId.Value + "\n");
        }
    }
}
