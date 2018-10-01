using System;
namespace backend_project
{
     public class SameNamePlayerException : Exception
    {
        public SameNamePlayerException(string message) : base(message)
        {
            
        }
    }
}