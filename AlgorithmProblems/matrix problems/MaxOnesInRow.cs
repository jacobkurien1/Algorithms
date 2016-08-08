using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.matrix_problems
{
    /// <summary>
    /// Given a matrix which is sorted by rows. The matrix has 0's and 1's.
    /// Find the row which has maximum number of 1's
    /// </summary>
    class MaxOnesInRow
    {
        /// <summary>
        /// Since the rows are sorted we can do a binary search on each row to get the first 1.
        /// This approach will have a running time of O(row.log(col))
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static int GetRowWithMaxOnes(int[,] mat)
        {
            int maxOnes = -1;
            int maxOnesRow = -1;
            for(int row = 0; row<mat.GetLength(0); row++)
            {
                int currentOneIndex = BinarySearch(mat, row, 0, mat.GetLength(1)-1);
                if (currentOneIndex != -1 && mat.GetLength(1)- currentOneIndex> maxOnes)
                {
                    maxOnes = mat.GetLength(0) - currentOneIndex;
                    maxOnesRow = row;
                }
            }

            return maxOnesRow;
        }

        /// <summary>
        /// The above algorithm can be optimized by only searching till the index till which the previous 1 index has been found and is max
        /// This optimization will increase the average running time but the worst case running time remains the same.
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static int GetRowWithMaxOnesOptimized(int[,] mat)
        {
            int maxOnes = -1;
            int maxOnesRow = -1;
            int maxOneIndex = -1;
            for (int row = 0; row < mat.GetLength(0); row++)
            {
                // the optimization is that we dont need to go till the end in a row. We know the max 1's seen till that row
                // and we just need to make sure that the new row will have more number of 1's than previously seen.
                int endIndex = (maxOneIndex != -1) ? maxOneIndex : mat.GetLength(1)-1;
                int currentOneIndex = BinarySearch(mat, row, 0, endIndex);
                if (currentOneIndex != -1 && mat.GetLength(1) - currentOneIndex > maxOnes)
                {
                    maxOneIndex = currentOneIndex;
                    maxOnes = mat.GetLength(0) - currentOneIndex;
                    maxOnesRow = row;
                }
            }

            return maxOnesRow;
        }

        /// <summary>
        /// This is the binary search subroutine.
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="row"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public static int BinarySearch(int[,] mat, int row, int startIndex, int endIndex)
        {
            if(startIndex <= endIndex)
            {
                int midIndex = endIndex - ((endIndex - startIndex) / 2);
                if ((midIndex == 0 || mat[row, midIndex-1] == 0) && mat[row, midIndex] == 1)
                {
                    // this will get the first element which matches 1
                    return midIndex;
                }
                else if (mat[row, midIndex]<1)
                {
                    return BinarySearch(mat, row, midIndex + 1, endIndex);
                }
                else
                {
                    // its not the first occurance of 1
                    return BinarySearch(mat, row, startIndex, midIndex-1);
                }
            }
            return -1;
        }

        public static void TestMaxOnesInRow()
        {
            int[,] mat = new int[,]
            {
                { 0,1,1,1},
                {0,0,0,0 },
                {1,1,1,1 },
                {0,0,1,1 }
            };
            Console.WriteLine("the row with max 1's is {0}", GetRowWithMaxOnes(mat));
            Console.WriteLine("the row with max 1's from optimized algo is {0}", GetRowWithMaxOnes(mat));
        }
    }
}
