using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    /// <summary>
    /// Course scheduling using topological sorting 
    /// Topological sorting can be only done on directed acyclic graph
    /// 1. Get the sink node(node which does not have any edge going to another node).
    /// 2. Give it the highest order (#graph_nodes)
    /// 3. Delete that node and all the edges.
    /// 4. Find the other sink nodes and do steps 1-3
    /// </summary>
    class CourseScheduling
    {
        public CourseScheduling(DirectedGraph graph)
        {
            this.TopologicalOrderPerVertex = new Dictionary<DirectedGraphVertex, int>();
            this.Graph = graph;
            this.TopologicalOrder = graph.AllNodes.Count;
        }
        public DirectedGraph Graph { get; set; }
        private int TopologicalOrder { get; set; }
        private Dictionary<DirectedGraphVertex, int> TopologicalOrderPerVertex { get; set; }

        /// <summary>
        /// The main call which will make sure that all the elements are topologically ordered
        /// </summary>
        private void DFSTopologicalSort()
        {
            foreach(DirectedGraphVertex vertex in Graph.AllNodes)
            {
                if(!vertex.IsVisited)
                {
                    DFS(vertex);
                }
            }

            // Print the topologically sorted order
            PrintTopologicalOrder();
        }

        /// <summary>
        /// The actual DFS operation takes place in this method
        /// </summary>
        /// <param name="currentVertex">current vertex on which the DFS needs to be performed</param>
        private void DFS(DirectedGraphVertex currentVertex)
        {
            if(currentVertex == null || currentVertex.IsVisited)
            {
                return;
            }
            currentVertex.IsVisited = true;
            foreach(DirectedGraphVertex neighbour in currentVertex.NeighbourVertices)
            {
                if(!neighbour.IsVisited)
                {
                    DFS(neighbour);
                }
            }

            // This step will find the sink element and then put it in the dictionary
            this.TopologicalOrderPerVertex[currentVertex] = this.TopologicalOrder;
            this.TopologicalOrder--;

        }

        /// <summary>
        /// Prints all the vertices with their topological order
        /// </summary>
        private void PrintTopologicalOrder()
        {
            foreach (DirectedGraphVertex vertex in Graph.AllNodes)
            {
                Console.WriteLine("The topological order of vertex: {0} is {1}", vertex.Data, TopologicalOrderPerVertex[vertex]);
            }
        }

        #region Test methods
        public static void TestCourseScheduling()
        {
            // Create a graph


            //CourseScheduling cs = new CourseScheduling();
        }
        #endregion
    }
}
