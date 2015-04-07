using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DatabaseManagementSystem
{
    class WriteLock
    {
        public String ID {get; private set;}
        private Transaction lockOwner { get; set;}
        private static int nextWriteLockID;

        public WriteLock(Client client, File file) {
            this.ID = this.getIncrementalID();
            this.lockOwner = new Transaction(client, file);
        }

        private String getIncrementalID() {
            return Interlocked.Increment(ref nextWriteLockID).ToString();
        }

    }
}
