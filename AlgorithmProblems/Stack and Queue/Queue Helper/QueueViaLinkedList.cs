using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue.Queue_Helper
{
    class QueueViaLinkedList<T>
    {
        public SingleLinkedListNode<T> First { get; set; }
        public SingleLinkedListNode<T> Last { get; set; }


        public void Enqueue(T data)
        {
            if(First != null)
            {
                SingleLinkedListNode<T> newNode = new SingleLinkedListNode<T>(data);
                Last.NextNode = newNode;
                Last = newNode;
            }
            else
            {
                First = Last = new SingleLinkedListNode<T>(data);
            }
        }

        public SingleLinkedListNode<T> Dequeue()
        {
            SingleLinkedListNode<T> dequeueNode = null;
            if(First != null)
            {
                dequeueNode = First;
                First = First.NextNode;
            }
            return dequeueNode;
        }
    }
}
