using AlgorithmProblems.Heaps.HeapHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.MinSpanningTree
{
    /// <summary>
    /// Get the minimum spanning tree of a graph using Prim's algorithm
    /// minimum spanning tree - Given connected graph G with positive edge weights, find a min weight set of edges that connects all of the vertices.
    /// 
    /// Algo:
    /// 1. Select any vertex from graph and put it in a set
    /// 2. Add all the edges from this vertex to the PriorityQueue
    /// 3. Select the min weight edge such that one vertex is in set and another is not in the set.
    /// 4. Add this to the mst (list<edge>)
    /// 5. Do this till all the vertices are in the set.
    /// 
    /// This is also a greedy algorithm.
    /// The running time of this algorithm is O(Elog(V))
    /// </summary>
    class PrimsAlgorithm
    {
        public List<Edge> GetMST(Graph graph)
        {
            List<Edge> mst = new List<Edge>();
            MinHeap<Edge> priorityQueue = new MinHeap<Edge>(graph.TotalNumOfEdges);
            Dictionary<int, bool> verticesInMst = new Dictionary<int, bool>();

            //1.Select any vertex from graph and put it in a set
            int vertexId = graph.Edges.ElementAt(0).Key;
            verticesInMst.Add(vertexId, true);

            while(verticesInMst.Count < graph.Edges.Count)
            {
                //2.Add all the edges from this vertex to the PriorityQueue
                foreach(Edge e in graph.Edges[vertexId])
                {
                    if ((verticesInMst.ContainsKey(e.StId) && !verticesInMst.ContainsKey(e.EndId))
                        || (!verticesInMst.ContainsKey(e.StId) && verticesInMst.ContainsKey(e.EndId)))
                    {
                        priorityQueue.Insert(e);

                    }
                }

                //3. Select the min weight edge such that one vertex is in set and another is not in the set.
                Edge minEdge = null;
                do
                {
                    minEdge = priorityQueue.ExtractMin();

                } while (minEdge!= null && !IsOneVertexInMst(verticesInMst, minEdge));
                mst.Add(minEdge);

                // 4. Add this to the mst (list<edge>)
                if(!verticesInMst.ContainsKey(minEdge.StId))
                {
                    verticesInMst[minEdge.StId] = true;
                    vertexId = minEdge.StId;
                }
                else if (!verticesInMst.ContainsKey(minEdge.EndId))
                {
                    verticesInMst[minEdge.EndId] = true;
                    vertexId = minEdge.EndId;
                }
            }
            return mst;
        }

        private bool IsOneVertexInMst(Dictionary<int, bool> verticesInMst, Edge minEdge)
        {
            if ((verticesInMst.ContainsKey(minEdge.StId) && !verticesInMst.ContainsKey(minEdge.EndId))
                        || (!verticesInMst.ContainsKey(minEdge.StId) && verticesInMst.ContainsKey(minEdge.EndId)))
            {
                return true;
            }
            return false;
        }

        internal class Graph
        {
            public Dictionary<int, List<Edge>>  Edges { get; set; }
            public int TotalNumOfEdges { get; set; }
            public Graph()
            {
                Edges = new Dictionary<int, List<Edge>>();
                TotalNumOfEdges = 0;
            }

            /// <summary>
            /// Adds an undirected weighted edge to the graph
            /// </summary>
            /// <param name="st"></param>
            /// <param name="end"></param>
            /// <param name="weight">weight of the edge</param>
            public void AddEdge(int st, int end, int weight)
            {
                if(!Edges.ContainsKey(st))
                {
                    Edges[st] = new List<Edge>() { new Edge(st, end, weight) };
                }
                else
                {
                    Edges[st].Add(new Edge(st, end, weight));
                }

                if (!Edges.ContainsKey(end))
                {
                    Edges[end] = new List<Edge>() { new Edge(end, st, weight) };
                }
                else
                {
                    Edges[end].Add(new Edge(end, st, weight));
                }
                TotalNumOfEdges += 2;
            }
        }

        /// <summary>
        /// This is a weighted undirected edge
        /// </summary>
        internal class Edge : IComparable
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

            public int CompareTo(object obj)
            {
                Edge newObj = (Edge)obj;
                return Weight.CompareTo(newObj.Weight);
            }
        }

        #region TestArea
        public static void TestPrimsAlgorithm()
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

            PrimsAlgorithm pa = new PrimsAlgorithm();
            List<Edge> mst = pa.GetMST(g);
            PrintMst(mst);
        }
        private static void PrintMst(List<Edge> mst)
        {
            Console.WriteLine("The mst edges are as shown below:");
            foreach (Edge e in mst)
            {
                Console.WriteLine(e.ToString());
            }
        }
        #endregion
    }
}
