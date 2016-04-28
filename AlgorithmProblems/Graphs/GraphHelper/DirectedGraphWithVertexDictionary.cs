using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.GraphHelper
{
    public class DirectedGraphWithVertexDictionary
    {
        public Dictionary<int, GraphVertex> AllVertices { get; set; }

        public DirectedGraphWithVertexDictionary()
        {
            AllVertices = new Dictionary<int, GraphVertex>();
        }

        public void AddEdge(int start, int end)
        {
            if(!AllVertices.ContainsKey(start))
            {
                AllVertices[start] = new GraphVertex(start);
            }
            if (!AllVertices.ContainsKey(end))
            {
                AllVertices[end] = new GraphVertex(end);
            }
            AllVertices[start].NeighbourVertices.Add(AllVertices[end]);
        }

        public void AddEdge(GraphEdge edge)
        {
            AddEdge(edge.StartEdgeId, edge.EndEdgeId);
        }

        public void RemoveEdge(int start, int end)
        {
            if (AllVertices.ContainsKey(start))
            {
                AllVertices[start].NeighbourVertices.Remove(AllVertices[end]);
            }
        }
        public void MergeVertex(int vertex1, int vertex2)
        {
            if (!AllVertices.ContainsKey(vertex1))
            {
                AllVertices[vertex1] = new GraphVertex(vertex1);
            }
            if (!AllVertices.ContainsKey(vertex2))
            {
                AllVertices[vertex2] = new GraphVertex(vertex2);
            }
            AllVertices[vertex1].NeighbourVertices.AddRange(AllVertices[vertex2].NeighbourVertices);
        }
    }
}
