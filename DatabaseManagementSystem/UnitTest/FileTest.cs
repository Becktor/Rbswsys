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
    public class FileTest
    {
        [TestMethod]
        public void FileTestMethod()
        {
            // arrange
            File a  = new File("a");
            File b = new File("b");
            File c = new File(""); // Here a file with an empty name is created, it should not be possible in our code, so not a problem I guess
            // act
            Console.WriteLine("The name of client c is " + c.fileName);
            // assert
            Assert.AreNotEqual(a.ID, b.ID);
            Assert.AreNotEqual(a.fileName, b.fileName);

        }
    }
}
