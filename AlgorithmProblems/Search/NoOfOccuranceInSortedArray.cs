using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Search
{
    /// <summary>
    /// Count the number of occurance of an element in a sorted array
    /// </summary>
    public class NoOfOccuranceInSortedArray
    {
        /// <summary>
        /// Gets the number of occurances of val in arr.
        /// Algo: 1. Get the last occurance and the first occurance
        /// 2. return lastOccur - firstOccur + 1
        /// 
        /// If the number is not present, we should return 0
        /// 
        /// Running time O(log(n))
        /// Space O(1)
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public int GetOccurances(int[] arr, int val)
        {
            int lastOccurIndex = GetLastOccurance(arr, val);
            int firstOccurIndex = GetFirstOccurance(arr, val);
            return (arr[firstOccurIndex] == val && arr[lastOccurIndex] == val) ? lastOccurIndex - firstOccurIndex + 1 : 0;
        }

        /// <summary>
        /// Given a number find the index of the last occurance of number in a sorted array
        /// 
        /// 
        /// The running time is O(log(n))
        /// The space reqirement is O(1)
        /// </summary>
        /// <param name="arr">sorted array</param>
        /// <param name="val"></param>
        /// <returns></returns>
        private int GetLastOccurance(int[] arr, int val)
        {
            // Error checks
            if(arr == null || arr.Length == 0)
            {
                throw new ArgumentException("The array is not a valid array");
            }

            int low = 0;
            int high = arr.Length -1;
            while (high > low)
            {
                int mid = low + (int)Math.Ceiling((high - low) / 2.0);
                if (arr[mid] <= val)
                {
                    low = mid;
                }
                else
                {
                    high = mid - 1;
                }
            }

            return low; 
        }

        /// <summary>
        /// Returns the index of the first occurance of the val in the given array
        /// 
        /// Running time is O(log(n))
        /// Space is O(1)
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private int GetFirstOccurance(int[] arr, int val)
        {
            // Error checks
            if(arr == null || arr.Length == 0)
            {
                throw new ArgumentException("The array is not a valid array");
            }

            int low = 0;
            int high = arr.Length -1;

            while(high > low)
            {
                int mid = low + ((high - low)/2);
                if(arr[mid] >= val)
                {
                    high = mid;
                }
                else
                {
                    low = mid + 1;
                }
            }
            return high;
        }
        public static void TestNoOfOccuranceInSortedArray()
        {
            NoOfOccuranceInSortedArray occurance = new NoOfOccuranceInSortedArray();
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 12, 14, 15, 19 };
            Console.WriteLine("The num of occurance of 4 is {0}. Expected: {1}", occurance.GetOccurances(arr, 4), 1);

            arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 12, 14, 15, 19 };
            Console.WriteLine("The num of occurance of 13 is {0}. Expected: {1}", occurance.GetOccurances(arr, 13), 0);

            arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 12, 14, 15, 19 };
            Console.WriteLine("The num of occurance of 20 is {0}. Expected: {1}", occurance.GetOccurances(arr, 20), 0);

            arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 12, 14, 15, 19 };
            Console.WriteLine("The num of occurance of 0 is {0}. Expected: {1}", occurance.GetOccurances(arr, 0), 0);

            arr = new int[] { 1, 2, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 19 };
            Console.WriteLine("The num of occurance of 4 is {0}. Expected: {1}", occurance.GetOccurances(arr, 4), 9);
            
            arr = new int[] { 1, 2, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 19, 20 };
            Console.WriteLine("The num of occurance of 4 is {0}. Expected: {1}", occurance.GetOccurances(arr, 4), 9);

            arr = new int[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 10 };
            Console.WriteLine("The num of occurance of 4 is {0}. Expected: {1}", occurance.GetOccurances(arr, 4), 9);
            
            arr = new int[] { 1, 2, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
            Console.WriteLine("The num of occurance of 4 is {0}. Expected: {1}", occurance.GetOccurances(arr, 4), 9);
            
            arr = new int[] { 4, 4, 4, 4, 4, 4, 4, 4, 4 };
            Console.WriteLine("The num of occurance of 4 is {0}. Expected: {1}", occurance.GetOccurances(arr, 4), 9);

            arr = new int[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
            Console.WriteLine("The num of occurance of 4 is {0}. Expected: {1}", occurance.GetOccurances(arr, 4), 10);

        }
    }
}
