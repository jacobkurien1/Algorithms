using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.matrix_problems
{
    /// <summary>
    /// Print a matrix as a snake starting from (0,0) and once the row is printed
    /// start from (1,columnLength-1) and print element backwards till (1,0) and so on
    /// </summary>
    class PrintMatrixAsSnake
    {
        public static string GetMatAsSnake(int[,] mat)
        {
            StringBuilder sb = new StringBuilder();
            if (mat != null)
            {
                int j = 0;
                for (int i = 0; i < mat.GetLength(0); i++)
                {
                    while(j>=0 && j<mat.GetLength(1))
                    {
                        sb.Append(string.Format("{0} ", mat[i, j]));
                        j = (i % 2 == 0) ? j + 1 : j - 1;
                    }
                    j = (j < 0) ? 0 : mat.GetLength(1) - 1;
                }

            }
            return sb.ToString();
        }
        public static void TestPrintMatrixAsSnake()
        {
            int[,] mat = new int[,] { { 1, 2, 3 }, { 4, 5, 6, }, { 7, 8, 9 } };
            Console.WriteLine(GetMatAsSnake(mat));
        }
    }
}
