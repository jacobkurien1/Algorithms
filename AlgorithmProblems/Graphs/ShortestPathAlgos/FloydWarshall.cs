using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.ShortestPathAlgos
{
    /// <summary>
    /// Given a directed graph and we want to calculate the shortest distance from any node i
    /// to another node j we should use this dynamic programming approach of floyd warshall algorithm
    /// 
    /// We need to iterate from k = 0 -> n-1
    /// and mat[i,j] =  mat[i,k]+mat[k,j] if(mat[i,j] > mat[i,k]+mat[k,j])
    ///                 0 if i==j;
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

        public static void TestFloydWarshall()
        {
            int inf = int.MaxValue;
            int[,] adjacencyMatrix = new int[,] { { 0, 3, 6, 15 }, { inf, 0, -2, inf }, { inf, inf, 0, 2 }, { 1, inf, inf, 0 } };
            FloydWarshall fw = new FloydWarshall(adjacencyMatrix);
            Console.WriteLine("The min distance from {0} to {1} is {2}", 3, 2, fw.Distance[3,2]);
            PrintPath(fw.PathFromUToV(3, 2));

            Console.WriteLine("The min distance from {0} to {1} is {2}", 2, 0, fw.Distance[2, 0]);
            PrintPath(fw.PathFromUToV(2, 0));
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
