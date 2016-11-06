using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Arrays
{
    /// <summary>
    /// Given an array of integers, return an index array such that the value in each index is greater than the current value in the input array.
    ///  for eg input arr : 5,1,2,1,3,2,9
    /// the output should be : 6,2,4,4,6,6,-1
    /// </summary>
    class ArrayWithGreaterElementToRight
    {
        /// <summary>
        /// The naive approach is to fo from left to right and for each index go from index to the end of the array 
        /// and get the next greater element than the current element. The running time is O(n^2).
        /// 
        /// So we can use a stack and save all the indices where arr[i-1] >= arr[i].
        /// And when we see arr[i-1] < arr[i] we can put the retArr[i-1]  = i and keep poping stack while the stacktop index is less than arr[i].
        /// 
        /// The running time of this approach is O(n)
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int[] GetIndexArray(int[] arr)
        {
            if (arr == null)
            {
                throw new ArgumentException("The input array is not valid");
            }

            int[] retArr = new int[arr.Length];
            Stack<int> st = new Stack<int>();
            for(int index = 1; index<arr.Length; index++)
            {
                retArr[index] = -1;
                if(arr[index -1]>= arr[index])
                {
                    st.Push(index - 1);
                }
                else
                {
                    retArr[index - 1] = index;
                    while(st.Count>0 && arr[st.Peek()]<arr[index])
                    {
                        retArr[st.Pop()] = index;
                    }
                }
            }
            return retArr;
        }

        public static void TestArrayWithGreaterElementToRight()
        {
            int[] arr = new int[] { 5, 1, 2, 1, 3, 2, 9 };
            int[] retArrIndex = new int[] { 6, 2, 4, 4, 6, 6, -1 };
            Console.WriteLine("The expected index array is:");
            ArrayHelper.PrintArray(retArrIndex);
            Console.WriteLine("The actual index array is:");
            ArrayHelper.PrintArray(GetIndexArray(arr));

            arr = new int[] { 23, 45, 2, 1, 3, 2, 9 };
            retArrIndex = new int[] { 1, -1, 4, 4, 6, 6, -1 };
            Console.WriteLine("The expected index array is:");
            ArrayHelper.PrintArray(retArrIndex);
            Console.WriteLine("The actual index array is:");
            ArrayHelper.PrintArray(GetIndexArray(arr));

        }
    }
}
