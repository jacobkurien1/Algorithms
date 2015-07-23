using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue
{
    class StackViaLinkedList<T>
    {
        public SingleLinkedListNode<T> Top { get; set; }

        public virtual SingleLinkedListNode<T> Pop()
        {
            if(Top != null)
            {
                SingleLinkedListNode<T> nodeToPop = Top;
                Top = Top.NextNode;
                return nodeToPop;
            }
            return null;
        }

        public virtual void Push(T dataToPush)
        {
            SingleLinkedListNode<T> nodeToPush = new SingleLinkedListNode<T>(dataToPush);
            nodeToPush.NextNode = Top;
            Top = nodeToPush;
        }

        public void Push(SingleLinkedListNode<T> nodeToPush)
        {
            if (nodeToPush != null)
            {
                nodeToPush.NextNode = Top;
                Top = nodeToPush;
            }
        }

        public SingleLinkedListNode<T> Peek()
        {
            return Top; // when Top == null, we will return null
        }
    }
}
