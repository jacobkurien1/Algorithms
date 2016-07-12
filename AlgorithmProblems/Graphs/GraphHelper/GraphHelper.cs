using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.GraphHelper
{
    class GraphProbHelper
    {
        public static DirectedGraph CreateDirectedAcyclicGraph(int numberOfVertices, int offset=0)
        {
            DirectedGraph dg = new DirectedGraph();
            for(int i=offset; i<offset+numberOfVertices; i++)
            {
                dg.AllVertices.Add(new GraphVertex(i));
            }

            for (int i = 0; i < (numberOfVertices/2); i++)
            {
                dg.AllVertices[i].NeighbourVertices.Add(dg.AllVertices[i + 1]);
                dg.AllVertices[i].NeighbourVertices.Add(dg.AllVertices[i + 2]);

            }
            return dg;
        }

        public static DirectedGraph CreateDirectedGraph()
        {
            DirectedGraph dg = new DirectedGraph();
            for (int i = 0; i < 4; i++)
            {
                dg.AllVertices.Add(new GraphVertex(i));
            }

            dg.AddEdge(2, 0);
            dg.AddEdge(0, 2);
            dg.AddEdge(2, 1);
            dg.AddEdge(0, 1);
            dg.AddEdge(0, 3);
            dg.AddEdge(1, 3);

            return dg;
        }

        public static UndirectedGraph CreateUndirectedGraph()
        {
            UndirectedGraph udg = new UndirectedGraph();
            for (int i = 0; i < 6; i++)
            {
                udg.AllVertices.Add(new GraphVertex(i));
            }

            udg.AddEdge(0, 1);
            udg.AddEdge(1, 2);
            udg.AddEdge(0, 2);
            udg.AddEdge(0, 3);
            udg.AddEdge(2, 3);
            udg.AddEdge(3, 4);
            udg.AddEdge(4, 5);
            udg.AddEdge(2, 5);

            return udg;
        }

        public static DirectedGraph CreatedirectedGraphWithCycle()
        {
            DirectedGraph dg = new DirectedGraph();
            for (int i = 0; i < 6; i++)
            {
                dg.AllVertices.Add(new GraphVertex(i));
            }

            dg.AddEdge(0, 1);
            dg.AddEdge(1, 2);
            dg.AddEdge(0, 2);
            dg.AddEdge(3, 0);
            dg.AddEdge(4, 3);
            dg.AddEdge(5, 4);
            dg.AddEdge(2, 5);

            return dg;
        }

        public static DirectedGraph CreatedirectedGraphWithoutCycle()
        {
            DirectedGraph dg = new DirectedGraph();
            for (int i = 0; i < 6; i++)
            {
                dg.AllVertices.Add(new GraphVertex(i));
            }

            dg.AddEdge(0, 1);
            dg.AddEdge(1, 2);
            dg.AddEdge(0, 2);
            dg.AddEdge(0, 3);
            dg.AddEdge(4, 3);
            dg.AddEdge(5, 4);
            dg.AddEdge(2, 5);

            return dg;
        }

        public static UndirectedGraph CreateUndirectedGraphWithoutCycle()
        {
            UndirectedGraph udg = new UndirectedGraph();
            for (int i = 0; i < 6; i++)
            {
                udg.AllVertices.Add(new GraphVertex(i));
            }

            udg.AddEdge(0, 1);
            udg.AddEdge(1, 2);
            udg.AddEdge(3, 2);
            udg.AddEdge(2, 4);

            return udg;
        }

        public static UndirectedGraph CreateUndirectedGraphWithoutCycleWithoutUnconnectedNodes()
        {
            UndirectedGraph udg = new UndirectedGraph();
            for (int i = 0; i < 6; i++)
            {
                udg.AllVertices.Add(new GraphVertex(i));
            }

            udg.AddEdge(0, 1);
            udg.AddEdge(1, 2);
            udg.AddEdge(3, 2);
            udg.AddEdge(2, 4);
            udg.AddEdge(2, 5);

            return udg;
        }

        internal static UndirectedGraph CreateUndirectedGraphWithCycle()
        {
            UndirectedGraph udg = new UndirectedGraph();
            for (int i = 0; i < 6; i++)
            {
                udg.AllVertices.Add(new GraphVertex(i));
            }

            udg.AddEdge(0, 1);
            udg.AddEdge(1, 2);
            udg.AddEdge(3, 2);
            udg.AddEdge(2, 4);
            udg.AddEdge(0, 3); // this is the back edge required for cycle

            return udg;
        }

        public static void PrintDirectedGraphInAdjacencyMatrix(DirectedGraph dg)
        {
            int matLength = dg.AllVertices.Count;
            int[,] readableAdjMat = new int[matLength + 1, matLength + 1];
            foreach (GraphVertex vertex in dg.AllVertices)
            {
                if (vertex.NeighbourVertices != null)
                {
                    foreach (GraphVertex neighbour in vertex.NeighbourVertices)
                    {
                        readableAdjMat[vertex.Data, neighbour.Data] = 1;
                    }
                }
            }

            for(int i = 0; i<=matLength; i++)
            {
                for(int j = 0; j<=matLength; j++)
                {
                    Console.Write("{0} ", readableAdjMat[i, j]);
                }
                Console.WriteLine();
            }
        }
        
    }
}
