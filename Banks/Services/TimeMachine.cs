using System;
using Banks.Interfaces;

namespace Banks.Services
{
    public class TimeMachine : ITimeMachine
    {
        private TimeSpan _time;
        public int HowMuchDaysToSkip(TimeSpan timeToSkip)
        {
            _time += timeToSkip;
            int days = _time.Days;
            _time -= _time.Days * new TimeSpan(1, 0, 0, 0);
            return days;
        }
    }
}