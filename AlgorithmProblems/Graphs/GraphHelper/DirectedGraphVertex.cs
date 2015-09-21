using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.GraphHelper
{
    class DirectedGraphVertex
    {
        public int Data { get; set; }
        public bool IsVisited { get; set; }
        public List<DirectedGraphVertex> NeighbourVertices { get; set; }

        public DirectedGraphVertex(int data)
        {
            this.Data = data;
        }
    }
}
