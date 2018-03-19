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

        #region Algo1 
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

        #endregion

        #region Algo2 - Cleaner approach
        /// <summary>
        /// Algo: 1. Whenever arr[i] < arr[i+1] then we have indexArr[i] = i+1
        /// 2. Else we can keep poping from stack till stack.top < arr[i] and make index[stack.top] = i
        /// Also push i into stack.
        /// 
        /// Running time O(n)
        /// Space requriement O(n)
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int[] GetIndexArray2(int[] arr)
        {
            if (arr == null)
            {
                throw new ArgumentException("The input array is not valid");
            }

            int[] retArr = Enumerable.Repeat(-1, arr.Length).ToArray(); // Initialize all array elements to -1
            Stack<int> st = new Stack<int>();
            for (int index = 0; index < arr.Length; index++)
            {
                if((index + 1<arr.Length) && arr[index] < arr[index + 1])
                {
                    retArr[index] = index + 1;
                }
                else
                {
                    while(st.Count >0 && arr[st.Peek()] < arr[index])
                    {
                        retArr[st.Pop()] = index;
                    }
                    st.Push(index);
                }
            }
            return retArr;
        }

        #endregion

        public static void TestArrayWithGreaterElementToRight()
        {
            int[] arr = new int[] { 5, 1, 2, 1, 3, 2, 9 };
            int[] retArrIndex = new int[] { 6, 2, 4, 4, 6, 6, -1 };
            Console.WriteLine("The expected index array is:");
            ArrayHelper.PrintArray(retArrIndex);
            Console.WriteLine("The actual index array is:");
            ArrayHelper.PrintArray(GetIndexArray(arr));
            ArrayHelper.PrintArray(GetIndexArray2(arr));


            arr = new int[] { 23, 45, 2, 1, 3, 2, 9 };
            retArrIndex = new int[] { 1, -1, 4, 4, 6, 6, -1 };
            Console.WriteLine("The expected index array is:");
            ArrayHelper.PrintArray(retArrIndex);
            Console.WriteLine("The actual index array is:");
            ArrayHelper.PrintArray(GetIndexArray(arr));
            ArrayHelper.PrintArray(GetIndexArray2(arr));

        }
    }
}
