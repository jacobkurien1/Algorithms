using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    /// <summary>
    /// Create a clone of a graph given the start vertex of the graph
    /// </summary>
    class CloneGraph
    {
        /// <summary>
        /// We can do a BFS to clone a Connected Graph.
        /// 
        /// Algo: 1. CloneDict will keep track of A -->A` mapping
        /// 2. Visiting the neighbours in BFS is equivalent to visiting the edges.
        /// While visiting the edges, check if both the start and the end vertices have the clone vertices in the cloneDict.
        /// 3. If not, create them and Add the forward edge to the clone vertices.
        /// 
        /// This code will work on directed graph, undirected graph and graph with cycles
        /// 
        /// The running time is O(V+E)
        /// The space requirement is O(V)
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static GraphVertex Clone(GraphVertex root)
        {
            if(root == null)
            {
                // Error case
                return null;
            }
            // This will have key as the actual vertex A and the value as its Clone A`
            Dictionary<GraphVertex, GraphVertex> cloneDict = new Dictionary<GraphVertex, GraphVertex>(new GraphVertexComparer());

            Queue<GraphVertex> queue = new Queue<GraphVertex>();
            queue.Enqueue(root);
            cloneDict[root] = new GraphVertex(root.Data);
            while(queue.Count >0)
            {
                GraphVertex v = queue.Dequeue();
                foreach(GraphVertex neighbour in v.NeighbourVertices)
                {
                    if(!cloneDict.ContainsKey(neighbour))
                    {
                        cloneDict[neighbour] = new GraphVertex(neighbour.Data);
                        queue.Enqueue(neighbour);
                    }
                    // Add the forward edges to the clone vertices
                    cloneDict[v].NeighbourVertices.Add(cloneDict[neighbour]);
                }
            }

            return cloneDict[root];
        }

        /// <summary>
        /// Graph vertex comparer class
        /// </summary>
        public class GraphVertexComparer : IEqualityComparer<GraphVertex>
        {
            public bool Equals(GraphVertex x, GraphVertex y)
            {
                return x.Data == y.Data;
            }

            public int GetHashCode(GraphVertex obj)
            {
                return obj.Data.GetHashCode();
            }
        }


        #region Test Area
        public static void TestCloneGraph()
        {
            UndirectedGraph udg = GraphProbHelper.CreateUndirectedGraphWithCycle();
            Console.WriteLine("The actual graph looks like:");
            GraphProbHelper.PrintGraphBFS(udg.AllVertices[0]);

            Console.WriteLine("The clone looks like");
            GraphVertex clone = Clone(udg.AllVertices[0]);
            GraphProbHelper.PrintGraphBFS(clone);

            DirectedGraph dg = GraphProbHelper.CreatedirectedGraphWithCycle();
            Console.WriteLine("The actual graph looks like:");
            GraphProbHelper.PrintGraphBFS(dg.AllVertices[0]);

            Console.WriteLine("The clone looks like");
            clone = Clone(dg.AllVertices[0]);
            GraphProbHelper.PrintGraphBFS(clone);
        }
        #endregion
    }
}
