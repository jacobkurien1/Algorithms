using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List
{
    class DeleteLinkedListNode
    {
        /// <summary>
        /// Delete the first node with the data "del"
        /// </summary>
        /// <param name="head"></param>
        /// <param name="del"></param>
        /// <returns></returns>
        public SingleLinkedListNode<int> DeleteFirstNode(SingleLinkedListNode<int> head, int del)
        {
            // Check for error condition
            if(head == null)
            {
                return null;
            }
            SingleLinkedListNode<int> currentNode = head;
            while(currentNode.NextNode != null)
            {
                if (currentNode.NextNode.Data == del)
                {
                    currentNode.NextNode = currentNode.NextNode.NextNode;
                    break;
                }
                currentNode = currentNode.NextNode;
            }
            return head;
        }

        public SingleLinkedListNode<int> DeleteAllNodes(SingleLinkedListNode<int> head, int del)
        {
            // Check for error condition
            if (head == null)
            {
                return null;
            }
            SingleLinkedListNode<int> currentNode = head;
            while (currentNode!= null && currentNode.NextNode != null)
            {
                if (currentNode.NextNode.Data == del)
                {
                    currentNode.NextNode = currentNode.NextNode.NextNode;
                }
                currentNode = currentNode.NextNode;
            }
            return head;
        }

        public static void TestDeleteFirstNode()
        {
            Console.WriteLine("Test delete node with first occurance");
            DeleteLinkedListNode dlt = new DeleteLinkedListNode();

            SingleLinkedListNode<int> ll = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(ll);
            dlt.DeleteFirstNode(ll, 2);
            LinkedListHelper.PrintSinglyLinkedList(ll);

            SingleLinkedListNode<int> ll1 = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(ll1);
            dlt.DeleteAllNodes(ll1, 2);
            LinkedListHelper.PrintSinglyLinkedList(ll1);

        }
    }
}
