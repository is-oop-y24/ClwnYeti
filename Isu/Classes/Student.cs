namespace Isu.Classes
{
    public class Student
    {
        public Student(int groupId, string name, int id, CourseNumber courseNumber)
        {
            GroupId = new Id(groupId);
            Id = new Id(id);
            CourseNumber = courseNumber;
            Name = name;
        }

        public Student(Id groupId, string name, Id id, CourseNumber courseNumber)
        {
            GroupId = groupId;
            Id = id;
            CourseNumber = courseNumber;
            Name = name;
        }

        private Student(Student other, int newGroupId)
        {
            GroupId = new Id(newGroupId);
            Id = other.Id;
            CourseNumber = other.CourseNumber;
            Name = other.Name;
        }

        public Id Id { get; }
        public string Name { get; }
        public Id GroupId { get; }
        public CourseNumber CourseNumber { get; }

        public Student ChangeGroup(int newGroupId)
        {
            return new Student(this, newGroupId);
        }
    }
}