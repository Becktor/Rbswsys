using System;
using System.Threading;

namespace DatabaseManagementSystem
{
	public class Transaction
	{
		public string ID{ get; private set;}
		private static int nextTransID;
		public transactionState state{get; set;}
		public Client transactionOwner{get; set;}
		public File transactionFile{get; set;}

		// Define transaction states
		public enum transactionState{
			None,
			Reading,
			Writing,
			Failed,
			Ended
		};
			
		public Transaction(Client owner, File file)
		{	
			//Initial values
			this.ID = this.getIncrementalID();
			this.state = transactionState.None;
			this.transactionOwner = owner;
			this.transactionFile = file;	
		}

				
		// Give every transaction a unique ID
		private string getIncrementalID(){
			return Interlocked.Increment(ref nextTransID).ToString();
		}




	}
}
	