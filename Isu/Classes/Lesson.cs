using System;

namespace Isu.Classes
{
    public class Lesson
    {
        public Lesson(TimeSpan startTime, Id groupId, DayOfWeek dayOfWeek, string cabinet, string nameOfTeacher)
        {
            StartTime = startTime;
            DayOfWeek = dayOfWeek;
            Cabinet = cabinet;
            NameOfTeacher = nameOfTeacher;
            GroupId = groupId;
        }

        public TimeSpan StartTime { get; }
        public string Cabinet { get; }
        public string NameOfTeacher { get; }
        public Id GroupId { get; }
        public DayOfWeek DayOfWeek { get; }
        public static bool LessonsHaveCollision(Lesson first, Lesson second)
        {
            if (first.DayOfWeek != second.DayOfWeek) return false;
            if (first.StartTime + new TimeSpan(1, 30, 0) < second.StartTime
                && second.StartTime >= first.StartTime)
            {
                return true;
            }

            return second.StartTime + new TimeSpan(1, 30, 0) < first.StartTime
                   && first.StartTime >= second.StartTime;
        }

        public bool LessonHaveCollisionWithAnother(TimeSpan startTime, DayOfWeek dayOfWeek)
        {
            if (DayOfWeek != dayOfWeek) return false;
            if (StartTime + new TimeSpan(1, 30, 0) < startTime
                && startTime >= StartTime)
            {
                return true;
            }

            return startTime + new TimeSpan(1, 30, 0) < StartTime
                   && StartTime >= startTime;
        }
    }
}