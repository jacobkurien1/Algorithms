using AlgorithmProblems.Arrays.ArraysHelper;
using AlgorithmProblems.Heaps.HeapHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Sorting
{
    /// <summary>
    /// Like merge sort the Heap sort algorithm has O(nlogn) running time.
    /// But unlike merge sort it does the sort inplace and hence space is O(1).
    /// 
    /// This is a good algorithm but a good implementation of quick sort beats this algo
    /// 
    /// This uses a data structure called heap to achieve this goal
    /// For heap sort we will use MaxHeap
    /// </summary>
    public class HeapSort<T> : MaxHeap<T> where T :IComparable
    {
        private int _sortedIndex { get; set; }
        public HeapSort(T[] inputArr) : base(inputArr)
        {
            _sortedIndex = inputArr.Length - 1;
            for(int index = _sortedIndex; index>=0; index--)
            {
                Swap(inputArr, 0, index);
                MaxHeapifyWithLeafNode(inputArr, 0, index);
            }
        }
    }

    public class HeapSortTester
    {
        public static void TestHeapSort()
        {
            int[] inputArr = ArrayHelper.CreateArray(10);
            Console.WriteLine("The input array is as shown below:");
            ArrayHelper.PrintArray(inputArr);

            HeapSort<int> hs = new HeapSort<int>(inputArr);
            Console.WriteLine("The sorted array is as shown below:");
            ArrayHelper.PrintArray(hs.HeapArray);
        }
    }
}
