using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// Get the min number of perfect squares that can be used to represent a number
    /// </summary>
    public class NumRepresentedByPerfectSquares
    {
        /// <summary>
        /// We will use dynamic programming to solve this problem.
        /// minPerfectSquares has row 0<=i<=sqrt(num)
        /// minPerfectSquares has col 0<=j<=num
        /// minPerfectSquares[i,j] = min | 1+minPerfectSquares[i,j-(i*i)]
        ///                              | minPerfectSquares[i-1,j]
        /// 
        /// We need a backtrack[i,j] to get the perfect squares which are used to represent num
        /// 
        /// The running time for this algo is O(num * sqrt(num))
        /// and the space required is O(num * sqrt(num))
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string GetPerfectSquaresForTheNum(int num)
        {
            int row = (int)Math.Sqrt(num);
            int col = num;
            int[,] minPerfectSquares = new int[row + 1, num + 1];
            bool[,] backtrack = new bool[row + 1, num + 1];

            // initialization
            for(int i = 0; i <= row; i++)
            {
                minPerfectSquares[i, 0] = 0;
            }
            for(int j = 0; j <= num; j++)
            {
                backtrack[1, j] = true;
                minPerfectSquares[1, j] = j;
            }

            // execution
            for (int i = 2; i <= row; i++)
            {
                for (int j = 1; j <= num; j++)
                {
                    if ((j - (i * i)) >= 0 && (1 + minPerfectSquares[i, j - (i * i)] < minPerfectSquares[i - 1, j]))
                    {
                        minPerfectSquares[i, j] = 1 + minPerfectSquares[i, j - (i * i)];
                        backtrack[i, j] = true;
                    }
                    else
                    {
                        minPerfectSquares[i, j] = minPerfectSquares[i - 1, j];
                        backtrack[i, j] = false;
                    }

                }
            }

            Console.WriteLine("The min number of perfect squares to represent {0} is {1}", num, minPerfectSquares[row, col]);

            // back track to get the perfect squares
            List<int> allSquares = new List<int>();

            while(row>0 && col>0)
            {
                if (backtrack[row, col])
                {
                    allSquares.Add(row);
                    col -= row * row;
                }
                else
                {
                    row -= 1;
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (int val in allSquares)
            {
                sb.AppendFormat("({0})^2 + ", val);

            }
            return sb.ToString();

        }

        public static void TestNumRepresentedByPerfectSquares()
        {
            NumRepresentedByPerfectSquares numRepresentByPerfectSq = new NumRepresentedByPerfectSquares();
            int num = 6;
            Console.WriteLine("{0} can be represented by the different perfect squares as shown {1}", num, numRepresentByPerfectSq.GetPerfectSquaresForTheNum(num));

            num = 100;
            Console.WriteLine("{0} can be represented by the different perfect squares as shown {1}", num, numRepresentByPerfectSq.GetPerfectSquaresForTheNum(num));

            num = 257;
            Console.WriteLine("{0} can be represented by the different perfect squares as shown {1}", num, numRepresentByPerfectSq.GetPerfectSquaresForTheNum(num));
        }
    }
}
