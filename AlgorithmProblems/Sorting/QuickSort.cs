using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Sorting
{
    class QuickSort
    {

        private static void QuickSortAlgo(ref int[] arr, int startIndex, int endIndex)
        {
            // Base case
            if(startIndex >= endIndex)
            {
                return;
            }
            // Get the pivot Index
            Random rnd = new Random();
            int pivotIndex = rnd.Next(startIndex, endIndex + 1);

            // parition the array arr based on the pivot arr[pivotIndex]
            int finalPivotIndex = Partition(ref arr, startIndex, endIndex, pivotIndex);

            // Now do quick sort on the 2 halves arr[startIndex, pivotIndex-1] and arr[pivotIndex+1, endIndex]
            QuickSortAlgo(ref arr, startIndex, finalPivotIndex - 1);
            QuickSortAlgo(ref arr, finalPivotIndex + 1, endIndex);
        }

        /// <summary>
        /// This partitions the array arr based on the pivotElement.
        /// The final pivotElement index is at startIndex.
        /// All elements lesser or equal to the pivot element will be on its left
        /// and the ones greater than the pivot element will be on its right
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="pivotIndex"></param>
        /// <returns>the final position of the pivot element(We always keep the pivot element in the start index)</returns>
        private static int Partition(ref int[] arr, int startIndex, int endIndex, int pivotIndex)
        {
            // Put the partition element at startIndex 
            // We will always store the pivot element in the startIndex
            SwapInArray(ref arr, startIndex, pivotIndex);
            
            while(endIndex>startIndex)
            {
                if (arr[startIndex] >= arr[startIndex+1])
                {
                    SwapInArray(ref arr, startIndex, startIndex+1);
                    startIndex++;
                }
                else
                {
                    SwapInArray(ref arr, endIndex, startIndex + 1);
                    endIndex--;
                }
            }
            return startIndex;
        }

        /// <summary>
        /// This will help to swap the values at 2 indices(indexToSwap1 and indexToSwap2) in array arr.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="indexToSwap1"></param>
        /// <param name="indexToSwap2"></param>
        private static void SwapInArray(ref int[] arr, int indexToSwap1, int indexToSwap2)
        {
            int temp = arr[indexToSwap1];
            arr[indexToSwap1] = arr[indexToSwap2];
            arr[indexToSwap2] = temp;
        }

        public static void TestQuickSort()
        {
            int[] arr = ArrayHelper.CreateArray(10);
            Console.WriteLine("The array before sorting is :");
            ArrayHelper.PrintArray(arr);

            QuickSortAlgo(ref arr, 0, arr.Length - 1);
            Console.WriteLine("The array after sorting is :");
            ArrayHelper.PrintArray(arr);
        }
    }
}
