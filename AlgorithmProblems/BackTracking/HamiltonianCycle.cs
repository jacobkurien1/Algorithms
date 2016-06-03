using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.BackTracking
{
    /// <summary>
    /// Hamiltonian path is a path in the graph where all vertices is visted once.
    /// Hamiltonian cycle is the Hamiltonian path which comes back to the start index
    /// </summary>
    public class HamiltonianCycle
    {
        List<GraphVertex> HamiltonianCycleList;
        public HamiltonianCycle()
        {
            HamiltonianCycleList = new List<GraphVertex>();
        }

        public List<GraphVertex> GetHamiltonianCycle(UndirectedGraphWithVertexDictionary ug, GraphVertex start)
        {
            if(CalculateHamiltonianCycle(ug, start, start))
            {
                return HamiltonianCycleList;
            }
            return null;
        }

        /// <summary>
        /// We will use the backtracking here. We follow each path and start backtracking once we figure out it is
        /// not the hamiltonian path.
        /// </summary>
        /// <param name="ug"></param>
        /// <param name="currentVertex"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        private bool CalculateHamiltonianCycle(UndirectedGraphWithVertexDictionary ug, GraphVertex currentVertex, GraphVertex start)
        {
            if (currentVertex != null)
            {
                currentVertex.IsVisited = true;
                HamiltonianCycleList.Add(currentVertex);
                foreach (GraphVertex neighbour in currentVertex.NeighbourVertices)
                {
                    if ((!neighbour.IsVisited && CalculateHamiltonianCycle(ug, neighbour, start)) ||
                        (neighbour == start && HamiltonianCycleList.Count == ug.AllVertices.Count))
                    {
                        return true;
                    }
                }
                //back tracking happens here
                currentVertex.IsVisited = false;
                HamiltonianCycleList.Remove(currentVertex);
            }
            return false;
        }

        public static void TestHamiltonianCycle()
        {
            /*
            (1)-------(2)------(3)
             | \        \       |
             |  \        \      |
             |   \        \     |
             |    \        \    |
            (5)----------------(4)
             |                /
             |               /
            (6)--------------
            */
            UndirectedGraphWithVertexDictionary ug = new UndirectedGraphWithVertexDictionary();
            ug.AddEdge(1, 2);
            ug.AddEdge(2, 3);
            ug.AddEdge(3, 4);
            ug.AddEdge(2, 4);
            ug.AddEdge(1, 4);
            ug.AddEdge(4, 5);
            ug.AddEdge(4, 6);
            ug.AddEdge(5, 6);
            ug.AddEdge(5, 1);
            HamiltonianCycle hc = new HamiltonianCycle();
            Console.WriteLine("The hamiltonian cycle is as shown below:");
            PrintHamiltonianCycle(hc.GetHamiltonianCycle(ug, ug.AllVertices[1]));

            /*
            (1)-------(2)------(3)
             | \        \       |
             |  \        \      |
             |   \        \     |
             |    \        \    |
            (5)----------------(4)
             |                
             |               
            (6)
            */
            ug = new UndirectedGraphWithVertexDictionary();
            ug.AddEdge(1, 2);
            ug.AddEdge(2, 3);
            ug.AddEdge(3, 4);
            ug.AddEdge(2, 4);
            ug.AddEdge(1, 4);
            ug.AddEdge(4, 5);
            ug.AddEdge(5, 6);
            ug.AddEdge(5, 1);
            hc = new HamiltonianCycle();
            Console.WriteLine("The hamiltonian cycle is as shown below:");
            PrintHamiltonianCycle(hc.GetHamiltonianCycle(ug, ug.AllVertices[1]));
        }
        private static void PrintHamiltonianCycle(List<GraphVertex> cycle)
        {
            if (cycle != null)
            {
                foreach (GraphVertex gVertex in cycle)
                {
                    Console.Write("{0} -> ", gVertex.Data);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("the hamiltonian cycle is not possible here");
            }
        }
    }
}
