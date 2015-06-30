using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.matrix_problems
{
    class Rotate_Matrix_90_degree
    {
        public int[,] RotateMatrix(int[,] mat)
        {
            // get the row length of the matrix
            int rowLength = mat.GetLength(0)-1;
            // get the column length of the matrix
            int colLength = mat.GetLength(1)-1;

            // We need to traverse the row of the matrix from 0 to rowLength/2, because 
            // for each row traversal we will traverse the outermost square with the row as 
            // one of the sides of the square
            for (int r =0; r <= (int)rowLength/2; r++)
            {
                // We will traverse the outermost square
                // top-left corner of the square will be (r,r)
                // top-right corner of the square will be (r,colLength-r)
                for (int c = r; c < colLength -r; c++)
                {
                    int temp = mat[r, c];
                    mat[r, c] = mat[rowLength - c, r];
                    mat[rowLength - c, r] = mat[rowLength - r, colLength - c];
                    mat[rowLength - r, colLength - c] = mat[c, colLength - r];
                    mat[c, colLength - r] = temp; 
                }
            }

            return mat;
        }

        public static void TestRotateMatrix()
        {
            Rotate_Matrix_90_degree rtMat = new Rotate_Matrix_90_degree();
            int[,] mat = MatrixProblemHelper.CreateMatrix(4, 4);
            MatrixProblemHelper.PrintMatrix(mat);
            Console.WriteLine();
            mat = rtMat.RotateMatrix(mat);
            MatrixProblemHelper.PrintMatrix(mat);
        }

        

    }
}
