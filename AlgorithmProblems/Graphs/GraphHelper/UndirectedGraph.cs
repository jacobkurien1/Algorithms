using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.GraphHelper
{
    public class UndirectedGraph
    {
        public List<GraphVertex> AllVertices { get; set; } 

        public UndirectedGraph()
        {
            AllVertices = new List<GraphVertex>();
        }

        public void AddEdge(int start, int end)
        {
            if(AllVertices.Count> start && AllVertices.Count> end)
            {
                AllVertices[start].NeighbourVertices.Add(AllVertices[end]);
                AllVertices[end].NeighbourVertices.Add(AllVertices[start]);
            }
        }
    }
}
