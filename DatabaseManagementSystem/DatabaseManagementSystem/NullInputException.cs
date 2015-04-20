using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseManagementSystem
{
    class NullInputException : Exception
    {
        public NullInputException(string message)
            :base(message)
        {
        }
    }
}
