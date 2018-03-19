using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Search
{
    /// <summary>
    /// Given an array which is sorted and rotated. Search for an element in the array
    /// and return its index in the array
    /// </summary>
    class SearchInSortedRotatedArray
    {
        #region Algo1 - Recursive approach
        /// <summary>
        /// We will use the binary search here and get the running time to be O(logn).
        /// Check in which side the pivot element(element where the sorted array starts if the array was not rotated).
        /// And based on whether searchVal is less than or greater than arr[mid] and other conditions we need to make
        /// a decision whether to search in left or right subarray.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="searchVal"></param>
        /// <returns></returns>
        public int Search(int[] arr, int start, int end, int searchVal)
        {
            if(start>end)
            {
                // Error condition
                return -1;
            }
            int mid = end - ((end - start) / 2);
            if (arr[mid] == searchVal)
            {
                return mid;
            }
            if (arr[mid] < arr[end])
            {
                // arr[mid - end] is sorted and the pivot point is in the arr[start - mid]
                if(searchVal < arr[mid] || searchVal > arr[end])
                {
                    // Search Left
                    return Search(arr, start, mid - 1, searchVal);
                }
                else
                {
                    //Search Right
                    return Search(arr, mid + 1, end, searchVal);
                }
            }
            else
            {
                // arr[start - mid] is sorted and the pivot point is in arr[mid-end]
                if(searchVal > arr[mid] || searchVal<arr[start])
                {
                    // Search Right
                    return Search(arr, mid + 1, end, searchVal);
                }
                else
                {
                    // Search Left
                    return Search(arr, start, mid - 1, searchVal);
                }
            }

        }
        #endregion

        #region Algo2 - iterative approach

        public int SearchIterative(int[] arr, int searchVal)
        {
            if(arr == null || arr.Length == 0)
            {
                throw new ArgumentException("The array is not valid");
            }
            int low = 0;
            int high = arr.Length - 1;

            while(low <= high)
            {
                int mid = low + ((high - low) / 2);
                if(arr[mid] == searchVal)
                {
                    return mid; // Element found
                }
                
                if(arr[high] >= arr[mid])
                {
                    if(searchVal > arr[mid] && searchVal <= arr[high])
                    {
                        low = mid + 1;//Search right
                    }
                    else
                    {
                        high = mid - 1;//Search left
                    }
                }
                else if(arr[low] <= arr[mid])
                {
                    if(searchVal >= arr[low] && searchVal < arr[mid])
                    {
                        high = mid - 1;//search left
                    }
                    else
                    {
                        low = mid + 1; // Search right
                    }
                }
            }

            return -1; // Element is not found
        }

        #endregion

        public static void TestSearchInSortedRotatedArray()
        {
            int[] arr = new int[] { 3, 4, 5, 6, 7, 8, 9, 1, 2 };
            int searchVal = 2;
            SearchInSortedRotatedArray search = new SearchInSortedRotatedArray();
            Console.WriteLine("The {0} is present at index {1}. Expected: 8", searchVal, search.Search(arr, 0, arr.Length - 1, searchVal));
            Console.WriteLine("The {0} is present at index {1}. Expected: 8", searchVal, search.SearchIterative(arr, searchVal));

            arr = new int[] { 3, 4, 5, 6, 7, 8, 9, 1, 2 };
            searchVal = 9;
            Console.WriteLine("The {0} is present at index {1}. Expected: 6", searchVal, search.Search(arr, 0, arr.Length - 1, searchVal));
            Console.WriteLine("The {0} is present at index {1}. Expected: 6", searchVal, search.SearchIterative(arr, searchVal));

            arr = new int[] { 3, 4, 5, 6, 7, 8, 9, 1, 2 };
            searchVal = 4;
            Console.WriteLine("The {0} is present at index {1}. Expected: 1", searchVal, search.Search(arr, 0, arr.Length - 1, searchVal));
            Console.WriteLine("The {0} is present at index {1}. Expected: 1", searchVal, search.SearchIterative(arr, searchVal));

            arr = new int[] { 8, 9, 1, 2, 3, 4, 5, 6, 7 };
            searchVal = 1;
            Console.WriteLine("The {0} is present at index {1}. Expected: 2", searchVal, search.Search(arr, 0, arr.Length - 1, searchVal));
            Console.WriteLine("The {0} is present at index {1}. Expected: 2", searchVal, search.SearchIterative(arr, searchVal));

            arr = new int[] { 8, 9, 1, 2, 3, 4, 5, 6, 7 };
            searchVal = 8;
            Console.WriteLine("The {0} is present at index {1}. Expected: 0", searchVal, search.Search(arr, 0, arr.Length - 1, searchVal));
            Console.WriteLine("The {0} is present at index {1}. Expected: 0", searchVal, search.SearchIterative(arr, searchVal));

            arr = new int[] { 8, 9, 1, 2, 3, 4, 5, 6, 7 };
            searchVal = 6;
            Console.WriteLine("The {0} is present at index {1}. Expected: 7", searchVal, search.Search(arr, 0, arr.Length - 1, searchVal));
            Console.WriteLine("The {0} is present at index {1}. Expected: 7", searchVal, search.SearchIterative(arr, searchVal));

            arr = new int[] { 2, 3, 3, 4, 4, 4, 4, 4, 4, 4 };
            searchVal = 2;
            Console.WriteLine("The {0} is present at index {1}. Expected: 0", searchVal, search.Search(arr, 0, arr.Length - 1, searchVal));
            Console.WriteLine("The {0} is present at index {1}. Expected: 0", searchVal, search.SearchIterative(arr, searchVal));

            arr = new int[] { 2, 3, 3, 4, 4, 4, 4, 4, 4, 4 };
            searchVal = 4;
            Console.WriteLine("The {0} is present at index {1}. Expected: [3-9]", searchVal, search.Search(arr, 0, arr.Length - 1, searchVal));
            Console.WriteLine("The {0} is present at index {1}. Expected: [3-9]", searchVal, search.SearchIterative(arr, searchVal));

            arr = new int[] { 2, 3, 3, 4, 4, 4, 4, 4, 4, 4 };
            searchVal = 8;
            Console.WriteLine("The {0} is present at index {1}. Expected: -1", searchVal, search.Search(arr, 0, arr.Length - 1, searchVal));
            Console.WriteLine("The {0} is present at index {1}. Expected: -1", searchVal, search.SearchIterative(arr, searchVal));

        }
    }
}
