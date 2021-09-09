using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Classes
{
    public class Group
    {
        public Group(string name, Course course)
        {
            if (IsNameCorrect(name))
            {
                Name = name;
                course.AddGroup(this);
                Students = new List<Student>(30);
            }
            else
            {
                throw new IsuException($"Group name \"{name}\" is invalid");
            }
        }

        public string Name { get; }
        public List<Student> Students { get; }

        public void DeleteStudent(Student st)
        {
            if (!IsStudentInGroup(st))
            {
                Students.Remove(st);
            }
            else
            {
                throw new IsuException($"{st.Name} isn't in a group \"{this.Name}\"");
            }
        }

        public void AddStudent(Student st)
        {
            if (!IsGroupCrowded())
            {
                Students.Add(st);
            }
            else
            {
                throw new IsuException($"Group \"{this.Name}\" is crowded");
            }
        }

        public int Count()
        {
            return Students.Count;
        }

        public bool IsStudentInGroup(Student st)
        {
            foreach (Student i in Students)
            {
                if (i == st)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsNameCorrect(string name)
        {
            return name.Length == 5 && name[0] == 'M' && name[1] == '3' && name[2] > '0' && name[2] <= '4';
        }

        private bool IsGroupCrowded()
        {
            return Students.Count >= 30;
        }
    }
}