using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// There is a strawberry garden, and each bush is planted in a 2_D matrix form.
    /// Each bush will have a different number of strawberries.
    /// You start at the top left corner and can move only right, bottom.
    /// Find the path which needs to be taken to get the max strawberries.
    /// </summary>
    class MaxPathThroughMatrix
    {
        /// <summary>
        /// We can solve this using dynamic programming.
        /// We need to fill each cell in the order where the first element is from the top row and then last column.
        /// Each cell can be reached from the left or from the top. 
        /// So get the max of these 2 cells and addit to the mat[r,c] value, to get the maxpath value till the cell(r,c)
        /// 
        /// Also save the path in the parent array and backtrack to get the path.
        /// 
        /// The running time of the algorithm is O(m*n)
        /// The space requirement is also O(m*n)
        /// </summary>
        /// <param name="mat">input matrix</param>
        /// <returns>path which gives max value</returns>
        public List<Cell> GetMaxPath(int[,] mat)
        {
            int Rows = mat.GetLength(0);
            int Cols = mat.GetLength(1);
            Cell[,] parent = new Cell[Rows, Cols];
            int[,] maxPath = new int[Rows, Cols];

            Cell c = new Cell(0, 0); // we will start from top left.
            while (c!=null)
            {
                int currentRow = c.Row;
                int currentCol = c.Col;

                while(currentRow<Rows && currentCol>=0)
                {
                    // we can reach (currentRow, currentCol) from top or left
                    if(currentCol-1>=0)
                    {
                        // path from left is the max path
                        maxPath[currentRow, currentCol] = maxPath[currentRow, currentCol - 1];
                        parent[currentRow, currentCol] = new Cell(currentRow, currentCol - 1);
                    }
                    if(currentRow-1>=0 && maxPath[currentRow-1, currentCol]>maxPath[currentRow, currentCol])
                    {
                        // path from top is max path
                        maxPath[currentRow, currentCol] = maxPath[currentRow - 1, currentCol];
                        parent[currentRow, currentCol] = new Cell(currentRow - 1, currentCol);
                    }
                    maxPath[currentRow, currentCol] += mat[currentRow, currentCol];

                    currentRow++;
                    currentCol--;
                }

                c = GetNextStartCell(c, Rows, Cols);
            }

            Console.WriteLine("The max path has a value of {0}", maxPath[Rows - 1, Cols - 1]);

            return BackTrackToGetPath(parent);
        }

        /// <summary>
        /// Back tracks to get the path having the max path value
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private List<Cell> BackTrackToGetPath(Cell[,] parent)
        {
            List<Cell> path = new List<Cell>();
            Cell currentCell = new Cell(parent.GetLength(0) - 1, parent.GetLength(1) - 1);
            while (currentCell != null)
            {
                path.Add(currentCell);
                currentCell = parent[currentCell.Row, currentCell.Col];
            }
            path.Reverse();

            return path;
        }

        /// <summary>
        /// Get the next start cell in the matrix
        /// </summary>
        /// <param name="c">current cell value</param>
        /// <param name="maxRows">number of rows in the matrix</param>
        /// <param name="maxCols">number of columns in the matrix</param>
        /// <returns></returns>
        private Cell GetNextStartCell(Cell c, int maxRows, int maxCols)
        {
            if(c.Col+1<maxCols)
            {
                return new Cell(c.Row, c.Col+1);
            }
            else if(c.Row+1<maxRows)
            {
                return new Cell(c.Row+1, c.Col);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Represents a cell location in the matrix
        /// </summary>
        public class Cell
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public Cell(int row, int col)
            {
                Row = row;
                Col = col;
            }
            public override string ToString()
            {
                return string.Format("({0}, {1})", Row, Col);
            }
        }

        #region TestArea
        public static void TestMaxPathThroughMatrix()
        {
            int[,] mat = new int[,]
            {
                { 1,2,3},
                {4,5,6 },
                {7,8,9 },
            };
            MaxPathThroughMatrix mp = new MaxPathThroughMatrix();
            PrintPath(mp.GetMaxPath(mat));

            mat = new int[,]
            {
                { 1,21,3},
                {4,5,6 },
                {7,8,9 },
            };
            PrintPath(mp.GetMaxPath(mat));

            mat = new int[,]
            {
                { 1,21,3},
                {4,5,6 },
            };
            PrintPath(mp.GetMaxPath(mat));
        }

        private static void PrintPath(List<Cell> path)
        {
            Console.WriteLine("The max int path is as shown below:");
            foreach(Cell c in path)
            {
                Console.Write("{0} -> ", c.ToString());
            }
            Console.WriteLine();
        }
        #endregion
    }
}
