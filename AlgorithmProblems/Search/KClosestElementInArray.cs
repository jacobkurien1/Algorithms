using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Search
{
    /// <summary>
    /// Find the k closest element in a sorted array to a given number num
    /// </summary>
    class KClosestElementInArray
    {
        /// <summary>
        /// this function gets the k numbers closest to the number num.
        /// Algo:
        /// 1. get the index which is equal or greater than the given num
        /// 2. keep 2 pointers at the i =index and j = index+1
        /// 3. keep finding the closest number and decrement i or increment j till we get k numbers
        /// 
        /// The running time of this algo is O(logn + k)
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="num"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int[] GetKClosestElementInArray(int[] arr, int num, int k)
        {
            if(k>arr.Length)
            {
                // Handle error condition
                throw new ArgumentException("k should be less than the length of the array");
            }
            int[] ret = new int[k];

            int j = GetElemGreaterThanNum(arr, num);
            int i = j - 1;
            int outputIndex = 0;
            while (i >= 0 && j < arr.Length && outputIndex < k)
            {

                if (Math.Abs(arr[i] - num) > Math.Abs(arr[j] - num))
                {
                    // add the value at jth index
                    ret[outputIndex++] = arr[j++];
                }
                else
                {
                    // add value at ith index
                    ret[outputIndex++] = arr[i--];
                }
            }
                
            while(i>=0 && outputIndex < k)
            {
                // add value at ith index
                ret[outputIndex++] = arr[i--];
            }
            while (j < arr.Length && outputIndex < k)
            {
                // add the value at jth index
                ret[outputIndex++] = arr[j++];
            }

            return ret;
        }

        /// <summary>
        /// This is a binary search sub routine to get the number which is equal or greater than num
        /// the running time of this subroutine is log(n)
        /// </summary>
        /// <param name="arr">sorted array on which search takes place</param>
        /// <param name="num"></param>
        /// <returns>index in arr whose value is equal or greater than num</returns>
        private static int GetElemGreaterThanNum(int[] arr, int num)
        {
            int st = 0;
            int end = arr.Length - 1;
            int indexToRet = -1;
            while(end>=st)
            {
                int mid = end - ((end - st) / 2);   // prevents overflow
                if(arr[mid] >= num)
                {
                    indexToRet = mid;
                    //go to the left side
                    end = mid - 1;
                }
                else
                {
                    // go to the right side
                    st = mid + 1;
                }
            }

            // indexToRet == -1 suggest that all elements in arr were less than the num
            // in which case we need to consider the index of the last element
            return (indexToRet == -1) ? arr.Length - 1 : indexToRet;
        }
        public static void TestKClosestElementInArray()
        {
            int[] arr = new int[] { 1, 4, 6, 7, 8, 9, 11, 21, 81 };
            int num = 8;
            int k = 4;
            ArrayHelper.PrintArray(GetKClosestElementInArray(arr, num, k));

            arr = new int[] { 1, 4, 6, 7, 8, 9, 11, 21, 81 };
            num = 82;
            k = 4;
            ArrayHelper.PrintArray(GetKClosestElementInArray(arr, num, k));

            arr = new int[] { 1, 4, 6, 7, 8, 9, 11, 21, 81 };
            num = -1;
            k = 4;
            ArrayHelper.PrintArray(GetKClosestElementInArray(arr, num, k));

            arr = new int[] { 1, 4, 6, 7, 8, 9, 11, 21, 81 };
            num = 30;
            k = 4;
            ArrayHelper.PrintArray(GetKClosestElementInArray(arr, num, k));
        }
    }
}
