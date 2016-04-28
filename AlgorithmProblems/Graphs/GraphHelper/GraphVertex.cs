using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.GraphHelper
{
    public class GraphVertex
    {
        public int Data { get; set; }
        public bool IsVisited { get; set; }
        public List<GraphVertex> NeighbourVertices { get; set; }

        public GraphVertex(int data)
        {
            this.Data = data;
            NeighbourVertices = new List<GraphVertex>();
        }
    }
}
