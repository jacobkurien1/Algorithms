using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List
{
    class CommonElementInTwoLinkedList
    {
        private static SingleLinkedListNode<int> CommonElement(SingleLinkedListNode<int> node1, SingleLinkedListNode<int> node2)
        {
            // get the length of both the linked list
            int len1 = GetLengthOfLinkedList(node1);
            int len2 = GetLengthOfLinkedList(node2);
            
            // find the larger/smaller linked list
            SingleLinkedListNode<int> larger = node1;
            SingleLinkedListNode<int> smaller = node2;
            int diff = 0;

            if(len1>len2)
            {
                diff = len1 - len2;
            }
            else
            {
                larger = node2;
                smaller = node1;
                diff = len2 - len1;
            }

            // move the diff down in the larger linked list
            while(diff>0)
            {
                larger = larger.NextNode;
                diff--;
            }

            // now traverse both the linked list linked list node in larger and smaller will have same length
            while(larger!=null)
            {
                if(larger==smaller)
                {
                    // we have found the first common element
                    return larger;
                }
                larger = larger.NextNode;
                smaller = smaller.NextNode;
            }
            return null;
        }

        private static int GetLengthOfLinkedList(SingleLinkedListNode<int> node)
        {
            int len = 0;
            while(node!=null)
            {
                len++;
                node = node.NextNode;
            }
            return len;
        }

        public static void TestCommonElement()
        {
            // Create 2 linked list of common nodes
            SingleLinkedListNode<int> node1 = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(node1);
            SingleLinkedListNode<int> node2 = LinkedListHelper.CreateSinglyLinkedList(5, 30,40);
            SingleLinkedListNode<int> currentNode2 = node2;
            while(currentNode2.NextNode!=null)
            {
                currentNode2 = currentNode2.NextNode;
            }
            SingleLinkedListNode<int> currentNode1 = node1;
            int diff = 4;
            while(diff>0)
            {
                currentNode1 = currentNode1.NextNode;
                diff--;
            }
            currentNode2.NextNode = currentNode1;
            LinkedListHelper.PrintSinglyLinkedList(node2);

            // Get the common Node
            SingleLinkedListNode<int> commonNode =CommonElement(node1, node2);
            if(commonNode!=null)
            {
                Console.WriteLine("the common node is {0} and the expected was {1}", commonNode.Data, currentNode1.Data);
            }
            else
            {
                Console.WriteLine("The common node is not found");
            }

            // Test case 2: Test the same with 2 different linked list w/o a common node
            SingleLinkedListNode<int> node3 = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(node3);
            SingleLinkedListNode<int> node4 = LinkedListHelper.CreateSinglyLinkedList(10, 20,40);
            LinkedListHelper.PrintSinglyLinkedList(node4);
            // Get the common Node
            SingleLinkedListNode<int> commonNode2 = CommonElement(node3, node4);
            if (commonNode2 == null)
            {
                Console.WriteLine("The common node is not found");
            }
        }
    }
}
