using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagementSystem
{
    public class Checker
    {
        List<Client> clients = new List<Client>();
        List<File> files = new List<File>();
        List<Transaction> transactions = new List<Transaction>();
        public void addClient(Client c)
        {
            //Console.WriteLine("Added client: " + c.name);
            clients.Add(c);
        }
        public void addTransaction(Transaction t)
        {
            transactions.Add(t);
            //Console.WriteLine("Added transaction: " + t.transactionNumber);
        }
        public void addFile(File f)
        {
            //Console.WriteLine("Added file: " + f.fileName);
            files.Add(f);
        }

        internal File getFile(string file) { 
            foreach(File f in files) {
                if (f.fileName.Equals(file)) {
                    //Console.WriteLine(f.fileName);
                    return f;
                }
            }
            throw new FiledoesNotExistException();
        }


        internal bool ClientDoesNotExist(string p)
        {
            //TODO: check if client exists already... return true/false
            return true;
            throw new NotImplementedException();
        }

        internal bool FileDoesNotExist(string p)
        {
            //TODO: check if file exists already... return true/false
            return true;
            throw new NotImplementedException();
        }

        internal bool validateTransactionParameters()
        {
            //TODO: implement this, not really sure what it is supposed to do, ask future rui
            return true;
            throw new NotImplementedException();
        }

        internal void AssignTransactionOwner(string client, string transactionID)
        {
            foreach (Transaction t in transactions) { 
                if(t.transactionNumber == Convert.ToInt32(transactionID)) {
                    t.transactionOwner = getClient(client);
                    //TODO: catch null of getClient
                }
            }
            return;
        }
        private Client getClient(string clientName)
        {
            Client result = null;
            foreach (Client c in clients) {
                if (c.name.Equals(clientName)) {
                    result = c;
                }
            }
            return result;
        }

        public void SerializabilityTest() {
            foreach (Transaction t in transactions) {
                Console.WriteLine(t.ToString());
            }
        }
    }
}
