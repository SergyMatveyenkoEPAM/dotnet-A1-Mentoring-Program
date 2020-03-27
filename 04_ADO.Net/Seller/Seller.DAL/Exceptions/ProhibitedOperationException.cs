using System;

namespace Seller.DAL.Exceptions
{
    public class ProhibitedOperationException : Exception
    {
        public ProhibitedOperationException(string message) : base(message)
        {

        }
    }
}
