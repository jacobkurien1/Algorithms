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
        public static SingleLinkedListNode<int> StartOfCircularLinkedList(SingleLinkedListNode<int> head)
        {
            SingleLinkedListNode<int> node1 = head;
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

            SingleLinkedListNode<int> node = StartOfCircularLinkedList(linkedlist);
            Console.WriteLine("The start of the circle is {0} and the expected node is {1}", node.Data, randomNode.Data);

        }
    }
}
