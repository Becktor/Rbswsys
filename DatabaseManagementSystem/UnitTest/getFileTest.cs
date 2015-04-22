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
    public class getFileTest
    {
        [TestMethod]
        public void getExistingFile()
        {
            Checker c = new Checker();
            File A= new File("A");
            c.files.Add(A);

            File f = c.getFile("A");
            Assert.AreEqual(A, f);
        }

        [TestMethod]
        public void getMissingFile()
        {
            Checker c = new Checker();
            File A= new File("A");
            c.files.Add(A);
            try{            
            File f = c.getFile("B");
            Assert.AreEqual(A, f);
            }
            catch (FiledoesNotExistException)
            {
            Assert.IsTrue(true);
            }
        }
    }
}
