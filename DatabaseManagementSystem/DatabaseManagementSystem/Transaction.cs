using System;
using System.Threading;

namespace DatabaseManagementSystem
{
	public class Transaction
	{
		public string ID{ get; private set;}
		public transactionState state;
		private static int nextTransID;
		public Client transactionOwner{ get; set;}

		// Define transaction states
		public enum transactionState{
			None,
			Reading,
			Writing,
			Failed,
			Ended
		};
			
		public Transaction()
		{
			//Initial values
			this.ID = this.getIncrementelID();
			this.state = transactionState.None;
		}



		void read(File filename){
			state = transactionState.Reading;
		
		}
		void write(File filename){
			state = transactionState.Writing;
		}
			
		// Give every transaction a unique ID
		private string getIncrementelID(){
			return Interlocked.Increment(ref nextTransID).ToString();
		}




	}
}
	