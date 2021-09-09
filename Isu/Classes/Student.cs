using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Isu.Tools;

namespace Isu.Classes
{
    public class Student
    {
        private Group _group = null;
        public Student(Group group, string name, int id)
        {
            Group = group;
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
        public Group Group
        {
            get => _group;
            set
            {
                if (_group != null)
                {
                    _group.DeleteStudent(this);
                }

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