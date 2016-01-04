using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Sorting
{
    /// <summary>
    /// Start from the end of the array and swap whenever a[i] > a[j] where i<j
    /// This operation will bubble up the smallest element in the 0th position.
    /// Do this iteration again from arr.Length - 1 to 1 and this will get the next smallest
    /// element in the 1th index
    /// 
    /// The interesting thing here is that after 1 or 2 iteration the entire list is sorted
    /// In one iteration if we do not do any swap, then we dont have to do any more iteration
    /// This makes the sorting adaptive.
    /// 
    /// Note: the number of swaps in a bubble sort is greater than the swaps in a selection sort
    /// 
    /// The running time is o(n^2), but this is better than selection sort due to its adaptive nature.
    /// The space requirement is O(1)
    /// </summary>
    class BubbleSort
    {
        public static int[] Sort(int[] arr)
        {
            for(int i = arr.Length -1; i>=0; i--)
            {
                //IsAnySwapPerformed will track whether a swap is performed or not
                // if a swap is not performed then we can conclude that the list is sorted
                bool IsAnySwapPerformed = false;
                for(int j=arr.Length -1; j>arr.Length -1 -i; j--)
                {
                    if(arr[j] < arr[j-1])
                    {
                        IsAnySwapPerformed = true;
                        Swap(arr, j, j - 1);
                    }
                }
                if(!IsAnySwapPerformed)
                {
                    // The list is sorted.
                    // Bubble sort is adaptive!!
                    break;
                }
            }
            return arr;
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
