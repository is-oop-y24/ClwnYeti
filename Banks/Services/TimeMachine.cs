using System;
using Banks.Classes;
using Banks.Interfaces;

namespace Banks.Services
{
    public class TimeMachine : ITimeMachine
    {
        private TimeSpan _timeForDays;
        private TimeSpan _timeForMonth;
        public TimeToSkip HowMuchToSkip(TimeSpan timeToSkip)
        {
            _timeForMonth += timeToSkip;
            _timeForDays += timeToSkip;
            int months = _timeForMonth.Days / 30;
            int days = _timeForDays.Days;
            _timeForMonth -= months * new TimeSpan(30, 0, 0, 0);
            _timeForDays -= days * new TimeSpan(1, 0, 0, 0);
            return new TimeToSkip(days, months);
        }
    }
}