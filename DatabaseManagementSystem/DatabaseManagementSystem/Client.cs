using System;
using System.Threading;

namespace DatabaseManagementSystem
{
	public class Client
	{
		public string ID{ get; private set;}
		public string name;
		private static int nextClientID;

		public Client(string nameOfClient)
		{
			this.ID = this.getIncrementelID();
            this.name = nameOfClient;

		}

		// Give every client a unique ID
		private string getIncrementelID(){
			return Interlocked.Increment(ref nextClientID).ToString();
		}
	}
}

