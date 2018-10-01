using System;
namespace ass3
{
     public class SameNamePlayerException : Exception
    {
        public SameNamePlayerException(string message) : base(message)
        {
            message = "Same name as a player currently in the list";
        }
    }
}