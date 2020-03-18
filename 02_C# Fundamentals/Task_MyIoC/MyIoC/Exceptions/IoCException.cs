using System;

namespace MyIoC.Exceptions
{
    public class IoCException : Exception
    {
        public IoCException()
        {

        }

        public IoCException(string message) : base(message)
        {

        }
    }
}
