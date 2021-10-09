namespace Isu.Classes
{
    public class Student
    {
        public Student(int groupId, string name, int id, CourseNumber courseNumber)
        {
            GroupId = groupId;
            Id = id;
            CourseNumber = courseNumber;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
        public int GroupId { get; internal set; }
        public CourseNumber CourseNumber { get; }
    }
}