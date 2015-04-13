using System;

namespace DatabaseManagementSystem
{
	public class InputFileReader
	{
        public Checker checker; //TODO: cant make this private... throws a build error
		public InputFileReader (Checker c)
		{
            this.checker = c;
		}

        public void readInput() {

            string user_input = "";

            Console.WriteLine("Please enter your transactions: (type \"exit\" to quit)");
            while (user_input != "exit")
            {
                user_input = Console.ReadLine();
                if (user_input != null) {
                    string[] words = user_input.Split(' ');
                    switch(words[0])
                    {
                        case "CLNT":
                            if (words.Length == 2)
                            {
                                if (checker.ClientDoesNotExist(words[1]))
                                {
                                    checker.addClient(new Client(words[1]));
                                }
                            }
                            else throw new InsufficientArgumentsException();
                            break;

                        case "draw":
                            Console.WriteLine("draw");
                            doDrawing();
                            // Thread drawingThread = new Thread(drawGraph) {IsBackground = true};
                            // drawingThread.Start();
                            //GraphGenerator graph = new GraphGenerator();
                            //graph.drawGraph();                        
                            break;

                        case "TRAN":
                            if (words.Length == 4)
                            {
                                words[2] = words[2].Replace("\"", "");
                                words[1] = words[1].Replace("\"", "");
                                Console.WriteLine(words[1]);
                                Console.WriteLine(words[2]);
                                Console.WriteLine(words[3]);
                                if (checker.validateTransactionParameters())
                                {
                                    //TODO: use classes of locks instead of enum in transaction class.
                                    checker.addTransaction(new Transaction(checker.getFile(words[2]),
                                                        Transaction.TranslateTransactionState(words[1]), Convert.ToInt32(words[3])));
                                }
                                else {
                                    throw new InvalidTransactionParameters();
                                }
                            }
                            else throw new InsufficientArgumentsException();
                            break;
                        case "FILE":
                            if (words.Length == 2)
                            {
                                if (checker.FileDoesNotExist(words[1]))
                                {
                                    checker.addFile(new File(words[1]));
                                }
                            }
                            else throw new InsufficientArgumentsException();
                            break;
                        case "SEND":
                            Console.WriteLine(words[0]);
                            Console.WriteLine(words[1]);
                            Console.WriteLine(words[2]);
                            if (words.Length == 3)
                            {
                                checker.AssignTransactionOwner(words[1], words[2]);
                            }
                            else throw new InsufficientArgumentsException();
                            break;
                    }
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

