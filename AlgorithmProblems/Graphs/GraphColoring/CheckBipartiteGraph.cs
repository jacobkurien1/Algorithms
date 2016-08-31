using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.GraphColoring
{
    /// <summary>
    /// Check whether a given graph is Bipartiate.
    /// A bipartiate graph is the one whose vertices can be divided into 2 disjoint sets, such that 
    /// each edge is starts from one set and ends up in the other set.
    /// </summary>
    class CheckBipartiteGraph
    {
        /// <summary>
        /// A graph is bi partite if the graph can be colored by 2 colors.
        /// This is similar to the approach in the ColorVertices.cs file.
        /// 
        /// We will assign a color to source and do BFS on the graph.
        /// the next level will be colored via the second color and so on.
        /// While coloring a neighbour if we encounter a node which is colored with the same color as the vertex,
        /// then we cannot color the whole graph using 2 colors and hence the graph is not BiPartite.
        /// 
        /// 
        /// Instead of color we can just add the graph vertex to the dictionary as the key and the set number as the value.
        /// 
        /// The running time is O(V+E)
        /// </summary>
        /// <returns></returns>
        public static bool IsBiPartiteGraph(GraphVertex source)
        {
            Queue<GraphVertex> queue = new Queue<GraphVertex>();
            queue.Enqueue(source);
            // We can also use this dictionary to get which vertices are visited
            Dictionary<GraphVertex, int> vertexSetMap = new Dictionary<GraphVertex, int>();
            vertexSetMap[source] = 0;

            while(queue.Count>0)
            {
                GraphVertex vertex = queue.Dequeue();
                foreach(GraphVertex neighbour in vertex.NeighbourVertices)
                {
                    if(!vertexSetMap.ContainsKey(neighbour))
                    {
                        queue.Enqueue(neighbour);
                        vertexSetMap[neighbour] = vertexSetMap[vertex] + 1 % 2;
                    }
                    else
                    {
                        // We have already visted this vertex, make sure that the color is correct
                        if(vertexSetMap[neighbour] == vertexSetMap[vertex])
                        {
                            // 2 coloring is not possible
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public static void TestCheckBipartiteGraph()
        {
            DirectedGraph dg = GraphProbHelper.CreateDirectedGraph();
            Console.WriteLine("Can the graph nodes be colored: {0}", IsBiPartiteGraph(dg.AllVertices[2]));

            dg.RemoveEdge(0, 1);
            Console.WriteLine("Can the graph nodes be colored: {0}", IsBiPartiteGraph(dg.AllVertices[2]));
        }
    }
}
