using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    /// <summary>
    /// This is a shortest path algorithm using BFS for graphs with unweighted edges
    /// </summary>
    class ShortestPath
    {
        /// <summary>
        /// This dictionary will keep track of the distance of each node from the start node
        /// Note: We can also use a property on the GraphVertex but then we will have to make sure 
        /// we dispose it before reusing the objects
        /// </summary>
        private static Dictionary<GraphVertex, int> distanceOfGraphVertex = new Dictionary<GraphVertex, int>();

        /// <summary>
        /// This dictionary will keep track of parent of each vertex when travelling from start vertex to end vertex
        /// This property will be used to trace back the path from start vertex to the end
        /// </summary>
        private static Dictionary<GraphVertex, GraphVertex> parentOfVertex = new Dictionary<GraphVertex, GraphVertex>();

        /// <summary>
        /// We can find whether path between 2 nodes exist or not using DFS
        /// But to find shortest path between 2 nodes we need to use BFS
        /// 
        /// The running time of the BFS algo is O(V+E) where V is the number of vertex and E is the number of Edges
        /// </summary>
        /// <param name="start">start vertex</param>
        /// <param name="end">end vertex</param>
        /// <returns>List of vertices which carves the path from start to end</returns>
        private static List<GraphVertex> GetShortestPathBetween2Vertex(GraphVertex start, GraphVertex end)
        {
            bool breakAllLoop = false;
            Queue<GraphVertex> queue = new Queue<GraphVertex>();
            start.IsVisited = true;
            distanceOfGraphVertex.Add(start, 0);
            parentOfVertex.Add(start, null);
            queue.Enqueue(start);

            while(queue.Count>0)
            {
                GraphVertex vertex = queue.Dequeue();
                foreach(GraphVertex neighbour in vertex.NeighbourVertices)
                {
                    if (!neighbour.IsVisited)
                    {
                        neighbour.IsVisited = true;
                        distanceOfGraphVertex[neighbour] = distanceOfGraphVertex[vertex] + 1;
                        parentOfVertex[neighbour] = vertex;
                        queue.Enqueue(neighbour);
                        if (neighbour == end)
                        {
                            breakAllLoop = true;
                            break;
                        }
                    }
                }
                if(breakAllLoop)
                {
                    break;
                }
            }

            if(!breakAllLoop)
            {
                // We did not find the end Vertex
                return null;
            }

            // Lets get the shortest path from start to end vertex
            GraphVertex currentVertex = end;
            List<GraphVertex> shortestPath = new List<GraphVertex>();
            while (currentVertex !=null )
            {
                shortestPath.Add(currentVertex);
                currentVertex = parentOfVertex[currentVertex];
            }

            shortestPath.Reverse();
            return shortestPath;
        }

        public static void TestGetShortestPathBetween2Vertex()
        {
            UndirectedGraph udg = GraphProbHelper.CreateUndirectedGraph();
            List<GraphVertex> shortestPath = GetShortestPathBetween2Vertex(udg.AllVertices[0], udg.AllVertices[5]);
            Console.WriteLine("The shortest path is as shown below:");
            foreach(GraphVertex vertex in shortestPath)
            {
                Console.Write(vertex.Data + " -> ");
            }
            Console.WriteLine();
        }
    }
}
