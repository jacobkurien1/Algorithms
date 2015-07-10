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

        public SingleLinkedListNode<T> Pop()
        {
            if(Top != null)
            {
                SingleLinkedListNode<T> nodeToPop = Top;
                Top = Top.NextNode;
                return nodeToPop;
            }
            return null;
        }

        public void Push(T dataToPush)
        {
            SingleLinkedListNode<T> nodeToPush = new SingleLinkedListNode<T>(dataToPush);
            nodeToPush.NextNode = Top;
            Top = nodeToPush;
        }
    }
}
