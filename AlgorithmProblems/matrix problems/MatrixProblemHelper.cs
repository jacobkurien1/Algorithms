using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.matrix_problems
{
    class MatrixProblemHelper
    {
        public static void PrintMatrix(int[,] mat)
        {
            for (int r = 0; r < mat.GetLength(0); r++)
            {
                for (int c = 0; c < mat.GetLength(1); c++)
                {
                    Console.Write(mat[r, c] + " ");
                }
                Console.WriteLine();
            }
        }

        public static int[,] CreateMatrix(int rowlength, int collength)
        {
            Random rnd = new Random();
            int[,] mat = new int[rowlength, collength];
            for (int r = 0; r < rowlength; r++)
            {
                for (int c = 0; c < collength; c++)
                {
                    mat[r, c] = rnd.Next(0, 100);
                }
            }
            return mat;
        }
    }
}
