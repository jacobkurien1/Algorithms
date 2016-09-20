using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// Given an array, find the max sum in the array such that no 2 elements are adjacent to each other.
    /// </summary>
    class MaxSumWithoutAdjacentElements
    {
        /// <summary>
        /// We can use dynamic programming to solve this problem
        /// We will have maxSum where maxSum[i] = max{ arr[i] + maxSum[i-2]
        ///                                             maxSum[i-1]
        /// Also we will have backtrack[i] = true if arr[i] is present in maxSum and false o.w.
        /// 
        /// The running time is O(n)
        /// And the space requirement is O(n)
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public List<int> GetElementsWhichGiveMaxSum(int[] arr)
        {
            int[] maxSum = new int[arr.Length];
            bool[] backTrack = new bool[arr.Length];

            for(int i = 0; i < arr.Length; i++)
            {
                // there are 2 cases: case1 -> we dont take the arr[i]
                int sumWithoutElem = (i - 1 >= 0) ? maxSum[i - 1] : 0;
                // case2 -> we will consider arr[i]
                int sumWithElem = (i - 2 >= 0) ? arr[i] + maxSum[i - 2] : arr[i];
                if(sumWithElem >= sumWithoutElem)
                {
                    // sum is greater when we consider the arr[i]
                    backTrack[i] = true;
                    maxSum[i] = sumWithElem;
                }
                else
                {
                    // sum is greater when we dont consider the arr[i]
                    backTrack[i] = false;
                    maxSum[i] = sumWithoutElem;
                }
            }

            // Now we need to backtrack to get the elements which form maxSum
            List<int> ret = new List<int>();

            int index = arr.Length - 1;
            while(index >= 0)
            {
                if(backTrack[index])
                {
                    // this means the arr[index] is present in the maxsum
                    ret.Add(arr[index]);
                    index -= 2;
                }
                else
                {
                    // this means arr[index] is not present in maxSum
                    index--;
                }
            }

            return ret;
        }

        #region TestArea
        public static void TestMaxSumWithoutAdjacentElements()
        {
            MaxSumWithoutAdjacentElements maxSum = new MaxSumWithoutAdjacentElements();
            int[] arr = new int[] { 3, 4, -1, 5, 6 };
            List<int> ret = maxSum.GetElementsWhichGiveMaxSum(arr);
            PrintElements(ret);

            arr = new int[] { -1, -3, -1, -9, -1 };
            ret = maxSum.GetElementsWhichGiveMaxSum(arr);
            PrintElements(ret);

            arr = new int[] { -1, -3, 0, -9, -43 };
            ret = maxSum.GetElementsWhichGiveMaxSum(arr);
            PrintElements(ret);

            arr = new int[] { 1, -3, 0, -9, -43 };
            ret = maxSum.GetElementsWhichGiveMaxSum(arr);
            PrintElements(ret);
        }

        private static void PrintElements(List<int> ret)
        {
            if(ret!=null)
            {
                foreach(int val in ret)
                {
                    Console.Write("{0}, ", val);
                }
                Console.WriteLine();
            }
        }
        #endregion
    }
}
