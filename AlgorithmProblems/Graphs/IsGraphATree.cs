using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    /// <summary>
    /// Check whether a graph is a tree or not
    /// </summary>
    class IsGraphATree
    {
        public IsGraphATree()
        {
            allVisitedVertices = new Dictionary<GraphVertex, bool>();
        }

        /// <summary>
        /// We will use this Dictionary to check whether all the vertices are connected
        /// </summary>
        Dictionary<GraphVertex, bool> allVisitedVertices;


        /// <summary>
        /// A graph is a tree if the following conditions are met
        /// 1. No cycles exist in the graph
        /// 2. All elements are connected
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public bool IsGraphTree(UndirectedGraph graph)
        {
            if (graph == null || graph.AllVertices.Count==0)
            {
                throw new ArgumentException("graph is null");
            }
            return !IsCyclePresent(graph.AllVertices[0], null) && IsAllVerticesConnected(graph);
        }

        /// <summary>
        /// Checks whether a cycle is present in the undirected graph using DFS
        /// </summary>
        /// <param name="currentNode">current vertex</param>
        /// <param name="parentNode">parent of current vertex</param>
        /// <returns>whether a cycle is present or not</returns>
        private bool IsCyclePresent(GraphVertex currentNode, GraphVertex parentNode)
        {
            currentNode.IsVisited = true;
            bool isCyclePresent = false;
            foreach (GraphVertex neighbour in currentNode.NeighbourVertices)
            {
                if (neighbour.IsVisited && neighbour == parentNode)
                {
                    // this is not a cycle check the other neighbours
                    continue;
                }
                else if (!neighbour.IsVisited)
                {
                    isCyclePresent |= IsCyclePresent(neighbour, currentNode);
                }
                else
                {
                    // We have a cycle in the graph
                    return true;
                }
            }
            return isCyclePresent;
        }

        /// <summary>
        /// Check whether all vertices in the graph is connected
        /// </summary>
        /// <param name="graph"></param>
        /// <returns>true if all vertices in the graph is connected</returns>
        private bool IsAllVerticesConnected(UndirectedGraph graph)
            {
            foreach(GraphVertex vertex in graph.AllVertices)
            {
                if(!vertex.IsVisited)
                {
                    return false;
                }
            }
            return true;
        }
        public static void TestIsGraphATree()
        {
            IsGraphATree gt = new IsGraphATree();

            UndirectedGraph udg = GraphProbHelper.CreateUndirectedGraphWithoutCycleWithoutUnconnectedNodes();
            Console.WriteLine("Is the cycle present in the undirected graph: {0}", gt.IsGraphTree(udg));

            udg = GraphProbHelper.CreateUndirectedGraphWithoutCycle();
            Console.WriteLine("Is the cycle present in the undirected graph: {0}", gt.IsGraphTree(udg));

            udg = GraphProbHelper.CreateUndirectedGraphWithCycle();
            Console.WriteLine("Is the cycle present in the undirected graph: {0}", gt.IsGraphTree(udg));

        }
    }
}
