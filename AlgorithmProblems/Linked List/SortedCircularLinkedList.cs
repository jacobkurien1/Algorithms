using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List
{

    /// <summary>
    /// Do an insertion in the sorted circular linked list
    /// </summary>
    class SortedCircularLinkedList
    {
        public SingleLinkedListNode<int> Head { get; set; }

        /// <summary>
        /// inserts a new value in the sorted linked list
        /// </summary>
        /// <param name="value"></param>
        public void Insert(int value)
        {
            if(Head == null)
            {
                // this the first element
                Head = new SingleLinkedListNode<int>(value);
                Head.NextNode = Head;
            }
            else if(Head.Data >= value)
            {
                // new node needs to be inserted at the start of the linked list
                SingleLinkedListNode<int> newHeadNode = new SingleLinkedListNode<int>(value);
                newHeadNode.NextNode = Head;

                // make the last node point to the newHeadNode
                SingleLinkedListNode<int> currentNode = Head;
                while (currentNode.NextNode != Head)
                {
                    currentNode = currentNode.NextNode;
                }
                currentNode.NextNode = newHeadNode;

                // make the newHeadNode = Head
                Head = newHeadNode;
            }
            else
            {
                // the insertion needs to happen at a different place other than the head

                SingleLinkedListNode<int> current = Head;
                while (current.NextNode != Head && current.NextNode.Data < value)
                {
                    // keep going fwd till the cycle ends or the node becomes greater than value
                    current = current.NextNode;
                }

                // do the new node insertion after the current node and assign current.NextNode to newNode.NextNode
                SingleLinkedListNode<int> nodeToInsert = new SingleLinkedListNode<int>(value);
                nodeToInsert.NextNode = current.NextNode;
                current.NextNode = nodeToInsert;
            }
        }

        public void Delete(int value)
        {
            if(Head != null)
            {
                SingleLinkedListNode<int> currentNode = Head;
                if (Head.Data == value)
                {
                    currentNode = Head;

                    // go to the end of the cycle
                    while(currentNode.NextNode != Head)
                    {
                        currentNode = currentNode.NextNode;
                    }

                    // delete the head
                    currentNode.NextNode = Head.NextNode;
                    Head = Head.NextNode;
                }
                else
                {
                    while (currentNode.NextNode != Head)
                    {
                        if (currentNode.NextNode.Data == value)
                        {
                            // we need to delete currentNode.NextNode
                            currentNode.NextNode = currentNode.NextNode.NextNode;
                            break;
                        }
                        currentNode = currentNode.NextNode;
                    }
                }
            }
        }

        /// <summary>
        /// Prints the circular linked list
        /// </summary>
        public void Print()
        {
            SingleLinkedListNode<int> current = Head;
            if (current != null)
            {
                do
                {
                    Console.Write("{0} ->", current.Data);
                    current = current.NextNode;
                } while (current != Head);
                Console.WriteLine();
            }
        }

        public static void TestCircularLinkedList()
        {
            SortedCircularLinkedList scll = new SortedCircularLinkedList();
            scll.Insert(5);
            scll.Insert(6);
            scll.Insert(8);
            scll.Insert(1);
            scll.Insert(3);
            scll.Insert(9);
            scll.Insert(3);
            scll.Print();
            scll.Delete(3);
            scll.Print();
            scll.Delete(1);
            scll.Print();
            scll.Delete(9);
            scll.Print();
        }
    }
}
