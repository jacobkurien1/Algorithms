using AlgorithmProblems.Linked_List.Linked_List_Helper;
using AlgorithmProblems.Stack_and_Queue.Stack_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue
{
    class StackOfPlates
    {
        List<StackViaLinkedListWithLimit<int>> stacks = new List<StackViaLinkedListWithLimit<int>>();
        public List<StackViaLinkedListWithLimit<int>> Stacks {
            get {
                return stacks;
            }
            set {
                stacks = value;
            }
        }
        int capacity;

        public StackOfPlates(int capacity)
        {
            this.capacity = capacity;
        }
        public void Push(int data)
        {
            if (stacks.Count != 0)
            {
                StackViaLinkedListWithLimit<int> lastStack = stacks[stacks.Count - 1];
                if(lastStack.CurrentCapacity == capacity)
                {
                    // The capacity has reached, need to create a new stack and add the data to that stack
                    AddNewStackAndAddData(data);
                }
                else
                {
                    lastStack.Push(data);
                }
            }
            else
            {
                // No stack is ppresent hence add the new stack and add data to that stack
                AddNewStackAndAddData(data);
            }
        }

        public int Pop()
        {
            if(stacks.Count == 0)
            {
                throw new Exception("Stack Empty");
            }
            StackViaLinkedListWithLimit<int> lastStack = stacks[stacks.Count - 1];
            SingleLinkedListNode<int> popedNode = lastStack.Pop();
            if (lastStack.CurrentCapacity == 0)
            {
                // Delete the last stack and return the poped element of the next stack
                stacks.RemoveAt(stacks.Count - 1);
            }
            return (popedNode == null) ? 0 : popedNode.Data;
        }

        private void AddNewStackAndAddData(int data)
        {
            StackViaLinkedListWithLimit<int> newStack = new StackViaLinkedListWithLimit<int>(capacity);
            newStack.Push(data);
            stacks.Add(newStack);
        }

        #region TestMethods
        public static void TestStackOfPlates()
        {
            StackOfPlates sp = new StackOfPlates(3);
            for(int i=0; i<=7; i++)
            {
                sp.Push(i);
            }
            PrintAllTheStacks(sp);
            for (int i = 0; i <= 8; i++)
            {
                try
                {
                    Console.WriteLine("The poped element is" + sp.Pop());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                PrintAllTheStacks(sp);
            }
        }

        private static void PrintAllTheStacks(StackOfPlates sp)
        {
            // Print data in the stack
            foreach(StackViaLinkedListWithLimit<int> st in sp.Stacks)
            {
                Console.WriteLine("Print the stack of plates");
                LinkedListHelper.PrintSinglyLinkedList(st.Peek());
            }
        }
        #endregion

    }
}
