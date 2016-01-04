using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Sorting
{
    /// <summary>
    /// We start with a sublist of size 1 and say it is sorted
    /// We look at the second element and see where it can be inserted in the sorted list
    /// Now we have 2 elements in our sorted list.
    /// We take the 3rd element and find the place where we need to insert this element. And so on.
    /// 
    /// In other words we do this by inserting into a sorted sublist at every step and soon the sorted
    /// sublist grows to be the entire list.
    /// 
    /// The running time is O(n^2)
    /// The space complexity is O(n^2)
    /// Insertion sort is also adaptive, ie if a list is almost sorted, then we break out of the loop faster.
    /// 
    /// How is insertion sort better than bubble sort?
    /// 1. Bubble sort requires an extra pass over all elements to make sure that the list is sorted
    /// 2. Bubble sort has to do n comparisons at each step. Insertion sort can stop once we find the right place for the new element
    /// So in avg cases insertion sort needs less # of comparisons than bubble sort
    /// 3. Bubble sorts needs more writes and swaps in comparison to the insertion sort in the average case.
    /// </summary>
    class InsertionSort
    {
        public static int[] Sort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int indexOfNewElement = i;
                for(int j = i-1 ; j>=0; j--)
                {
                    if(arr[indexOfNewElement] < arr[j])
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
