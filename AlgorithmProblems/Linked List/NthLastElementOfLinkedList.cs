using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List
{
    class NthLastElementOfLinkedList
    {
        public SingleLinkedListNode<int> NthLastNodeOfLinkedListAlgo1(SingleLinkedListNode<int> head, int n)
        {
            // Step1: Get the length of the linked list
            SingleLinkedListNode<int> currentNode = head;
            int length = 0;
            while(currentNode != null)
            {
                length++;
                currentNode = currentNode.NextNode;
            }
            // Step2: Get the index of the element from the start
            int indexOfNodeFromStart = length - n;

            // Error condition where the length of the linked list is less than n
            if(indexOfNodeFromStart <0 || indexOfNodeFromStart >= length)
            {
                return null;
            }
            
            // Step3: traverse the linked list to get the element
            currentNode = head;
            while (indexOfNodeFromStart > 0)
            {
                currentNode = currentNode.NextNode;
                indexOfNodeFromStart--;
            }
            return currentNode;
        }

        public SingleLinkedListNode<int> NthLastNodeOfLinkedListAlgo2(SingleLinkedListNode<int> head, int n)
        {
            // Step1: Have 2 pointers to the head of the linked list
            SingleLinkedListNode<int> slowPointer = head;
            SingleLinkedListNode<int> fastPointer = head;

            // Step2: Move the fast pointer till we are n distance from the slow pointer
            while(n!=0)
            {
                n--;
                // Error condition where the length of the linked list is less than n
                if(fastPointer == null)
                {
                    return null;
                }
                fastPointer = fastPointer.NextNode;
            }

            // Step3: Move both pointers till next node of fast pointer is not null
            while(fastPointer.NextNode != null)
            {
                fastPointer = fastPointer.NextNode;
                slowPointer = slowPointer.NextNode;
            }

            // Step4: The position of the slow pointer is our nth element from the last element
            return slowPointer;

        }

        public static void TestNthLastNodeOfLinkedList()
        {
            Console.WriteLine("Test the nth node from the last for the linked list");
            NthLastElementOfLinkedList nthLstElem = new NthLastElementOfLinkedList();
            SingleLinkedListNode<int> ll = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(ll);
            SingleLinkedListNode<int> nthNode = nthLstElem.NthLastNodeOfLinkedListAlgo1(ll, 4);

            Console.WriteLine("The 4th node is "+ nthNode!= null? nthNode.Data.ToString() : "null");

            ll = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(ll);
            nthNode = nthLstElem.NthLastNodeOfLinkedListAlgo1(ll, 4);

            Console.WriteLine("The 4th node is " + nthNode != null ? nthNode.Data.ToString() : "null");
        }
    }
}
