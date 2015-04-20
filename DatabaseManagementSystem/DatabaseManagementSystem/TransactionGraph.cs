using Microsoft.Glee.Drawing;
using Microsoft.Glee.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseManagementSystem
{
    class TransactionGraph
    {
        private const int _MODE_SERIALIZIBILITY = 0;
        private const int _MODE_DEADLOCK = 1;

        public class Dependency
        {
            public String transactionFrom { get; private set;  }
            public String transactionTo { get; private set; }
            public Boolean isCausingTrouble { get; set; }

            public Dependency (String transactionFrom, String transactionTo, Boolean isCausingTrouble = false)
            {
                this.transactionFrom = transactionFrom;
                this.transactionTo = transactionTo;
                this.isCausingTrouble = isCausingTrouble;
            }
        }

        public List<String> listTransactions { get; private set; }
        public List<Dependency> listDependencies { get; private set; }
        public Graph graph { get; private set; }

        private static Form form;
        private static GViewer viewer;

        public TransactionGraph()
        {
            //create a form 
            form = new System.Windows.Forms.Form();
            //create a viewer object 
            viewer = new Microsoft.Glee.GraphViewerGdi.GViewer();
        
            listTransactions = new List<String>();
            listDependencies = new List<Dependency>();
            graph = new Graph("graph");
        }

        public int addTransaction(String transactionName)
        {
            // PRECONDITION
            
            Contract.Requires<ArgumentNullException>(transactionName != null,
                "transactionName must not be null!");

            int transactionIndex = listTransactions.IndexOf(transactionName);
            Contract.Requires<ArgumentOutOfRangeException>(transactionIndex == -1,
                "transactionFrom is not UNIQUE in listTransactions!");

            // POSTCONDITION

            // Make sure at the end of the execution the result is smaller than the list size
            Contract.Ensures(Contract.Result<int>() >= -1);
            Contract.Ensures(Contract.Result<int>() < listTransactions.Count);

            // Make sure at the end of the execution the transactionIndex is not -1
            Contract.Ensures(transactionIndex == -1);

            // EXECUTION
            listTransactions.Add(transactionName);
            int i = listTransactions.Count - 1;

            // Returns the index in which an item was inserted.
            return i;
        }

        public int addDependency(String transactionFrom, String transactionTo, Boolean isCausingTrouble = false)
        {
            // PRECONDITION

            Contract.Requires<ArgumentNullException>(transactionFrom != null,
                "transactionFrom must not be null!");
            Contract.Requires<ArgumentNullException>(transactionTo != null,
                "transactionTo must not be null!");
            Contract.Requires<ArgumentNullException>(listDependencies != null,
                "listDependencies must not be null!");

            int transactionFromIndex = listTransactions.IndexOf(transactionFrom);

            Contract.Requires<ArgumentOutOfRangeException>(transactionFromIndex != -1,
                "transactionFrom is not found in listTransactions!");

            int transactionToIndex = listTransactions.IndexOf(transactionTo);
            Contract.Requires<ArgumentOutOfRangeException>(transactionToIndex != -1,
                "transactionTo is not found in listTransactions!");

            // POSTCONDITION

            // Make sure at the end of the execution the result is smaller than the list size
            Contract.Ensures(Contract.Result<int>() >= -1);
            Contract.Ensures(Contract.Result<int>() < listDependencies.Count);

            // EXECUTION
            
            // Before adding make sure that newly added dependency is not in the list
            int occurance = listDependencies.Count(item => item.transactionFrom.Equals(transactionFrom) &&
                item.transactionTo.Equals(transactionTo));

            // Add dependency to the list
            int i = -1;
            if (occurance == 0) 
            {
                listDependencies.Add(new Dependency(transactionFrom, transactionTo, isCausingTrouble));
                i = listDependencies.Count - 1;
            }

            // Returns the index in which an item was inserted.
            return i;
        }

        public void resetGraph()
        {
            Contract.Requires<ArgumentNullException>(graph != null,
                "graph must not be null!");

            listDependencies = new List<Dependency>();

            graph = new Graph("graph");
        }

        public void drawGraph(int mode = _MODE_SERIALIZIBILITY)
        {
            Contract.Requires<ArgumentNullException>(graph != null,
                "graph must not be null!");
            Contract.Requires<ArgumentNullException>(listDependencies != null,
                 "listDependencies must not be null!");
            Contract.Assert(listDependencies != null);
            //create the graph content 

            // draw transactions
            foreach (String t in listTransactions)
            {
                graph.AddNode(t);
            }

            // draw dependencies
            for (int i = 0; i < listDependencies.Count; i++)
            {
                Dependency dependency = listDependencies[i];

                // PAINT RED FOR SERIALIZIBILITY PROBLEM
                if (mode == _MODE_SERIALIZIBILITY)
                {
                    bool isPaired = false;
                    for (int j = i + 1; j < listDependencies.Count && !isPaired; j++)
                    {
                        Dependency dependencyPair = listDependencies[j];
                        if(dependencyPair != null && dependency != null &&
                            dependency.transactionFrom.Equals(dependencyPair.transactionTo) &&
                            dependency.transactionTo.Equals(dependencyPair.transactionFrom))
                        {
                            dependency.isCausingTrouble = true;
                            dependencyPair.isCausingTrouble = true;
                            isPaired = true;
                        }
                    }
                }

                if (dependency != null)
                {
                    Edge edge = graph.AddEdge(dependency.transactionFrom, dependency.transactionTo);
                    if (dependency.isCausingTrouble && edge != null && edge.EdgeAttr != null)
                    {
                        edge.EdgeAttr.Color = Microsoft.Glee.Drawing.Color.Red;
                    }
                }
            }

            //graph.AddEdge("T1", "T2").EdgeAttr.Color = Microsoft.Glee.Drawing.Color.Red;
            //graph.AddEdge("T2", "T1").EdgeAttr.Color = Microsoft.Glee.Drawing.Color.Red;
            //graph.AddEdge("T2", "T3");
            // graph.AddEdge("A", "C").EdgeAttr.Color = Microsoft.Glee.Drawing.Color.Green;

            //graph.FindNode("T1").Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Magenta;
            //graph.FindNode("T2").Attr.Fillcolor = Microsoft.Glee.Drawing.Color.MistyRose;
            //Microsoft.Glee.Drawing.Node c = graph.FindNode("T3");
            //c.Attr.Fillcolor = Microsoft.Glee.Drawing.Color.PaleGreen;
            // c.Attr.Shape = Microsoft.Glee.Drawing.Shape.Diamond;
            //bind the graph to the viewer 
            viewer.Graph = graph;
            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            //show the form 
            form.ShowDialog();

            graph = new Microsoft.Glee.Drawing.Graph("graph");
        }
    }
}
