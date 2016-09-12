using AlgorithmProblems.DisjointSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.MinSpanningTree
{
    /// <summary>
    /// Get the minimum spanning tree of a graph using Kruskal's algorithm
    /// minimum spanning tree - Given connected graph G with positive edge weights, find a min weight set of edges that connects all of the vertices.
    /// 
    /// Algo:
    /// 1. Sort the edges in ascending order of their weights
    /// 2. Keep adding edge to the MST if the addition does not add a cycle to the graph
    /// 
    /// We need a quick way to check whether adding an edge creates a cycle. 
    /// DFS is the naive approach and it will take O(V) running time. E `= V in case of MST
    /// We can use disjointSets/ union-find algo to check whether adding an edge creates a cycle in O(log(V)) time
    /// 
    /// The overall time taken becomes O(E log(V)) |  E = V^2 and log(E) `= log(V) 
    /// 
    /// Note: This is a greedy approach and it gives optimal results
    /// </summary>
    class KruskalAlgorithm
    {
        public List<Edge> GetMST(Graph graph)
        {
            List<Edge> mst = new List<Edge>();

            // Sort the edges in ascending order of their weights
            List<Edge> sortedEdges = graph.AllEdges.OrderBy(e => e.Weight).ToList();

            int totalNumOfVertices = graph.AllVertices.Count();
            DisjointSets<int> ds = new DisjointSets<int>();


            foreach (Edge e in sortedEdges)
            {
                if(mst.Count() >= totalNumOfVertices)
                {
                    // total number of edges in an mst will be total number of vertices in a graph -1
                    break;
                }
                if(!ds.Find(e.StId, e.EndId))
                {
                    // the stId and endId vertex of the edge are not in the same set
                    // hence adding this edge wont create a cycle
                    ds.Union(e.StId, e.EndId);
                    mst.Add(e);
                }
            }

            return mst;
        }


        /// <summary>
        /// This is a weighted undirected graph
        /// </summary>
        public class Graph
        {
            public Graph()
            {
                AllEdges = new List<Edge>();
                AllVertices = new Dictionary<int, bool>();
            }
            /// <summary>
            /// Saves all the edges
            /// </summary>
            public List<Edge> AllEdges { get; set; }

            public Dictionary<int, bool> AllVertices { get; set; }

            public void AddEdge(int st, int end, int weight)
            {
                AllEdges.Add(new Edge(st, end, weight));
                AllVertices[st] = true;
                AllVertices[end] = true;
            }
        }

        /// <summary>
        /// This is a weighted undirected edge
        /// </summary>
        public class Edge
        {
            public int StId { get; set; }
            public int EndId { get; set; }
            public int Weight { get; set; }

            public Edge(int stId, int endId, int weight)
            {
                StId = stId;
                EndId = endId;
                Weight = weight;
            }

            /// <summary>
            /// Print a weighted Edge
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return string.Format("{0} --{1}--> {2}", StId, Weight, EndId);
            }
        }

        #region TestArea
        public static void TestKruskalAlgorithm()
        {
            Graph g = new Graph();
            g.AddEdge(0, 1, 4);
            g.AddEdge(0, 2, 6);
            g.AddEdge(0, 3, 16);
            g.AddEdge(1, 4, 24);
            g.AddEdge(2, 4, 23);
            g.AddEdge(3, 5, 10);
            g.AddEdge(2, 5, 5);
            g.AddEdge(2, 3, 8);
            g.AddEdge(5, 4, 18);
            g.AddEdge(5, 6, 11);
            g.AddEdge(6, 4, 9);
            g.AddEdge(6, 7, 7);
            g.AddEdge(5, 7, 14);
            g.AddEdge(3, 7, 21);

            KruskalAlgorithm ka = new KruskalAlgorithm();
            List<Edge> mst = ka.GetMST(g);
            PrintMst(mst);
        }

        private static void PrintMst(List<Edge> mst)
        {
            Console.WriteLine("The mst edges are as shown below:");
            foreach(Edge e in mst)
            {
                Console.WriteLine(e.ToString());
            }
        }
        #endregion
    }
}
