using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.matrix_problems
{
    class Matrix_Column_Rows_0
    {
        // Algorithm 1: O(m+n) space
        public static int[,] MakeRowColZero1(int[,] mat)
        {
            // Step 1:
            // Find all the rows and columns that needs to be made zero
            // and store it in a Dictionary/Hashset
            HashSet<int> rowsAffected = new HashSet<int>();
            HashSet<int> colsAffected = new HashSet<int>();
            for (int r = 0; r < mat.GetLength(0); r++ )
            {
                for (int c = 0; c < mat.GetLength(1); c++)
                {
                    if(mat[r,c] == 0)
                    {
                        rowsAffected.Add(r);
                        colsAffected.Add(c);
                    }
                }
            }

            // Step 2:
            // Make all the affected rows 0
            foreach (int r in rowsAffected)
            {
                for (int c = 0; c < mat.GetLength(1); c++)
                {
                    mat[r, c] = 0;
                }
            }

            // Make all affected columns 0
            foreach(int c in colsAffected)
            {
                for (int r = 0; r < mat.GetLength(0); r++ )
                {
                    mat[r, c] = 0;
                }
            }

            return mat;
        }

        // Algorithm 2: O(1) space
        public static int[,] MakeRowColZero2(int[,] mat)
        {
            bool isFirstRowZero = false;
            // Make the first element in row and column 0
            // if the element is having a 0 value
            for (int r = 0; r < mat.GetLength(0); r++)
            {
                for (int c = 0; c < mat.GetLength(1); c++)
                {
                    if (mat[r, c] == 0)
                    {
                        if(r==0)    // Check for the first row
                        {
                            isFirstRowZero = true;
                        }
                        else
                        {
                            mat[0, c] = 0;
                            mat[r, 0] = 0;
                        }
                    }
                }
            }

            // Make the matrix rows and column elements 0 if the 
            // first element in the row or column is zero
            for (int r = 1; r < mat.GetLength(0); r++)
            {
                for (int c = 1; c < mat.GetLength(1); c++)
                {
                    if (mat[r, 0] == 0 || mat[0, c] == 0)
                    {
                        mat[r, c] = 0;
                    }
                }
            }

            // Make the first column elements 0 if mat[0,0] = 0
            if(mat[0,0] == 0)
            {
                for (int r = 1; r < mat.GetLength(0); r++)
                {
                    mat[r, 0] = 0;
                }
            }

            // Make the first row elements 0 if the flag IsFirstRowZero is set
            if (isFirstRowZero)
            {
                for (int c = 0; c < mat.GetLength(1); c++)
                {
                    mat[0, c] = 0;
                }
            }

            return mat;
        }

        public static void TestMakeRowColZero1()
        {
            int[,] mat = MatrixProblemHelper.CreateMatrix(4, 5);
            // Make a few places 0
            Random rnd = new Random();
            mat[rnd.Next(0,4), rnd.Next(0,5)] = 0;
            mat[rnd.Next(0, 4), rnd.Next(0, 5)] = 0;
            Console.WriteLine("original matrix");
            MatrixProblemHelper.PrintMatrix(mat);
            Console.WriteLine();

            Console.WriteLine("all affected rows and columns are 0 using Algorithm 1");
            mat = MakeRowColZero1(mat);
            MatrixProblemHelper.PrintMatrix(mat);
        }

        public static void TestMakeRowColZero2()
        {
            int[,] mat = MatrixProblemHelper.CreateMatrix(4, 5);
            // Make a few places 0
            Random rnd = new Random();
            mat[rnd.Next(0, 4), rnd.Next(0, 5)] = 0;
            mat[rnd.Next(0, 4), rnd.Next(0, 5)] = 0;
            mat[0, 0] = 0;
            Console.WriteLine("original matrix");
            MatrixProblemHelper.PrintMatrix(mat);
            Console.WriteLine();

            Console.WriteLine("all affected rows and columns are 0 using Algorithm 2");
            mat = MakeRowColZero2(mat);
            MatrixProblemHelper.PrintMatrix(mat);
        }
    }
}
