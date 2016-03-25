using AlgorithmProblems.Arrays.ArraysHelper;
using AlgorithmProblems.Heaps.HeapHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Heaps
{
    /// <summary>
    /// This is only a test class. The max heap implementation is present in HeapHelper
    /// </summary>
    class BasicMaxHeap
    {
        public static void TestMaxHeap()
        {
            //-----------------heapify------------------------------------------------------
            int[] arr = ArrayHelper.CreateArray(10);
            Console.WriteLine("The array before heapifying");
            ArrayHelper.PrintArray(arr);
            MaxHeap<int> mh = new MaxHeap<int>(arr);
            Console.WriteLine("The array after heapifying");
            ArrayHelper.PrintArray(mh.HeapArray);

            // ---------------------insertion and Peek max value----------------------------
            mh = new MaxHeap<int>(10);
            Console.WriteLine("Now lets test insertion to the heap");
            for(int i=0; i<=10; i++)
            {
                try
                {
                    Console.WriteLine("Inserting {0} in the MaxHeap", i);
                    mh.Insert(i);
                    Console.WriteLine("The elements in the heap is as follows:");
                    ArrayHelper.PrintArray(mh.HeapArray);
                    Console.WriteLine("The max value in the heap is {0}", mh.PeekMax());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception:{0} i:{1}", e.Message, i);
                }
            }

            // ------------------extract max value------------------------------------------
            for (int i = 0; i <= 10; i++)
            {
                try
                {
                    Console.WriteLine("Extracting the max value in the MaxHeap: {0}", mh.ExtractMax());
                    Console.WriteLine("The elements in the heap is as follows:");
                    ArrayHelper.PrintArray(mh.HeapArray);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception:{0} index:{1}", e.Message, i);
                }
            }
        }
    }
}
