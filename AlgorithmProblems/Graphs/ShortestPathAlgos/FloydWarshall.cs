using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.ShortestPathAlgos
{
    /// <summary>
    /// Given a directed graph and we want to calculate the shortest distance from any node i
    /// to another node j we should use this dynamic programming approach of floyd warshall algorithm.
    /// 
    /// Note: we could use this algorithm on a Directed graph with negative weighted edges but
    /// the graph should not have any negative cycles
    /// This algorithm will give the shortest distance between all pairs of vertices
    /// 
    /// We need to iterate from k = 0 -> n-1
    /// and mat[i,j] =  mat[i,k]+mat[k,j] if(mat[i,j] > mat[i,k]+mat[k,j])
    ///                 0 if i==j;
    ///                 
    /// The running time for this algorithm is O(V^3)
    ///
    /// We can also check whether there is a negative cycle present or not.
    /// 
    /// </summary>
    public class FloydWarshall
    {
        public int[,] Distance { get; set; }
        public int[,] Backtrack { get; set; }
        public FloydWarshall(int[,] mat)
        {
            if (mat == null || mat.GetLength(0) != mat.GetLength(1))
            {
                throw new ArgumentException("the adjacency matrix is not well formed");
            }
            int numberOfGraphVertex = mat.GetLength(0);
            // initializations
            Distance = mat;
            Backtrack = new int[numberOfGraphVertex, numberOfGraphVertex];
            for(int i = 0; i< numberOfGraphVertex; i++)
            {
                for(int j=0; j<numberOfGraphVertex; j++)
                {
                    if (mat[i, j] != int.MaxValue)
                    {
                        Backtrack[i, j] = i;
                    }
                    else
                    {
                        Backtrack[i, j] = -1;
                    }
                }
            }

            Run(mat);

        }
        private void Run(int[,] mat)
        {
            int numberOfGraphVertex = mat.GetLength(0);
            for(int k=0; k<numberOfGraphVertex; k++)
            {
                for(int i = 0; i<numberOfGraphVertex; i++)
                {
                    for(int j=0; j<numberOfGraphVertex; j++)
                    {
                        int newVal = int.MaxValue;
                        if (Distance[i, k] !=int.MaxValue && Distance[k, j]!= int.MaxValue)
                        {
                            newVal = Distance[i, k] + Distance[k, j];
                        }
                        if (newVal < Distance[i,j])
                        {
                            Distance[i, j] = newVal;
                            Backtrack[i, j] = k;
                        }
                    }
                }
            }
        }

        public List<int> PathFromUToV(int u, int v)
        {
            List<int> ret = new List<int>();
            while (v != u & v != -1)
            {
                ret.Add(v);
                v = Backtrack[u, v];
            }
            if (v != -1)
            {
                ret.Add(v);
            }
            ret.Reverse();
            return ret;
        }

        /// <summary>
        /// We can check whether there is a negative weight cycle present or not
        /// 
        /// We can do this by checking Distance[i,j] + Distance[j,i] <0 means we have a negative cycle
        /// </summary>
        /// <returns></returns>
        public bool IsNegativeCyclePresent()
        {
            int numberOfGraphVertex = Distance.GetLength(0);
            for (int i = 0; i < numberOfGraphVertex; i++)
            {
                for (int j = i+1; j < numberOfGraphVertex; j++)
                {
                    if(Distance[i,j] +Distance[j,i]<0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public static void TestFloydWarshall()
        {
            int inf = int.MaxValue;
            int[,] adjacencyMatrix = new int[,] { { 0, 3, 6, 15 }, { inf, 0, -2, inf }, { inf, inf, 0, 2 }, { 1, inf, inf, 0 } };
            FloydWarshall fw = new FloydWarshall(adjacencyMatrix);
            Console.WriteLine("Is there a negative cycle present: {0}, Expected: false",fw.IsNegativeCyclePresent());
            Console.WriteLine("The min distance from {0} to {1} is {2}", 3, 2, fw.Distance[3,2]);
            PrintPath(fw.PathFromUToV(3, 2));

            Console.WriteLine("The min distance from {0} to {1} is {2}", 2, 0, fw.Distance[2, 0]);
            PrintPath(fw.PathFromUToV(2, 0));

            // lets introduce a negative cycle
            adjacencyMatrix[2, 3] = -3;
            fw = new FloydWarshall(adjacencyMatrix);
            Console.WriteLine("Is there a negative cycle present: {0}, Expected: true", fw.IsNegativeCyclePresent());
        }

        private static void PrintPath(List<int> path)
        {
            foreach(int p in path)
            {
                Console.Write("{0} -> ", p);
            }
            Console.Write("*");
            Console.WriteLine();
        }
    }
}
