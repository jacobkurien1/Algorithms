using AlgorithmProblems.Heaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.ShortestPathAlgos.AStar
{
    /// <summary>
    /// Use the AStar Algo to get the shortest path in a graph
    /// Dijkstra’s Algorithm works well to find the shortest path, but it wastes time exploring in directions that aren’t promising. 
    /// Heuristic/Greedy Best First Search explores in promising directions but it may not find the shortest path. 
    /// The A* algorithm uses both the actual distance from the start and the estimated distance to the goal.
    /// 
    /// The algorithm will be same as Dijkstra. the extra step would be adding heruistic(node, end) to the priority in the priorityqueue.
    /// So that the first path that is checked is closer to/ in the direction of the end vertex
    /// 
    /// The running time is N+1=1+b^{*}+(b^{*})^(2)+ ... +(b^{*})^(d)
    /// here N is the number of nodes expanded
    /// b is the branching factor (average number of neighbours per node)
    /// d is the depth of the shortest path
    /// 
    /// Good Heuristic function will prune away many of the b^(d) nodes that dijkstra would expand
    /// 
    /// So Worst case running time is O(b^(d))
    /// 
    /// </summary>
    class AStar
    {
        public List<GraphVertex> GetPath(Graph graph, GraphVertex start, GraphVertex end)
        {
            if(graph == null || start == null || end == null || !graph.AllVertices.ContainsKey(start.Id) || !graph.AllVertices.ContainsKey(end.Id))
            {
                // error condition pertaining to the input arguments
                throw new ArgumentException();
            }

            MinHeapMap<GraphVertex> priorityQueue = new MinHeapMap<GraphVertex>(graph.AllVertices.Count);
            Dictionary<GraphVertex, GraphVertex> backtrack = new Dictionary<GraphVertex, GraphVertex>();
            Dictionary<GraphVertex, int> finalDistance = new Dictionary<GraphVertex, int>();
            // start node will have a distance of 0+manhattandistance
            start.CurrentDistance = ManhattanDistance(start, end);
            priorityQueue.Insert(start);
            backtrack[start] = null;

            while(priorityQueue.Count>0)
            {
                GraphVertex vertex = priorityQueue.ExtractMin();
                finalDistance[vertex] = vertex.CurrentDistance;
                if (vertex == end)
                { 
                    // return the path from start to end
                    return BackTractToGetThePath(backtrack, start, end);
                }
                foreach(KeyValuePair<GraphVertex, int> edge in vertex.NeighbourEdges)
                {
                    GraphVertex neighbour = edge.Key;
                    int edgeWeight = edge.Value;

                    if(!finalDistance.ContainsKey(neighbour))
                    {
                        // this node's minimum distance has not yet been calculated
                        if (neighbour.CurrentDistance > vertex.CurrentDistance + edgeWeight)
                        {
                            // A better distance has been found for neighbour
                            neighbour.CurrentDistance = vertex.CurrentDistance + edgeWeight;
                            neighbour.DistanceWithHeuristic = vertex.CurrentDistance + edgeWeight + ManhattanDistance(vertex, neighbour);
                            priorityQueue.AddOrChangePriority(neighbour);
                            backtrack[neighbour] = vertex;
                        }
                    }
                }
            }


            // we do not have a path from source to destination
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

            while (end != null)
            {
                path.Add(end);
                end = backtract[end];
            }
            path.Reverse();
            return path;
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
            return Math.Abs(v1.Xcoordinate - v2.Xcoordinate) + Math.Abs(v1.Ycoordinate - v2.Ycoordinate);
        }

        /// <summary>
        /// Represents the graph vertex
        /// </summary>
        internal class GraphVertex : IComparable, IKey
        {
            // the x and y coordinates will help us get the manhattan distance
            public int Xcoordinate { get; set; }
            public int Ycoordinate { get; set; }

            public int Id { get; set; }

            public int CurrentDistance { get; set; }
            /// <summary>
            /// This will be used as priority in the priority queue
            /// </summary>
            public int DistanceWithHeuristic { get; set; }

            public List<KeyValuePair<GraphVertex, int>> NeighbourEdges { get; set; }

            public GraphVertex(int id, int xcoordinate, int ycoordinate)
            {
                Id = id;
                Xcoordinate = xcoordinate;
                Ycoordinate = ycoordinate;
                NeighbourEdges = new List<KeyValuePair<GraphVertex, int>>();
                // initially the manhattan distance is set to infinity
                CurrentDistance = int.MaxValue;
            }

            public int CompareTo(object obj)
            {
                return DistanceWithHeuristic.CompareTo(((GraphVertex)obj).DistanceWithHeuristic);
            }

            public override string ToString()
            {
                return string.Format("{0}", Id);
            }
        }

        internal class Graph
        {
            public Graph()
            {
                AllVertices = new Dictionary<int, GraphVertex>();
            }
            public Dictionary<int, GraphVertex> AllVertices { get; set; }
            public void AddWeightedEdge(GraphVertex vertex1, GraphVertex vertex2, int weight)
            {
                // Add the vertex to dictionary if it is not already present
                if(!AllVertices.ContainsKey(vertex1.Id))
                {
                    AllVertices[vertex1.Id] = vertex1;
                }
                if (!AllVertices.ContainsKey(vertex2.Id))
                {
                    AllVertices[vertex2.Id] = vertex2;
                }

                // Now create a weighted edge
                vertex1.NeighbourEdges.Add(new KeyValuePair<GraphVertex, int> ( vertex2, weight ));
                vertex2.NeighbourEdges.Add(new KeyValuePair<GraphVertex, int> ( vertex1, weight ));
            }

            public void AddWeightedEdge(int vertex1Id, int vertex2Id, int weight)
            {
                if (!AllVertices.ContainsKey(vertex1Id) || !AllVertices.ContainsKey(vertex2Id))
                {
                    throw new ArgumentException("Vertex id is not present in the AllVertices Dictionary");
                }
                AddWeightedEdge(AllVertices[vertex1Id], AllVertices[vertex2Id], weight);
            }
        }
        #region TestArea
        public static void TestAStar()
        {
            /*
            (6)(0,2)-------------------------2-----------------------------(7)(2,2)
             |                                                              |
             |                                                              |
             |                                                              |
             |                                                              |
             2                                                              2
             |                                                              |
             |                                                              |
             |                                                              |
            (0)(0,1)------8------------(1)(1,1)-------------1--------------(2)(2,1)
             |                          \
             |                            \
             |                              \
             2                                1
             |                                 \ 
             |                                   \
             |                                     \
            (3)(0,0)-----3-----------(4)(1,0)---5----(5)(2,0)
            */
            Graph g = new Graph();
            g.AllVertices[3] = new GraphVertex(3, 0, 0);
            g.AllVertices[4] = new GraphVertex(4, 1, 0);
            g.AllVertices[5] = new GraphVertex(5, 2, 0);
            g.AllVertices[0] = new GraphVertex(0, 1, 0);
            g.AllVertices[1] = new GraphVertex(1, 1, 1);
            g.AllVertices[2] = new GraphVertex(2, 2, 1);
            g.AllVertices[6] = new GraphVertex(6, 0, 2);
            g.AllVertices[7] = new GraphVertex(7, 2, 2);

            g.AddWeightedEdge(3, 4, 3);
            g.AddWeightedEdge(5, 4, 5);
            g.AddWeightedEdge(1, 5, 1);
            g.AddWeightedEdge(0, 1, 8);
            g.AddWeightedEdge(1, 2, 1);
            g.AddWeightedEdge(3, 0, 2);
            g.AddWeightedEdge(2, 7, 2);
            g.AddWeightedEdge(6, 7, 2);
            g.AddWeightedEdge(0, 6, 2);
            AStar astar = new AStar();
            List<GraphVertex> path = astar.GetPath(g, g.AllVertices[0], g.AllVertices[5]);
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
