using System;
using System.Threading;

namespace DatabaseManagementSystem
{
	public class Transaction
	{
		public string ID{ get; private set;}
		private static int nextTransID;
		public transactionState state{get; set;}
        public int transactionNumber { get; set; }
		public Client transactionOwner{get; set;}
		public File transactionFile{get; set;}

		// Define transaction states
		public enum transactionState{
			None,
			Read,
			Write,
            LockWrite,
            LockRead,
            UnlockRead,
            UnlockWrite,
			Fail,
			End
		};
			
		public Transaction(Client owner, File file, transactionState state)
		{	
			//Initial values
			this.ID = this.getIncrementalID();
			this.state = state;
			this.transactionOwner = owner;
			this.transactionFile = file;	
		}
        public Transaction(File file, transactionState state, int transactionNumber)
        {
            //Initial values
            this.ID = this.getIncrementalID();
            this.state = state;
            this.transactionFile = file;
            this.transactionNumber = transactionNumber;
        }

				
		// Give every transaction a unique ID
		private string getIncrementalID(){
			return Interlocked.Increment(ref nextTransID).ToString();
		}

        public static transactionState TranslateTransactionState(string s) {
            transactionState result = transactionState.None;
            switch (s) {
                case "READ":
                    result =  transactionState.Read;
                    break;
                case "WRITE":
                    result = transactionState.Write;
                    break;
                case "LOCK-S":
                    result = transactionState.LockRead;
                    break;
                case "LOCK-X":
                    result = transactionState.LockWrite;
                    break;
                case "UNLOCK-X":
                    result = transactionState.UnlockWrite;
                    break;
                case "UNLOCK-S":
                    result = transactionState.UnlockRead;
                    break;
            }
            if (result == transactionState.None) { 
                throw new InvalidTransactionParameters();
            }
            return result;

        }

        public override String ToString() {
            return "Transaction Number: " + transactionNumber +
                ". Owned by " + transactionOwner.name +
                    ". Action: " + state + " " +
                    transactionFile.fileName + ".";
        }


	}
}
	