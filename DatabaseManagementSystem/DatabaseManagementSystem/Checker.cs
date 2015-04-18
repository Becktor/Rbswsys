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
            foreach (Client c in clients)
            {
                if (c.name == p)
                {
                    Console.WriteLine("Client already exists");
                    return false;
                }
            }
            return true;
        }

        internal bool FileDoesNotExist(string p)
        {
            foreach (File f in files) { 
                if(f.fileName == p) {
                    Console.WriteLine("File already exists");
                    return false;
                }
            }
            return true;
        }

        internal bool validateTransactionParameters()
        {
            //TODO: implement this, not really sure what it is supposed to do, ask future rui
            return true;
            throw new NotImplementedException();
        }

        internal void AssignTransactionOwner(string client, string transactionID)
        {
            if (clients.Any(c=> c.name==client))
            {
                foreach (Transaction t in transactions)
                {
                    if (t.transactionNumber == Convert.ToInt32(transactionID))
                    {
                        t.transactionOwner = getClient(client);
                        //TODO: catch null of getClient
                        //edit Laurent : I just checked if client was in the list, is that good?
                    }
                }
            }
            else
            {
                throw new ClientdoesNotExistException("This client does not exist");
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
                                    t.transactionNumber.ToString());
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
                                    t.transactionNumber.ToString());
                            }
                            // check for write conflicts before
                            if (t2.transactionFile.fileName == currentObject &&
                                t2.transactionNumber != t.transactionNumber &&
                                t2.state == Transaction.transactionState.Write) {
                                //coflict found of type 3 : write before write
                                transactionGraph.addDependency(t2.transactionNumber.ToString(),
                                    t.transactionNumber.ToString());
                            }
                        }
                    }
                else throw new InvalidTransactionParameters();

                pastReadsAndWrites.Add(t);
            }
            // after the algorithm, display the graph:
            transactionGraph.drawGraph();
            transactionGraph.resetGraph();
        }

        public void DeadlockTest()
        {
            Dictionary<File, Transaction.transactionState> fileLockStatus = 
                new Dictionary<File, Transaction.transactionState>();
            foreach (File file in files)
            {
                fileLockStatus.Add(file, Transaction.transactionState.UnlockRead);
            }

            // we dont need the reads and writes for the deadlock test
            List<Transaction> locks = new List<Transaction>();

            // use past locks to check if there are conflicts with the same object (file) before
            List<Transaction> pastLocks = new List<Transaction>();

            // iterate all transactions and filter out non locks
            foreach (Transaction t in transactions)
            {
                if (t.state != Transaction.transactionState.Read && 
                    t.state != Transaction.transactionState.Write)
                {
                    locks.Add(t);
                }
                //Console.WriteLine(t.ToString());
            }

            // for each transaction check if it creates a conflict with any of the transaction actions before it.
            foreach (Transaction t in locks)
            {
                /**
                 * There is a wait-for link form Tk to Ti when:
                 * Ti lock-X(Q) before Tk: lock-S(Q);
                 * Ti lock-X(Q) before Tk: lock-X(Q);
                 * Ti lock-S(Q) before Tk: lock-X(Q);
                 * */

                // Current state is lock-S - there is wait-for if there is a lock-X before
                if (t.state == Transaction.transactionState.LockRead)
                {
                    String currentObject = t.transactionFile.fileName;
                    // check if current file status is lock-x
                    if (fileLockStatus[t.transactionFile] == Transaction.transactionState.LockWrite)
                    {
                        // if it is write-locked, start iterating backwards to see who locks it
                        bool isLockerFound = false;
                        for (int i = pastLocks.Count - 1; i >= 0 && !isLockerFound; i--)
                        {
                            Transaction t2 = pastLocks[i];
                            if (t2.transactionFile.fileName == currentObject &&
                                t2.transactionNumber != t.transactionNumber &&
                                t2.state == Transaction.transactionState.LockWrite)
                            {
                                //wait-for found of type 1 : lock-x before lock-s
                                transactionGraph.addDependency(t2.transactionNumber.ToString(),
                                    t.transactionNumber.ToString());
                                isLockerFound = true;
                            }
                        }
                    }
                }
                // Current state is lock-X - there is wait-for if there is a lock-X or lock-S before
                else if (t.state == Transaction.transactionState.LockWrite)
                {
                    String currentObject = t.transactionFile.fileName;

                    // check if current file status is lock-x
                    if (fileLockStatus[t.transactionFile] == Transaction.transactionState.LockWrite)
                    {
                        // if it is write-locked, start iterating backwards to see who locks it
                        bool isLockerFound = false;
                        for (int i = pastLocks.Count - 1; i >= 0 && !isLockerFound; i--)
                        {
                            Transaction t2 = pastLocks[i];
                            // check for read conflicts before
                            if (t2.transactionFile.fileName == currentObject &&
                                t2.transactionNumber != t.transactionNumber &&
                                t2.state == Transaction.transactionState.LockWrite)
                            {
                                //wait-for found of type 2 : lock-x before lock-x
                                transactionGraph.addDependency(t2.transactionNumber.ToString(),
                                    t.transactionNumber.ToString());
                                isLockerFound = true;
                            }
                        }
                    }

                    // check if current file status is lock-s
                    if (fileLockStatus[t.transactionFile] == Transaction.transactionState.LockRead)
                    {
                        // if it is read-locked, start iterating backwards to see who locks it
                        bool isLockerFound = false;
                        for (int i = pastLocks.Count - 1; i >= 0 && !isLockerFound; i--)
                        {
                            Transaction t2 = pastLocks[i];
                            // check for read conflicts before
                            if (t2.transactionFile.fileName == currentObject &&
                                t2.transactionNumber != t.transactionNumber &&
                                t2.state == Transaction.transactionState.LockRead)
                            {
                                //wait-for found of type 3 : lock-s before lock-x
                                transactionGraph.addDependency(t2.transactionNumber.ToString(),
                                    t.transactionNumber.ToString());
                                isLockerFound = true;
                            }
                        }
                    }
                }
                else if (t.state == Transaction.transactionState.UnlockRead)
                {

                }
                else if (t.state == Transaction.transactionState.UnlockWrite)
                {

                }
                else throw new InvalidTransactionParameters();

                // Change current file status
                fileLockStatus[t.transactionFile] = t.state;

                pastLocks.Add(t);
            }
            // after the algorithm, display the graph:
            transactionGraph.drawGraph();
            transactionGraph.resetGraph();
        }
    }
}
