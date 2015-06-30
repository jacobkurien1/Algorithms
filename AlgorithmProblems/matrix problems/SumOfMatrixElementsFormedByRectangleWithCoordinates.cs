using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.matrix_problems
{
    class SumOfMatrixElementsFormedByRectangleWithCoordinates
    {
        public int[,] PreProcessMatrix(int[,] mat)
        {
            int rowLength = mat.GetLength(0);
            int colLength = mat.GetLength(1);

            for (int r = 0; r < rowLength; r++ )
            {
                for(int c = 0; c < colLength; c++)
                {
                    int rowSum = 0;
                    int columnSum = 0;
                    int diagSum = 0;
                    if(r-1 >= 0)
                    {
                        columnSum = mat[r - 1, c];
                    }
                    if (c-1 >= 0)
                    {
                        rowSum = mat[r, c - 1];
                    }
                    if(r-1 >= 0 && c-1 >= 0)
                    {
                        diagSum = mat[r - 1, c - 1];
                    }
                    mat[r, c] += rowSum + columnSum - diagSum;
                }
            }

            return mat;
        }

        public int GetArea(int r1, int c1, int r2, int c2, int[,] mat)
        {
            int area = mat[r2, c2] - mat[r1 - 1, c2] - mat[r2, c1 - 1] + mat[r1 - 1, c1 - 1];
            return area;
        }

        public static void TestSumOfMatrixElements()
        {
            int[,] mat = MatrixProblemHelper.CreateMatrix(4, 5);
            Console.WriteLine("The actual matrix");
            MatrixProblemHelper.PrintMatrix(mat);

            Console.WriteLine("Sum of matrix elements in the rectangle");
            SumOfMatrixElementsFormedByRectangleWithCoordinates sumMat = new SumOfMatrixElementsFormedByRectangleWithCoordinates();
            mat = sumMat.PreProcessMatrix(mat);
            MatrixProblemHelper.PrintMatrix(mat);

            Console.WriteLine("the area is: " + sumMat.GetArea(2, 2, 3, 4, mat));
        }
    }
}
