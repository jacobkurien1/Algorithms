using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue.Stack_Helper
{
    class StackViaLinkedListWithLimit<T> : StackViaLinkedList<T>
    {
        private int capacity;
        public int Capacity
        {
            get
            {
                return capacity;
            }

            set
            {
                capacity = value;
            }
        }
        private int currentCapacity;
        public int CurrentCapacity
        {
            get
            {
                return currentCapacity;
            }

            set
            {
                currentCapacity = value;
            }
        }
        public StackViaLinkedListWithLimit(int capacity)
        {
            this.capacity = capacity;
        }

        public override void Push(T dataToPush)
        {
            if(currentCapacity >= capacity)
            {
                throw new Exception("Stack Full");
            }
            base.Push(dataToPush);
            currentCapacity++;
        }

        public void Push(SingleLinkedListNode<T> nodeToPush)
        {
            if(nodeToPush!=null)
            {
                if (currentCapacity >= capacity)
                {
                    throw new Exception("Stack Full");
                }
                base.Push(nodeToPush);
                currentCapacity++;
            }
        }

        public override SingleLinkedListNode<T> Pop()
        {
            SingleLinkedListNode<T> retNode = base.Pop();
            if(retNode!=null)
            {
                currentCapacity--;
            }
            return retNode;
        }
    }
}
