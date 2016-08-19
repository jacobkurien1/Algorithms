using AlgorithmProblems.Heaps;
using AlgorithmProblems.Heaps.HeapHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.ShortestPathAlgos
{
    /// <summary>
    /// Do the Heuristic path finding algorithm in a graph.
    /// This is also known as "Greedy best first search"
    /// </summary>
    class HeuristicSearch
    {
        /// <summary>
        /// Get the path using the Heuristic search
        /// Here we will use the manhattan distance as the heuristic and 
        /// the algorithm would optimize in the path with less manhattan distance from the endpoint
        /// 
        /// This is different than the breadth first search which expands in all direction, 
        /// here we travel the path which points to the direction where the endpoint is located
        /// 
        /// Heuristic seach is faster than the BFS in cases where we dont have many obstracles
        /// But as the number of obstracles increases this would become closer to BFS.
        /// 
        /// A* algorithm uses a combination of dijkstra and heuristic search
        /// The running time here is O(V + E)
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public List<GraphVertex> GetPath(int[,] mat, int stRow, int stCol, int endRow, int endCol)
        {
            // Create the graph
            GraphVertex[,] AllVerticesMat = CreateGraph(mat);
            GraphVertex endVertex = AllVerticesMat[endRow, endCol];
            GraphVertex startVertex = AllVerticesMat[stRow, stCol];
            MinHeap<GraphVertex> priorityQueue = new MinHeap<GraphVertex>(mat.GetLength(0) * mat.GetLength(1));
            Dictionary<GraphVertex, GraphVertex> backtract = new Dictionary<GraphVertex, GraphVertex>();
            backtract[startVertex] = null;
            startVertex.ManhattanDistance = ManhattanDistance(startVertex, endVertex);
            priorityQueue.Insert(startVertex);

            while(priorityQueue.HeapSize>0)
            {
                GraphVertex closestVertex = priorityQueue.ExtractMin();
                
                if(closestVertex == endVertex)
                {
                    // We have reached the endvertex
                    return BackTractToGetThePath(backtract, startVertex, endVertex);
                }

                foreach(GraphVertex neighbour in closestVertex.Neighbours)
                {
                    if(!backtract.ContainsKey(neighbour))
                    {
                        // this condition indicates that the node has not been visited yet, so add the node to backtrack dictionary
                        // to indicate that we have visited neighbour
                        backtract[neighbour] = closestVertex;

                        // calculate the manhattan distance and insert it into priority queue
                        neighbour.ManhattanDistance = ManhattanDistance(neighbour, endVertex);
                        priorityQueue.Insert(neighbour);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the path from the parent/backtrack dictionary
        /// </summary>
        /// <param name="backtract">backtrack or parent dictionary</param>
        /// <param name="start">start node</param>
        /// <param name="end">end node</param>
        /// <returns></returns>
        private List<GraphVertex> BackTractToGetThePath(Dictionary<GraphVertex, GraphVertex> backtract, GraphVertex start, GraphVertex end)
        {
            List<GraphVertex> path = new List<GraphVertex>();

            while(end!=null)
            {
                path.Add(end);
                end = backtract[end];
            }
            path.Reverse();
            return path;
        }

        /// <summary>
        /// Create a graph from the matrix
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
                        AllVerticesMat[r, c] = new GraphVertex(r, c);
                    }
                }
            }

            for (int r = 0; r < mat.GetLength(0); r++)
            {
                for (int c = 0; c < mat.GetLength(1); c++)
                {
                    // Add the edges
                    if (r + 1 < mat.GetLength(0) && AllVerticesMat[r + 1, c] != null && AllVerticesMat[r, c] != null)
                    {
                        AllVerticesMat[r, c].AddEdge(AllVerticesMat[r + 1, c]);
                    }
                    if (c + 1 < mat.GetLength(1) && AllVerticesMat[r, c + 1] != null && AllVerticesMat[r, c] != null)
                    {
                        AllVerticesMat[r, c].AddEdge(AllVerticesMat[r, c + 1]);
                    }

                }
            }

            return AllVerticesMat;
        }

        /// <summary>
        /// Calculate the manhattan distance between 2 points in the graph
        /// Manhattan distance is the sum of difference of distance along x and y axis
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private int ManhattanDistance(GraphVertex v1, GraphVertex v2)
        {
            return Math.Abs(v1.Row - v2.Row) + Math.Abs(v1.Column - v2.Column);
        }



        /// <summary>
        /// Represents the graph vertex
        /// </summary>
        internal class GraphVertex : IComparable
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public int ManhattanDistance { get; set; }
            public List<GraphVertex> Neighbours { get; set; }

            public GraphVertex(int row, int column)
            {
                Row = row;
                Column = column;
                Neighbours = new List<GraphVertex>();
                // initially the manhattan distance is set to infinity
                ManhattanDistance = int.MaxValue;
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

            public int CompareTo(object obj)
            {
                return ManhattanDistance.CompareTo(((GraphVertex)obj).ManhattanDistance);
            }

            public override string ToString()
            {
                return string.Format("{0}#{1}", Row, Column);
            }
        }

        #region TestArea
        public static void TestHeuristicSearch()
        {
            int[,] mat = new int[,]
            {
                { 1,1,1,1 },
                {1,0,0,0 },
                {1,0,1,1 },
                {1,1,1,1 }

            };
            HeuristicSearch hs = new HeuristicSearch();
            List<GraphVertex> path = hs.GetPath(mat, 0,0,3,3);
            PrintPath(path);

            mat = new int[,]
            {
                { 1,1,1,1 },
                {1,0,0,0 },
                {1,0,1,1 },
                {1,1,1,1 },
                {1,0,0,1 },
                {1,0,0,1 },
                {1,1,1,1 }

            };
            hs = new HeuristicSearch();
            path = hs.GetPath(mat, 0, 0, 3, 3);
            PrintPath(path);

            mat = new int[,]
            {
                {1,0,1,1,1 },
                {1,0,1,0,1 },
                {1,0,1,0,1 },
                {1,1,1,0,1 },
                {1,0,0,0,1 },
                {1,0,0,1,1 },
                {1,1,1,1,1 }

            };
            hs = new HeuristicSearch();
            path = hs.GetPath(mat, 0, 0, 4, 4);
            PrintPath(path);
        }

        private static void PrintPath(List<GraphVertex> path)
        {
            Console.WriteLine("The path is as shown below:");
            foreach (GraphVertex vertex in path)
            {
                Console.Write("{0} ->", vertex.ToString());
            }
            Console.WriteLine();
        }

        #endregion
    }
}
