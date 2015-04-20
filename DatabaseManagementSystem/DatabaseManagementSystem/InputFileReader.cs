using System;
using System.IO;

namespace DatabaseManagementSystem
{
	public class InputFileReader
	{
        private Checker checker; 
        string user_input = "";
        String filename;
        int fileLineNumber = 0;
        bool readingFromFile = false;
        string[] lines;

		public InputFileReader (Checker c)
		{
            this.checker = c;
		}

        public void runFileAsInput(string filename)
        {
            string fullPath = Directory.GetCurrentDirectory() + "\\inputExamples\\" + filename;
            
            //Console.WriteLine(fullPath);
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
            if (!readingFromFile)
            {
                Console.WriteLine("Please enter your transactions or type 'f' to specify a transaction file: (type \"exit\" to quit)");
                user_input = Console.ReadLine();
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
                                    if (checker.ClientDoesNotExist(words[1]))
                                    {
                                        checker.addClient(new Client(words[1]));
                                        
                                        if (readingFromFile) 
                                            fileLineNumber++;
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
                                        if (readingFromFile) 
                                            fileLineNumber++;
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
                                        if (readingFromFile) 
                                            fileLineNumber++;
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
                                    if (readingFromFile) 
                                        fileLineNumber++;
                                }
                                else if (words.Length < 3) throw new InsufficientArgumentsException("Not enough arguments");
                                else throw new InsufficientArgumentsException("Too many arguments");
                                break;
                            case "f":
                                Console.WriteLine("Write the file name of input file - see folder inputExamples inside \\bin\\debug\\");
                                filename = Console.ReadLine();
                                runFileAsInput(filename);
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

                if (readingFromFile && user_input != "exit")
                {
                    runFileAsInput(filename);
                }
                user_input = Console.ReadLine();
            }
            checker.SerializabilityTest();
            checker.DeadlockTest();
        }
	}
}

