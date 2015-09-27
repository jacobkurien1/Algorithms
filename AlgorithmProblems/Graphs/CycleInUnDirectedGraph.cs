using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    class CycleInUnDirectedGraph
    {
        /// <summary>
        /// We will store all the parent of the traversal in this dictionary
        /// </summary>
        private static Dictionary<GraphVertex, GraphVertex> parentDictionary = new Dictionary<GraphVertex, GraphVertex>();
        /// <summary>
        /// Here we do DFS and as we progress when we hit a visited node that will be an
        /// indication of having a cycle in the graph.
        /// Note: this visited node should not be the parent of the current node.
        /// To track this we will need to store all the parent of the traversal in this dictionary
        /// 
        /// We dont need to keep a dictionary of all the nodes visited in the current path of DFS as we did in
        /// the algo to find cycle in the directed graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        private static bool IsCycleInUnDirectedGraph(UndirectedGraph graph)
        {
            foreach(GraphVertex vertex in graph.AllVertices)
            {
                if(!vertex.IsVisited && DFS(vertex))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool DFS(GraphVertex vertex)
        {
            vertex.IsVisited = true;
            foreach(GraphVertex neighbour in vertex.NeighbourVertices)
            {
                if(!neighbour.IsVisited)
                {
                    parentDictionary[neighbour] = vertex;
                    return DFS(neighbour);
                }
                else
                {
                    if (parentDictionary.ContainsKey(vertex) && parentDictionary[vertex] != neighbour)
                    {
                        // the neighbour node is not the parent of the current node (vertex)
                        // we have found a cycle here
                        return true;
                    }
                }
            }
            // we have not found any cycle in the undirected graph
            return false;
        }

        public static void TestIsCycleInUnDirectedGraph()
        {
            UndirectedGraph udg = GraphProbHelper.CreateUndirectedGraphWithoutCycle();
            Console.WriteLine("Is the cycle present in the undirected graph: {0}", IsCycleInUnDirectedGraph(udg));

            udg = GraphProbHelper.CreateUndirectedGraphWithCycle();
            Console.WriteLine("Is the cycle present in the undirected graph: {0}", IsCycleInUnDirectedGraph(udg));
        }
    }
}
