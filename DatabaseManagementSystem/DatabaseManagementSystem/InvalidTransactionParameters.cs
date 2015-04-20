using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseManagementSystem
{
    class InvalidTransactionParameters : Exception
    {
             public InvalidTransactionParameters()
        {
        }

        public InvalidTransactionParameters(string message)
            : base(message)
        {
        }

        public InvalidTransactionParameters(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
