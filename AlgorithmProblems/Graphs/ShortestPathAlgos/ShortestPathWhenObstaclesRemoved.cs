using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.ShortestPathAlgos.ShortestPathWhenObstaclesRemoved
{
    /// <summary>
    /// Given a 2-D grid with obstacles, find the shortest path if we can remove atmost K obstacles.
    /// Note: We will consider each point in an obstacle as individual obstracles.
    /// </summary>
    class ShortestPathWhenObstaclesRemoved
    {

        /// <summary>
        /// We can use BFS to get the shortest path and keep track of the obstacles reached for each vertex in a 
        /// dictionary with key as the vertex and list of keyvalue pair as value.
        /// The key value pair has the parent vertex and the value is the number of obstacles already crossed to reach the current vertex.
        /// The list of key value pair has the shortest path parent vertex in acending order and number of obstacles in decending order
        /// for eg if the list at vertex X is like [(A,4),(B,2),(C,1),(D,0)]
        /// So the path to X from A will be the shortest with 4 obstacles and 
        /// B will be greater than path from A with obstacles 2.
        /// 
        /// As we reach the path where the number of obstacles is greater than the maxVal we need to abandon those paths
        /// 
        /// When we backtrack we need to get only the key value pair from each list which is one less than the current number of the obstacles
        /// if the current vertex is an obstacle and get the same number of obstacle if the current vertex is not an obstacle.
        /// 
        /// The running time is O(E*E)
        /// Cause the the number of times the keyvalue pair list is executed is O(E)
        /// and the number of times the outer for loop is executed is also O(E)
        /// </summary>
        /// <param name="mat">2-D matrix</param>
        /// <param name="maxNumOfObs">maximum number of obstacle that can be ignored</param>
        /// <returns></returns>
        public List<GraphVertex> GetPath(int[,] mat, int maxNumOfObs, int stRow, int stCol, int endRow, int endCol)
        {
            // Create the graph
            GraphVertex[,] AllVerticesMat = CreateGraph(mat);
            GraphVertex endVertex = AllVerticesMat[endRow, endCol];
            GraphVertex startVertex = AllVerticesMat[stRow, stCol];
            Dictionary<GraphVertex, List<KeyValuePair<GraphVertex, int>>> obstaclesAndBackTrack = new Dictionary<GraphVertex, List<KeyValuePair<GraphVertex, int>>>();
            obstaclesAndBackTrack[startVertex] = new List<KeyValuePair<GraphVertex, int>>() { new KeyValuePair<GraphVertex, int>( null, 0 ) };
            Queue<GraphVertex> queue = new Queue<GraphVertex>();
            queue.Enqueue(startVertex);

            while(queue.Count>0)
            {
                GraphVertex vertex = queue.Dequeue();
                if(vertex == endVertex)
                {
                    // return the path
                    return BackTrackToGetThePath(obstaclesAndBackTrack, startVertex, endVertex);
                }
                foreach (GraphVertex neighbour in vertex.Neighbours)
                {
                    foreach (KeyValuePair<GraphVertex, int> kvPair in obstaclesAndBackTrack[vertex])
                    {
                        int obsFromVertex = kvPair.Value;

                        int currentObs = (neighbour.IsObstacle) ? 1 : 0;
                        if (obsFromVertex + currentObs <= maxNumOfObs)
                        {
                            // dont pursue the path where the obstacles are more than the maxNumOfObs
                            if (!obstaclesAndBackTrack.ContainsKey(neighbour))
                            {
                                obstaclesAndBackTrack[neighbour] = new List<KeyValuePair<GraphVertex, int>>() { new KeyValuePair<GraphVertex, int>(vertex, obsFromVertex + currentObs) };
                                queue.Enqueue(neighbour);
                            }
                            else if (obstaclesAndBackTrack[neighbour][obstaclesAndBackTrack[neighbour].Count - 1].Value > obsFromVertex + currentObs)
                            {
                                obstaclesAndBackTrack[neighbour].Add(new KeyValuePair<GraphVertex, int>(vertex, obsFromVertex + currentObs));
                            }
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Backtrack obstaclesAndBackTrack dictionary to find the path
        /// </summary>
        /// <param name="obstaclesAndBackTrack"></param>
        /// <param name="startVertex"></param>
        /// <param name="endVertex"></param>
        /// <returns></returns>
        private List<GraphVertex> BackTrackToGetThePath(Dictionary<GraphVertex, List<KeyValuePair<GraphVertex, int>>> obstaclesAndBackTrack, GraphVertex startVertex, GraphVertex endVertex)
        {
            List<GraphVertex> path = new List<GraphVertex>();
            path.Add(endVertex);
            if (obstaclesAndBackTrack.ContainsKey(endVertex))
            {
                // This check is needed to make sure that there is a path. if there is no path we will return an empty list
                KeyValuePair<GraphVertex, int> currentPair = obstaclesAndBackTrack[endVertex][0];
                int previousObstacleVal = currentPair.Value;
                while (!currentPair.Equals(default(KeyValuePair<GraphVertex, int>)))
                {
                    path.Add(currentPair.Key);
                    KeyValuePair<GraphVertex, int>  nextPair = default(KeyValuePair<GraphVertex, int>);
                    if (currentPair.Key != null)
                    {
                        foreach (KeyValuePair<GraphVertex, int> pathPair in obstaclesAndBackTrack[currentPair.Key])
                        {
                            if (pathPair.Value == previousObstacleVal)
                            {
                                nextPair = pathPair;
                                break;
                            }
                        }
                        if (currentPair.Key.IsObstacle)
                        {
                            previousObstacleVal--;
                        }
                    }
                    currentPair = nextPair;
                    
                }
                path.Reverse();
            }
            return path;
        }

        /// <summary>
        /// Create a graph from the matrix
        /// We need to make all the matrix cells as graph vertex
        /// if the matrix location is having a value of 0 indicating that it is an obstacle then
        /// set the IsObstacle bit in the graph vertex to true else keep it false
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        private GraphVertex[,] CreateGraph(int[,] mat)
        {
            // Create the graph
            GraphVertex[,] AllVerticesMat = new GraphVertex[mat.GetLength(0), mat.GetLength(1)];

            for (int r = 0; r < mat.GetLength(0); r++)
            {
                for (int c = 0; c < mat.GetLength(1); c++)
                {
                    if (mat[r, c] == 1)
                    {
                        // Add new graph vertex
                        AllVerticesMat[r, c] = new GraphVertex(string.Format("{0}#{1}", r, c), false);
                    }
                    else
                    {
                        // Add an obstacle vertex
                        AllVerticesMat[r, c] = new GraphVertex(string.Format("{0}#{1}", r, c), true);
                    }
                }
            }

            for (int r = 0; r < mat.GetLength(0); r++)
            {
                for (int c = 0; c < mat.GetLength(1); c++)
                {
                    
                    // Add the  South edges
                    if (r + 1 < mat.GetLength(0))
                    {
                        AllVerticesMat[r, c].AddEdge(AllVerticesMat[r + 1, c]);
                    }
                    //Add the East edges
                    if (c + 1 < mat.GetLength(1))
                    {
                        AllVerticesMat[r, c].AddEdge(AllVerticesMat[r, c + 1]);
                    }
                    //Add the South-East edges
                    if(c+1 <mat.GetLength(1) && r+1< mat.GetLength(0))
                    {
                        AllVerticesMat[r, c].AddEdge(AllVerticesMat[r + 1, c + 1]);
                    }
                    //Add the North-East edges
                    if (c + 1 < mat.GetLength(1) && r -1 >=0)
                    {
                        AllVerticesMat[r, c].AddEdge(AllVerticesMat[r -1, c + 1]);
                    }
                }
            }

            return AllVerticesMat;
        }

        /// <summary>
        /// Represents the graph vertex
        /// </summary>
        internal class GraphVertex
        {
            public string Id { get; set; }
            public bool IsObstacle { get; set; }
            public List<GraphVertex> Neighbours { get; set; }

            public GraphVertex(string id, bool isObstacle)
            {
                Id = id;
                IsObstacle = isObstacle;
                Neighbours = new List<GraphVertex>();
            }

            /// <summary>
            /// Add an undirected edge
            /// </summary>
            /// <param name="neighbour"></param>
            public void AddEdge(GraphVertex neighbour)
            {
                Neighbours.Add(neighbour);
                neighbour.Neighbours.Add(this);
            }

            public override string ToString()
            {
                return string.Format("{0}", Id);
            }
        }

        #region TestArea
        public static void TestShortestPathWhenObstaclesRemoved()
        {
            int[,] mat = new int[,]
            {
                {0,0,0,0,0,1 },
                {0,0,0,0,0,0 },
                {0,0,0,0,0,0 },
                {1,1,0,1,1,1 },
                {1,0,1,1,1,1 },
                {1,1,1,1,1,1 }

            };
            ShortestPathWhenObstaclesRemoved hs = new ShortestPathWhenObstaclesRemoved();
            List<GraphVertex> path = hs.GetPath(mat, 2, 5, 0, 0, 5);
            PrintPath(path);

            path = hs.GetPath(mat, 3, 5, 0, 0, 5);
            PrintPath(path);

            path = hs.GetPath(mat, 1, 5, 0, 0, 5);
            PrintPath(path);

            mat = new int[,]
            {
                {1,1,1,1 },
                {1,0,0,0 },
                {1,0,1,0 },
                {1,1,1,1 }

            };
            hs = new ShortestPathWhenObstaclesRemoved();
            path = hs.GetPath(mat, 1, 3, 3, 0, 3);
            PrintPath(path);
        }

        private static void PrintPath(List<GraphVertex> path)
        {
            Console.WriteLine("The path is as shown below:");
            if (path == null)
            {
                Console.WriteLine("The path is empty");
            }
            else
            {
                foreach (GraphVertex vertex in path)
                {
                    if (vertex != null)
                    {
                        Console.Write("{0} ->", vertex.ToString());
                    }
                }
                Console.WriteLine();
            }
        }
        #endregion
    }
}
