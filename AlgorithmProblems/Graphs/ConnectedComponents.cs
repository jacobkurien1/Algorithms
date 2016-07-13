using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmProblems.Graphs.GraphHelper;

namespace AlgorithmProblems.Graphs
{
    /// <summary>
    /// Find all the strongly connected components of a graph.
    /// Strongly connected graph means any the vertex of the graph will have a path to go to any other vertex of the graph
    /// </summary>
    class ConnectedComponents
    {
        /// <summary>
        /// Gets the SCC of a directed graph.
        /// For an undirected graph this process is trivial. You have to do DFS on all vertices of the graph and connected components 
        /// will be found in one pass of the DFS algorithm
        /// 
        /// For a directed graph we will use Kosaraju's algorithm.
        /// 1. Initialize all vertices' isvisited as false
        /// 2. Do DFS on an arbitarily selected vertex v. And push the vertex into a stack once all its neighbours are visited
        /// 3. Initialize all vertices' isvisited as false
        /// 4. Reverse all the edges of the graph
        /// 5. Pop vertex from graph and do DFS if the vertex is not visited. All the vertices reached by DFS will for one SCC
        /// Keep doing 5 till the stack is empty
        /// 
        /// The intiution is for a graph with 2 SCC as shown below
        /// (A,B,C) ---> (E,F,G)
        /// where (A,B,C) and (E,F,G) are the 2 SCC and ---> is the connection b/w the 2 SCC
        /// then atleast one of the element in (A,B,C) will finish later than all the elements in (E,F,G).
        /// So when we enter elements in the stack we are basically ordering them based on the finishing times.
        /// Elements at top of stack finished later than the lower elements. And hence they will be processed first after transposing the graph.
        /// So the reveresed graph will be (A,B,C) <--- (E,F,G)
        /// and (A,B,C) will be processed first and will be represented as 1st SCC
        /// and then (E,F,G) will be processed second and will be represented as 2nd SCC
        /// 
        /// </summary>
        /// <param name="dg"></param>
        /// <returns></returns>
        public List<List<int>> GetStronglyConnectedComponents(DirectedGraph dg)
        {

        }

        public static void TestConnectedComponents()
        {
        }
    }
}
