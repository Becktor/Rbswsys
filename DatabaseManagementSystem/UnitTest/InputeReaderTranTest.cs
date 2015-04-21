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
    public class InputReaderFileTest
    {
        Checker c = new Checker();
       [TestMethod]
       public void ReadInputileEmptyName()
        {
            InputFileReader testFileEmptyName = new InputFileReader(c);
            testFileEmptyName.user_input="FILE  ";
            testFileEmptyName.test=true;
            try
            {
                testFileEmptyName.readInput();
            }
            catch(InsufficientArgumentsException)
            {
                Assert.IsTrue(true);
            }
       }
        [TestMethod]
       public void ReadInputFileValid()
       {
            InputFileReader testFileA = new InputFileReader(c);
            testFileA.user_input = "FILE A";
            testFileA.test = true;
            testFileA.readInput();
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
