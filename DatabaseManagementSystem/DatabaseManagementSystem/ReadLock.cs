using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DatabaseManagementSystem
{
    class ReadLock
    {
        public String ID { get; private set; }
        private Transaction LockOwner { get; set; }
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
