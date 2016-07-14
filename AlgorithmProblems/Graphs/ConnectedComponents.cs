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
            if (dg.AllVertices.Count == 0)
            {
                // error condition
                throw new ArgumentException("The graph is empty");
            }
            List<List<int>> allSCC = new List<List<int>>();

            // 1. Initialize all vertices' isvisited as false
            foreach (GraphVertex vertex in dg.AllVertices)
            {
                vertex.IsVisited = false;
            }

            // 2.Do DFS on an arbitarily selected vertex v.And push the vertex into a stack once all its neighbours are visited
            Stack<GraphVertex> st = new Stack<GraphVertex>();
            foreach (GraphVertex vertex in dg.AllVertices)
            {
                if(!vertex.IsVisited)
                {
                    DFS(vertex, st);
                }
            }

            // 3. Initialize all vertices' isvisited as false
            foreach (GraphVertex vertex in dg.AllVertices)
            {
                vertex.IsVisited = false;
            }

            // 4. Reverse all the edges of the graph
            dg = dg.ReverseDirectedGraph();

            // 5. Pop vertex from stack and do DFS if the vertex is not visited. All the vertices reached by DFS will for one SCC
            while(st.Count>0)
            {
                GraphVertex vertex = st.Pop();
                if (!vertex.IsVisited)
                {
                    List<int> oneSCC = new List<int>();
                    DFS(vertex, oneSCC);
                    allSCC.Add(oneSCC);
                }

            }
            return allSCC;
        }

        /// <summary>
        /// This is the DFS subroutine
        /// </summary>
        /// <param name="vertex"></param>
        private void DFS(GraphVertex vertex, Stack<GraphVertex> st)
        {
            if (vertex != null)
            {
                vertex.IsVisited = true;
                if (vertex.NeighbourVertices != null)
                {
                    foreach (GraphVertex neighbour in vertex.NeighbourVertices)
                    {
                        if (!neighbour.IsVisited)
                        {
                            DFS(neighbour, st);
                        }
                    }
                }
                st.Push(vertex);
            }
        }

        private void DFS(GraphVertex vertex, List<int> scc)
        {
            if (vertex != null)
            {
                vertex.IsVisited = true;
                scc.Add(vertex.Data);
                if (vertex.NeighbourVertices != null)
                {
                    foreach (GraphVertex neighbour in vertex.NeighbourVertices)
                    {
                        if (!neighbour.IsVisited)
                        {
                            DFS(neighbour, scc);
                        }
                    }
                }
            }
        }

        public static void TestConnectedComponents()
        {
            DirectedGraph dg = new DirectedGraph();
            for(int i=0; i<=7; i++)
            {
                dg.AllVertices.Add(new GraphVertex(i));
            }
            dg.AddEdge(1, 0);
            dg.AddEdge(0, 2);
            dg.AddEdge(2, 1);
            dg.AddEdge(0, 5);
            dg.AddEdge(5, 6);
            dg.AddEdge(6, 7);
            dg.AddEdge(7, 5);
            ConnectedComponents cc = new ConnectedComponents();
            List<List<int>> allSCC = cc.GetStronglyConnectedComponents(dg);
            Console.WriteLine("The SCC are as shown below:");
            PrintSCC(allSCC);
        }

        private static void PrintSCC(List<List<int>> allSCC)
        {
            int sccCount = 1;
            
            foreach (List<int> oneSCC in allSCC)
            {
                Console.Write("{0} : ", sccCount);
                foreach (int graphVertex in oneSCC)
                {
                    Console.Write("{0}, ", graphVertex);
                }
                Console.WriteLine();
                sccCount++;
            }

        }

    }
}
