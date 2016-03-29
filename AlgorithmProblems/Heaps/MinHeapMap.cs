using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Heaps
{
    /// <summary>
    /// This data structure is a combination of a Minheap and map
    /// This data structure is used in Dijkstra's shortest path algorithm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MinHeapMap<T> where T : IComparable
    {
        /// <summary>
        /// This will help to get the O(1) lookup for all the entities's index in heap
        /// We need a list<int> to take care of duplicates in the value type
        /// </summary>
        private Dictionary<T, List<int>> AllEntitiesIndex { get; set; }

        /// <summary>
        /// we will store the complete binary tree in this array
        /// 0th index gives the root element
        /// So in array: index of left subtree of ith node= 2*i +1
        ///              index of right subtree of the ith node = 2*i+2;
        ///              parent of the ith node = Math.Floor((i-1)/2)
        /// </summary>
        private T[] _arrayToStoreTree { get; set; }

        private void ChangeValueAtIndex(int arrayIndex, T newVal)
        {
            if(AllEntitiesIndex.ContainsKey(_arrayToStoreTree[arrayIndex]))
            {
                AllEntitiesIndex[_arrayToStoreTree[arrayIndex]].Remove(arrayIndex);
            }
            _arrayToStoreTree[arrayIndex] = newVal;
            if(AllEntitiesIndex.ContainsKey(newVal))
            {
                AllEntitiesIndex[newVal].Add(arrayIndex);
            }
            else
            {
                AllEntitiesIndex[newVal] = new List<int> { arrayIndex };
            }
        }
        private int _currentNumberOfElements { get; set; }

        public MinHeapMap(int size)
        {
            AllEntitiesIndex = new Dictionary<T, List<int>>();  
            _arrayToStoreTree = new T[size];
        }

        public T[] HeapArray
        {
            get
            {
                T[] heapArray = new T[_currentNumberOfElements];
                for (int index = 0; index < _currentNumberOfElements; index++)
                {
                    heapArray[index] = _arrayToStoreTree[index];
                }
                return heapArray;
            }
        }

        /// <summary>
        /// This is basically used to change the value of the array at index and 
        /// rebalance the tree to follow the min heap property
        /// </summary>
        /// <param name="newVal"></param>
        /// <param name="index"></param>
        public void ChangePriority(T newVal, int index)
        {
            if (index < 0 || index >= _currentNumberOfElements)
            {
                throw new Exception("Illegal operation");
            }
            ChangeValueAtIndex(index, newVal);
            while (index >= 0)
            {
                MinHeapify(index);
                index = (int)Math.Floor((index - 1) / 2.0);
            }
        }

        public void ChangePriority(T oldVal, T newVal)
        {
            if (!AllEntitiesIndex.ContainsKey(oldVal))
            {
                throw new Exception("Illegal operation");
            }
            //get index in which old val is present
            int oldIndex = AllEntitiesIndex[oldVal][0];
            ChangeValueAtIndex(oldIndex, newVal);
            while (oldIndex >= 0)
            {
                MinHeapify(oldIndex);
                oldIndex = (int)Math.Floor((oldIndex - 1) / 2.0);
            }
        }

        /// <summary>
        /// This method assumes that the left and right subtree from node at index 
        /// is following the min heap property but arr[i] might not be following the 
        /// minheap property correctly.
        /// 
        /// So we get the index in which the smallest value is present and if smallestIndex != index,
        /// we swap the value at index with value at smallestIndex and now we need to call MinHeapify again.
        /// 
        /// The running time is height of the tree = O(logn)
        /// The space requirement is O(1)
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="index"></param>
        private void MinHeapify(int index)
        {
            int leftSubtreeIndex = 2 * index + 1; // we can also use the shift all bits to left to achieve this
            int rightSubtreeIndex = 2 * index + 2;

            int smallestIndex = index;
            if (leftSubtreeIndex < _currentNumberOfElements && _arrayToStoreTree[leftSubtreeIndex].CompareTo(_arrayToStoreTree[smallestIndex]) < 0)
            {
                smallestIndex = leftSubtreeIndex;
            }
            if (rightSubtreeIndex < _currentNumberOfElements && _arrayToStoreTree[rightSubtreeIndex].CompareTo(_arrayToStoreTree[smallestIndex]) < 0)
            {
                smallestIndex = rightSubtreeIndex;
            }

            if (smallestIndex != index)
            {
                // this will make sure that the recursion converges
                Swap(index, smallestIndex);
                MinHeapify(smallestIndex);
            }
        }

        /// <summary>
        /// Add the value to the end of the array as a leaf node and keep checking the parent
        /// if the parent is greater than the newVal then swap parent and newVal.
        /// 
        /// The running time for this operation is O(logn)
        /// </summary>
        /// <param name="newVal"></param>
        public void Insert(T newVal)
        {
            if (_arrayToStoreTree.Length == _currentNumberOfElements)
            {
                throw new Exception("Heap overflow");
            }
            ChangeValueAtIndex(_currentNumberOfElements, newVal);
            int currentIndex = _currentNumberOfElements;

            // the parent of currentIndex will be present at (currentIndex-1)/2
            while (currentIndex != (currentIndex - 1) / 2 && _arrayToStoreTree[(currentIndex - 1) / 2].CompareTo(_arrayToStoreTree[currentIndex]) > 0)
            {
                Swap((currentIndex - 1) / 2, currentIndex);
                currentIndex = (currentIndex - 1) / 2;
            }
            _currentNumberOfElements++;
        }

        /// <summary>
        /// To get the min element get the element at index 0.
        /// this element is the root of the tree and has the minimum value
        /// 
        /// The running time of this operation is O(1)
        /// </summary>
        /// <returns></returns>
        public T PeekMin()
        {
            if (_currentNumberOfElements == 0)
            {
                throw new Exception("The heap is empty");
            }
            return _arrayToStoreTree[0];
        }

        /// <summary>
        /// this would get the min value and delete the value from the heap.
        /// 
        /// The running time for this operation is O(logn)
        /// </summary>
        /// <returns></returns>
        public T ExtractMin()
        {
            if (_currentNumberOfElements == 0)
            {
                throw new Exception("The heap is empty");
            }
            T retVal = _arrayToStoreTree[0];
            _arrayToStoreTree[0] = _arrayToStoreTree[_currentNumberOfElements - 1];
            _arrayToStoreTree[_currentNumberOfElements - 1] = default(T);
            if(AllEntitiesIndex[retVal].Count>1)
            {
                // Duplicate case: we have more than one index with the same value type
                AllEntitiesIndex[retVal].Remove(0);
            }
            else
            {
                // we can safely remove the whole object from the dictionary
                AllEntitiesIndex.Remove(retVal);
            }
            _currentNumberOfElements--;
            MinHeapify(0);
            return retVal;
        }

        private void Swap(int index1, int index2)
        {
            T temp = _arrayToStoreTree[index1];
            ChangeValueAtIndex(index1, _arrayToStoreTree[index2]);
            ChangeValueAtIndex(index2, temp);
        }

    }

    public class TestMinHeapMap
    {
        public static void DoTest()
        {
            // ---------------------insertion and Peek min value----------------------------
            int[] arr = ArrayHelper.CreateArray(10);
            Console.WriteLine("The array before heapifying");
            ArrayHelper.PrintArray(arr);
            MinHeapMap<int> mhm = new MinHeapMap<int>(10);
            for(int i=0; i<arr.Length; i++)
            {
                
                mhm.Insert(arr[i]);
                ArrayHelper.PrintArray(mhm.HeapArray);
                Console.WriteLine("The min value in the heap is {0}", mhm.PeekMin());

            }
            // -------------------change priority -----------------------------------------------
            Console.WriteLine("Change {0} -> {1}", mhm.HeapArray[4], 888);
            mhm.ChangePriority(oldVal: mhm.HeapArray[4], newVal: 888);
            ArrayHelper.PrintArray(mhm.HeapArray);

            Console.WriteLine("Change {0} -> {1}", mhm.HeapArray[4], -1);
            mhm.ChangePriority(oldVal: mhm.HeapArray[4], newVal: -1);
            ArrayHelper.PrintArray(mhm.HeapArray);

            Console.WriteLine("Change {0} -> {1}", mhm.HeapArray[4], 21);
            mhm.ChangePriority(oldVal: mhm.HeapArray[4], newVal: 21);
            ArrayHelper.PrintArray(mhm.HeapArray);

            // ------------------extract min value------------------------------------------
            for (int i = 0; i <= 10; i++)
            {
                try
                {
                    Console.WriteLine("Extracting the min value in the MinHeap: {0}", mhm.ExtractMin());
                    Console.WriteLine("The elements in the heap is as follows:");
                    ArrayHelper.PrintArray(mhm.HeapArray);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception:{0} index:{1}", e.Message, i);
                }
            }
        }
    }
}
