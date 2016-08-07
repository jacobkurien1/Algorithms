using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.BackTracking
{
    /// <summary>
    /// Given a graph and a number m, make sure a graph can be colored by atmost m colors such that no 2 adjacent vetices have the same color.
    /// </summary>
    class GraphColoringWithMColors
    {

        /// <summary>
        /// Coloring the graph is a 
        /// </summary>
        /// <param name="vertex">start vertex of a graph. This can be any vertex in the graph</param>
        /// <returns>whether the whole graph can be colored using MaxColors different color</returns>
        public bool ColorGraph(GraphVertex vertex, int maxColors)
        {
            for (int index = 0; index < maxColors; index++)
            {
                if (CanColor(vertex, index))
                {
                    vertex.Color = index;
                    bool allNeighboursCanBeColored = true;
                    foreach(GraphVertex neighbour in vertex.Neighbours)
                    {
                        if(neighbour.Color ==-1 && !ColorGraph(neighbour, maxColors))
                        {
                            allNeighboursCanBeColored = false;
                            break;
                        }
                    }
                    if (allNeighboursCanBeColored)
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        /// <summary>
        /// Check whether a vertex can have a color
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool CanColor(GraphVertex vertex, int color)
        {
            foreach (GraphVertex neighbour in vertex.Neighbours)
            {
                if(neighbour.Color == color)
                {
                    return false;
                }
            }
            return true;
        }

        public static void TestGraphColoringWithMColors()
        {
            /*
            The graph looks like like this
		    (0)
	         /\
		    /  \
	       /    \
	      (2)	(1)
	       | \   |
	       |  \  |
	       |   \ |
	      (4)--(3)
	       \    /
	        \  /
		     (5)
            */
            Graph graph = new Graph();
            graph.AddUnDirectedEdge(0, 1);
            graph.AddUnDirectedEdge(0, 2);
            graph.AddUnDirectedEdge(1, 3);
            graph.AddUnDirectedEdge(2, 3);
            graph.AddUnDirectedEdge(2, 4);
            graph.AddUnDirectedEdge(3, 4);
            graph.AddUnDirectedEdge(3, 5);
            graph.AddUnDirectedEdge(5, 4);

            GraphColoringWithMColors coloring = new GraphColoringWithMColors();
            int maxColor = 3;
            Console.WriteLine("Can this graph be colored with {0} colors: {1}", maxColor, coloring.ColorGraph(graph.AllVertices[0], maxColor));

            maxColor = 2;
            graph.ResetGraph();
            Console.WriteLine("Can this graph be colored with {0} colors: {1}", maxColor, coloring.ColorGraph(graph.AllVertices[0], maxColor));

            maxColor = 1;
            graph.ResetGraph();
            Console.WriteLine("Can this graph be colored with {0} colors: {1}", maxColor, coloring.ColorGraph(graph.AllVertices[0], maxColor));

            maxColor = 5;
            graph.ResetGraph();
            Console.WriteLine("Can this graph be colored with {0} colors: {1}", maxColor, coloring.ColorGraph(graph.AllVertices[0], maxColor));
        }

        /// <summary>
        /// Represents the vertex of a graph
        /// </summary>
        internal class GraphVertex
        {
            public int Id { get; set; }
            public List<GraphVertex> Neighbours { get; set; }
            public int Color { get; set; }
            public GraphVertex(int id)
            {
                Id = id;
                Neighbours = new List<GraphVertex>();
                Color = -1;
            }
        }

        /// <summary>
        /// Represents the graph class
        /// </summary>
        internal class Graph
        {
            
            public Graph()
            {
                AllVertices = new Dictionary<int, GraphVertex>();
            }
            /// <summary>
            /// Dictionary to store all the graph vertices and give O(1) access using the vertex ids
            /// </summary>
            public Dictionary<int, GraphVertex> AllVertices { get; set; }

            /// <summary>
            /// Add the edge from the node having start as the id to node having end as the id
            /// </summary>
            /// <param name="start"></param>
            /// <param name="end"></param>
            public void AddDirectedEdge(int start, int end)
            {
                if (!AllVertices.ContainsKey(start))
                {
                    AllVertices[start] = new GraphVertex(start);
                }
                if (!AllVertices.ContainsKey(end))
                {
                    AllVertices[end] = new GraphVertex(end);
                }
                AllVertices[start].Neighbours.Add(AllVertices[end]);
            }

            /// <summary>
            /// Add an undirected edge between start and end vertex
            /// </summary>
            /// <param name="start"></param>
            /// <param name="end"></param>
            public void AddUnDirectedEdge(int start, int end)
            {
                if (!AllVertices.ContainsKey(start))
                {
                    AllVertices[start] = new GraphVertex(start);
                }
                if (!AllVertices.ContainsKey(end))
                {
                    AllVertices[end] = new GraphVertex(end);
                }
                AllVertices[start].Neighbours.Add(AllVertices[end]);
                AllVertices[end].Neighbours.Add(AllVertices[start]);
            }

            /// <summary>
            /// This method will be used to reset the color of all the vertices in the graph to -1
            /// </summary>
            public void ResetGraph()
            {
                foreach (GraphVertex vertex in AllVertices.Values)
                {
                    vertex.Color = -1;
                }

            }
        }
    }
}
