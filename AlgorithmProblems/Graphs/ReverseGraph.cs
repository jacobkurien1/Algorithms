using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    /// <summary>
    /// Reverse a directed graph, in other words reverse the directions of all the edges in a directed graph
    /// </summary>
    public static class ReverseGraph
    {
        
        public static DirectedGraph ReverseDirectedGraph(this DirectedGraph dg)
        {
            // Add all edges to this dictionary where for an edge u->v add it as dict[v].Add(u)
            Dictionary<GraphVertex, List<GraphVertex>> allVertices = new Dictionary<GraphVertex, List<GraphVertex>>();

            foreach(GraphVertex vertex in dg.AllVertices)
            {
                if (vertex.NeighbourVertices != null)
                {
                    foreach (GraphVertex neighbour in vertex.NeighbourVertices)
                    {
                        if (!allVertices.ContainsKey(neighbour))
                        {
                            allVertices.Add(neighbour, new List<GraphVertex> { vertex });
                        }
                        else
                        {
                            allVertices[neighbour].Add(vertex);
                        }
                    }
                }
            }

            // Make the changes to all the vertices in the directed graph
            foreach(GraphVertex vertex in dg.AllVertices)
            {
                if (allVertices.ContainsKey(vertex))
                {
                    vertex.NeighbourVertices = allVertices[vertex];
                }
                else
                {
                    vertex.NeighbourVertices = null;
                }
            }

            return dg;
        }

        public static DirectedGraphWithVertexDictionary ReverseDirectedGraphWithVertexDictionary(this DirectedGraphWithVertexDictionary dg)
        {
            // Add all edges to this dictionary where for an edge u->v add it as dict[v].Add(u)
            Dictionary<GraphVertex, List<GraphVertex>> allVertices = new Dictionary<GraphVertex, List<GraphVertex>>();

            foreach (GraphVertex vertex in dg.AllVertices.Values)
            {
                if (vertex.NeighbourVertices != null)
                {
                    foreach (GraphVertex neighbour in vertex.NeighbourVertices)
                    {
                        if (!allVertices.ContainsKey(neighbour))
                        {
                            allVertices.Add(neighbour, new List<GraphVertex> { vertex });
                        }
                        else
                        {
                            allVertices[neighbour].Add(vertex);
                        }
                    }
                }
            }

            // Make the changes to all the vertices in the directed graph
            foreach (GraphVertex vertex in dg.AllVertices.Values)
            {
                if (allVertices.ContainsKey(vertex))
                {
                    vertex.NeighbourVertices = allVertices[vertex];
                }
                else
                {
                    vertex.NeighbourVertices = null;
                }
            }

            return dg;
        }

        public static void TestReverseGraph()
        {
            DirectedGraph dg = GraphProbHelper.CreatedirectedGraphWithCycle();
            Console.WriteLine("Adjacency matrix of a input directed graph is as shown below");
            GraphProbHelper.PrintDirectedGraphInAdjacencyMatrix(dg);

            Console.WriteLine("Adjacency matrix of a reversed directed graph is as shown below");
            GraphProbHelper.PrintDirectedGraphInAdjacencyMatrix(dg.ReverseDirectedGraph());

            dg = GraphProbHelper.CreatedirectedGraphWithoutCycle();
            Console.WriteLine("Adjacency matrix of a input directed graph is as shown below");
            GraphProbHelper.PrintDirectedGraphInAdjacencyMatrix(dg);

            Console.WriteLine("Adjacency matrix of a reversed directed graph is as shown below");
            GraphProbHelper.PrintDirectedGraphInAdjacencyMatrix(dg.ReverseDirectedGraph());
        }

    }
}
