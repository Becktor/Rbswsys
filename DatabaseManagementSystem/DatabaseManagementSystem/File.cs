﻿using System;
using System.Threading;


namespace DatabaseManagementSystem
{
	public class File
	{
		public string ID{ get; private set;}
		public 		string fileName;
		private static int nextFileID;


		public File(string filename)
		{
			this.fileName = filename;
			this.ID = this.getIncrementelID();
		}

		// Give every file a unique ID
		private string getIncrementelID(){
			return Interlocked.Increment(ref nextFileID).ToString();
		}


	}
}

