using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    class AllPathsGivenStartEndVertex
    {
        /// <summary>
        /// This stores the current path during the DFS traversal
        /// </summary>
        private static List<GraphVertex> CurrentPath = new List<GraphVertex>();

        /// <summary>
        /// We will use DFS to get all the paths from startVertex to endVertex.
        /// </summary>
        /// <param name="startVertex"></param>
        /// <param name="endVertex"></param>
        private static void GetAllPathsInGraphFromStartVertexToEndVertex(GraphVertex startVertex, GraphVertex endVertex)
        {
            if(startVertex == null)
            {
                return;
            }
            if( startVertex == endVertex)
            {
                //We have reached the end
                CurrentPath.Add(endVertex);

                // Print the path from start to end
                foreach(GraphVertex vertex in CurrentPath)
                {
                    Console.Write(vertex.Data + " -> ");
                }
                Console.WriteLine();

                // lets back track to find other paths
                CurrentPath.Remove(endVertex);
                return;
            }
            startVertex.IsVisited = true;
            CurrentPath.Add(startVertex);
            foreach(GraphVertex neighbour in startVertex.NeighbourVertices)
            {
                if(!neighbour.IsVisited)
                {
                    GetAllPathsInGraphFromStartVertexToEndVertex(neighbour,endVertex);
                }
            }
            startVertex.IsVisited = false; // this will be used for backtracking
            CurrentPath.Remove(startVertex);
        }

        public static void TestGetAllPathsInGraphFromStartVertexToEndVertex()
        {
            DirectedGraph dg = GraphProbHelper.CreateDirectedGraph();
            Console.WriteLine("All the paths from start vertex to the end vertex are:");
            GetAllPathsInGraphFromStartVertexToEndVertex(dg.AllVertices[2], dg.AllVertices[3]);
        }
    }
}
