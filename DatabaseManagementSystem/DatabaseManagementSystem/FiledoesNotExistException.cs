using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseManagementSystem
{
    class FiledoesNotExistException : Exception
    {
        public FiledoesNotExistException()
        {
        }

        public FiledoesNotExistException(string message)
            : base(message)
        {
        }

        public FiledoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
