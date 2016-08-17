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
    public class MinHeapMap<T> where T : IComparable, IKey
    {
        /// <summary>
        /// This will help to get the O(1) lookup for all the entities's index in heap
        /// We need a list<int> to take care of duplicates in the value type
        /// </summary>
        private Dictionary<int, int> AllEntitiesIndex { get; set; }

        public Dictionary<int, T> AllEntities { get; set; }

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
            _arrayToStoreTree[arrayIndex] = newVal;
            AllEntitiesIndex[newVal.Id] = arrayIndex;

        }
        private int _currentNumberOfElements { get; set; }

        public int Count
        {
            get
            {
                return _currentNumberOfElements;
            }
        }

        public MinHeapMap(int size)
        {
            AllEntitiesIndex = new Dictionary<int, int>();
            AllEntities = new Dictionary<int, T>();
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
        /// 
        /// We will first check whether we are increasing the priority or decreasing it,
        /// If we are increasing the priority, we should percolate downwards
        /// if we are decreasing the priority, we should percolate upwards
        /// 
        /// The running time needed for this operation is O(log(n))
        /// </summary>
        /// <param name="newVal"></param>
        /// <param name="index"></param>
        public void ChangePriority(T newVal, int Id)
        {
            
            if (!AllEntitiesIndex.ContainsKey(Id))
            {
                throw new Exception("Illegal operation");
            }
            int index = AllEntitiesIndex[Id];
            if(_arrayToStoreTree[index].CompareTo(newVal)>0)
            {
                // We have decreased the priority
                // We need to percolate upwards
                ChangeValueAtIndex(index, newVal);

                while (index>0)
                {
                    int parentIndex = (int)Math.Floor((index - 1) / 2.0);
                    if (parentIndex>=0 && _arrayToStoreTree[parentIndex].CompareTo(_arrayToStoreTree[index]) >0)
                    {
                        // We need to swap the element at index with its parent
                        Swap(index, parentIndex);
                        index = parentIndex;
                    }
                    else
                    {
                        // The new added value is still greater than its parent, hence break
                        break;
                    }
                }
            }
            else
            {
                //We have increased the priority
                // we need to percolate the changes downwards
                ChangeValueAtIndex(index, newVal);
                MinHeapify(index);
            }
            
        }

        public void ChangePriority(T oldVal, T newVal)
        {
            if (oldVal == null || newVal == null)
            {
                throw new ArgumentException("Arguments are null");
            }
            ChangePriority(newVal, oldVal.Id);
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
            AllEntities[newVal.Id] = newVal;
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
            Swap(0, _currentNumberOfElements - 1);
            _arrayToStoreTree[_currentNumberOfElements - 1] = default(T);
            AllEntities.Remove(retVal.Id);
            AllEntitiesIndex.Remove(retVal.Id);
            
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

    public interface IKey
    {
        int Id { get; }
    }

    public class FakeClassForTest : IKey, IComparable
    {
        public FakeClassForTest(int id, int distance)
        {
            Id = id;
            Distance = distance;
        }
        public int Id { get; set; }
        public int Distance { get; set; }

        public int CompareTo(object obj)
        {
            return Distance.CompareTo(((FakeClassForTest)obj).Distance);
        }
        public override string ToString()
        {
            return Distance.ToString();
        }
        public static void PrintArray(FakeClassForTest[] ft)
        {
            for(int i=0; i<ft.Length; i++)
            {
                Console.Write("{0} ", ft[i].ToString());
            }
            Console.WriteLine();
        }
        public FakeClassForTest ShallowClone()
        {
            return new FakeClassForTest(Id, Distance);
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
            MinHeapMap<FakeClassForTest> mhm = new MinHeapMap<FakeClassForTest>(10);
            for(int i=0; i<arr.Length; i++)
            {
                
                mhm.Insert(new FakeClassForTest(i, arr[i]));
                FakeClassForTest.PrintArray(mhm.HeapArray);
                Console.WriteLine("The min value in the heap is {0}", mhm.PeekMin());

            }
            // -------------------change priority -----------------------------------------------
            Console.WriteLine("Change {0} -> {1}", mhm.HeapArray[4], 888);
            FakeClassForTest oldObj = mhm.HeapArray[4];
            FakeClassForTest newObj = oldObj.ShallowClone();
            newObj.Distance = 888;
            mhm.ChangePriority(oldVal: oldObj, newVal: newObj);
            //mhm.ChangePriority(newObj, oldObj.Id);
            FakeClassForTest.PrintArray(mhm.HeapArray);

            oldObj = mhm.HeapArray[4];
            newObj = oldObj.ShallowClone();
            newObj.Distance = -1;
            Console.WriteLine("Change {0} -> {1}", mhm.HeapArray[4], -1);
            mhm.ChangePriority(oldVal: oldObj, newVal: newObj);
            FakeClassForTest.PrintArray(mhm.HeapArray);

            oldObj = mhm.HeapArray[4];
            newObj = oldObj.ShallowClone();
            newObj.Distance = 21;
            Console.WriteLine("Change {0} -> {1}", mhm.HeapArray[4], 21);
            mhm.ChangePriority(oldVal: oldObj, newVal: newObj);
            FakeClassForTest.PrintArray(mhm.HeapArray);

            // ------------------extract min value------------------------------------------
            for (int i = 0; i <= 10; i++)
            {
                try
                {
                    Console.WriteLine("Extracting the min value in the MinHeap: {0}", mhm.ExtractMin());
                    Console.WriteLine("The elements in the heap is as follows:");
                    FakeClassForTest.PrintArray(mhm.HeapArray);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception:{0} index:{1}", e.Message, i);
                }
            }
        }
    }
}
