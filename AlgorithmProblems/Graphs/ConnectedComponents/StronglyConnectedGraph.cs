using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    /// <summary>
    /// Check whether a graph is strongly connected or not.
    /// Strongly connected graph means any the vertex of the graph will have a path to go to any other vertex of the graph
    /// </summary>
    class StronglyConnectedGraph
    {
        #region For undirected graphs
        /// <summary>
        /// For an undirected graph we can do a DFS or BFS and check whether all the elements of the graph are visited or not
        /// 
        /// The running time here is O(V+E)
        /// </summary>
        /// <param name="udg"></param>
        /// <returns></returns>
        public bool IsUndirectedGraphStronglyConnected(UndirectedGraph udg)
        {
            // Do DFS in the undirected graph
            DFS(udg.AllVertices[0]);

            // Check if all the vertices are visited by the DFS
            foreach(GraphVertex vertex in udg.AllVertices)
            {
                if(!vertex.IsVisited)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        /// <summary>
        /// This is the DFS subroutine
        /// </summary>
        /// <param name="vertex"></param>
        private void DFS(GraphVertex vertex)
        {
            if (vertex != null)
            {
                vertex.IsVisited = true;
                if (vertex.NeighbourVertices != null)
                {
                    foreach (GraphVertex neighbour in vertex.NeighbourVertices)
                    {
                        if (!neighbour.IsVisited)
                        {
                            DFS(neighbour);
                        }
                    }
                }
            }
        }

        #region For Directed graphs

        /// <summary>
        /// We cannot use the same method as we used for undirected graphs
        /// We can use Kosaraju’s DFS based simple algorithm with 2 DFS passes
        /// Steps are as follows:
        /// 1. Initialize all vertices' isvisited as false
        /// 2. Do DFS on an arbitarily selected vertex v. If all the vertices is not visited return false
        /// 3. Initialize all vertices' isvisited as false
        /// 4. Reverse all the edges of the graph
        /// 5. Do DFS on the same vertex v. If all the vertices are not visited return false.
        /// Return true
        /// 
        /// 
        /// The running time here is O(V+E)
        /// </summary>
        /// <param name="udg"></param>
        /// <returns></returns>
        public bool IsDirectedGraphStronglyConnected(DirectedGraphWithVertexDictionary dg)
        {
            if(dg.AllVertices.Values.Count ==0)
            {
                // error condition
                throw new ArgumentException("The graph is empty");
            }

            // 1. Initialize all vertices' isvisited as false
            foreach(GraphVertex vertex in dg.AllVertices.Values)
            {
                vertex.IsVisited = false;
            }

            // select the arbitrary vertex
            GraphVertex arbVertex = dg.AllVertices.Values.ElementAt(0);

            //2. Do DFS on an arbitarily selected vertex v. If all the vertices is not visited return false
            DFS(arbVertex);

            foreach (GraphVertex vertex in dg.AllVertices.Values)
            {
                if(!vertex.IsVisited)
                {
                    return false;
                }
            }

            //3.Initialize all vertices' isvisited as false
            foreach (GraphVertex vertex in dg.AllVertices.Values)
            {
                vertex.IsVisited = false;
            }

            //4. Reverse all the edges of the graph
            dg = dg.ReverseDirectedGraphWithVertexDictionary();

            // 5. Do DFS on the same vertex v. If all the vertices are not visited return false.
            DFS(dg.AllVertices[arbVertex.Data]);

            foreach (GraphVertex vertex in dg.AllVertices.Values)
            {
                if (!vertex.IsVisited)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        public static void TestStronglyConnectedGraph()
        {
            StronglyConnectedGraph scg = new StronglyConnectedGraph();
            UndirectedGraph notStronglyConnected = GraphProbHelper.CreateUndirectedGraphNotStronglyConnected();
            Console.WriteLine("The graph is strongly connected. Expected: False, Actual: {0}", scg.IsUndirectedGraphStronglyConnected(notStronglyConnected));

            UndirectedGraph stronglyConnected = GraphProbHelper.CreateUndirectedGraphStronglyConnected();
            Console.WriteLine("The graph is strongly connected. Expected: True, Actual: {0}", scg.IsUndirectedGraphStronglyConnected(stronglyConnected));

            DirectedGraphWithVertexDictionary dg = GraphProbHelper.CreateDirectedGraphWithVertexDictStronglyConnected();
            Console.WriteLine("The graph is strongly connected. Expected: True, Actual: {0}", scg.IsDirectedGraphStronglyConnected(dg));

            dg = GraphProbHelper.CreateDirectedGraphWithVertexDictNotStronglyConnected();
            Console.WriteLine("The graph is strongly connected. Expected: False, Actual: {0}", scg.IsDirectedGraphStronglyConnected(dg));

        }
    }
}
