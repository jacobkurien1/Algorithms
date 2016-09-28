using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.matrix_problems
{
    /// <summary>
    /// Given a plot in 2_D matrix and places which is marked 0 has trees
    /// and places which is marked 1 donot have trees.
    /// Find the max square on which a house can be built.
    /// 
    /// This question can be also asked as follows:
    /// Given a matrix having 0's and 1's find the rectangle(constituted of all 1's) having max square area
    /// </summary>
    class MaxSqAreaInBinaryMatrix
    {
        /// <summary>
        /// To get the max square at a cell(i,j) we will get the min(mat[i-1,j-1], mat[i-1,j], mat[i,j-1]) + 1 if mat[i,j] == 1
        /// else we keep mat[i,j] = 0
        /// 
        /// Since we are using the space in the input matrix we wont need extra space but we will be affecting the input matrix.
        /// The running time for this algo is O(Rows*Cols)
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public Square GetMaxRectArea(int[,] mat)
        {
            Square sq = null;
            int maxArea = 0;
            for(int i =0; i<mat.GetLength(0); i++)
            {
                for(int j = 0; j<mat.GetLength(1); j++)
                {
                    if(mat[i,j]==1)
                    {
                        // we need to fill the value only if the current cell has 1
                        int lefttop = (i - 1 >= 0 && j - 1 >= 0) ? mat[i - 1, j - 1] : 0;
                        int righttop = (i - 1 >= 0) ? mat[i - 1, j] : 0;
                        int bottomLeft = (j - 1 >= 0) ? mat[i, j - 1] : 0;
                        mat[i, j] = GetMin(righttop , bottomLeft , lefttop) + mat[i, j];

                        if(mat[i, j] > maxArea)
                        {
                            // we have found the new maximum
                            maxArea = mat[i, j];
                            sq = new Square(i + 1 - mat[i, j], j + 1 - mat[i, j], i, j);
                        }
                    }
                }
            }

            return sq;
        }

        /// <summary>
        /// gets the minimum of the 3 numbers
        /// </summary>
        /// <param name="righttop"></param>
        /// <param name="bottomLeft"></param>
        /// <param name="lefttop"></param>
        /// <returns></returns>
        private int GetMin(int righttop, int bottomLeft, int lefttop)
        {
            return Math.Min(Math.Min(righttop, bottomLeft), lefttop);
        }

        /// <summary>
        /// Class to represent a rectangle object
        /// </summary>
        public class Square
        {
            public int TopLeftX { get; set; }
            public int TopLeftY { get; set; }
            public int BottomRightX { get; set; }
            public int BottomRightY { get; set; }

            public Square(int topLeftX, int topLeftY, int bottomRightX, int bottomRightY)
            {
                TopLeftX = topLeftX;
                TopLeftY = topLeftY;
                BottomRightX = bottomRightX;
                BottomRightY = bottomRightY;
            }

            public override string ToString()
            {
                return string.Format("({0}, {1}) -> ({2}, {3})", TopLeftX, TopLeftY, BottomRightX, BottomRightY);
            }

        }

        public static void TestMaxRectAreaInBinaryMatrix()
        {
            MaxSqAreaInBinaryMatrix mr = new MaxSqAreaInBinaryMatrix();
            int[,] mat = new int[,]
            {
                {0,1,1,1 },
                {1,1,0,1 },
                {0,0,0,1 },
                {0,1,1,1 },
                {1,1,1,1 },
                {0,1,1,1 },
                {0,1,1,1 },
            };

            Console.WriteLine("The input matrix is as shown below:");
            MatrixProblemHelper.PrintMatrix(mat);
            Console.WriteLine("The max square is at {0}", mr.GetMaxRectArea(mat));

            mat = new int[,]
            {
                {1,0,1 },
                {0,1,0 },
                {1,0,1 }
            };

            Console.WriteLine("The input matrix is as shown below:");
            MatrixProblemHelper.PrintMatrix(mat);
            Console.WriteLine("The max square is at {0}", mr.GetMaxRectArea(mat));
        }
    }
}
