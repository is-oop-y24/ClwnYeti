using System;

namespace Banks.Interfaces
{
    public interface ITimeMachine
    {
        int HowMuchDaysToSkip(TimeSpan timeToSkip);
    }
}