using System;

namespace Reports.Application.Tools
{
    public class InputException : Exception
    {
        public InputException()
        {
        }

        public InputException(string message)
            : base(message)
        {
        }

        public InputException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}