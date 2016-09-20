using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Heaps.HeapHelper
{
    class PriorityQueue<T> where T : IEquatable<T>
    {
        PriorityQueueElement<T>[] elementStore;
        int currentNumOfElement = 0;

        public PriorityQueue(int capacity)
        {
            elementStore = new PriorityQueueElement<T>[capacity];
        }

        /// <summary>
        /// Inserts a new value in the priority queue
        /// 
        /// Step: insert the new value at the end and percolate upwards
        /// </summary>
        /// <param name="item"></param>
        /// <param name="priority"></param>
        public void Insert(T item, int priority)
        {
            if(currentNumOfElement >= elementStore.Length)
            {
                // condition where the priority queue is full
                throw new OverflowException("The priority queue is full");
            }
            elementStore[currentNumOfElement] = new PriorityQueueElement<T>(item, priority);
            int currentIndex = currentNumOfElement;
            int parentIndex = (int)Math.Floor((currentIndex - 1) / 2.0);
            while (parentIndex >= 0)
            {
                if (elementStore[parentIndex].CompareTo(elementStore[currentIndex]) > 0)
                {
                    Swap(currentIndex, parentIndex);
                }
                currentIndex = parentIndex;
                parentIndex = (int)Math.Floor((currentIndex - 1) / 2.0);
            }
            currentNumOfElement++;
        }

        /// <summary>
        /// Returns the min value from the priority queue and deletes the element
        /// 
        /// Steps: keep the element at elementStore[0] as min.
        /// Swap(0 and currentNumOfElement -1) and percolate downwards
        /// </summary>
        /// <returns></returns>
        public T ExtractMin()
        {
            if(currentNumOfElement == 0)
            {
                throw new Exception("Priority Queue is empty");
            }
            T minElem = elementStore[0].Element;

            Swap(0, currentNumOfElement - 1);
            elementStore[currentNumOfElement - 1] = null;
            currentNumOfElement--;

            // Percolate downwards
            MinHeapify(0);

            return minElem;
        }

        /// <summary>
        /// Recursive subroutine of minheapify
        /// </summary>
        /// <param name="index"></param>
        private void MinHeapify(int index)
        {
            while (index < currentNumOfElement)
            {
                int leftIndex = 2 * index + 1;
                int rightIndex = 2 * index + 2;
                int smaller = index;
                if (leftIndex < currentNumOfElement && elementStore[leftIndex].CompareTo(elementStore[smaller]) < 0)
                {
                    smaller = leftIndex;
                }
                if (rightIndex < currentNumOfElement && elementStore[rightIndex].CompareTo(elementStore[smaller]) < 0)
                {
                    smaller = rightIndex;
                }
                if (smaller == index)
                {
                    // the priority queue is heapified
                    break;
                }
                Swap(smaller, index);
                index = smaller;
            }
        }

        /// <summary>
        /// Swap elements in elementStore at index1 and index2
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        private void Swap(int index1, int index2)
        {
            PriorityQueueElement<T> temp = elementStore[index1];
            elementStore[index1] = elementStore[index2];
            elementStore[index2] = temp;
        }

        /// <summary>
        /// Gets the total capacity of the priority queue
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Gets the total number of elements in the priority queue
        /// </summary>
        public int Count
        {
            get
            {
                return currentNumOfElement;
            }
        }

        public T[] MinHeapElements
        {
            get
            {
                T[] ret = new T[currentNumOfElement];
                for(int i=0; i<currentNumOfElement; i++)
                {
                    ret[i] = elementStore[i].Element;
                }
                return ret;
            }
        }

    }

    /// <summary>
    /// Represents a priority queue element with its priority
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class PriorityQueueElement<T> :IComparable
    {
        public T Element { get; set; }
        public int Priority { get; set; }
        public PriorityQueueElement(T element, int priority)
        {
            Element = element;
            Priority = priority;
        }

        public int CompareTo(object obj)
        {
            PriorityQueueElement<T> newElem = (PriorityQueueElement<T>)obj;
            return Priority.CompareTo(newElem.Priority);
        }
    }

    #region TestArea
    public class TestPriorityQueue
    {
        public static void Run()
        {
            PriorityQueue<int> pq = new PriorityQueue<int>(9);
            pq.Insert(5, 5);
            PrintPriorityQueue(pq.MinHeapElements);
            pq.Insert(4,4);
            PrintPriorityQueue(pq.MinHeapElements);
            pq.Insert(11,11);
            PrintPriorityQueue(pq.MinHeapElements);
            pq.Insert(1,1);
            PrintPriorityQueue(pq.MinHeapElements);
            pq.Insert(2,2);
            PrintPriorityQueue(pq.MinHeapElements);
            pq.Insert(1,1);
            PrintPriorityQueue(pq.MinHeapElements);
            pq.Insert(121,121);
            PrintPriorityQueue(pq.MinHeapElements);
            pq.Insert(3,3);
            PrintPriorityQueue(pq.MinHeapElements);
            pq.Insert(7,7);
            PrintPriorityQueue(pq.MinHeapElements);
            try
            {
                pq.Insert(1, 1);
                PrintPriorityQueue(pq.MinHeapElements);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("The min extraction gives {0}", pq.ExtractMin().ToString());
            PrintPriorityQueue(pq.MinHeapElements);
            Console.WriteLine("The min extraction gives {0}", pq.ExtractMin().ToString());
            PrintPriorityQueue(pq.MinHeapElements);
            Console.WriteLine("The min extraction gives {0}", pq.ExtractMin().ToString());
            PrintPriorityQueue(pq.MinHeapElements);
            Console.WriteLine("The min extraction gives {0}", pq.ExtractMin().ToString());
            PrintPriorityQueue(pq.MinHeapElements);
            Console.WriteLine("The min extraction gives {0}", pq.ExtractMin().ToString());
            PrintPriorityQueue(pq.MinHeapElements);
            Console.WriteLine("The min extraction gives {0}", pq.ExtractMin().ToString());
            PrintPriorityQueue(pq.MinHeapElements);
            Console.WriteLine("The min extraction gives {0}", pq.ExtractMin().ToString());
            PrintPriorityQueue(pq.MinHeapElements);
            Console.WriteLine("The min extraction gives {0}", pq.ExtractMin().ToString());
            PrintPriorityQueue(pq.MinHeapElements);
            Console.WriteLine("The min extraction gives {0}", pq.ExtractMin().ToString());
            PrintPriorityQueue(pq.MinHeapElements);
            try
            {
                Console.WriteLine("The min extraction gives {0}", pq.ExtractMin().ToString());
                PrintPriorityQueue(pq.MinHeapElements);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void PrintPriorityQueue(int[] pqArr)
        {
            if(pqArr != null)
            {
                foreach(int i in pqArr)
                {
                    Console.Write("{0}, ", i);
                }
                Console.WriteLine();
            }
        }
    }
    #endregion
}
