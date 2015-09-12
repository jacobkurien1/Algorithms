using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List
{
    class ReverseAdjacentNodesInLinkedList
    {
        /// <summary>
        /// This reversal is done using 3 pointers by actually rearranging the node
        /// </summary>
        /// <param name="HeadNode"></param>
        /// <returns></returns>
        private static SingleLinkedListNode<int> ReverseAdjacentNodes(SingleLinkedListNode<int> HeadNode)
        {
            SingleLinkedListNode<int> currentNode = HeadNode;
            SingleLinkedListNode<int> previousNode = null;
            // We will move forward in twos
            while(currentNode!=null && currentNode.NextNode!=null)
            {
                SingleLinkedListNode<int> llNode1 = currentNode;
                SingleLinkedListNode<int> llNode2 = currentNode.NextNode;

                // Reverse the adjacent nodes
                SingleLinkedListNode<int> temp = llNode2.NextNode;
                llNode2.NextNode = llNode1;
                llNode1.NextNode = temp;
                if(previousNode!=null)
                {
                    previousNode.NextNode = llNode2;
                }
                else
                {
                    HeadNode = llNode2;
                }

                currentNode = temp;
                previousNode = llNode1;
            }
            return HeadNode;
        }

        /// <summary>
        /// This reversal is done by swapping the data in the linked list
        /// This is a much simpler algorithm
        /// </summary>
        /// <param name="HeadNode"></param>
        /// <returns></returns>
        private static SingleLinkedListNode<int> ReverseAdjacentNodesAlgo2(SingleLinkedListNode<int> HeadNode)
        {
            SingleLinkedListNode<int> currentNode = HeadNode;
            while (currentNode != null && currentNode.NextNode != null)
            {
                SingleLinkedListNode<int> llNode1 = currentNode;
                SingleLinkedListNode<int> llNode2 = currentNode.NextNode;

                // Reverse the adjacent nodes
                int temp = llNode2.Data;
                llNode2.Data = llNode1.Data;
                llNode1.Data = temp;

                currentNode = llNode2.NextNode;
            }
            return HeadNode;
        }

        public static void TestReverseAdjacentNodes()
        {
            SingleLinkedListNode<int> node1 = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(node1);
            SingleLinkedListNode<int> node2 = ReverseAdjacentNodes(node1);
            Console.WriteLine("the reversed adjacent nodes for algo1 are:");
            LinkedListHelper.PrintSinglyLinkedList(node2);

            SingleLinkedListNode<int> node3 = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(node3);
            SingleLinkedListNode<int> node4 = ReverseAdjacentNodesAlgo2(node3);
            Console.WriteLine("the reversed adjacent nodes for algo2 are:");
            LinkedListHelper.PrintSinglyLinkedList(node4);

            SingleLinkedListNode<int> node5 = LinkedListHelper.CreateSinglyLinkedList(11);
            LinkedListHelper.PrintSinglyLinkedList(node5);
            SingleLinkedListNode<int> node6 = ReverseAdjacentNodes(node5);
            Console.WriteLine("the reversed adjacent nodes for algo1 are:");
            LinkedListHelper.PrintSinglyLinkedList(node6);

            SingleLinkedListNode<int> node7 = LinkedListHelper.CreateSinglyLinkedList(11);
            LinkedListHelper.PrintSinglyLinkedList(node7);
            SingleLinkedListNode<int> node8 = ReverseAdjacentNodesAlgo2(node7);
            Console.WriteLine("the reversed adjacent nodes for algo2 are:");
            LinkedListHelper.PrintSinglyLinkedList(node8);
        }
    }
}
