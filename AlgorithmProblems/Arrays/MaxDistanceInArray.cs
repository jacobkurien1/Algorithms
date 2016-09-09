using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Arrays
{
    /// <summary>
    /// Get the maximum distance between 2 elements in the array such that the larger element comes after the smaller element.
    /// For eg [3,5,6,7,81,1,2,101] should give a maximum distance of 100 (101 - 1)
    /// </summary>
    class MaxDistanceInArray
    {
        /// <summary>
        /// We will keep track of the min value seen till now and the maxDifference
        /// and keep updating the maxDifference when it is less than the currentDifference.
        /// Also when arr[i]<minValue then we should update the minValue and make currentDifference 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int GetMaxDistanceInArray(int[] arr)
        {
            int min = int.MaxValue;
            int maxDifference = 0;
            int currentDifference = 0;
            for(int i=0; i<arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    //update min
                    min = arr[i];
                    currentDifference = 0;
                }
                if(arr[i] - min > currentDifference )
                {
                    currentDifference = arr[i] - min;
                    if(maxDifference<currentDifference)
                    {
                        // we have a new max difference
                        maxDifference = currentDifference;
                    }
                }

            }

            return maxDifference;
        }
        public static void TestMaxDistanceInArray()
        {
            int[] arr = new int[] { 3, 5, 6, 7, 81, 1, 2, 101 };
            Console.WriteLine("The max difference is {0}. Expected:100", GetMaxDistanceInArray(arr));

            arr = new int[] { 1 };
            Console.WriteLine("The max difference is {0}. Expected:0", GetMaxDistanceInArray(arr));

            arr = new int[] { 1, 1 };
            Console.WriteLine("The max difference is {0}. Expected:0", GetMaxDistanceInArray(arr));
        }
    }
}
