using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using System.Threading;

namespace DatabaseManagementSystem
{
    class Program
    {
        //create a form 
        static System.Windows.Forms.Form form = new System.Windows.Forms.Form();
        //create a viewer object 
        static Microsoft.Glee.GraphViewerGdi.GViewer viewer = new Microsoft.Glee.GraphViewerGdi.GViewer();
        //create a graph object 
        static Microsoft.Glee.Drawing.Graph graph = new Microsoft.Glee.Drawing.Graph("graph");
            
        [STAThread]
        static void Main(string[] args)
        {
            var n = 10;
            var d = 2;
            var x = Divide(n, d);

            string message = "Hello, welcome to the Transaction Analyser!";
            bool is_valid_graph = false;
            string user_name;
            string password;
            string user_input = "";

            if (!is_valid_graph) {

                Console.WriteLine(message);
 
                // what a nice little comment here :)
                Console.WriteLine("Press any key to continue:");
                Console.ReadKey();
                Console.WriteLine(Environment.NewLine + "Press enter your username: ");
                user_name = Console.ReadLine();
                Console.WriteLine("Press enter your password: ");
                password = Console.ReadLine();

                Console.WriteLine("Welcome, " + user_name + "! Please enter your transactions: (enter \"exit\" to quit)");
                while (user_input != "exit")
                {
                    user_input = Console.ReadLine();
                    if (user_input.Equals("draw"))
                    {
                        // Thread drawingThread = new Thread(drawGraph) {IsBackground = true};
                        // drawingThread.Start();
                        drawGraph();
                    }
                }

            }
        }

        private static int Divide(int p1, int p2)
        {
            Contract.Requires<ArgumentNullException>(p2 != 0, "Divide by zero is forbidden");

            return p1 / p2;
        }

        private static void drawGraph()
        {
            //create the graph content 
            graph.AddEdge("T1", "T2").EdgeAttr.Color = Microsoft.Glee.Drawing.Color.Red;
            graph.AddEdge("T2", "T1").EdgeAttr.Color = Microsoft.Glee.Drawing.Color.Red;
            graph.AddEdge("T2", "T3");
            // graph.AddEdge("A", "C").EdgeAttr.Color = Microsoft.Glee.Drawing.Color.Green;

            graph.FindNode("T1").Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Magenta;
            graph.FindNode("T2").Attr.Fillcolor = Microsoft.Glee.Drawing.Color.MistyRose;
            Microsoft.Glee.Drawing.Node c = graph.FindNode("T3");
            c.Attr.Fillcolor = Microsoft.Glee.Drawing.Color.PaleGreen;
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
