using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    class CycleInDirectedGraph
    {
        /// <summary>
        /// We can find a cycle in a graph using the DFS
        /// We need to keep a dictionary of all the nodes visited in the current path of DFS and check 
        /// whether the new visited node is already present in that dictionary.
        /// If yes, we have a back edge/ cycle.
        /// </summary>
        /// <param name="graph">Directed graph</param>
        /// <returns>whether the directed graph has cycle or not</returns>
        private static bool IsCycleInDirectedGraph(DirectedGraph graph)
        {
            foreach(GraphVertex vertex in graph.AllVertices)
            {
                if(!vertex.IsVisited && DFS(vertex, new Dictionary<GraphVertex,bool>()))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool DFS(GraphVertex vertex, Dictionary<GraphVertex,bool> nodesInPath)
        {
            vertex.IsVisited = true;
            nodesInPath.Add(vertex, true);
            foreach(GraphVertex neighbour in vertex.NeighbourVertices)
            {
                if (nodesInPath.ContainsKey(neighbour) || (!neighbour.IsVisited && DFS(neighbour, nodesInPath)))
                {
                    return true;
                }
            }
            nodesInPath.Remove(vertex);
            return false;
        }

        public static void TestIsCycleInDirectedGraph()
        {
            DirectedGraph dg = GraphProbHelper.CreatedirectedGraphWithCycle();
            Console.WriteLine("Does this graph has cycle: {0}", IsCycleInDirectedGraph(dg));

            dg = GraphProbHelper.CreatedirectedGraphWithoutCycle();
            Console.WriteLine("Does this graph has cycle: {0}", IsCycleInDirectedGraph(dg));
        }
    }
}
