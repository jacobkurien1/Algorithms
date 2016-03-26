using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Heaps.HeapHelper
{
    /// <summary>
    /// Max heap is a complete binary tree in which the parent is always greater than all its child
    /// 
    /// We are not going to construct a tree but just have a tree in the array form.
    /// This will save us lot of space. 
    /// 
    /// So in array: index of left subtree of ith node= 2*i +1
    ///              index of right subtree of the ith node = 2*i+2;
    ///              parent of the ith node = Math.Floor((i-1)/2)
    /// 
    /// </summary>
    public class MaxHeap<T> where T : IComparable
    {
        /// <summary>
        /// we will store the complete binary tree in this array
        /// 0th index gives the root element
        /// So in array: index of left subtree of ith node= 2*i +1
        ///              index of right subtree of the ith node = 2*i+2;
        ///              parent of the ith node = Math.Floor((i-1)/2)
        /// </summary>
        protected T[] _arrayToStoreTree { get; set; }
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
                for(int index = 0; index < _currentNumberOfElements; index++)
                {
                    heapArray[index] = _arrayToStoreTree[index];
                }
                return heapArray;
            }
        }

        public MaxHeap(int size)
        {
            _arrayToStoreTree = new T[size];
        }

        /// <summary>
        /// Build maxheap when we have an input array which is not heapified
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
        public MaxHeap(T[] inputArr)
        {
            _currentNumberOfElements = inputArr.Length;
            _arrayToStoreTree = inputArr;

            // for a complete tree we have the leaf nodes from Math.Floor(n/2)+1 to n-1
            // hence it is fine if we traverse only to till the last node which is not a leaf node
            for(int i= _currentNumberOfElements / 2; i>=0; i--)
            {
                MaxHeapify(_arrayToStoreTree, i);
            }
        }

        /// <summary>
        /// This method assumes that the left and right subtree from node at index 
        /// is following the max heap property but arr[i] might not be following the 
        /// maxheap property correctly.
        /// 
        /// So we get the index in which the largest value is present and if largestIndex != index,
        /// we swap the value at index with value at largestIndex and now we need to call MaxHeapify again.
        /// 
        /// The running time is height of the tree = O(logn)
        /// The space requirement is O(1)
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="index"></param>
        protected void MaxHeapify(T[] arr, int index)
        {
            MaxHeapifyWithLeafNode(arr, index, _currentNumberOfElements);
        }

        /// <summary>
        /// This method assumes that the left and right subtree from node at index 
        /// is following the max heap property but arr[i] might not be following the 
        /// maxheap property correctly.
        /// 
        /// So we get the index in which the largest value is present and if largestIndex != index,
        /// we swap the value at index with value at largestIndex and now we need to call MaxHeapify again.
        /// 
        /// The running time is height of the tree = O(logn)
        /// The space requirement is O(1)
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="index"></param>
        /// <param name="leafNodeIndex"> the index till which we want to heapify</param>
        protected void MaxHeapifyWithLeafNode(T[] arr, int index, int leafNodeIndex)
        {
            int leftSubtreeIndex = 2 * index + 1; // we can also use the shift all bits to left to achieve this
            int rightSubtreeIndex = 2 * index + 2;

            int largerIndex = index;
            if (leftSubtreeIndex < leafNodeIndex && arr[leftSubtreeIndex].CompareTo(arr[largerIndex]) > 0)
            {
                largerIndex = leftSubtreeIndex;
            }
            if (rightSubtreeIndex < leafNodeIndex && arr[rightSubtreeIndex].CompareTo(arr[largerIndex]) > 0)
            {
                largerIndex = rightSubtreeIndex;
            }

            if (largerIndex != index)
            {
                // this condition will lead the heap recursion to converge
                Swap(arr, index, largerIndex);
                MaxHeapifyWithLeafNode(arr, largerIndex, leafNodeIndex);
            }
        }

        /// <summary>
        /// Add the value to the end of the array as a leaf node and keep checking the parent
        /// if the parent is smaller than the newVal then swap parent and newVal.
        /// 
        /// The running time for this operation is O(logn)
        /// </summary>
        /// <param name="newVal"></param>
        public void Insert(T newVal)
        {
            if (_arrayToStoreTree.Length==_currentNumberOfElements)
            {
                throw new Exception("Heap overflow");
            }
            _arrayToStoreTree[_currentNumberOfElements] = newVal;
            int currentIndex = _currentNumberOfElements;

            // the parent of currentIndex will be present at (currentIndex-1)/2
            while (currentIndex!=((currentIndex-1)/2) && _arrayToStoreTree[(currentIndex-1)/2].CompareTo(_arrayToStoreTree[currentIndex])<0)
            {
                Swap(_arrayToStoreTree, (currentIndex-1)/2, currentIndex);
                currentIndex = (currentIndex-1) / 2;
            }
            _currentNumberOfElements++;
        }

        /// <summary>
        /// To get the max element get the element at index 0.
        /// this element is the root of the tree and has the maximum value
        /// 
        /// The running time of this operation is O(1)
        /// </summary>
        /// <returns></returns>
        public T PeekMax()
        {
            if(_currentNumberOfElements==0)
            {
                throw new Exception("The heap is empty");
            }
            return _arrayToStoreTree[0];
        }

        /// <summary>
        /// this would get the max value and delete the value from the heap.
        /// 
        /// The running time for this operation is O(logn)
        /// </summary>
        /// <returns></returns>
        public T ExtractMax()
        {
            if (_currentNumberOfElements == 0)
            {
                throw new Exception("The heap is empty");
            }
            T retVal = _arrayToStoreTree[0];
            _arrayToStoreTree[0] = _arrayToStoreTree[_currentNumberOfElements-1];
            _arrayToStoreTree[_currentNumberOfElements-1] = default(T);
            _currentNumberOfElements--;

            MaxHeapify(_arrayToStoreTree, 0);
            return retVal;
        }

        protected void Swap(T[] arr, int index1, int index2)
        {
            T temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;
        }

    }
}
