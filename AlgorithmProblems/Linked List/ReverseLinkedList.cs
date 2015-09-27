using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List
{
    class ReverseLinkedList
    {
        private static SingleLinkedListNode<int> Reverse(SingleLinkedListNode<int> head)
        {
            SingleLinkedListNode<int> previousNode = null;
            SingleLinkedListNode<int> currentNode = head;
            SingleLinkedListNode<int> nextNode = null;
            while (currentNode != null)
            {
                nextNode = currentNode.NextNode;
                currentNode.NextNode = previousNode;
                previousNode = currentNode;
                currentNode = nextNode;
            }
            return previousNode;// this will be the head of the reversed linked list
        }

        private static SingleLinkedListNode<int> ReverseRecursively(SingleLinkedListNode<int> currentNode, SingleLinkedListNode<int> previousNode)
        {
            if(currentNode == null)
            {
                return previousNode;
            }
            SingleLinkedListNode<int> nextNode = currentNode.NextNode;
            currentNode.NextNode = previousNode;
            return ReverseRecursively(nextNode, currentNode);
        }

        public static void TestReverseLinkedList()
        {
            SingleLinkedListNode<int> ll = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(ll);
            LinkedListHelper.PrintSinglyLinkedList(Reverse(ll));

            ll = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(ll);
            LinkedListHelper.PrintSinglyLinkedList(ReverseRecursively(ll, null));
        }
    }
}
