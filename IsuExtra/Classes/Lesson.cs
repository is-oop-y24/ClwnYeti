using System;

namespace IsuExtra.Classes
{
    public class Lesson
    {
        public Lesson(TimeSpan startTime, int groupId, DayOfWeek dayOfWeek)
        {
            StartTime = startTime;
            DayOfWeek = dayOfWeek;
            GroupId = groupId;
        }

        public TimeSpan StartTime { get; }
        public int GroupId { get; }
        public DayOfWeek DayOfWeek { get; }
    }
}