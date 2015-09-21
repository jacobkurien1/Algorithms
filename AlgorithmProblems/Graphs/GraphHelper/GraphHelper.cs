using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.GraphHelper
{
    class GraphHelper
    {
        public static DirectedGraph CreateDirectedAcyclicGraph(int numberOfVertices, int offset=0)
        {
            DirectedGraph dg = new DirectedGraph();
            for(int i=offset; i<offset+numberOfVertices; i++)
            {
                dg.AllNodes.Add(new DirectedGraphVertex(i));
            }

            for (int i = 0; i < (numberOfVertices/2); i++)
            {
                dg.AllNodes[i].NeighbourVertices.Add(dg.AllNodes[i + 1]);
                dg.AllNodes[i].NeighbourVertices.Add(dg.AllNodes[i + 2]);

            }
            return dg;
        }

        public static void PrintDirectedGraphInAdjacencyMatrix(DirectedGraph dg)
        {
            int matLength = dg.AllNodes.Count;
            string[,] readableAdjMat = new string[matLength+1, matLength+1];
            for(int i = 1; i< matLength+1; i++)
            {
                readableAdjMat[i, 0] = dg.AllNodes[i-1].Data.ToString();
                readableAdjMat[0, i] = dg.AllNodes[i-1].Data.ToString();
            }
        }
    }
}
