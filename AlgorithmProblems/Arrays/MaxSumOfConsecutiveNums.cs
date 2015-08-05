using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Arrays
{
    class MaxSumOfConsecutiveNums
    {
        /// <summary>
        /// Running time is O(n)
        /// Space requirement is O(1)
        /// </summary>
        /// <param name="arr">The array whose maximum sum of x consecutive numbers</param>
        /// <param name="x">x is the number of consecutives that needs to be considered</param>
        /// <returns>the maximum sum of x consecutive numbers in the array arr</returns>
        private static int MaxSumOfXConsecutiveNums(int[] arr, int x)
        {
            // Error Check conditions
            if(arr == null || arr.Length == 0|| x==0 || arr.Length<x)
            {
                throw new Exception("Invalid input parameters");
            }

            int max = 0;
            for(int i=0; i<arr.Length-x; i++)
            {
                int currentSum = GetSumOfXConsecutiveNums(arr,x,i);
                if(max < currentSum)
                {
                    max = currentSum;
                }
            }
            return max;
        }

        private static int GetSumOfXConsecutiveNums(int[] arr, int x, int startIndex)
        {
            // Error Check conditions
            if(x == 0)
            {
                throw new Exception("num of consecutive ints is invalid");
            }
            int sum = 0;
            for(int i= startIndex; i<startIndex+x; i++)
            {
                sum += arr[i];
            }
            return sum;
        }

        public static void TestMaxSumOfConsecutiveNums()
        {
            int[] arr = ArrayHelper.CreateArray(20);
            Console.WriteLine("The array is as follows:");
            ArrayHelper.PrintArray(arr);
            Console.WriteLine("The max sum of 3 consecutive numbers in array is: "+MaxSumOfConsecutiveNums.MaxSumOfXConsecutiveNums(arr, 3));
        }
    }
}
