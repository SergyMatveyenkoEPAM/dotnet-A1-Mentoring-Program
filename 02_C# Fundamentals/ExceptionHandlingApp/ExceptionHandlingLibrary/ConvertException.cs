using System;

namespace ExceptionHandlingLibrary
{
    public class ConvertException : Exception
    {
        public ConvertException()
        {

        }

        public ConvertException(string message) : base(message)
        {

        }
    }
}
