using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.matrix_problems
{
    /// <summary>
    /// We are given a matrix where all the element to the right and bottom is greater than the current element
    /// We need to get all the matrix elements in a sorted array
    /// </summary>
    public class SortedArrayFromSortedMatrix
    {
        public int[] SortedArray { get; set; }
        private int SortedArrayIndex { get; set; }
        public SortedArrayFromSortedMatrix(int[,] mat)
        {
            // We can do a check on the matrix to make sure that all the elements to right and bottom are greater
            // i have not done that step here. it is assumed we are getting the sorted matrix
            SortedArray = new int[mat.GetLength(0)*mat.GetLength(1)];
            SortedArrayIndex = 0;
            GetSortedArrayFromSortedMatrix(mat, 0);
        }

        /// <summary>
        /// This is the divide and conquer routine which will be called which handles the merge of
        /// 1 row and 1 column whose intersection matrix element is mat[currentIndex,currentIndex]
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        private void GetSortedArrayFromSortedMatrix(int[,] mat, int currentIndex)
        {
            if(currentIndex == mat.GetLength(0)|| currentIndex == mat.GetLength(1))
            {
                // so when the currentIndex is hits the end of the row or column boundary
                // we must have considered all the elements in the matrix and hence
                // it is safe to return
                return;
            }
            int row1 = currentIndex+1;
            int col1 = currentIndex;
            int row2 = currentIndex;
            int col2 = currentIndex+1;
            SortedArray[SortedArrayIndex++] = mat[currentIndex, currentIndex];
            
            // the following code is similar to merge subroutine in the mergesort algo
            // We will be merging the row starting at mat[currentIndex, currentIndex] with
            // the column starting at mat[currentIndex, currentIndex]
            while(row1<mat.GetLength(0) && col2<mat.GetLength(1))
            {
                if(mat[row1,col1] < mat[row2,col2])
                {
                    SortedArray[SortedArrayIndex++] = mat[row1++, col1];
                }
                else
                {
                    SortedArray[SortedArrayIndex++] = mat[row2, col2++];
                }
            }
            while(row1<mat.GetLength(0))
            {
                SortedArray[SortedArrayIndex++] = mat[row1++, col1];
            }
            while(col2<mat.GetLength(1))
            {
                SortedArray[SortedArrayIndex++] = mat[row2, col2++];
            }
            GetSortedArrayFromSortedMatrix(mat, ++currentIndex);
        }
        public static void TestSortedArrayFromSortedMatrix()
        {
            int[,] mat = new int[,] { { 1, 2, 5 }, { 3, 6, 10 }, { 4, 20, 28 } };
            MatrixProblemHelper.PrintMatrix(mat);
            SortedArrayFromSortedMatrix sm = new SortedArrayFromSortedMatrix(mat);
            Console.WriteLine("the sorted array is as shown below:");
            ArrayHelper.PrintArray(sm.SortedArray);

            mat = new int[,] { { 1, 2, 5 }, { 3, 6, 10 }, { 4, 20, 28 } , { 4, 20, 28 } };
            MatrixProblemHelper.PrintMatrix(mat);
            sm = new SortedArrayFromSortedMatrix(mat);
            Console.WriteLine("the sorted array is as shown below:");
            ArrayHelper.PrintArray(sm.SortedArray);
        }
    }
}
