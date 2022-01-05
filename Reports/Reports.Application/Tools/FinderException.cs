using System;

namespace Reports.Application.Tools
{
    public class FinderException : Exception
    {
        public FinderException()
        {
        }

        public FinderException(string message)
            : base(message)
        {
        }

        public FinderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}