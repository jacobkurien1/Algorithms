using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.GraphHelper
{
    public class GraphEdge
    {
        public int StartEdgeId { get; set; }
        public int EndEdgeId { get; set; }
        public GraphEdge(int start, int end)
        {
            StartEdgeId = start;
            EndEdgeId = end;
        }
    }
}
