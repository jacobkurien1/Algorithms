using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Sorting
{
    /// <summary>
    /// At the first iteration get the smallest element(by comparing with all the other elements)
    /// and place it at 0th position. In the next iteration, get the second smallest element
    /// and place it at 1st position.
    /// 
    /// The running time - O(n^2)
    /// The space requirement is O(1)
    /// </summary>
    class SelectionSort
    {
        public static int[] Sort(int[] arr)
        {
            for(int i=0; i< arr.Length; i++)
            {
                for (int j=i+1; j<arr.Length; j++)
                {
                    if(arr[j]<arr[i])
                    {
                        Swap(arr, i, j);
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
