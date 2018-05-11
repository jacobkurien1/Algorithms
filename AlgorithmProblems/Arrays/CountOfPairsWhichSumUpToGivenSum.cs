using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Arrays
{
    /// <summary>
    /// Given an array of sorted integers and a sum value.
    /// We need to calculate the count of all pairs of elements which will sum up to a particular value.
    /// for eg [0,2,3,5] and sum = 5 should return the count as 2
    /// [1,1,3,5,5,5,5] and sum = 6 should return the count as 8
    /// </summary>
    class CountOfPairsWhichSumUpToGivenSum
    {
        /// <summary>
        /// We can use the 2 pointers left and right.
        /// if we have arr[left] + arr[right] == sum we do left++ and right--
        /// If we have the pair sum greater we do right--
        /// If we have the pair sum less than sum we do left++
        /// 
        /// Special cases when we have duplicates which sum to a particular value, like [1,1,3,5,5,5,5].
        /// We need to get the leftCount * rightCount = 2*4 = 8
        /// 
        /// Another edge case is when all the values are same. [1,1,1,1,1] sum = 2
        /// We need to calculate nC2 where n=5 5C2 = 10
        /// 
        /// The running time is O(n)
        /// The space is O(1)
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public static int GetCount(int[] arr, int sum)
        {
            int count = 0;
            int left = 0;
            int right = arr.Length - 1;
            while(left<right)
            {
                int pairSum = arr[left] + arr[right];
                if(pairSum == sum)
                {
                    if(arr[left] == arr[right])
                    {
                        //case: all the elements are same from left to right
                        int n = right - left + 1;
                        count += (n * (n - 1)) / 2; // nC2
                        break;
                    }

                    // case: where we have duplicates in left or right
                    int rightCount = 1;
                    int leftCount = 1;
                    while(left+1 < right && arr[left] == arr[left+1])
                    {
                        leftCount++;
                        left++;
                    }
                    while (left < right-1 && arr[right] == arr[right-1])
                    {
                        rightCount++;
                        right--;
                    }
                    left++;
                    right--;
                    count += rightCount * leftCount;
                }
                else if(pairSum > sum)
                {
                    right--;
                }
                else
                {
                    left++;
                }
            }
            return count;
        }

        public static void TestCountOfPairsWhichSumUpToGivenSum()
        {
            int[] arr = new int[] { 0,1, 2, 3, 5 };
            int sum = 5;
            Console.WriteLine("The count of paris is {0}. Expected : 2", GetCount(arr, sum));

            arr = new int[] { 1, 1, 2,2,3,4,4, 5, 5, 5, 5 };
            sum = 6;
            Console.WriteLine("The count of paris is {0}. Expected : 12", GetCount(arr, sum));

            arr = new int[] { 1, 1, 1,1,1,5 };
            sum = 2;
            Console.WriteLine("The count of paris is {0}. Expected : 10", GetCount(arr, sum));
        }
    }
}
