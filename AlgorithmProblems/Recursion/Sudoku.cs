using AlgorithmProblems.matrix_problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Recursion
{
    class Sudoku
    {
        private int[,] grid;
        public int[,] Grid 
        { 
            get
            {
                return grid;
            }
            set
            {
                grid = value;
            }
        }
        public Sudoku(int[,] grid)
        {
            this.grid = grid;
        }
        public bool SolveSudoku()
        {
            int row =0, col = 0;
            if (!FindUnassignedSpace(ref row, ref col))
            {
                return true;    // base condition
            }
            else
            {
                for (int i = 1; i <= grid.GetLength(0); i++) // Using i <= grid.GetLength(0) assumin this is a square matrix
                {
                    if (!IsConflict(row, col, i))
                    {
                        grid[row, col] = i;
                        if (!SolveSudoku())
                        {
                            grid[row, col] = 0;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                return false; // trigger back tracking
            }
        }

        private bool IsConflict(int row, int col, int val)
        {
            // Make sure that the row does not contain the same value
            for (int r = 0; r < grid.GetLength(0); r++)
            {
                if(grid[r,col] == val)
                {
                    return true;
                }
            }

            // Make sure that the column does not contain the same value
            for (int c = 0; c < grid.GetLength(1); c++)
            {
                if(grid[row,c] == val)
                {
                    return true;
                }
            }

            // Make sure that the box does not contain the same value
            int boxRow = (row / 3)*3;   // Get the row value of the top left of the square box
            int boxCol = (col / 3)*3;   // Get the col value of the top left of the square box
            // Here change 3 to square root of GetLength(0) for more genaralized solution

            for(int r= boxRow; r<boxRow + grid.GetLength(0)/3; r++)
            {
                for(int c = boxCol; c<boxCol + grid.GetLength(1)/3; c++)
                {
                    if(grid[r,c] == val)
                    {
                        return true;
                    }
                }
            }
            return false;
            
        }
        private bool FindUnassignedSpace(ref int row, ref int col)
        {
            for (int r = 0; r < grid.GetLength(0); r++)
            {
                for (int c = 0; c < grid.GetLength(1); c++)
                {
                    if (grid[r, c] == 0)
                    {
                        row = r;
                        col = c;
                        return true;
                    }
                }
            }
            return false;
        }

        public static void TestSudokuSolver()
        {
            int[,] grid = new int[,]
            {
                {0,0,0,0,0,0,0,0,0},
                {0,0,0,0,7,9,6,0,0},
                {2,9,3,0,5,6,0,0,0},
                {4,0,0,5,0,2,0,0,0},
                {3,8,1,4,0,0,5,0,0},
                {0,5,2,0,1,0,0,9,0},
                {9,0,0,0,3,0,0,0,0},
                {0,0,5,0,0,0,0,0,0},
                {0,6,0,0,0,8,0,0,7}

            };
            Sudoku sk = new Sudoku(grid);
            Console.WriteLine("Sudoku problem");
            MatrixProblemHelper.PrintMatrix(sk.Grid);
            bool IsSudokuSolved = sk.SolveSudoku();
            if (IsSudokuSolved)
            {
                Console.WriteLine("Sudoku solution");
                MatrixProblemHelper.PrintMatrix(sk.Grid);
            }
            else
            {
                Console.WriteLine("Sudoku cannot be solved");
            }
        }
    }
}
