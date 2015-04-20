using System;

namespace DatabaseManagementSystem
{
	public class InputFileReader
	{
        private Checker checker; 
		public InputFileReader (Checker c)
		{
            this.checker = c;
		}

        public void readInput() {

            string user_input = "";

            Console.WriteLine("Please enter your transactions: (type \"exit\" to quit)");
            user_input = Console.ReadLine();
            while (user_input != "exit")
            {
                //Console.WriteLine(user_input);
                if (user_input != null)
                {
                    string[] words = user_input.Split(' ');
                    try
                    {
                        switch (words[0])
                        {
                            case "CLNT":
                                if (words.Length == 2)
                                {
                                    if (checker.ClientDoesNotExist(words[1]))
                                    {
                                        checker.addClient(new Client(words[1]));
                                    }
                                }
                                else if (words.Length < 2) throw new InsufficientArgumentsException("Not enough arguments");
                                else throw new InsufficientArgumentsException("Too many arguments");
                                break;

                            case "TRAN":
                                if (words.Length == 4)
                                {
                                    words[2] = words[2].Replace("\"", "");
                                    words[1] = words[1].Replace("\"", "");
                                    //Console.WriteLine(words[1]);
                                    //Console.WriteLine(words[2]);
                                    //Console.WriteLine(words[3]);
                                    if (checker.validateTransactionParameters())
                                    {
                                        //TODO: use classes of locks instead of enum in transaction class.
                                        checker.addTransaction(new Transaction(checker.getFile(words[2]),
                                                            Transaction.TranslateTransactionState(words[1]), Convert.ToInt32(words[3])));
                                        //Console.WriteLine("Done with checker.add Transation.");
                                    }
                                    else
                                    {
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
                                else if (words.Length < 2) throw new InsufficientArgumentsException("Not enough arguments");
                                else throw new InsufficientArgumentsException("Too many arguments");
                                break;
                            case "SEND":
                                //Console.WriteLine(words[0]);
                                //Console.WriteLine(words[1]);
                                //Console.WriteLine(words[2]);
                                if (words.Length == 3)
                                {
                                    checker.AssignTransactionOwner(words[1], words[2]);
                                }
                                else if (words.Length < 3) throw new InsufficientArgumentsException("Not enough arguments");
                                else throw new InsufficientArgumentsException("Too many arguments");
                                break;
                            case "exit":
                                Console.WriteLine("End of requests");
                                break;
                            default:
                                throw new InvalidArgumentException("The first argument must be CLNT, draw, TRAN, FILE, SEND or exit");
                        }
                    }

                    catch (InvalidArgumentException)
                    {
                        Console.WriteLine("The first argument must be CLNT, draw, TRAN, FILE, SEND or exit");
                    }
                    catch (InsufficientArgumentsException)
                    {
                        Console.WriteLine("Invalid number of arguments for this command");
                    }
                }
                else throw new NullInputException("User input can not be Null.");
                user_input = Console.ReadLine();
            }
            checker.SerializabilityTest();
            checker.DeadlockTest();
        }
	}
}

