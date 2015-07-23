using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue
{
    class StackWithMinElement
    {
        StackViaLinkedList<int> mainStack = new StackViaLinkedList<int>();
        StackViaLinkedList<int> minStack = new StackViaLinkedList<int>();

        public void Push(int data)
        {
            mainStack.Push(data);
            SingleLinkedListNode<int> minStackTopNode = minStack.Peek();
            if(minStackTopNode == null || minStackTopNode.Data >= data)
            {
                minStack.Push(data);
            }
        }

        public SingleLinkedListNode<int> Pop()
        {
            SingleLinkedListNode<int> retVal = mainStack.Pop();
            SingleLinkedListNode<int> minStackTopNode = minStack.Peek();

            if(minStackTopNode != null && retVal != null && minStackTopNode.Data == retVal.Data)
            {
                minStack.Pop();
            }
            return retVal;
        }

        public SingleLinkedListNode<int> Peek()
        {
            return mainStack.Peek();
        }

        public SingleLinkedListNode<int> Min()
        {
            return minStack.Peek();
        }

        public static void TestStackWithMinElement()
        {
            Console.WriteLine("Test the stack with minimum element(get the min element in O(1)");
            StackWithMinElement st = new StackWithMinElement();
            st.Push(2);
            st.Push(3);
            st.Push(6);
            st.Push(1);
            Console.WriteLine("The main stack is as shown below:");
            LinkedListHelper.PrintSinglyLinkedList(st.Peek());
            Console.WriteLine("The min stack is as shown below:");
            LinkedListHelper.PrintSinglyLinkedList(st.Min());
            st.Pop();
            Console.WriteLine("The main stack is as shown below:");
            LinkedListHelper.PrintSinglyLinkedList(st.Peek());
            Console.WriteLine("The min stack is as shown below:");
            LinkedListHelper.PrintSinglyLinkedList(st.Min());
            st.Pop();
            Console.WriteLine("The main stack is as shown below:");
            LinkedListHelper.PrintSinglyLinkedList(st.Peek());
            Console.WriteLine("The min stack is as shown below:");
            LinkedListHelper.PrintSinglyLinkedList(st.Min());
        }
    }
}
