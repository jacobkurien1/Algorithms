using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Arrays
{
    /// <summary>
    /// You have 2 sorted arrays arr1 and arr2. arr1 is large enough to hold arr2.
    /// We need to merge the 2 sorted arrays to get the final sorted array.
    /// </summary>
    class Merge2SortedArrays
    {
        /// <summary>
        /// Start from the end and do it inline
        /// </summary>
        /// <param name="arr1">larger array which can hold arr2</param>
        /// <param name="arr2">smaller array which needs to be merged with arr1</param>
        /// <returns>merged array which is basically arr1</returns>
        private static int[] MergeSortedArrays(int[] arr1, int[] arr2)
        {
            int index2 = arr2.Length - 1;
            int mergedArrIndex = arr1.Length - 1;
            int index1 = mergedArrIndex - index2 - 1;

            while(index1 >= 0 && index2 >= 0)
            {
                if(arr1[index1] > arr2[index2])
                {
                    arr1[mergedArrIndex--] = arr1[index1--];
                }
                else
                {
                    arr1[mergedArrIndex--] = arr2[index2--];
                }
            }

            while (index2 >= 0) 
            {
                arr1[mergedArrIndex--] = arr2[index2--];
            }

            return arr1;
        }

        public static void TestMergeSortedArrays()
        {
            int[] largerArr = new int[10] { 5, 6, 7, 8, 0, 0, 0, 0, 0, 0 };
            int[] smallerArr = new int[6] { 1, 2, 3, 4, 5, 6 };
            Console.WriteLine("The larger array is : ");
            ArrayHelper.PrintArray(largerArr);
            Console.WriteLine("The smaller array is : ");
            ArrayHelper.PrintArray(smallerArr);

            int[] mergedArr = MergeSortedArrays(largerArr, smallerArr);
            Console.WriteLine("The merged array is : ");
            ArrayHelper.PrintArray(mergedArr);

            largerArr = new int[10] { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            smallerArr = new int[9] { 1, 2, 3, 4, 6, 7, 8, 9, 10 };
            Console.WriteLine("The larger array is : ");
            ArrayHelper.PrintArray(largerArr);
            Console.WriteLine("The smaller array is : ");
            ArrayHelper.PrintArray(smallerArr);

            mergedArr = MergeSortedArrays(largerArr, smallerArr);
            Console.WriteLine("The merged array is : ");
            ArrayHelper.PrintArray(mergedArr);
        }
    }
}
