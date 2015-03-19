using System;

namespace DatabaseManagementSystem
{
	public class Transaction
	{
		Client sender,reciever;
		string transID;

		public Transaction(Client sender,Client reciever)
		{
			this.sender = sender;
			this.reciever = reciever;
		}
	}
}

