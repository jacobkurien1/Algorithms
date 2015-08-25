using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List.Linked_List_Helper
{
    class SinglyLinkedList<T>
    {
        public SingleLinkedListNode<T> Head { get; set; }
        public SingleLinkedListNode<T> CurrentNode { get; set; }

        public void AppendToEnd(T data)
        {
            if(Head == null)
            {
                Head = new SingleLinkedListNode<T>(data);
                CurrentNode = Head;
            }
            else
            {
                CurrentNode.NextNode = new SingleLinkedListNode<T>(data);
                CurrentNode = CurrentNode.NextNode;
            }
        }
    }
}
