using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List
{
    class CircularLinkedList
    {
        /// <summary>
        /// We will use floyd's cycle detection.
        /// We will have two runners. One will move by going to the next node
        /// whereas other will traverse by moving 2 nodes at the same time.
        /// One both the runners meet, that will be the offset point.
        /// So keep a runner at the offset point and another at the head and move one by one.
        /// The node where both the runners meet will be the start of the cycle in the linked list
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static SingleLinkedListNode<int> StartOfCircularLinkedListAlgo1(SingleLinkedListNode<int> head)
        {
            SingleLinkedListNode<int> node1 = head.NextNode;
            SingleLinkedListNode<int> node2 = (head.NextNode!=null)? head.NextNode.NextNode: null;

            while(node1 != null && node2 != null)
            {
                if(node1== node2)
                {
                    break;
                }
                node1 = node1.NextNode;
                node2 = (node2.NextNode != null) ? node2.NextNode.NextNode : null;
            }
            if(node1 ==null || node2==null)
            {
                // there is no cycle
                return null;
            }
            node1 = head;
            while(node1 != node2)
            {
                node1 = node1.NextNode;
                node2 = node2.NextNode;
            }
            return node1;
        }

        public static SingleLinkedListNode<int> StartOfCircularLinkedListAlgo2(SingleLinkedListNode<int> head)
        {
            Dictionary<SingleLinkedListNode<int>, bool> visitedNodes = new Dictionary<SingleLinkedListNode<int>, bool>();
            SingleLinkedListNode<int> currentNode = head;
            while(currentNode!=null && !visitedNodes.ContainsKey(currentNode))
            {
                visitedNodes[currentNode] = true;
                currentNode = currentNode.NextNode;
            }
            return currentNode;
        }

        public static void TestStartOfCircularLinkedList()
        {
            Console.WriteLine("Find the start of the circular linked list");
            SingleLinkedListNode<int> linkedlist = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(linkedlist);

            SingleLinkedListNode<int> randomNode = LinkedListHelper.GetRandomNode(linkedlist, 10);
            Console.WriteLine("The random node is {0}", randomNode.Data);

            SingleLinkedListNode<int> lastNode = linkedlist;
            while(lastNode.NextNode != null)
            {
                lastNode = lastNode.NextNode;
            }
            // Create the circular node
            lastNode.NextNode = randomNode;

            SingleLinkedListNode<int> node = StartOfCircularLinkedListAlgo1(linkedlist);
            Console.WriteLine("The start of the circle is {0} and the expected node is {1}", node.Data, randomNode.Data);

            node = StartOfCircularLinkedListAlgo2(linkedlist);
            Console.WriteLine("The start of the circle is {0} and the expected node is {1}", node.Data, randomNode.Data);

        }
    }
}
