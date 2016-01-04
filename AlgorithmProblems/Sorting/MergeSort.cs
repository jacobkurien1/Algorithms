using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Sorting
{
    /// <summary>
    /// Write the merge sort algo.
    /// This is a divide and conquer approach.
    /// The running time is O(nlogn) in worst case and average case
    /// The space complexity is O(n)
    /// 
    /// Merge sort is not adaptive.
    /// </summary>
    class MergeSort
    {
        private static int[] Merge(int[] arr1, int[] arr2)
        {
            int[] mergedArr = new int[arr1.Length + arr2.Length];
            int index1 = 0;
            int index2 = 0;
            int mergedIndex = 0;

            while (index1 < arr1.Length && index2 < arr2.Length)
            {
                if (arr1[index1] < arr2[index2])
                {
                    mergedArr[mergedIndex++] = arr1[index1++];
                }
                else
                {
                    mergedArr[mergedIndex++] = arr2[index2++];
                }
            }

            //We need to take care of the case where the arr2 element is present in mergedArr 
            //but arr1 elements are still not present completly
            while (index1 < arr1.Length)
            {
                mergedArr[mergedIndex++] = arr1[index1++];
            }

            //We need to take care of the case where the arr1 element is present in mergedArr 
            //but arr2 elements are still not present completly
            while (index2 < arr2.Length)
            {
                mergedArr[mergedIndex++] = arr2[index2++];
            }

            return mergedArr;
        }

        private static int[] MergeSortAlgo(int[] arr, int start, int end)
        {
            if(end<start)
            {
                // return empty array
                return new int[0];
            }
            else if(end == start)
            {
                // we have only one element, just return it
                return new int[1] { arr[end] };
            }
            int mid = start + ((end - start) / 2); // prevents overflow
            int[] left = MergeSortAlgo(arr, start, mid);
            int[] right = MergeSortAlgo(arr, mid + 1, end);
            return Merge(left, right);
        }

        public static void TestMergeSort()
        {
            int[] arr = ArrayHelper.CreateArray(10);
            Console.WriteLine("The array before sorting is :");
            ArrayHelper.PrintArray(arr);

            arr = MergeSortAlgo(arr, 0, arr.Length - 1);
            Console.WriteLine("The array after sorting is :");
            ArrayHelper.PrintArray(arr);
        }
    }
}
