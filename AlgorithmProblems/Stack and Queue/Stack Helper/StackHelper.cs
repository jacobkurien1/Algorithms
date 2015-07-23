using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue.Stack_Helper
{
    class StackHelper
    {
        public static StackViaLinkedList<int> CreateAStack(int num)
        {
            // Create the stack
            StackViaLinkedList<int> stack = new StackViaLinkedList<int>();

            // Populate the unsorted stack
            SingleLinkedListNode<int> node = LinkedListHelper.CreateSinglyLinkedList(num);
            while (node != null)
            {
                stack.Push(node.Data);
                node = node.NextNode;
            }
            return stack;
        }
    }
}
