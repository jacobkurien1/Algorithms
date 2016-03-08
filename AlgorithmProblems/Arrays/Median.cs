using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Arrays
{
    /// <summary>
    /// Get the median of 2 sorted arrays
    /// </summary>
    class Median
    {
        /// <summary>
        /// Algo1
        /// Merge both the array and find the  finalArrLength = arr1.Length + arr2.Length 
        /// if finalArrLength is odd, we get the (finalArrLength/2) and get the elememt in that index
        /// if finalArrLength is even, we get the average of elements at index finalArrLength/2 and ((finalArrLength/2)-1)
        /// 
        /// The time complexity of this approach is O(n)
        /// </summary>
        /// <param name="arr1">sorted arr1</param>
        /// <param name="arr2">sorted arr2</param>
        /// <returns>median of 2 arrays arr1 and arr2</returns>
        private static double GetMedianOf2SortedArrayAlgo1(int[] arr1, int[] arr2)
        {
            int finalArrLength = arr1.Length + arr2.Length;
            int index1 = 0;
            int index2 = 0;
            int mergedIndex = 0;
            int previous = 0;
            int current = 0;

            while(index1 < arr1.Length && index2 < arr2.Length)
            {
                
                if(arr1[index1] < arr2[index2])
                {
                    current = arr1[index1];
                    index1++;
                }
                else
                {
                    current = arr2[index2];
                    index2++;
                }
                if (mergedIndex == finalArrLength / 2)
                {
                    return GetMedian(previous, current, finalArrLength % 2 == 0);
                }
                mergedIndex++;
                previous = current;
            }
            // Case where we have not traversed the complete arr1
            while (index1 < arr1.Length)
            {
                current = arr1[index1];
                if (mergedIndex == finalArrLength / 2)
                {
                    return GetMedian(previous, current, finalArrLength % 2 == 0);
                }
                index1++;
                mergedIndex++;
                previous = current;

            }
            while (index2 < arr2.Length)
            {
                current = arr2[index2];
                if (mergedIndex == finalArrLength / 2)
                {
                    return GetMedian(previous, current, finalArrLength % 2 == 0);
                }
                index2++;
                mergedIndex++;
                previous = current;
            }

            //we did not find the median
            return 0;
        }

        /// <summary>
        /// Subroutine for code sharing in GetMedianOf2SortedArrayAlgo1() method
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="current"></param>
        /// <param name="IsEven"></param>
        /// <returns></returns>
        private static double GetMedian(int previous, int current, bool IsEven)
        {
            if (IsEven)
            {
                //even
                return (previous + current) / 2.0;
            }
            else
            {
                //odd
                return current;
            }
        }

        /// <summary>
        /// We will use divide and conquer approach to reduce the time significantly
        /// 
        /// find the median in both arr1 and arr2,
        /// if median1 == median2 return median1
        /// if median1 > median2 , the median of the merged array lies in arr1[0, medianIndex] or arr2[medianIndex, arr2.Length -1]
        /// if median1 < median2 , the median of the merged array lies in arr1[medianIndex, arr1.Length -1] or arr2[0, medianIndex] 
        /// 
        /// An easier version will be the recursive version of this function.
        /// 
        /// The running time of this approach is O(log(n))
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <returns></returns>
        private static double GetMedianOf2SortedArrayAlgo2(int[] arr1, int[] arr2)
        {
            int finalLength = arr1.Length + arr2.Length;
            int startIndex1 = 0;
            int endIndex1 = arr1.Length - 1;
            int startIndex2 = 0;
            int endIndex2 = arr2.Length - 1;

            while(startIndex1<endIndex1 && startIndex2<endIndex2)
            {
                // The conditions where the median needs 2 element as the finalLength is even
                // and one of the element is in arr1 and the other is in arr2
                if(endIndex1-startIndex1 == 1 && endIndex2-startIndex2 ==1 && finalLength%2==0)
                {
                    return (Math.Max(arr1[startIndex1], arr2[startIndex2]) + Math.Min(arr1[endIndex1], arr2[endIndex2])) / 2.0;
                }
                if(endIndex1-startIndex1 == 0 && endIndex2-startIndex2 ==0 && finalLength%2==0)
                {
                    return (arr1[startIndex1] + arr2[startIndex2])/2.0;
                }

                int medianIndex1Start = 0;
                int medianIndex1End = 0;
                double median1 = GetMedian(arr1, startIndex1, endIndex1, out medianIndex1Start, out medianIndex1End);

                int medianIndex2Start = 0;
                int medianIndex2End = 0;
                double median2 = GetMedian(arr2, startIndex2, endIndex2, out medianIndex2Start, out medianIndex2End);

                if(median1==median2)
                {
                    return median2;
                }
                else if(median1>median2)
                {
                    //endIndex1 = (medianIndex1End == -1) ? medianIndex1Start : medianIndex1End;
                    //startIndex2 = (medianIndex2End == -1) ? medianIndex2Start : medianIndex2End;
                    endIndex1 = medianIndex1Start;
                    startIndex2 = (medianIndex2End == -1) ? medianIndex2Start : medianIndex2End;
                }
                else //median1<median2
                {
                    //startIndex1 = (medianIndex1End == -1) ? medianIndex1Start : medianIndex1End;
                    //endIndex2 = (medianIndex2End == -1) ? medianIndex2Start : medianIndex2End;
                    startIndex1 = (medianIndex1End == -1) ? medianIndex1Start : medianIndex1End;
                    endIndex2 = medianIndex2Start;
                }
            }
            if (endIndex1 - startIndex1 == 0 && endIndex2 - startIndex2 == 0 && finalLength % 2 == 0)
            {
                return (arr1[startIndex1] + arr2[startIndex2]) / 2.0;
            }
            if (startIndex1 < endIndex1)
            { 
                // This means that the median is present in arr1
                return GetMedian(arr1, startIndex1, endIndex1);
            }
            if (startIndex2 < endIndex2)
            {
                // This means that the median is present in arr2
                return GetMedian(arr2, startIndex2, endIndex2);
            }
            // We were not able to get the median
            return 0;
        }

        /// <summary>
        /// This is a subroutine to help the GetMedianOf2SortedArrayAlgo2
        /// 
        /// </summary>
        /// <param name="arr">array whose median needs to be found</param>
        /// <param name="startIndex">start index of arr from which we need to calculate the median</param>
        /// <param name="endIndex">end index of arr till which we need to calculate the median<</param>
        /// <param name="medianIndex1">index of the median(if the number of elements are odd). If the number of elements are even, then this is the index of the first median element</param>
        /// <param name="medianIndex2">this will be -1, if the number of elements are odd, else it will be the index of the second median element</param>
        /// <returns>median of the array arr</returns>
        private static double GetMedian(int[]arr, int startIndex, int endIndex, out int medianIndex1, out int medianIndex2)
        {
            int length = endIndex - startIndex + 1;
            if(length%2 == 0)
            {
                // number of elements are even
                medianIndex2 = length / 2;
                medianIndex1 = medianIndex2 - 1;
                return (arr[medianIndex1] + arr[medianIndex2]) / 2.0;
            }
            else
            {
                // number of elements are odd
                medianIndex1 = length / 2;
                medianIndex2 = -1;
                return arr[medianIndex1];
            }
        }

        /// <summary>
        /// Overloading the above method so that it works without giving out the medianIndex1 and medianIndex2
        /// </summary>
        /// <param name="arr">array whose median needs to be found</param>
        /// <param name="startIndex">start index of arr from which we need to calculate the median</param>
        /// <param name="endIndex">end index of arr till which we need to calculate the median<</param>
        /// <returns>median of the array arr</returns>
        private static double GetMedian(int[] arr, int startIndex, int endIndex)
        {
            int medianIndex1 = 0;
            int medianIndex2 = 0;
            return GetMedian(arr, startIndex, endIndex, out medianIndex1, out medianIndex2);
        }

        public static void TestGetMedianOf2SortedArray()
        {
            int[] arr1 = new int[5] { 1, 3, 5, 7, 8 };
            int[] arr2 = new int[2] { 2, 4 };
            Console.WriteLine("The median from algo1 is {0}", GetMedianOf2SortedArrayAlgo1(arr1, arr2));
            Console.WriteLine("The median from algo2 is {0}", GetMedianOf2SortedArrayAlgo2(arr1, arr2));

            int[] arr3 = new int[5] { 1, 3, 5, 7, 9 };
            int[] arr4 = new int[5] { 2, 4, 6, 8, 20 };
            Console.WriteLine("The median from algo1 is {0}", GetMedianOf2SortedArrayAlgo1(arr3, arr4));
            Console.WriteLine("The median from algo2 is {0}", GetMedianOf2SortedArrayAlgo2(arr3, arr4));

            int[] arr5 = new int[3] { 1,2,3};
            int[] arr6 = new int[5] { 6,7,8,9,10 };
            Console.WriteLine("The median from algo1 is {0}", GetMedianOf2SortedArrayAlgo1(arr5, arr6));
            Console.WriteLine("The median from algo2 is {0}", GetMedianOf2SortedArrayAlgo2(arr5, arr6));

            int[] arr7 = new int[5] { 1, 2, 3, 4, 5 };
            int[] arr8 = new int[2] { 7,8 };
            Console.WriteLine("The median from algo1 is {0}", GetMedianOf2SortedArrayAlgo1(arr7, arr8));
            Console.WriteLine("The median from algo2 is {0}", GetMedianOf2SortedArrayAlgo2(arr7, arr8));
        }
    }
}
