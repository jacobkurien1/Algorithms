using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Heaps.HeapHelper
{
    /// <summary>
    /// Min heap is a complete binary tree in which the parent is always less than all its child
    /// 
    /// We are not going to construct a tree but just have a tree in the array form.
    /// This will save us lot of space. 
    /// 
    /// So in array: index of left subtree of ith node= 2*i +1 
    ///              index of right subtree of the ith node = 2*i+2;
    ///              parent of the ith node = Math.Floor(i-1/2)
    /// </summary>
    public class MinHeap<T> where T :IComparable
    {
        /// <summary>
        /// we will store the complete binary tree in this array
        /// 0th index gives the root element
        /// So in array: index of left subtree of ith node= 2*i +1
        ///              index of right subtree of the ith node = 2*i+2;
        ///              parent of the ith node = Math.Floor((i-1)/2)
        /// </summary>
        private T[] _arrayToStoreTree { get; set; }
        private int _currentNumberOfElements { get; set; }

        public int HeapSize
        {
            get
            {
                return _currentNumberOfElements;
            }
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

        public MinHeap(int size)
        {
            _arrayToStoreTree = new T[size];
        }

        /// <summary>
        /// Build minheap when we have an input array which is not heapified
        /// 
        /// Now lets calculate the running time.
        /// The upper bound on the running time will be O(nlogn)
        /// Lets find a tighter upperbound
        ///     Each node needs O(1) work on it.
        ///     For a complete binary tree we have Math.Ceil(n/(2^(h+1)) nodes at each level
        ///     Total Running time = Summation( Math.Ceil(n/(2^(h+1)) * O(h) )
        ///                          h = 0->logn
        /// note: Summation(h/(2^h)) = 2 where h 0->infinity
        /// So the running time is reduced to O(n)
        /// </summary>
        /// <param name="inputArr"></param>
        public MinHeap(T[] inputArr)
        {
            _currentNumberOfElements = inputArr.Length;
            _arrayToStoreTree = inputArr;

            // for a complete tree we have the leaf nodes from Math.Floor(n/2)+1 to n-1
            // hence it is fine if we traverse only to till the last node which is not a leaf node
            for (int i = _currentNumberOfElements / 2; i >= 0; i--)
            {
                MinHeapify(_arrayToStoreTree, i);
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
            if(index<0 || index>=_currentNumberOfElements)
            {
                throw new Exception("Illegal operation");
            }
            _arrayToStoreTree[index] = newVal;
            while(index >=0)
            {
                MinHeapify(_arrayToStoreTree, index);
                index = (int)Math.Floor((index - 1) / 2.0);
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
        private void MinHeapify(T[] arr, int index)
        {
            int leftSubtreeIndex = 2 * index + 1; // we can also use the shift all bits to left to achieve this
            int rightSubtreeIndex = 2 * index + 2;

            int smallestIndex = index;
            if (leftSubtreeIndex < _currentNumberOfElements && arr[leftSubtreeIndex].CompareTo(arr[smallestIndex]) < 0)
            {
                smallestIndex = leftSubtreeIndex;
            }
            if (rightSubtreeIndex < _currentNumberOfElements && arr[rightSubtreeIndex].CompareTo(arr[smallestIndex]) < 0)
            {
                smallestIndex = rightSubtreeIndex;
            }

            if (smallestIndex != index)
            {
                // this will make sure that the recursion converges
                Swap(arr, index, smallestIndex);
                MinHeapify(arr, smallestIndex);
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
            _arrayToStoreTree[_currentNumberOfElements] = newVal;
            int currentIndex = _currentNumberOfElements;

            // the parent of currentIndex will be present at (currentIndex-1)/2
            while (currentIndex != (currentIndex - 1) / 2 && _arrayToStoreTree[(currentIndex - 1) / 2].CompareTo(_arrayToStoreTree[currentIndex]) > 0)
            {
                Swap(_arrayToStoreTree, (currentIndex - 1) / 2, currentIndex);
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
            _currentNumberOfElements--;
            MinHeapify(_arrayToStoreTree, 0);
            return retVal;
        }

        private void Swap(T[] arr, int index1, int index2)
        {
            T temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;
        }
    }
}
