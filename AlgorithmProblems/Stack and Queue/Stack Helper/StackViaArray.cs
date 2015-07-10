using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue.Stack_Helper
{
    class StackViaArray<T>
    {
        private T[] storage;
        private int top;

        public StackViaArray(int capacity)
        {
            if(capacity <= 0)
            {
                throw new Exception("Needs to have more size");
            }
            else
            {
                storage = new T[capacity];
                top = 0;
            }
        }

        public T Pop()
        {
            if(top <= 0)
            {
                throw new Exception("The Stack is empty");
            }
            T retVal = storage[top - 1];
            top--;
            return retVal;
        }

        public void Push(T data)
        {
            if(top >= storage.Length)
            {
                throw new Exception("Stack is full");
            }
            storage[top] = data;
            top++;
        }
    }
}
