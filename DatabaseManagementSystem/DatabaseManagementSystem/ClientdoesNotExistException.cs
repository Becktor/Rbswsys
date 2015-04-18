using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseManagementSystem
{
    class ClientdoesNotExistException : Exception
    {
        public ClientdoesNotExistException()
        {
        }

        public ClientdoesNotExistException(string message)
            : base(message)
        {
        }

        public ClientdoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
