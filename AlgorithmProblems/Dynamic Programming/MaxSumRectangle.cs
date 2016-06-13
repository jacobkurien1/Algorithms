using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// Get the rectangle with maximum sum in a 2-D matrix
    /// </summary>
    public class MaxSumRectangle
    {
        /// <summary>
        /// We can use dynamic programming to get the maximum sum in a 2-D matrix.
        /// Keep getting the cummulative sum of rectangles with different left and right index
        /// And do Kadanes 1D max sum algorithm to get the top and bottom index.
        /// 
        /// The total running time of this algorithm is O(row*col*col)
        /// And the space requirement is O(row)
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public Rectangle GetMaxRectangleInMatrix(int[,] mat)
        {
            Rectangle rect = new Rectangle(0, 0, 0, 0, int.MinValue);
            int[] cummulativeSum = new int[mat.GetLength(0)];
            for(int currentLeft = 0; currentLeft< mat.GetLength(1); currentLeft++)
            {
                //Make all elements in cummulativeSum =0
                for(int i=0; i<cummulativeSum.Length; i++)
                {
                    cummulativeSum[i] = 0;
                }
                for (int currentRight = currentLeft; currentRight < mat.GetLength(1); currentRight++)
                {
                    for(int i=0; i<mat.GetLength(0); i++)
                    {
                        cummulativeSum[i] += mat[i, currentRight];
                    }
                    KadaneResult result = GetMaxInArray(cummulativeSum);
                    if(result.MaximumSum>rect.MaxSum)
                    {
                        // We have a new maximum
                        rect.MaxSum = result.MaximumSum;
                        rect.LeftIndex = currentLeft;
                        rect.RightIndex = currentRight;
                        rect.TopIndex = result.StartIndex;
                        rect.BottomIndex = result.EndIndex;
                    }
                }
            }
            return rect;
        }


        /// <summary>
        /// Here given an array we find the maximum sum in the 1_D array
        /// This can be done using the Kadane Algorithm
        /// </summary>
        /// <param name="arr"></param>
        /// <returns>KadaneResult object</returns>
        public KadaneResult GetMaxInArray(int[] arr)
        {
            KadaneResult result = new KadaneResult(0, 0, arr[0]);
            int currentSum = (arr[0] >= 0) ? arr[0] : 0;
            int currentStart = (arr[0] >= 0) ? 0 : 1;
            for(int i=1; i< arr.Length; i++)
            {
                currentSum += arr[i];
                if(currentSum>result.MaximumSum)
                {
                    // Here we have found a 1-D array which has the maxSum
                    result.StartIndex = currentStart;
                    result.EndIndex = i;
                    result.MaximumSum = currentSum;
                }
                if(currentSum < 0)
                {
                    // if the currentSum is negative we need to reset the startIndex and the currentSum
                    currentSum = 0;
                    currentStart = i+1;
                }
            }

            return result;
        }

        /// <summary>
        /// This class is used to store the result of the Kadane Algo
        /// which finds the largest max sum of consecutive elements in the array
        /// </summary>
        public class KadaneResult
        {
            public int StartIndex { get; set; }
            public int EndIndex { get; set; }
            public int MaximumSum { get; set; }
            public KadaneResult(int startIndex, int endIndex, int maximumSum)
            {
                StartIndex = startIndex;
                EndIndex = endIndex;
                MaximumSum = maximumSum;
            }
        }

        /// <summary>
        /// Class to represent a Rectangle in a 2-D array
        /// </summary>
        public class Rectangle
        {
            public int LeftIndex { get; set; }
            public int RightIndex { get; set; }
            public int TopIndex { get; set; }
            public int BottomIndex { get; set; }
            public int MaxSum { get; set; }
            public Rectangle(int leftIndex, int rightIndex, int topIndex, int bottomIndex, int maxSum)
            {
                LeftIndex = leftIndex;
                RightIndex = rightIndex;
                TopIndex = topIndex;
                BottomIndex = bottomIndex;
                MaxSum = maxSum;
            }
        }

        public static void TestMaxSumRectangle()
        {
            int[,] mat = new int[,] { { 2, 1, -3, -4, 5 }, { 0, 6, 3, 4, 1 }, { 2, -2, -1, 4, -5 }, { -3, 3, 1, 0, 3 } };
            MaxSumRectangle maxSum = new MaxSumRectangle();
            Rectangle rect = maxSum.GetMaxRectangleInMatrix(mat);

            Console.WriteLine("The max 2-D rectangle sum is {0}", rect.MaxSum);
            Console.WriteLine("The left of rectangle is {0}", rect.LeftIndex);
            Console.WriteLine("The Right of rectangle is {0}", rect.RightIndex);
            Console.WriteLine("The Top of rectangle is {0}", rect.TopIndex);
            Console.WriteLine("The bottom of rectangle is {0}", rect.BottomIndex);

            mat = new int[,] { { 1, 2, -1, -4, -20 }, { -8, -3, 4, 2, 1 }, { 3, 8, 10, 1, 3 }, { -4, -1, 1, 7, -6 } };
            maxSum = new MaxSumRectangle();
            rect = maxSum.GetMaxRectangleInMatrix(mat);

            Console.WriteLine("The max 2-D rectangle sum is {0}", rect.MaxSum);
            Console.WriteLine("The left of rectangle is {0}", rect.LeftIndex);
            Console.WriteLine("The Right of rectangle is {0}", rect.RightIndex);
            Console.WriteLine("The Top of rectangle is {0}", rect.TopIndex);
            Console.WriteLine("The bottom of rectangle is {0}", rect.BottomIndex);
        }
    }


}
