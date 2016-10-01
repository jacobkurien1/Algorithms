using AlgorithmProblems.Arrays.ArraysHelper;
using AlgorithmProblems.Heaps.HeapHelper;
using AlgorithmProblems.matrix_problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Distributed_Algorithms
{
    /// <summary>
    /// You are given k sorted arrays and you need to merge them into one sorted array.
    /// Usually the k sorted arrays are in different computers and they gets merged in one computer and are written back to a cluster.
    /// </summary>
    class KWayMerge
    {
        public int[] DoKWayMerge(int[,] mat)
        {
            int[] sortedArr = new int[mat.GetLength(0) * mat.GetLength(1)];
            int sortedArrIndex = 0;
            int k = mat.GetLength(0);

            // initialization of the priority queue
            PriorityQueue<Cell> pq = new PriorityQueue<Cell>(k);
            for(int i=0;i< k; i++)
            {
                pq.Insert(new Cell(i, 0), mat[i, 0]);
            }

            while(pq.Count>0)
            {
                Cell minCell = pq.ExtractMin();
                sortedArr[sortedArrIndex++] = mat[minCell.Row, minCell.Col];
                if(minCell.Col +1 < mat.GetLength(1))
                {
                    pq.Insert(new Cell(minCell.Row, minCell.Col + 1), mat[minCell.Row, minCell.Col + 1]);
                }
            }
            return sortedArr;
        }

        /// <summary>
        /// Represents a cell location in the matrix
        /// In a distributed system, row represents computerId
        /// and column represents the index of the element in the file present in computerId
        /// </summary>
        public class Cell
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public Cell(int row, int col)
            {
                Row = row;
                Col = col;
            }
            public override string ToString()
            {
                return string.Format("({0}, {1})", Row, Col);
            }
        }
        public static void TestKWayMerge()
        {
            KWayMerge merge = new KWayMerge();
            int[,] mat = new int[,]
            {
                {2,4,6,7,90 },
                {3,4,66,77,88 },
                {55,65,88,90,101 },
                {1,2,3,4,5 }
            };
            Console.WriteLine("the input matrix is as shown below");
            MatrixProblemHelper.PrintMatrix(mat);
            Console.WriteLine("The sorted array is as shown below");
            ArrayHelper.PrintArray(merge.DoKWayMerge(mat));
        }
    }
}
