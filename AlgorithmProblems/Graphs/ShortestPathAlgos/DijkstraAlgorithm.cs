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
    /// Given a directed/undirected graphs with weighted edges and 
    /// we want to find the lowest distance from one source vertex to all the other vertices
    /// We can only have positive weights on the edges
    /// 
    /// This is a greedy algorithm
    /// 
    /// We will use a data structure known as heap-map which will make the task easy
    /// and reduce the running time of the algorithm to O(Elog(V)) where E is the 
    /// number of edges and V is the number of vertices
    /// </summary>
    public class DijkstraAlgorithm
    {
        public MinHeapMap<GraphVertexWithDistance> DistanceHeapMap { get; set; }

        /// <summary>
        /// This dictionary will contain all the final distance of every other vertex from the 
        /// source vertex. This can also be used to figure out which vertices have 
        /// their distance from source vertex finalized.
        /// </summary>
        public Dictionary<int, int> FinalDistance { get; set; }

        public Dictionary<int, int> ParentBacktrack { get; set; }

        public List<int> GetShortestPath(List<GraphVertex> allVertices, GraphVertex source, GraphVertex destination)
        {
            List<GraphVertex> shortestPath = new List<GraphVertex>();
            DistanceHeapMap = new MinHeapMap<GraphVertexWithDistance>(allVertices.Count);
            ParentBacktrack = new Dictionary<int, int>();
            FinalDistance = new Dictionary<int, int>();

            foreach (GraphVertex gv in allVertices)
            {
                if (gv == source)
                {
                    DistanceHeapMap.Insert(new GraphVertexWithDistance(gv, 0));
                }
                else
                {
                    DistanceHeapMap.Insert(new GraphVertexWithDistance(gv));
                }
            }

            while (DistanceHeapMap.Count != 0)
            {
                
                GraphVertexWithDistance currentVertex = DistanceHeapMap.ExtractMin();
                FinalDistance[currentVertex.Id] = currentVertex.Distance;
                
                if (currentVertex.Id == destination.Id)
                {
                    // we have reached the destination
                    return BacktrackToGetPath(currentVertex.Id, 0);
                }
                foreach (KeyValuePair<GraphVertex, int> edge in currentVertex.NeighbouringEdgesWithWeight)
                {
                    if (DistanceHeapMap.AllEntities.ContainsKey(edge.Key.Id))
                    {
                        GraphVertexWithDistance endVertex = DistanceHeapMap.AllEntities[edge.Key.Id];
                        if (endVertex.Distance > currentVertex.Distance + edge.Value)
                        {
                            GraphVertexWithDistance gvClone = endVertex.ShallowClone();
                            gvClone.Distance = currentVertex.Distance + edge.Value;
                            ParentBacktrack[endVertex.Id] = currentVertex.Id;
                            DistanceHeapMap.ChangePriority(endVertex, gvClone);
                        }
                    }
                }
            }
            // we do not have a path from source to destination
            return null;
        }

        private List<int> BacktrackToGetPath(int finalVertexId, int startVertexId)
        {
            List<int> ret = new List<int>();
            while(ParentBacktrack.ContainsKey(finalVertexId))
            {
                ret.Add(finalVertexId);
                finalVertexId = ParentBacktrack[finalVertexId];
            }
            ret.Add(startVertexId);
            ret.Reverse();
            return ret;
        }


        public static void TestDijkstraAlgorithm()
        {
            Graph g = new Graph(false);
            g.AddEdge(0, 1, 5);
            g.AddEdge(0, 3, 9);
            g.AddEdge(0, 4, 2);
            g.AddEdge(1, 2, 2);
            g.AddEdge(4, 5, 3);
            g.AddEdge(3, 5, 2);
            g.AddEdge(2, 3, 3);
            DijkstraAlgorithm da = new DijkstraAlgorithm();
            List<int> shortestPath = da.GetShortestPath(g.AllVertices.Values.ToList(), g.AllVertices[0], g.AllVertices[3]);
            Console.WriteLine("The shortest distance from {0} to {1} is {2}", 0, 3, da.FinalDistance[3]);
            PrintPath(shortestPath);

            shortestPath = da.GetShortestPath(g.AllVertices.Values.ToList(), g.AllVertices[0], g.AllVertices[2]);
            Console.WriteLine("The shortest distance from {0} to {1} is {2}", 0, 2, da.FinalDistance[2]);
            PrintPath(shortestPath);

        }

        private static void PrintPath(List<int> path)
        {
            foreach(int g in path)
            {
                Console.Write("{0} -> ", g);
            }
            Console.WriteLine();
        }
    }

    public class GraphVertexWithDistance : GraphVertex, IComparable
    {
        public GraphVertexWithDistance(GraphVertex gv, int distance = int.MaxValue) : base(gv.Id,gv.NeighbouringEdgesWithWeight)
        {
            Distance = distance;
        }
        public GraphVertexWithDistance(int distance, int id, List<KeyValuePair<GraphVertex, int>> neighbouringEdgesWithWeight):base(id, neighbouringEdgesWithWeight)
        {
            Distance = distance;
        }
        public int Distance { get; set; }

        public int CompareTo(object obj)
        {
            return Distance.CompareTo(((GraphVertexWithDistance)obj).Distance);
        }

        public GraphVertexWithDistance ShallowClone()
        {
            return new GraphVertexWithDistance(Distance, Id, NeighbouringEdgesWithWeight);
        }

        public GraphVertex ToGraphVertex()
        {
            return this;
        }
    }

    public class GraphVertex : IKey
    {
        public int Id { get; set; }
        public List<KeyValuePair<GraphVertex, int>> NeighbouringEdgesWithWeight { get; set; }
        public GraphVertex(int id, List<KeyValuePair<GraphVertex, int>> neighbouringEdgesWithWeight)
        {
            Id = id;
            NeighbouringEdgesWithWeight = neighbouringEdgesWithWeight;
        }

        public GraphVertex()
        {
            
        }
        
        public GraphVertex(int id)
        {
            Id = id;
            NeighbouringEdgesWithWeight = new List<KeyValuePair<GraphVertex, int>>();
        }
    }

    public class Graph
    {
        public Dictionary<int, GraphVertex> AllVertices { get; set; }
        public bool IsDirected { get; set; }
        public Graph(bool isDirected)
        {
            IsDirected = isDirected;
            AllVertices = new Dictionary<int, GraphVertex>();
        }
        public void AddEdge(int startVertexId, int endVertexId, int weight)
        {
            if(!AllVertices.ContainsKey(startVertexId))
            {
                // if the graphvertex is not present then we need to add it
                AllVertices[startVertexId] = new GraphVertex(startVertexId);
            }
            
            if (!AllVertices.ContainsKey(endVertexId))
            {
                // if the graphvertex is not present then we need to add it
                AllVertices[endVertexId] = new GraphVertex(endVertexId);
            }

            AllVertices[startVertexId].NeighbouringEdgesWithWeight.Add(new KeyValuePair<GraphVertex, int>(AllVertices[endVertexId], weight));
            if (!IsDirected)
            {
                AllVertices[endVertexId].NeighbouringEdgesWithWeight.Add(new KeyValuePair<GraphVertex, int>(AllVertices[startVertexId], weight));
            }
        }
    }
}
