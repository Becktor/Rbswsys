using System;
using System.IO;

namespace DatabaseManagementSystem
{
	public class InputFileReader
	{
        private Checker checker; 
        public string user_input = "";

        // Variables used for testing
        public bool test = false;
        public bool testSuccess = false;

        // Variables used for filetext input
        String filename;
        int fileLineNumber = 0;
        string[] lines;
        bool readingFromFile = false;


		public InputFileReader (Checker c)
		{
            this.checker = c;
		}

        public void readFile(string filename)
        {
            string fullPath = Directory.GetCurrentDirectory() + "\\inputExamples\\" + filename;
            try
            {
                String fileContent = System.IO.File.ReadAllText(fullPath);
                lines = fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                readingFromFile = true;
            }
            catch (DirectoryNotFoundException dirEx)
            {
                // Let the user know that the directory did not exist.
                Console.WriteLine("Directory not found: " + dirEx.Message);
            }
            catch (FileNotFoundException filEx)
            {
                // Let the user know that the file did not exist.
                Console.WriteLine("File not found: " + filEx.Message);
                Console.WriteLine("");
                readInput();
            }
            
            if (fileLineNumber >= lines.Length -1 )
            {
                readingFromFile = false;
                readInput();
            }else {
                user_input = lines[fileLineNumber];
                Console.WriteLine(user_input);
                readInput();
            }

            
        }

        public void readInput() {
            if (!readingFromFile && !test)
            {
                Console.WriteLine("Please enter your transactions or type 'f' to specify a transaction file: (type \"exit\" to quit)");
                user_input = Console.ReadLine();
            }
            if(test)
            {
                Console.WriteLine("test is running");
            }
            while (user_input != "exit" )
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
                                    createClient(words[1]);
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
                                        File file = checker.getFile(words[2]);
                                        Transaction.transactionState state = Transaction.
                                            TranslateTransactionState(words[1]);
                                        int transactionNumber = Convert.ToInt32(words[3]);
            
                                        createTransaction(state, file, transactionNumber);
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
                                        createFile(words[1]);
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
                                    assignTransactionOwner(words[1], words[2]);
                                }
                                else if (words.Length < 3) throw new InsufficientArgumentsException("Not enough arguments");
                                else throw new InsufficientArgumentsException("Too many arguments");
                                break;
                            case "f":
                                Console.WriteLine("Write the file name of input file - see folder inputExamples inside \\bin\\debug\\");
                                filename = Console.ReadLine();
                                readFile(filename);
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
                else
                {
                    throw new NullInputException("User input can not be Null.");
                   /* if (test)
                    {
                        testSuccess = true;
                     
                        break;
                    }*/
                }
                if (readingFromFile && user_input != "exit")
                {
                    readFile(filename);
                }
                if(test)
                {
                    Console.WriteLine("test");
                    break;
                }
                user_input = Console.ReadLine();
            }
            if (!test)
            {
                checker.SerializabilityTest();
                checker.DeadlockTest();
            }
        }

        private void assignTransactionOwner(string client, string transactionID)
        {
            checker.AssignTransactionOwner(client, transactionID);
            if (readingFromFile)
                fileLineNumber++;
        }

        private void createClient(string name)
        {
            if (checker.ClientDoesNotExist(name))
            {
                checker.addClient(new Client(name));

                if (readingFromFile)
                    fileLineNumber++;
            }
        }

        private void createFile(string fileName)
        {
            checker.addFile(new File(fileName));
            if (readingFromFile)
                fileLineNumber++;
        }

        private void createTransaction(Transaction.transactionState state, 
            File file, int transactionNumber)
        {
            //TODO: use classes of locks instead of enum in transaction class.
            checker.addTransaction(new Transaction(file, state, transactionNumber));
            //Console.WriteLine("Done with checker.add Transation.");
            if (readingFromFile)
                fileLineNumber++;
        }

	}
}

