using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    /// <summary>
    /// Color the vertices in a graph with 2 different colors such that no 2 connected vertices have the same color
    /// </summary>
    class ColorVertices
    {

        private static Dictionary<GraphVertex, int> vertexColorDict = new Dictionary<GraphVertex, int>();
        /// <summary>
        /// Color the vertices in a graph with 2 different colors such that no 2 connected vertices have the same color
        /// 
        /// We do a BFS here and color all odd layer node with the same color 
        /// if you encounter any node in the odd layer with the color of the even layer then return false
        /// </summary>
        /// <returns></returns>
        private static bool ColorVerticesWithDifferentColor(GraphVertex vertex)
        {
            Queue<GraphVertex> queueForBFS = new Queue<GraphVertex>();
            queueForBFS.Enqueue(vertex);
            vertexColorDict[vertex] = 0;
            while(queueForBFS.Count>0)
            {
                GraphVertex currentVertex = queueForBFS.Dequeue();
                foreach (GraphVertex neighbour in vertex.NeighbourVertices)
                {
                    if (vertexColorDict.ContainsKey(neighbour) && vertexColorDict[neighbour] == vertexColorDict[currentVertex])
                    {
                        // This is a visited node
                        // We have hit the case where we cannot meet the condition that 2 vertices of the edge has different color
                        return false;
                    }
                    else if (!vertexColorDict.ContainsKey(neighbour))
                    {
                        // This is an unvisited node
                        vertexColorDict[neighbour] = (vertexColorDict[currentVertex] + 1) % 2;
                        queueForBFS.Enqueue(neighbour);
                    }
                }
            }
            return true;
        }

        public static void TestColorVerticesWithDifferentColor()
        {
            DirectedGraph dg = GraphProbHelper.CreateDirectedGraph();
            Console.WriteLine("Can the graph nodes be colored: {0}", ColorVerticesWithDifferentColor(dg.AllVertices[2]));

            dg.RemoveEdge(0, 1);
            Console.WriteLine("Can the graph nodes be colored: {0}", ColorVerticesWithDifferentColor(dg.AllVertices[2]));
        }
    }
}
