using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.ShortestPathAlgos
{
    /// <summary>
    /// Bellman ford is also a single source shortest path algorithm like dijkstras
    /// But unlike dijkstra, we can have negative edges and also this algorithm will find negative cycles in the graph
    /// 
    /// This algorithm is less efficient than Dijkstras algo
    /// 
    /// The logic behind bellman ford algorithm: A shortest path can have atmost V-1 edges
    /// At Kth iteration all the shortest path using K or less edges will be calculated
    /// After V-1 iteration all the distance must be final unless a negative cycle exists in the graph
    /// 
    /// The running time for single source is O(VE). Note: In worst case E == V^2
    /// 
    /// </summary>
    public class BellmanFord
    {
        public Dictionary<int,int> FinalDistance { get; set; }
        public Dictionary<int,int> ParentBacktrack { get; set; }

        public List<int> GetShortestPath(CustomGraph graph, int sourceIndex, int destinationIndex)
        {
            FinalDistance = new Dictionary<int, int>();
            ParentBacktrack = new Dictionary<int, int>();

            foreach(int vertexIndex in graph.AllVertexIndices.Keys)
            {
                FinalDistance[vertexIndex] = int.MaxValue;
            }
            FinalDistance[sourceIndex] = 0;
            ParentBacktrack[sourceIndex] = -1;// we can use this value are termination condition while backtracking to find the path

            for (int i = 0; i < graph.AllVertexIndices.Count - 1; i++) // We need to do V-1 iterations
            {
                foreach (GraphEdge edge in graph.AllEdges)
                {
                    int currentDistance = (FinalDistance[edge.StartVertexId] != int.MaxValue) ? FinalDistance[edge.StartVertexId] + edge.Weight : int.MaxValue;
                    if (FinalDistance[edge.EndVertexId]> currentDistance)
                    {
                        FinalDistance[edge.EndVertexId] = currentDistance;
                        ParentBacktrack[edge.EndVertexId] = edge.StartVertexId;
                    }
                }
            }

            // Detect Negative cycle: we can detect the negative cycle in the graph by doing the above operations one more time and make sure we dont get
            // a lesser value for the final distance for any other vertex
            foreach (GraphEdge edge in graph.AllEdges)
            {
                int currentDistance = (FinalDistance[edge.StartVertexId] != int.MaxValue) ? FinalDistance[edge.StartVertexId] + edge.Weight : int.MaxValue;
                if (FinalDistance[edge.EndVertexId] > currentDistance)
                {
                    throw new Exception("Negative cycle detected");
                }
            }

            if(FinalDistance[destinationIndex] != int.MaxValue)
            {
                return BackTrackToGetPath(destinationIndex);
            }
            else
            {
                // we dont have a path from source to destination
                return null;
            }
        }

        private List<int> BackTrackToGetPath(int destinationId)
        {
            List<int> ret = new List<int>();
            while(destinationId != -1)
            {
                ret.Add(destinationId);
                destinationId = ParentBacktrack[destinationId];
            }
            ret.Reverse();
            return ret;
        }

        public static void TestBellmanFord()
        {
            CustomGraph graph = new CustomGraph();
            graph.AddEdge(3, 4, 2);
            graph.AddEdge(4, 3, 1);
            graph.AddEdge(2, 4, 4);
            graph.AddEdge(0,2,5);
            graph.AddEdge(1,2,-3);
            graph.AddEdge(0,3,8);
            graph.AddEdge(0,1,4);
            BellmanFord bf = new BellmanFord();
            List<int> shortestPath = bf.GetShortestPath(graph, 0,2);
            Console.WriteLine("The shortest distance from {0} to {1} is {2}", 0, 2, bf.FinalDistance[2]);
            PrintPath(shortestPath);

            shortestPath = bf.GetShortestPath(graph, 0, 4);
            Console.WriteLine("The shortest distance from {0} to {1} is {2}", 0, 4, bf.FinalDistance[4]);
            PrintPath(shortestPath);

            shortestPath = bf.GetShortestPath(graph, 0, 3);
            Console.WriteLine("The shortest distance from {0} to {1} is {2}", 0, 3, bf.FinalDistance[3]);
            PrintPath(shortestPath);

            Console.WriteLine("Adding negative cycle to the graph");
            graph.AddEdge(2, 0, -2);
            try
            {
                shortestPath = bf.GetShortestPath(graph, 0, 2);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
        private static void PrintPath(List<int> path)
        {
            foreach (int g in path)
            {
                Console.Write("{0} -> ", g);
            }
            Console.WriteLine();
        }
    }

    public class CustomGraph
    {
        public List<GraphEdge> AllEdges { get; set; }
        public Dictionary<int, bool> AllVertexIndices { get; set; }
        public CustomGraph()
        {
            AllEdges = new List<GraphEdge>();
            AllVertexIndices = new Dictionary<int, bool>();
        }
        public void AddEdge(int startVertexId, int endVertexId, int weight)
        {
            if(!AllVertexIndices.ContainsKey(startVertexId))
            {
                AllVertexIndices[startVertexId] = true;
            }
            if (!AllVertexIndices.ContainsKey(endVertexId))
            {
                AllVertexIndices[endVertexId] = true;
            }
            AllEdges.Add(new GraphEdge(startVertexId, endVertexId, weight));

        }
    }

    public class GraphEdge
    {
        public GraphEdge(int startVertexId, int endVertexId, int weight)
        {
            StartVertexId = startVertexId;
            EndVertexId = endVertexId;
            Weight = weight;
        }
        public int StartVertexId { get; set; }
        public int EndVertexId { get; set; }
        public int Weight { get; set; }
    }
}
