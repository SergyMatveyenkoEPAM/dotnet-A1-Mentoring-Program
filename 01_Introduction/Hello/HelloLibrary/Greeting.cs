using System;

namespace HelloLibrary
{
    public class Greeting
    {
        public static string Hello(string name)
        {
            return (DateTime.Now.ToLongTimeString() + $" Hello, { name}!");
        }
    }
}
