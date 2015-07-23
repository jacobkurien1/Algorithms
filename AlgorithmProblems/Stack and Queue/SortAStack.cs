using AlgorithmProblems.Linked_List.Linked_List_Helper;
using AlgorithmProblems.Stack_and_Queue.Stack_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue
{
    class SortAStack
    {
        public enum SortingOrder
        {
            Ascending,
            Descending
        }
        public static StackViaLinkedList<int> SortAStackAscendingOrDescending(StackViaLinkedList<int> unsortedStack, SortingOrder sortOrder)
        {
            StackViaLinkedList<int> sortedStack = new StackViaLinkedList<int>(); // This will be the sorted stack

            while(unsortedStack.Peek() != null)
            {
                SingleLinkedListNode<int> currentNode = unsortedStack.Pop();
                // if the current node from unsorted stack is greater than the top of the sorted stack
                // then keep popping the sorted stack till this condition holds true
                // Here we are finding the final position where the current element should be placed in sorted stack
                while (sortedStack.Peek() != null && ((sortOrder == SortingOrder.Ascending) ? sortedStack.Peek().Data < currentNode.Data : sortedStack.Peek().Data > currentNode.Data))
                {
                    // We will push the popped elements from sorted stack to unsorted stack
                    unsortedStack.Push(sortedStack.Pop());
                }
                sortedStack.Push(currentNode);
            }

            return sortedStack;
        }

        #region TestMethod
        public static void TestSortAStackAscending()
        {
            // Create an populate unsorted stack
            StackViaLinkedList<int> unsortedStack = StackHelper.CreateAStack(10);

            Console.WriteLine("The unsorted stack is as shown below:");
            LinkedListHelper.PrintSinglyLinkedList(unsortedStack.Peek());

            // Get the sorted stack
            StackViaLinkedList<int> sortedStack = SortAStack.SortAStackAscendingOrDescending(unsortedStack, SortingOrder.Ascending);

            Console.WriteLine("The sorted stack in ascending order is as shown below:");
            LinkedListHelper.PrintSinglyLinkedList(sortedStack.Peek());

            // Create an populate unsorted stack
            unsortedStack = StackHelper.CreateAStack(10);

            // Get the sorted stack
            sortedStack = SortAStack.SortAStackAscendingOrDescending(unsortedStack, SortingOrder.Descending);

            Console.WriteLine("The sorted stack in desending order is as shown below:");
            LinkedListHelper.PrintSinglyLinkedList(sortedStack.Peek());
        }
        #endregion
    }
}
