using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    /// <summary>
    /// Get all the paths in a graph
    /// 
    /// Algo: We can do DFS and Keep track of the current path.
    /// Print the path when we have no non-visited neighbours.
    /// 
    /// The running time is O(V+E) to do DFS
    /// The spave requirement is O(V) to store the path
    /// </summary>
    class AllPaths
    {

        /// <summary>
        /// Keeps track of the current path
        /// </summary>
        private static List<GraphVertex> CurrentPath = new List<GraphVertex>();

        /// <summary>
        /// Get all the paths in the graph from the start vertex
        /// </summary>
        /// <param name="startVertex">start vertex</param>
        private static void GetAllPathsInGraphFromStartVertex(GraphVertex startVertex)
        {
            if(startVertex == null)
            {
                return;
            }
            startVertex.IsVisited = true;
            CurrentPath.Add(startVertex);

            bool NoNonVisitedNeighbour = true;
            foreach(GraphVertex neighbour in startVertex.NeighbourVertices)
            {
                if(!neighbour.IsVisited)
                {
                    NoNonVisitedNeighbour = false;
                    GetAllPathsInGraphFromStartVertex(neighbour);
                }
            }

            // if none of the nodes can be visited from the start vertex then we have found a path and we need to print it
            if (NoNonVisitedNeighbour)
            {
                // We have reached the end of all the visited nodes
                // We need to print this path
                foreach (GraphVertex vertex in CurrentPath)
                {
                    Console.Write(vertex.Data + " -> ");
                }
                Console.WriteLine();
            }

            // the below steps will be used for backtracking
            startVertex.IsVisited = false;
            CurrentPath.Remove(startVertex);
        }

        public static void TestGetAllPathsInGraphFromStartVertex()
        {
            DirectedGraph dg = GraphProbHelper.CreateDirectedGraph();
            Console.WriteLine("All the paths from start vertex 0 are:");
            GetAllPathsInGraphFromStartVertex(dg.AllVertices[0]);

            Console.WriteLine("All the paths from start vertex 2 are:");
            GetAllPathsInGraphFromStartVertex(dg.AllVertices[2]);

        }
    }
}
