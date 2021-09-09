using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Isu.Tools;

namespace Isu.Classes
{
    public class Student
    {
        private Group _group;
        public Student(Group group, string name, int id)
        {
            try
            {
                group.AddStudent(this);
            }
            catch (IsuException e)
            {
                Console.WriteLine(e);
                return;
            }

            Id = id;
            Name = name;
            Group = group;
        }

        public int Id { get; }
        public string Name { get; }
        public Group Group
        {
            get => _group;
            set
            {
                _group.DeleteStudent(this);
                value.AddStudent(this);
                _group = value;
            }
        }

        private bool Equals(Student obj)
        {
            return Id == obj.Id;
        }
    }
}