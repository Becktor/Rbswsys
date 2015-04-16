using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseManagementSystem
{
    class InsufficientArgumentsException : Exception
    {
         public InsufficientArgumentsException()
        {
        }

        public InsufficientArgumentsException(string message)
            : base(message)
        {
        }

        public InsufficientArgumentsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
