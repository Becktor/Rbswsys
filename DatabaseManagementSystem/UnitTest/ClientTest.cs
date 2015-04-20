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
    public class ClientTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            Client client  = new Client("a");
            Client b = new Client("b");
            Client c = new Client("");
            // act
            Console.WriteLine("The name of client c is " + c.name);
            // assert
            Assert.AreNotEqual(a.ID, b.ID);
            Assert.AreNotEqual(a.name, b.name);

        }
    }
}
