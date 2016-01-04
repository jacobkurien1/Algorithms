using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue.Queue_Helper
{
    public class CircularQueue<T>
    {
        // When the array is empty we will make the head -1
        private int HeadIndex = -1;

        private int TailIndex = 0;

        private T[] array = null;

        public CircularQueue(int size)
        {
            array = new T[size];
        }

        /// <summary>
        /// Add data in the array at the tail index and increment tail.
        /// Make sure that the array is not full
        /// </summary>
        /// <param name="data"></param>
        public void Enqueue(T data)
        {
            if(data == null)
            {
                return;
            }
            if(TailIndex == HeadIndex)
            {
                // This means the array is full
                throw new StackOverflowException();
            }
            if(HeadIndex == -1)
            {
                // We will be adding our first element in the array
                // So we need to change the Head Index to the correct value
                HeadIndex = TailIndex;
            }
            array[TailIndex] = data;
            TailIndex = (TailIndex + 1) % array.Length;
        }

        /// <summary>
        /// Remove the data from the HeadIndex and increment HeadIndex
        /// Make sure that the array is not empty
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            if(HeadIndex == -1)
            {
                // The array is empty
                throw new Exception("Stack is empty");
            }
            T dataToReturn = array[HeadIndex];
            if((HeadIndex+1)%array.Length == TailIndex)
            {
                HeadIndex = -1;
            }
            else
            {
                HeadIndex = (HeadIndex + 1) % array.Length;
            }
            return dataToReturn;
        }

        public int Count
        {
            get
            {
                if(HeadIndex == -1)
                {
                    // The queue is empty
                    return 0;
                }
                if(HeadIndex < TailIndex)
                {
                    return TailIndex - HeadIndex;
                }
                else if(TailIndex < HeadIndex)
                {
                    return array.Length - HeadIndex + TailIndex;
                }
                else
                {
                    // The queue is full
                    return array.Length;
                }
            }
        }

        public override string ToString()
        {
            if(HeadIndex == -1)
            {
                // Queue is empty
                return "";
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Count; i++)
            {
                int arrayindex = (HeadIndex + i) % array.Length;
                sb.Append(array[arrayindex] + " , ");
            }
            return sb.ToString();
        }

    }

    public class TestCircularQueue
    {
        public static void TestCircularQueueWithDifferentCases()
        {
            CircularQueue<int> cq = new CircularQueue<int>(5);
            for (int i = 0; i < 5; i++)
            {
                cq.Enqueue(i);
                Console.WriteLine("The contents of the queue are : {0}", cq.ToString());
                Console.WriteLine("The number of items in the queue is : {0}", cq.Count);
            }

            Console.WriteLine("The contents of the queue are: {0}", cq.ToString());
            try
            {
                // Over flow should happen here.
                cq.Enqueue(3);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            for (int i = 4; i >=0 ; i--)
            {
                Console.WriteLine("The item dequeued is : "+ cq.Dequeue());
                Console.WriteLine("The contents of the queue are : {0}", cq.ToString());
                Console.WriteLine("The number of items in the queue is : {0}", cq.Count);
            }
            
            try
            {
                // Empty queue should throw exception
                cq.Dequeue();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
