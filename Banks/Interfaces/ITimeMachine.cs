using System;
using Banks.Classes;

namespace Banks.Interfaces
{
    public interface ITimeMachine
    {
        TimeToSkip HowMuchToSkip(TimeSpan timeToSkip);
    }
}