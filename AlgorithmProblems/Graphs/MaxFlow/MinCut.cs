using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.MaxFlow
{
    /// <summary>
    /// In a flow network, make an cut with min capacity such that it seperates the source and sink into different subsets.
    /// 
    /// Return the min cut as the list of edges from set A to set B, where set A and B are formed due to the cut.
    /// </summary>
    class MinCut
    {

        /// <summary>
        /// We can get the min cut using the max-flow min-cut theorm, which states that the max flow in a n/w = min-cut of the network.
        /// Steps:
        /// 1. Run FordFulkerson algo to get the residual graph.
        /// 2. Find the set of vertices that can be reached from the source vertex.
        /// 3. Get all the edges from vertices in this set to the vertices which are not present in this set.
        /// this will give all the edges of the min cut.
        /// 
        /// The running time of this algo is same as the ford fulkerson's max flow algo with edward karp optimization
        /// O(V(E^2))
        /// </summary>
        /// <param name="graph">graph object</param>
        /// <param name="startId">id of the source vertex</param>
        /// <param name="endId">id of the sink vertex</param>
        /// <returns></returns>
        public List<string> GetMinCut(GraphWithFwdEdges graph, string startId, string endId)
        {
            List<string> setOfEdges = new List<string>();

            // 1. Run FordFulkerson algo to get the residual graph.
            FordFulkersonEdmondKarp ffkp = new FordFulkersonEdmondKarp();
            ffkp.GetTheMaxFlow(graph, startId, endId);

            // 2. Find the set of vertices that can be reached from the source vertex.
            Dictionary<GraphVertex, bool> setOfVerticesFromSource = GetAllVerticesThatCanBeReachedFromSrc(graph, startId);

            // 3.Get all the forward edges from vertices in this set to the vertices which are not present in this set.
            foreach(GraphVertex vertex in setOfVerticesFromSource.Keys)
            {
                foreach(GraphVertex neighbour in vertex.Neighbours)
                {
                    string edgeId = vertex.Id + "#" + neighbour.Id;
                    if (graph.IsEdgeFwd[edgeId] && !setOfVerticesFromSource.ContainsKey(neighbour))
                    {
                        // the graph edge should not have 0 flow and also should not be present in the current set(found after the cut)
                        setOfEdges.Add(edgeId);
                    }
                }
            }

            return setOfEdges;
        }

        /// <summary>
        /// Gets all the vertices that can be reached from the source
        /// We can do BFS to get this.
        /// </summary>
        /// <param name="graph">graph object</param>
        /// <param name="startId">id of the source vertex</param>
        /// <returns></returns>
        private Dictionary<GraphVertex, bool> GetAllVerticesThatCanBeReachedFromSrc(Graph graph, string startId)
        {
            Dictionary<GraphVertex, bool> setOfVerticesFromSource = new Dictionary<GraphVertex, bool>();

            //Do a BFS to get all the vertices that can be reached from the source
            Queue<GraphVertex> queue = new Queue<GraphVertex>();
            GraphVertex source = graph.AllVertices[startId];
            queue.Enqueue(source);
            setOfVerticesFromSource[source] = true;

            while (queue.Count >0)
            {
                GraphVertex vertex = queue.Dequeue();
                foreach(GraphVertex neighbour in vertex.Neighbours)
                {
                    if(graph.EdgeWeights[vertex.Id+"#"+neighbour.Id] != 0 && !setOfVerticesFromSource.ContainsKey(neighbour))
                    {
                        // Edges with flow 0 should not be considered
                        // vertices in setOfVerticesFromSource keeps track of the visited vertices
                        setOfVerticesFromSource[neighbour] = true;
                        queue.Enqueue(neighbour);
                    }
                }
            }

            return setOfVerticesFromSource;
        }

        #region TestArea
        public static void TestMinCut()
        {
            MinCut mc = new MinCut();

            /*
            (A)<------3--------(B)
             | X               /x
             |  \             / |
             |    3         4   |
             |      \      /    |
             3        \  x      |
             |         (C)      1
             |       /   \      |
             |     /       \    |
             |    1          2  |
             |  x             x |
             (D)-------2------>(E)
             |                  |
             |                  |
             |                  |
             6                  1
             |                  |
             x                  X
            (F)-------9------->(G)
            */
            GraphWithFwdEdges graph = new GraphWithFwdEdges();
            graph.AddEdge("A", "B", 3);
            graph.AddEdge("B", "A", 0, false);
            graph.AddEdge("A", "D", 3);
            graph.AddEdge("D", "A", 0, false);
            graph.AddEdge("D", "E", 2);
            graph.AddEdge("E", "D", 0, false);
            graph.AddEdge("E", "B", 1);
            graph.AddEdge("B", "E", 0, false);
            graph.AddEdge("C", "A", 3);
            graph.AddEdge("A", "C", 0, false);
            graph.AddEdge("B", "C", 4);
            graph.AddEdge("C", "B", 0, false);
            graph.AddEdge("C", "D", 1);
            graph.AddEdge("D", "C", 0, false);
            graph.AddEdge("C", "E", 2);
            graph.AddEdge("E", "C", 0, false);
            graph.AddEdge("D", "F", 6);
            graph.AddEdge("F", "D", 0, false);
            graph.AddEdge("F", "G", 9);
            graph.AddEdge("G", "F", 0, false);
            graph.AddEdge("E", "G", 1);
            graph.AddEdge("G", "E", 0, false);

            List<string> setOfEdges = mc.GetMinCut(graph, "A", "G");
            PrintEdges(setOfEdges);


            graph = new GraphWithFwdEdges();
            graph.AddEdge("0", "1", 16);
            graph.AddEdge("1", "2", 10);
            graph.AddEdge("0", "2", 13);
            graph.AddEdge("2", "1", 4);
            graph.AddEdge("1", "3", 12);
            graph.AddEdge("3", "2", 9);
            graph.AddEdge("2", "4", 14);
            graph.AddEdge("4", "3", 7);
            graph.AddEdge("4", "5", 4);
            graph.AddEdge("3", "5", 20);

            graph.AddEdge("1", "0", 0, false);
            graph.AddEdge("2", "0", 0, false);
            graph.AddEdge("3", "1", 0, false);
            graph.AddEdge("2", "3", 0, false);
            graph.AddEdge("4", "2", 0, false);
            graph.AddEdge("3", "4", 0, false);
            graph.AddEdge("5", "4", 0, false);
            graph.AddEdge("5", "3", 0, false);
            setOfEdges = mc.GetMinCut(graph, "0", "5");
            PrintEdges(setOfEdges);
        }
        public static void PrintEdges(List<string> setOfEdges)
        {
            Console.WriteLine("The edges at the cut are as shown below:");
            foreach(string edge in setOfEdges)
            {
                Console.Write("{0} , ", edge);
            }
            Console.WriteLine();
        }

        #endregion

        /// <summary>
        /// Graph with the indication whether an edge is a forward edge or backward edge
        /// </summary>
        internal class GraphWithFwdEdges : Graph
        {
            public Dictionary<string, bool> IsEdgeFwd { get; }
            public GraphWithFwdEdges()
            {
                IsEdgeFwd = new Dictionary<string, bool>();
            }

            public void AddEdge(string startId, string endId, int weight, bool IsFwdEdge = true)
            {
                IsEdgeFwd[startId + "#" + endId] = IsFwdEdge;
                base.AddEdge(startId, endId, weight);
            }
        }
    }
}
