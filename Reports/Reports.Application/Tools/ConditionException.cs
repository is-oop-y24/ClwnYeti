using System;

namespace Reports.Application.Tools
{
    public class ConditionException : Exception
    {
        public ConditionException()
        {
        }

        public ConditionException(string message)
            : base(message)
        {
        }

        public ConditionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}