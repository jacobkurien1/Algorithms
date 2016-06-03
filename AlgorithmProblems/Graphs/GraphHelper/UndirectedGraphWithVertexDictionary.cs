using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.GraphHelper
{
    public class UndirectedGraphWithVertexDictionary
    {
        public Dictionary<int, GraphVertex> AllVertices { get; set; }

        public UndirectedGraphWithVertexDictionary()
        {
            AllVertices = new Dictionary<int, GraphVertex>();
        }

        public void AddEdge(int start, int end)
        {
            if (!AllVertices.ContainsKey(start))
            {
                AllVertices[start] = new GraphVertex(start);
            }
            if (!AllVertices.ContainsKey(end))
            {
                AllVertices[end] = new GraphVertex(end);
            }
            AllVertices[start].NeighbourVertices.Add(AllVertices[end]);
            AllVertices[end].NeighbourVertices.Add(AllVertices[start]);
        }

        public void AddEdge(GraphEdge edge)
        {
            AddEdge(edge.StartEdgeId, edge.EndEdgeId);
        }

        public void RemoveEdge(int start, int end)
        {
            if (AllVertices.ContainsKey(start) && AllVertices.ContainsKey(end))
            {
                AllVertices[start].NeighbourVertices.Remove(AllVertices[end]);
                AllVertices[end].NeighbourVertices.Remove(AllVertices[start]);
            }
        }
    }
}
