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
            storage = new T[capacity];
            first = last = 0;
        }

        public void Enqueue(T data)
        {

        }

    }
}
