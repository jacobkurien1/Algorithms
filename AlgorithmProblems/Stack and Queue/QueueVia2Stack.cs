using AlgorithmProblems.Linked_List.Linked_List_Helper;
using AlgorithmProblems.Stack_and_Queue.Stack_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue
{
    /// <summary>
    /// Here the trick is to always push to stack1 and 
    /// pop from stack 2. When stack 2 is empty, pop all elements from stack1
    /// and push it to stack2 and then pop stack2. When stack1 is full and
    /// if stack2 is empty we can pop all elements from stack1 to stack2 
    /// and then push the new element to stack1.
    /// </summary>
    class QueueVia2Stack
    {
        int capacityOfIndividualStack;
        StackViaLinkedListWithLimit<int> stack1;
        StackViaLinkedListWithLimit<int> stack2;

        public QueueVia2Stack(int capacity)
        {
            this.capacityOfIndividualStack = capacity;
            stack1 = new StackViaLinkedListWithLimit<int>(capacity);
            stack2 = new StackViaLinkedListWithLimit<int>(capacity);
        }

        public void Push(int dataToPush)
        {
            try
            {
                stack1.Push(dataToPush);
            }
            catch(Exception exp)
            {
                if(exp.Message == "Stack Full" && stack2.CurrentCapacity == 0)
                {
                    // add all elements from stack1 to stack2
                    while(stack1.CurrentCapacity != 0)
                    {
                        stack2.Push(stack1.Pop());
                    }
                    // now push the new data to stack1
                    stack1.Push(dataToPush);

                }
                else
                {
                    throw exp;
                }
            }
        }

        public SingleLinkedListNode<int> Pop()
        {
            // if stack2 is empty then pop all elements from stack1 to stack2
            if(stack2.CurrentCapacity == 0)
            {
                while (stack1.CurrentCapacity != 0)
                {
                    stack2.Push(stack1.Pop());
                }
            }
            // pop from stack2
            return stack2.Pop();
        }

        public static void TestQueueVia2Stack()
        {
            QueueVia2Stack queue = new QueueVia2Stack(5);
            for(int i=0; i<7; i++)
            {
                queue.Push(i);
            }

            Console.WriteLine("The 2 stacks are as shown below:");
            LinkedListHelper.PrintSinglyLinkedList(queue.stack1.Peek());
            LinkedListHelper.PrintSinglyLinkedList(queue.stack2.Peek());

            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine("The poped value is "+queue.Pop().Data);
            }

            Console.WriteLine("The 2 stacks are as shown below:");
            LinkedListHelper.PrintSinglyLinkedList(queue.stack1.Peek());
            LinkedListHelper.PrintSinglyLinkedList(queue.stack2.Peek());
        }
    }
}
