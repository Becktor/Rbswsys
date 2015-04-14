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

        //the graph to draw the conflicts
        TransactionGraph transactionGraph = new TransactionGraph(); 


        public void addClient(Client c)
        {
            //Console.WriteLine("Added client: " + c.name);
            clients.Add(c);
        }
        public void addTransaction(Transaction t)
        {
            // add new transaction numbers to the graph.
            bool newTransactionNumber = true;
            foreach (Transaction t2 in transactions) {
                if (t2.transactionNumber == t.transactionNumber) {
                    newTransactionNumber = false;
                    break;
                }
            }
            if (newTransactionNumber)
            {
                transactionGraph.addTransaction(t.transactionNumber.ToString());
                //Console.WriteLine("Added transaction number: " + t.transactionNumber.ToString());
            }
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
            
            
            // we dont need the locks for the serializability test
            List<Transaction> readsAndWrites = new List<Transaction>(); 
            
            // use past reads and writes to check if there are conflicts with the same object (file) before
            List<Transaction> pastReadsAndWrites = new List<Transaction>(); 
           
           // iterate all transactions and filter out non reads and non writes
           foreach (Transaction t in transactions) {
                if (t.state == Transaction.transactionState.Read || t.state == Transaction.transactionState.Write) {
                    readsAndWrites.Add(t);
                }
                //Console.WriteLine(t.ToString());
            }

            // for each transaction check if it creates a conflict with any of the transaction actions before it.
            foreach (Transaction t in readsAndWrites) {
                if(t.state == Transaction.transactionState.Read) {
                    String currentObject = t.transactionFile.fileName;
                    // check for write conflicts before
                    foreach(Transaction t2 in pastReadsAndWrites) {
                        if(t2.transactionFile.fileName == currentObject &&
                            t2.transactionNumber != t.transactionNumber &&
                            t2.state == Transaction.transactionState.Write) {
                            //coflict found of type 1 : write before write
                                transactionGraph.addDependency(t2.transactionNumber.ToString(),
                                    t.transactionNumber.ToString(), true);
                        }
                    }
                }
                else if(t.state == Transaction.transactionState.Write) {
                        String currentObject = t.transactionFile.fileName;
                        foreach (Transaction t2 in pastReadsAndWrites)
                        {
                            // check for read conflicts before
                            if (t2.transactionFile.fileName == currentObject &&
                                                            t2.transactionNumber != t.transactionNumber &&
                                                            t2.state == Transaction.transactionState.Read) {
                                //coflict found of type 2 : read before write
                                transactionGraph.addDependency(t2.transactionNumber.ToString(),
                                    t.transactionNumber.ToString(), true);
                            }
                            // check for write conflicts before
                            if (t2.transactionFile.fileName == currentObject &&
                                t2.transactionNumber != t.transactionNumber &&
                                t2.state == Transaction.transactionState.Write) {
                                //coflict found of type 3 : write before write
                                transactionGraph.addDependency(t2.transactionNumber.ToString(),
                                    t.transactionNumber.ToString(), true);
                            }
                        }
                    }
                else throw new InvalidTransactionParameters();

                pastReadsAndWrites.Add(t);
            }
            // after the algorithm, display the graph:
            transactionGraph.drawGraph();
        }
    }
}
