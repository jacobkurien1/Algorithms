using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue.Queue_Helper
{
    class QueueViaArray<T>
    {
        private T[] storage;
        private int first;
        private int last;

        public QueueViaArray(int capacity)
        {
            if (capacity > 0)
            {
                storage = new T[capacity];
                first = 0;
                last = 1;
            }
            else
            {
                throw new Exception("Capacity is very low/invalid");
            }
        }

        public void Enqueue(T data)
        {
            if(last < storage.Length)
            {
                storage[last-1] = data;
                last++;
            }
            else
            {
                throw new Exception("Array full");
            }
        }

        public T Dequeue()
        {
            if(first < last)
            {
                T retVal = storage[first++];
                return retVal;
            }
            else
            {
                // Note here the max number of dequeues can be equal to the storage capacity
                throw new Exception("Storage full"); 
            }
        }

    }
}
