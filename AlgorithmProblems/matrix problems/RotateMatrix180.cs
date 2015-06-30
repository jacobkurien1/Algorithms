using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.matrix_problems
{
    class RotateMatrix180
    {
        public enum FlipDirection
        {
            Horizontal,
            Vertical
        }

        /// <summary>
        /// This function can flip the whole matrix horizontally or vertically
        /// </summary>
        /// <param name="mat">matrix that needs to be flipped</param>
        /// <param name="direction">direction in which the matrix needs to be flipped</param>
        /// <returns>returns the transformed matrix</returns>
        public int[,] FlipMatrix(int[,] mat, FlipDirection direction)
        {
            int outerloopLength = 0;
            int innerloopLength = 0;

            if (direction == FlipDirection.Horizontal)
            {
                outerloopLength = mat.GetLength(0); // row is the outerloop
                innerloopLength = mat.GetLength(1); // column is the innerloop
            }
            else // direction == FlipDirection.Vertical
            {
                outerloopLength = mat.GetLength(1); // column is the outerloop
                innerloopLength = mat.GetLength(0); // row is the innerloop
            }

            for (int i = 0; i < outerloopLength; i++ )
            {
                for (int j = 0; j < innerloopLength/2; j++)
                {
                    if (direction == FlipDirection.Horizontal)
                    {
                        // Swap the elements in the matrix
                        int temp = mat[i, j];
                        mat[i, j] = mat[i, innerloopLength -1 - j];
                        mat[i, innerloopLength -1 - j] = temp;
                    }
                    else // direction == FlipDirection.Vertical
                    {
                        // Swap the elements in the matrix
                        int temp = mat[j, i];
                        mat[j, i] = mat[innerloopLength -1 -j, i];
                        mat[innerloopLength -1 - j, i] = temp;
                    }
                }
            }
            return mat;
        }

        /// <summary>
        /// This function rotates the matrix in 180 degrees inplace
        /// </summary>
        /// <param name="mat">matrix that needs to be roatated</param>
        /// <returns>returns the transformed matrix</returns>
        public int[,] RotateMatrix180Degree(int[,] mat)
        {
            // Flip the matrix horizontally
            mat = FlipMatrix(mat, FlipDirection.Horizontal);
            // Flip the matrix vertically
            mat = FlipMatrix(mat, FlipDirection.Vertical);
            return mat;
        }

        public static void TestRotateMatrix180()
        {
            int[,] mat = MatrixProblemHelper.CreateMatrix(4,5);
            Console.WriteLine("The actual matrix");
            MatrixProblemHelper.PrintMatrix(mat);

            Console.WriteLine("Rotate matrix 180 degree");
            RotateMatrix180 rotateMat = new RotateMatrix180();
            rotateMat.RotateMatrix180Degree(mat);
            MatrixProblemHelper.PrintMatrix(mat);
        }
    }
}
