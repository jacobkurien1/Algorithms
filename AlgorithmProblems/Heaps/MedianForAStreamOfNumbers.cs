using AlgorithmProblems.Heaps.HeapHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Heaps
{
    /// <summary>
    /// Given a stream of numbers, return the median in O(1) at any instance of the stream
    /// Algo:
    /// To find median, we will basically divide the stream into 2 and keep one part in MaxHeap and another in MinHeap.
    /// In this implementation the MaxHeap will be always (equal/greater by 1) in size w.r.t the MinHeap.
    /// If the number of elements in Stream is odd we will return the max element from the MaxHeap.
    /// If the number of elements in Stream is even we will return the avg og the max from MaxHeap and min from MinHeap.
    /// 
    /// The running time for insert will be O(log(n))
    /// The running time for getMedian will be O(1)
    /// </summary>
    class MedianForAStreamOfNumbers
    {
        public MinHeap<int> MinHeapInst { get; set; }
        public MaxHeap<int> MaxHeapInst { get; set; }

        public MedianForAStreamOfNumbers(int streamSize)
        {
            MinHeapInst = new MinHeap<int>(streamSize);
            MaxHeapInst = new MaxHeap<int>(streamSize);
        }

        /// <summary>
        /// Insert a value to the stream
        /// </summary>
        /// <param name="val"></param>
        public void Insert(int val)
        {
            if(MaxHeapInst.HeapSize == 0)
            {
                MaxHeapInst.Insert(val);
                return;
            }
            if(MaxHeapInst.PeekMax() > val) // The val needs to go in MaxHeap
            {
                if(MaxHeapInst.HeapSize > MinHeapInst.HeapSize) // Make sure MaxHeap.Count does not go beyond MinHeap.Count + 1
                {
                    MinHeapInst.Insert(MaxHeapInst.ExtractMax());
                }
                MaxHeapInst.Insert(val);
            }
            else // The val needs to go in MinHeap
            {
                if(MinHeapInst.HeapSize == MaxHeapInst.HeapSize) // Make sure that the MinHeap.Count does not go beyond MaxHeap.Count
                {
                    MaxHeapInst.Insert(MinHeapInst.ExtractMin());
                }
                MinHeapInst.Insert(val);
            }
        }

        /// <summary>
        /// Gets the median based on the current elements in the stream
        /// </summary>
        /// <returns>median of the stream</returns>
        public double GetMedian()
        {
            if((MinHeapInst.HeapSize + MaxHeapInst.HeapSize)%2 !=0)
            {
                // odd number of elements in the stream
                return MaxHeapInst.PeekMax();
            }
            // even number of elements in the stream
            int mid1 = MaxHeapInst.PeekMax();
            int mid2 = MinHeapInst.PeekMin();

            return mid1 + ((mid2 - mid1) / 2.0); // This will prevent overflow
        }

        #region TestArea
        public static void TestMedianForAStreamOfNumbers()
        {
            MedianForAStreamOfNumbers med = new MedianForAStreamOfNumbers(100);
            med.Insert(-3);
            Console.WriteLine("The median of the current stream is {0}. Expected: -3", med.GetMedian());
            med.Insert(5);
            Console.WriteLine("The median of the current stream is {0}. Expected: 1", med.GetMedian());
            med.Insert(8);
            med.Insert(20);
            med.Insert(-8);
            med.Insert(0);
            med.Insert(-2);
            med.Insert(9);
            Console.WriteLine("The median of the current stream is {0}. Expected: 2.5", med.GetMedian());

            med.Insert(-1);
            med.Insert(2);
            med.Insert(3);
            Console.WriteLine("The median of the current stream is {0}. Expected: 2", med.GetMedian());
        }
        #endregion

    }
}
