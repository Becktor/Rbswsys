﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using System.Threading;

namespace DatabaseManagementSystem
{
    class Program
    {

            
        [STAThread]
        static void Main(string[] args)
        {
            InputFileReader reader = new InputFileReader();

            string message = "Hello, welcome to the Transaction Analyser!";
            Console.WriteLine(message);

            reader.readInput();
        }

        private static int Divide(int p1, int p2)
        {
            Contract.Requires<ArgumentNullException>(p2 != 0, "Divide by zero is forbidden");

            return p1 / p2;
        }

        
    }
}
