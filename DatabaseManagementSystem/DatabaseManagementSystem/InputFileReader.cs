using System;

namespace DatabaseManagementSystem
{
	public class InputFileReader
	{
		public InputFileReader ()
		{

		}

        public void readInput() {

            string user_input = "";

            Console.WriteLine("Please enter your transactions: (type \"exit\" to quit)");
            while (user_input != "exit")
            {
                user_input = Console.ReadLine();
                if (user_input.Equals("draw"))
                {
                    doDrawing();
                    // Thread drawingThread = new Thread(drawGraph) {IsBackground = true};
                    // drawingThread.Start();
                    //GraphGenerator graph = new GraphGenerator();
                    //graph.drawGraph();
                }
            }
        }

        private void doDrawing()
        {
            TransactionGraph transactionGraph = new TransactionGraph();

            transactionGraph.addDependency("T1", null);

            transactionGraph.drawGraph();
        }
	}
}

