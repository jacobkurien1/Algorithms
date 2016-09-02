using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.MaxFlow
{
    /// <summary>
    /// Find the max flow in a graph
    /// </summary>
    class FordFulkersonEdmondKarp
    {
        /// <summary>
        /// Gets the max flow in a network when the source and sink of the network is specified
        /// We can use ford fulkerson method with Edward-Karp Algo.
        /// 
        /// Ford fulkerson method gets the augmented path from source to destination using DFS
        /// And then gets the critical edge with min residual capacity and assigns that flow to the path
        /// The residual capacities of the other edges are changed accordingly.
        /// The running time for this algo is O(E*max flow)
        /// This is a bad running time cause it depends on max flow and it can be very large
        /// 
        /// Ford Fulkerson with Edward karp suggests that we get the augmented path using BFS
        /// The rest of the algo is same.
        /// The running time for this is O(E*#of augmented paths)
        /// so it comes to be O(V(E^2))
        /// 
        /// </summary>
        /// <param name="graph">represents the graph object</param>
        /// <param name="startId">source in the graph</param>
        /// <param name="endId">sink in the graph</param>
        /// <returns>returns the maximum flow in the network</returns>
        public int GetTheMaxFlow(Graph graph, string startId, string endId)
        {
            int maxFlow = 0;
            int currentFlow = BFS(graph, startId, endId);

            while (currentFlow != -1)
            {
                // for each augmented path find the flow and add it to max flow
                maxFlow += currentFlow;
                currentFlow = BFS(graph, startId, endId);
            }

            return maxFlow;
        }

        /// <summary>
        /// Do the BFS and get the augmented path and return the current flow in that path
        /// </summary>
        /// <param name="graph">graph object</param>
        /// <param name="startId">source in the graph</param>
        /// <param name="endId">sink in the graph</param>
        /// <returns>current flow in that path</returns>
        public int BFS(Graph graph, string startId, string endId)
        {
            Dictionary<string, string> parentDict = new Dictionary<string, string>();
            Queue<GraphVertex> queue = new Queue<GraphVertex>();
            queue.Enqueue(graph.AllVertices[startId]);
            parentDict[startId] = string.Empty;

            while(queue.Count >0)
            {
                GraphVertex vertex = queue.Dequeue();
                if(vertex.Id == endId)
                {
                    // found the augmented path and get the current flow
                    return GetTheFlowInCurrentPath(graph, parentDict, startId, endId);
                }
                foreach(GraphVertex neighbour in vertex.Neighbours)
                {
                    if(!parentDict.ContainsKey(neighbour.Id) && graph.EdgeWeights[vertex.Id+"#"+neighbour.Id] != 0)
                    {
                        // the neighbour should not have been visited and the weight of the edge should not be 0
                        queue.Enqueue(neighbour);
                        parentDict[neighbour.Id] = vertex.Id;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// This function gets the parentDict which helps to do the backtracking.
        /// Here we get the critical edge which has the min residual capacity.
        /// the whole path will need to subtrack the weight of the critical edge.
        /// Also we need to add the weight of the critical edge to the back edges.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="parentDict">this dict has key as the current vertex id and its parent as the value</param>
        /// <param name="startId">source in the graph</param>
        /// <param name="endId">sink in the graph</param>
        /// <returns>the current flow in the current path. this is same as the weight of the critical edge</returns>
        private int GetTheFlowInCurrentPath(Graph graph, Dictionary<string, string> parentDict, string startId, string endId)
        {
            string currentId = endId;
            int criticalEdgeWeight = int.MaxValue;
            
            // Find the critical Edge weight
            while (currentId != string.Empty)
            {
                string parentId = parentDict[currentId];
                if (parentId != string.Empty && criticalEdgeWeight > graph.EdgeWeights[parentId + "#" + currentId])
                {
                    criticalEdgeWeight = graph.EdgeWeights[parentId + "#" + currentId];
                }
                currentId = parentId;
            }
            currentId = endId;

            // Make sure that the residual graph edges have the correct weights
            while (currentId != string.Empty)
            {
                string parentId = parentDict[currentId];
                if (parentId != string.Empty)
                {
                    graph.DecrementEdgeWeightByFactor(parentId, currentId, criticalEdgeWeight);
                    graph.IncrementEdgeWeightByFactor(currentId, parentId, criticalEdgeWeight);
                }
                currentId = parentId;
            }

            return (criticalEdgeWeight == int.MaxValue) ? -1: criticalEdgeWeight;
        }

        #region Test Area
        public static void TestFordFulkersonEdmondKarp()
        {
            FordFulkersonEdmondKarp ffek = new FordFulkersonEdmondKarp();

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
            Graph graph = new Graph();
            graph.AddEdge("A", "B", 3);
            graph.AddEdge("B", "A", 0);
            graph.AddEdge("A", "D", 3);
            graph.AddEdge("D", "A", 0);
            graph.AddEdge("D", "E", 2);
            graph.AddEdge("E", "D", 0);
            graph.AddEdge("E", "B", 1);
            graph.AddEdge("B", "E", 0);
            graph.AddEdge("C", "A", 3);
            graph.AddEdge("A", "C", 0);
            graph.AddEdge("B", "C", 4);
            graph.AddEdge("C", "B", 0);
            graph.AddEdge("C", "D", 1);
            graph.AddEdge("D", "C", 0);
            graph.AddEdge("C", "E", 2);
            graph.AddEdge("E", "C", 0);
            graph.AddEdge("D", "F", 6);
            graph.AddEdge("F", "D", 0);
            graph.AddEdge("F", "G", 9);
            graph.AddEdge("G", "F", 0);
            graph.AddEdge("E", "G", 1);
            graph.AddEdge("G", "E", 0);

            int maxFlow = ffek.GetTheMaxFlow(graph, "A", "G");
            Console.WriteLine("The max flow in the graph is {0}", maxFlow);
        }
        #endregion
    }

    
}
