using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "Hello, welcome to the Transaction Analyser!";
            bool is_valid_graph = false;
            string user_name;
            string password;
            string user_input = "";

            if (!is_valid_graph) {

                Console.WriteLine(message);
 
                // what a nice little comment here :)
                Console.WriteLine("Press any key to continue:");
                Console.ReadKey();
                Console.WriteLine(Environment.NewLine + "Press enter your username: ");
                user_name = Console.ReadLine();
                Console.WriteLine("Press enter your password: ");
                password = Console.ReadLine();

                Console.WriteLine("Welcome, " + user_name + "! Please enter your transactions: (enter \"exit\" to quit)");
                while (user_input != "exit")
                {
                    user_input = Console.ReadLine();
                }

            }

        }
    }
}
