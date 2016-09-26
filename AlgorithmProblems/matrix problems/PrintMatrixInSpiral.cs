using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.matrix_problems
{
    /// <summary>
    /// Given a matrix print all the elements in a spiral order
    /// </summary>
    class PrintMatrixInSpiral
    {
        /// <summary>
        /// Keep pointers to the topRow, bottomRow, leftCol and rightCol.
        /// And keep updating them after printing the row/column
        /// 
        /// The running time of this algo is O(rowLength*colLength)
        /// </summary>
        /// <param name="mat"></param>
        public static void Print(int[,] mat)
        {
            int leftCol = 0;
            int topRow = 0;
            int rightCol = mat.GetLength(1) - 1;
            int bottomRow = mat.GetLength(0) - 1;

            while(leftCol <= rightCol && topRow <= bottomRow)
            {
                // print toprow
                for(int i = leftCol; i<=rightCol; i++)
                {
                    Console.Write("{0} ", mat[topRow, i]);
                }
                topRow++;

                //print right column
                for (int i = topRow; i <= bottomRow; i++)
                {
                    Console.Write("{0} ", mat[i, rightCol]);
                }
                rightCol--;

                if (topRow <= bottomRow)
                {
                    // print bottom row
                    for (int i = rightCol; i >= leftCol; i--)
                    {
                        Console.Write("{0} ", mat[bottomRow, i]);
                    }
                    bottomRow--;
                }

                if (leftCol <= rightCol)
                {
                    // print left column
                    for (int i = bottomRow; i >= topRow; i--)
                    {
                        Console.Write("{0} ", mat[i, leftCol]);
                    }
                    leftCol++;
                }
            }
            Console.WriteLine();
        }
        public static void TestPrintMatrixInSpiral()
        {
            int[,] mat = MatrixProblemHelper.CreateMatrix(4, 4);
            MatrixProblemHelper.PrintMatrix(mat);

            Console.WriteLine("The matrix spirally is as shown below:");
            Print(mat);

            mat = MatrixProblemHelper.CreateMatrix(3, 2);
            MatrixProblemHelper.PrintMatrix(mat);

            Console.WriteLine("The matrix spirally is as shown below:");
            Print(mat);

            mat = MatrixProblemHelper.CreateMatrix(2,3);
            MatrixProblemHelper.PrintMatrix(mat);

            Console.WriteLine("The matrix spirally is as shown below:");
            Print(mat);

            mat = MatrixProblemHelper.CreateMatrix(3, 3);
            MatrixProblemHelper.PrintMatrix(mat);


            Console.WriteLine("The matrix spirally is as shown below:");
            Print(mat);

            mat = MatrixProblemHelper.CreateMatrix(3, 4);
            MatrixProblemHelper.PrintMatrix(mat);


            Console.WriteLine("The matrix spirally is as shown below:");
            Print(mat);
        }
    }
}
