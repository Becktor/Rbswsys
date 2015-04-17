using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagementSystem
{
    class InvalidArgumentException : Exception
    {
     public InvalidArgumentException()
        {
        }

        public InvalidArgumentException(string message)
            : base(message)
        {
        }

        public InvalidArgumentException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
