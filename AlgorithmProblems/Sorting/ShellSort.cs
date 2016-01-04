using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Sorting
{
    /// <summary>
    /// This uses insertion sort, but is more performant.
    /// 
    /// We will partition the original list into sublist. 
    /// The sublist will not be contiguous. We will get an increment and get all element with that increment.
    /// For eg, if increment is 2, 0,2,4,6,8 is one sublist and the other is 1,3,5,7.
    /// 
    /// Do the insertion sort on the sublist.
    /// Decrease the increment value by 1 till the final increment value is 1.
    /// The final sublist containing all the elements will be sorted
    /// 
    /// This is also an adaptive algo as insertion sort is adaptive
    /// 
    /// Getting the exact running time is difficult as it depends on the increment value.
    /// And there is no science behind choosing the increment value.
    /// 
    /// The running time is between O(N) to O(N^2)
    /// The space requirement is O(1)
    /// 
    /// </summary>
    class ShellSort
    {
        public static int[] Sort(int[] arr)
        {
            // We have picked an increment in random
            int increment = arr.Length / 2;
            while(increment >=1)
            {
                for(int startIndex = 0; startIndex<increment; startIndex++)
                {
                    ModifiedInsertionSort(arr, startIndex, increment);
                }
                increment = increment/2;
            }
            return arr;
        }

        public static void ModifiedInsertionSort(int[] arr, int startIndex, int increment)
        {
            for(int i=startIndex; i<arr.Length; i = i+ increment)
            {
                int indexOfNewElement = i;
                for(int j= i-increment; j>=0; j = j-increment)
                {
                    if (arr[indexOfNewElement] < arr[j])
                    {
                        Swap(arr, indexOfNewElement, j);
                        indexOfNewElement = j;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Swap the elements in the 2 different index in the array arr
        /// </summary>
        /// <param name="arr">array are reference types, hence changing value of array here will be reflected in the original method making the call</param>
        /// <param name="index1ToSwap">first index whose element needs to be swapped with index2ToSwap</param>
        /// <param name="index2ToSwap">second index whose element needs to be swapped with index1ToSwap</param>
        private static void Swap(int[] arr, int index1ToSwap, int index2ToSwap)
        {
            int temp = arr[index1ToSwap];
            arr[index1ToSwap] = arr[index2ToSwap];
            arr[index2ToSwap] = temp;
        }

        public static void TestSorting()
        {
            int[] arr = ArrayHelper.CreateArray(10);
            Console.WriteLine("The unsorted array is as shown below:");
            ArrayHelper.PrintArray(arr);
            arr = Sort(arr);
            Console.WriteLine("The sorted array is as shown below:");
            ArrayHelper.PrintArray(arr);
        }
    }
}
