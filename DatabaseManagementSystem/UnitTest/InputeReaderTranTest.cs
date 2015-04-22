using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseManagementSystem;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class InputReaderTranTest
    {
        Checker c = new Checker();
       [TestMethod]
       public void ReadInputTranEmptyName()
        {
            InputFileReader testTranEmptyName = new InputFileReader(c);
            testTranEmptyName.user_input="TRAN  ";
            testTranEmptyName.test=true;
            try
            {
                testTranEmptyName.readInput();
            }
            catch(InsufficientArgumentsException)
            {
                Assert.IsTrue(true);
            }
       }
        [TestMethod]
       public void ReadInputTranValid()
       {
            c.addFile(new File("X"));
            InputFileReader testTranA = new InputFileReader(c);
            testTranA.user_input = "TRAN \"WRITE X\" 1";
            testTranA.test = true;
            testTranA.readInput();
            Assert.IsTrue(c.files.Count == 1);
       }
 
        [TestMethod]
        public void ReadInputFileTooMany()
        {
            InputFileReader testFileTooMany = new InputFileReader(c);
            testFileTooMany.user_input = "FILE test test";
            testFileTooMany.test = true;
            try
            {
                testFileTooMany.readInput();
            }
            catch(InsufficientArgumentsException)
            {
                Assert.IsTrue(true);
            }
        } 
    }
}
