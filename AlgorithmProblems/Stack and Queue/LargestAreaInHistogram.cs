using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue
{
    /// <summary>
    /// Get the largest area in a histogram
    /// </summary>
    class LargestAreaInHistogram
    {
        #region Algo1: Using stacks
        /// <summary>
        /// Algo: 1. Traverse the different histogram columns and keep adding index to stack where hist[st.top] <= hist[i]
        /// 2. When this condition is not met, pop index from stack and calculate the area. 
        /// 3. Store if this is the max area
        /// 4. Once the histogram is traversed, Do steps 2-3 till the stack is empty
        /// 
        /// The running time of this algo is O(n)
        /// The space requirement of the algo is O(n)
        /// </summary>
        /// <param name="histogram">represents the values of the histogram at equal intervals</param>
        /// <returns>max area in the histogram</returns>
        private static long GetLargestArea(long[] histogram)
        {
            if(histogram == null)
            {
                throw new Exception("The histogram is null");
            }

            Stack<int> st = new Stack<int>();
            int i = 0;
            long maxArea = 0;
            while(i<histogram.Length)
            {
                if(st.Count == 0 || histogram[st.Peek()] <= histogram[i])
                {
                    // when the histogram columns are increasing then add them to the stack
                    st.Push(i++);
                }
                else
                {
                    // When histogram columns are decreasing we need to pop and calculate the rect area
                    int stIndex = st.Pop();
                    int width = (st.Count == 0) ? i : i - st.Peek()-1;
                    long area = histogram[stIndex] * width;
                    if(area>maxArea)
                    {
                        maxArea = area;
                    }
                }
            }
            while(st.Count!=0)
            {
                // keep poping and calculating the rect area till the stack is not empty
                int stIndex = st.Pop();
                int width = (st.Count == 0) ? i : i - st.Peek()-1;
                long area = histogram[stIndex] * width;
                if (area > maxArea)
                {
                    maxArea = area;
                }
            }
            return maxArea;
        }
        #endregion

        #region TestArea
        public static void TestLargestAreaInHistogram()
        {
            long[] hist = new long[] { 1, 2, 3, 1, 4 };
            Console.WriteLine("The max area of the above histogram is {0}. Expected:5", GetLargestArea(hist));

            hist = new long[] { 8979, 4570, 6436, 5083, 7780, 3269, 5400, 7579, 2324, 2116 };
            Console.WriteLine("The max area of the above histogram is {0}. Expected:26152", GetLargestArea(hist));
        }
        #endregion
    }
}
