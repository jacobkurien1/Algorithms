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
    /// This is only a test class. The min heap implementation is present in HeapHelper
    /// </summary>
    public class BasicMinHeap
    {
        public static void TestMinHeap()
        {
            //-----------------heapify------------------------------------------------------
            int[] arr = ArrayHelper.CreateArray(10);
            Console.WriteLine("The array before heapifying");
            ArrayHelper.PrintArray(arr);
            MinHeap<int> mh = new MinHeap<int>(arr);
            Console.WriteLine("The array after heapifying");
            ArrayHelper.PrintArray(mh.HeapArray);

            // ---------------------insertion and Peek min value----------------------------
            mh = new MinHeap<int>(10);
            Console.WriteLine("Now lets test insertion to the heap");
            for (int i = 0; i <= 10; i++)
            {
                try
                {
                    Console.WriteLine("Inserting {0} in the MinHeap", i);
                    mh.Insert(i);
                    Console.WriteLine("The elements in the heap is as follows:");
                    ArrayHelper.PrintArray(mh.HeapArray);
                    Console.WriteLine("The min value in the heap is {0}", mh.PeekMin());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception:{0} i:{1}", e.Message, i);
                }
            }
            // -------------------change priority -----------------------------------------------
            Console.WriteLine("Change 5 -> 10");
            mh.ChangePriority(10, 5);
            ArrayHelper.PrintArray(mh.HeapArray);

            Console.WriteLine("Change 10 -> -1");
            mh.ChangePriority(-1, 5);
            ArrayHelper.PrintArray(mh.HeapArray);

            Console.WriteLine("Change 4 -> 100");
            mh.ChangePriority(100, 4);
            ArrayHelper.PrintArray(mh.HeapArray);

            // ------------------extract min value------------------------------------------
            for (int i = 0; i <= 10; i++)
            {
                try
                {
                    Console.WriteLine("Extracting the min value in the MinHeap: {0}", mh.ExtractMin());
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
