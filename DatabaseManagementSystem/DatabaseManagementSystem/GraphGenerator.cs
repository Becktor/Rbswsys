using System;

namespace DatabaseManagementSystem
{
	public class GraphGenerator
	{
        //create a form 
        static System.Windows.Forms.Form form = new System.Windows.Forms.Form();
        //create a viewer object 
        static Microsoft.Glee.GraphViewerGdi.GViewer viewer = new Microsoft.Glee.GraphViewerGdi.GViewer();
        //create a graph object 
        static Microsoft.Glee.Drawing.Graph graph = new Microsoft.Glee.Drawing.Graph("graph");
		public GraphGenerator ()
		{
		}

        public void drawGraph() {

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

