using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List
{
    class MedianForCircularLinkedList
    {
        /// <summary>
        /// Get median from a sorted circular linked list
        /// </summary>
        public static double GetMedian(SingleLinkedListNode<int> head)
        {
            // Step1: get the inflex point and get the length
            SingleLinkedListNode<int> currentNode = head;
            SingleLinkedListNode<int> nextNode = (head!=null)? head.NextNode: null;
            if(head == null || nextNode == null)
            {
                // the linked list is not circular
                return 0;
            }
            int length = 0;
            while(currentNode.Data <= nextNode.Data)
            {
                currentNode = currentNode.NextNode;
                nextNode = nextNode.NextNode;
                if(currentNode==null || nextNode==null)
                {
                    // the linked list is not circular
                    return 0;
                }
                length++;
            }
            length++;

            // Step3: Get the median by moving length/2
            currentNode = head;
            int IsEvenNumbered = length % 2;
            length = (int)Math.Ceiling(length/2.0);
            while (length !=1)
            {
                length--;
                currentNode = currentNode.NextNode;
            }
            if (IsEvenNumbered == 0)
            {
                return (currentNode.Data + currentNode.NextNode.Data) / 2.0;
            }
            else
            {
                return currentNode.Data;
            }

        }

        public static void TestGetMedian()
        {
            SingleLinkedListNode<int> headNode = LinkedListHelper.SortedLinkedList(10, 5);
            LinkedListHelper.PrintSinglyLinkedList(headNode);
            SingleLinkedListNode<int> currentNode = headNode;
            // get the last node
            while(currentNode.NextNode!=null)
            {
                currentNode = currentNode.NextNode;
            }
            currentNode.NextNode = headNode;

            Console.WriteLine("The median for the circular linked list is {0}", GetMedian(headNode));

            headNode = LinkedListHelper.SortedLinkedList(9, 5);
            LinkedListHelper.PrintSinglyLinkedList(headNode);
            currentNode = headNode;
            // get the last node
            while (currentNode.NextNode != null)
            {
                currentNode = currentNode.NextNode;
            }
            currentNode.NextNode = headNode;

            Console.WriteLine("The median for the circular linked list is {0}", GetMedian(headNode));
        }
    }
}
