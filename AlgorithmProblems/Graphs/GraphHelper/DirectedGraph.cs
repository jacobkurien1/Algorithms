using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.GraphHelper
{
    public class DirectedGraph
    {
        public List<GraphVertex> AllVertices { get; set; }

        public DirectedGraph()
        {
            AllVertices = new List<GraphVertex>();
        }

        public void AddEdge(int start, int end)
        {
            if(AllVertices.Count> start && AllVertices.Count> end)
            {
                AllVertices[start].NeighbourVertices.Add(AllVertices[end]);
            }
        }

        public void RemoveEdge(int start, int end)
        {
            if (AllVertices.Count > start && AllVertices.Count > end)
            {
                AllVertices[start].NeighbourVertices.Remove(AllVertices[end]);
            }
        }
    }
}
