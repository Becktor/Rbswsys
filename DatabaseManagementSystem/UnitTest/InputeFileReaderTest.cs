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
    public class InputFileReaderTest
    {
        Checker c = new Checker();

        [TestMethod]
        public void ReadInputVoid()
        {
            // arrange
            InputFileReader testClientVoid = new InputFileReader(c);
            testClientVoid.user_input = null;
            testClientVoid.test = true;
           
            // act
            try
            {
            testClientVoid.readInput();
            }
            catch(NullInputException)
            {
                Assert.IsTrue(true);                
            }
           
                       

        }
        [TestMethod]
        public void ReadInputWrong()
        {
            InputFileReader testClientWrong = new InputFileReader(c);
            testClientWrong.user_input = "test";
            testClientWrong.test=true;
            try 
            {
                testClientWrong.readInput();
            }
            catch(InvalidArgumentException)
            {
                Assert.IsTrue(true);
            }

        }
       [TestMethod]
       public void ReadInputClientEmptyName()
        {
            InputFileReader testClientEmptyName = new InputFileReader(c);
            testClientEmptyName.user_input="CLNT  ";
            testClientEmptyName.test=true;
            try
            {
                testClientEmptyName.readInput();
            }
            catch(InsufficientArgumentsException)
            {
                Assert.IsTrue(true);
            }
       }
        [TestMethod]
       public void ReadInputClientValid()
       {
            InputFileReader testClientA = new InputFileReader(c);
            testClientA.user_input = "CLNT A";
            testClientA.test = true;
            testClientA.readInput();
            Assert.IsTrue(c.clients.Count == 1);
       }
 
        [TestMethod]
        public void ReadInputClientTooMany()
        {
            InputFileReader testClientTooMany = new InputFileReader(c);
            testClientTooMany.user_input = "CLNT test test";
            testClientTooMany.test = true;
            try
            {
                testClientTooMany.readInput();
            }
            catch(InsufficientArgumentsException)
            {
                Assert.IsTrue(true);
            }
        } 
    }
}
