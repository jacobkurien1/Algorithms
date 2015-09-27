using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
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

            // Need to get all the nodes that can be visited from the start vertex
            List<GraphVertex> nonVisitedGraphVertex = new List<GraphVertex>();
            foreach(GraphVertex neighbour in startVertex.NeighbourVertices)
            {
                if(!neighbour.IsVisited)
                {
                    nonVisitedGraphVertex.Add(neighbour);
                }
            }

            // if none of the nodes can be visited from the start vertex then we have found a path and we need to print it
            if(nonVisitedGraphVertex.Count == 0)
            {
                // We have reached the end of all the visited nodes
                // We need to print this path
                foreach(GraphVertex vertex in CurrentPath)
                {
                    Console.Write(vertex.Data + " -> ");
                }
                Console.WriteLine();
            }

            // for all the neighbouring nodes which have not been visited, do the DFS
            foreach(GraphVertex neighbour in nonVisitedGraphVertex)
            {
                GetAllPathsInGraphFromStartVertex(neighbour);
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
