using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// We are given a matrix of integers and we need to traverse from the top-left to bottom-right most cell.
    /// From a cell[i,j] we can go to cell[i+1,j], cell[i+1,j+1], cell[i,j+1].
    /// the value in each cell of the matrix is the cost to visit that cell and we need to minimize the cost needed
    /// to traverse from the top-leftmost cell to bottom-rightmost cell.
    /// </summary>
    class MinCostPath
    {
        /// <summary>
        /// This class can be used to pinpoint a position in the matrix
        /// </summary>
        class PositionOfCell
        {
            public PositionOfCell(int row, int column)
            {
                Row = row;
                Column = column;
            }
            public int Row { get; set; }
            public int Column { get; set; }

            public override string ToString()
            {
                return string.Format("({0},{1})", Row, Column);
            }
        }

        class ComparatorForDictionary : IEqualityComparer<PositionOfCell>
        {
            public bool Equals(PositionOfCell x, PositionOfCell y)
            {
                return x.Row == y.Row && x.Column == y.Column;
            }

            public int GetHashCode(PositionOfCell obj)
            {
                return obj.Column.GetHashCode() + obj.Row.GetHashCode();
            }
        }

        /// <summary>
        /// We can use dynamic programming to get the minimum cost path
        /// minCost[i,j] =  min{minCost[i-1,j] , minCost[i,j-1], minCost[i-1,j-1]} + mat[i,j]
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        List<PositionOfCell> GetMinCostPath(int[,] mat)
        {
            List<PositionOfCell> path = new List<PositionOfCell>();

            int[,] minCost = new int[mat.GetLength(0), mat.GetLength(1)];
            Dictionary<PositionOfCell, PositionOfCell> backtrackPath = new Dictionary<PositionOfCell, PositionOfCell>(new ComparatorForDictionary());

            // initializations
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    minCost[i, j] = int.MaxValue;
                }
            }
            minCost[0, 0] = mat[0, 0];

            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    if(i-1>=0 && minCost[i,j]>minCost[i-1,j] + +mat[i, j])
                    {
                        minCost[i, j] = minCost[i - 1, j] + mat[i,j];
                        backtrackPath[new PositionOfCell(i, j)] = new PositionOfCell(i - 1, j);
                    }
                    if(j-1>=0 && minCost[i,j]>minCost[i,j-1] + mat[i, j])
                    {
                        minCost[i, j] = minCost[i, j-1] + mat[i, j];
                        backtrackPath[new PositionOfCell(i, j)] = new PositionOfCell(i, j-1);
                    }
                    if(i-1>=0 && j-1>=0 && minCost[i,j]> minCost[i-1,j-1] + mat[i, j])
                    {
                        minCost[i, j] = minCost[i-1, j - 1] + mat[i, j];
                        backtrackPath[new PositionOfCell(i, j)] = new PositionOfCell(i-1, j - 1);
                    }
                }
            }

            Console.WriteLine("the minimum cost path is having the cost :{0}", minCost[mat.GetLength(0)-1, mat.GetLength(1)-1]);
            //Now lets find the complete path
            PositionOfCell currentPosition = new PositionOfCell(mat.GetLength(0)-1, mat.GetLength(1)-1);
            while(currentPosition!=null)
            {
                path.Add(currentPosition);
                backtrackPath.TryGetValue(currentPosition, out currentPosition);
            }

            path.Reverse();
            return path;
        }

        public static void TestMinCostPath()
        {
            int[,] mat = new int[,] { { 1, 2, 3 }, { 4, 8, 2 }, { 1, 5, 3 } };
            MinCostPath mcp = new MinCostPath();
            List<PositionOfCell> ret = mcp.GetMinCostPath(mat);
            PrintPath(ret);
        }

        private static void PrintPath(List<PositionOfCell> path)
        {
            foreach(PositionOfCell cell in path)
            {
                Console.Write("{0} -> ", cell.ToString());
            }
            Console.Write("*");
            Console.WriteLine();
        }
    }
}
