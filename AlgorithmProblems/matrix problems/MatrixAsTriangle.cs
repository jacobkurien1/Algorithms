using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.matrix_problems
{
    /// <summary>
    /// Print the matrix as a triangle
    /// for eg
    /// 1,2,3
    /// 4,5,6
    /// 7,8,9
    /// 
    /// should be printed as 
    /// 1,
    /// 2,4
    /// 3,5,7
    /// 6,8
    /// 9,
    /// </summary>
    class MatrixAsTriangle
    {
        /// <summary>
        /// the number of lines is same as the top line and the right most column
        /// 1,2,3,6,9
        /// get the row and column of the cell in the top line and the rightmost column.
        /// then keep printing the mat[row,col] and row++ and col--;
        /// 
        /// The running time will be O(n*m) where n is the number of rows and m is the number of columns
        /// </summary>
        /// <param name="mat"></param>
        private void PrintMatrixAsTriangle(int[,] mat)
        {
            Cell c = new Cell(0, 0);
            while (c != null)
            {
                int row = c.Row;
                int col = c.Col;
                while(row<mat.GetLength(0) && col>=0)
                {
                    Console.Write("{0}, ", mat[row, col]);
                    row++;
                    col--;
                }
                Console.WriteLine();
                c = GetNextPoint(c, mat);
            }

        }

        /// <summary>
        /// We need to keep incrementing the column till we hit mat.GetLength(1)
        /// and then we need to increment the rows till we hit mat.GetLength(0)
        /// </summary>
        /// <param name="c"></param>
        /// <param name="mat"></param>
        /// <returns></returns>
        private Cell GetNextPoint(Cell c, int[,] mat)
        {
            if(c.Col+1<mat.GetLength(1))
            {
                return new Cell(c.Row, c.Col+1);
            }
            else if (c.Row+1<mat.GetLength(0))
            {
                return new Cell(c.Row + 1, c.Col);
            }
            else
            {
                return null;
            }
        }

        class Cell
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public Cell(int row, int col)
            {
                Row = row;
                Col = col;
            }
        }
        public static void TestMatrixAsTriangle()
        {
            int[,] mat = new int[,] { { 1, 2, 3 }, { 4, 5, 6, }, { 7, 8, 9 } };
            MatrixAsTriangle matAsTriangle = new MatrixAsTriangle();
            matAsTriangle.PrintMatrixAsTriangle(mat);


            mat = new int[,] { { 1, 2, 3 }, { 4, 5, 6, }};
            matAsTriangle.PrintMatrixAsTriangle(mat);
        }
    }
}
