using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseManagementSystem
{
    public class NullInputException : Exception
    {
        public NullInputException(string message)
            :base(message)
        {
        }
    }
}
