using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Search
{
    /// <summary>
    /// Get the smallest element index(infux point) in the sorted rotated array
    /// 
    /// In arr, 4,5,6,7,8,1,2,3 return 5
    /// In arr 6,7,8,1,2,3,4,5 return 3
    /// </summary>
    class GetSmallestInSortedRotatedArray
    {
        /// <summary>
        /// It is assummed that the array is sorted and rotated
        /// 
        /// Algo: 1. if arr[mid] > arr[high] then influx point is to the right
        /// 2. if arr[low] > arr[mid] then influx point is to the left
        /// 3. if 1 and 2 are not hit then the array is either not rotated or has all same elements
        /// 
        /// Running time O(logn)
        /// Space O(1)
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int GetInfluxPoint(int[] arr)
        {
            if(arr == null || arr.Length == 0)
            {
                throw new ArgumentException("The input array is not valid");
            }

            int low = 0;
            int high = arr.Length - 1;
            while (low<high)
            {
                int mid = low + ((high - low) / 2);
                if(arr[high] < arr[mid])
                {
                    low = mid + 1;
                }
                else if(arr[low] > arr[mid])
                {
                    high = mid;
                }
                else
                {
                    // the array is not sorted or all elements are equal
                    high = mid - 1;
                }
            }
            return low;
        }
        public static void TestGetSmallestInSortedRotatedArray()
        {
            int[] arr = new int[] { 4, 5, 6, 7, 8, 1, 2, 3 };
            Console.WriteLine("The influx point is at{0}. Expected {1}", GetInfluxPoint(arr), 5);

            arr = new int[] { 4, 5, 6, 7, 8, 1, 2, 3, 4 };
            Console.WriteLine("The influx point is at{0}. Expected {1}", GetInfluxPoint(arr), 5);

            arr = new int[] { 4, 5, 6, 7, 8, 1, 1, 2, 3, 4 };
            Console.WriteLine("The influx point is at{0}. Expected {1}", GetInfluxPoint(arr), 5);

            arr = new int[] { 6, 7, 8, 1, 2, 3, 4, 5 };
            Console.WriteLine("The influx point is at{0}. Expected {1}", GetInfluxPoint(arr), 3);

            arr = new int[] { 6, 7, 8, 1, 2, 3, 4, 5, 6 };
            Console.WriteLine("The influx point is at{0}. Expected {1}", GetInfluxPoint(arr), 3);

            arr = new int[] { 4,4,4,4,4,4,4,4,4,4,4,4,4 };
            Console.WriteLine("The influx point is at{0}. Expected {1}", GetInfluxPoint(arr), 0);
        }
    }
}
