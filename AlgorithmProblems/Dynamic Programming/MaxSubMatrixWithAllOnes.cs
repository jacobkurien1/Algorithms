using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    public class MaxSubMatrixWithAllOnes
    {
        public class MaxtrixOutline
        {
            public int RightmostRow { get; set; }
            public int RightmostCol { get; set; }
            public int SizeOfMatrix { get; set; }
            public MaxtrixOutline(int row, int col, int size)
            {
                RightmostRow = row;
                RightmostCol = col;
                SizeOfMatrix = size;
            }
        }

        /// <summary>
        /// We can solve this problem using dynamic programming
        /// we need to create a new matrix maxSubMatrix
        /// 
        /// the dynamic programming equation is as follows:
        /// intialize the maxSubMatrix[0,j] and maxSubMatrix[i,0] to mat[0,j] and mat[i,0] respectively
        /// maxSubMatrix[i,j] = min{maxSubMatrix[i-1,j], maxSubMatrix[i,j-1], maxSubMatrix[i-1,j-1]} +1 if mat[i,j] ==1
        ///                     0 if mat[i,j] == 0
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public MaxtrixOutline GetMaxMatrix(int[,] mat)
        {
            int[,] maxSubMatrix = new int[mat.GetLength(0), mat.GetLength(1)];

            // initialization
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                maxSubMatrix[i, 0] = mat[i, 0];
            }
            for (int j = 0; j < mat.GetLength(1); j++)
            {
                maxSubMatrix[0, j] = mat[0, j];
            }
            int maxMatrixSize = 0;
            MaxtrixOutline matOutline = null;

            for (int i = 1; i < mat.GetLength(0); i++)
            {
                for (int j = 1; j < mat.GetLength(1); j++)
                {
                    if (mat[i, j] == 1)
                    {
                        maxSubMatrix[i, j] = Math.Min(Math.Min(maxSubMatrix[i - 1, j], maxSubMatrix[i, j - 1]), maxSubMatrix[i - 1, j - 1]) + 1;
                        if(maxMatrixSize < maxSubMatrix[i, j])
                        {
                            // this is the rightmost corner of the matrix with maxSize
                            matOutline = new MaxtrixOutline(i, j, maxSubMatrix[i, j]);
                            maxMatrixSize = maxSubMatrix[i, j];
                        }
                    }
                    else
                    {
                        maxSubMatrix[i, j] = 0;
                    }
                }
            }
            return matOutline;
        }

        public static void TestMaxSubMatrixWithAllOnes()
        {
            MaxSubMatrixWithAllOnes maxSubMat = new MaxSubMatrixWithAllOnes();
            int[,] mat = new int[,] { { 0, 1, 1, 0, 1 }, { 1, 1, 0, 1, 0 }, { 0, 1, 1, 1, 0 }, { 1, 1, 1, 1, 0 }, { 1, 1, 1, 1, 1 }, { 0, 0, 0, 0, 0 } };
            MaxtrixOutline matOutline = maxSubMat.GetMaxMatrix(mat);
            Console.WriteLine("The matrix outline of the max matrix is {0},{1} and the size is {2}", matOutline.RightmostRow, matOutline.RightmostCol, matOutline.SizeOfMatrix);

            mat = new int[,] { { 1,1,1,1,1 }, { 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 } };
            matOutline = maxSubMat.GetMaxMatrix(mat);
            Console.WriteLine("The matrix outline of the max matrix is {0},{1} and the size is {2}", matOutline.RightmostRow, matOutline.RightmostCol, matOutline.SizeOfMatrix);
        }
    }
}
