using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DatabaseManagementSystem
{
    class ReadLock
    {
        private String ID { get; private set; }
        private Transaction LockOwner { get; private set; }
        private static int nextReadLockID;

        public ReadLock(Client client, File file) {
            this.ID = this.getIncrementalID();
            this.LockOwner = new Transaction(client, file);
        }

        private String getIncrementalID() { 
            return Interlocked.Increment(ref nextReadLockID).ToString();
        }
    }
}
