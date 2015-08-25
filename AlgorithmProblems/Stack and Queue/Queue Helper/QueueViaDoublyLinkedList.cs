using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue.Queue_Helper
{
    class QueueViaDoublyLinkedList<T>
    {
        private DoublyLinkedListNode<T> First { get; set; }
        private DoublyLinkedListNode<T> Last { get; set; }

        public int Capacity { get; set; }

        public void Enqueue(T data)
        {
            if (First != null)
            {
                DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(data);
                Last.NextNode = newNode;
                newNode.PreviousNode = Last;
                Last = newNode;
            }
            else
            {
                First = Last = new DoublyLinkedListNode<T>(data);
            }
            Capacity++;
        }

        public void Enqueue(DoublyLinkedListNode<T> dataNode)
        {
            // Make sure that this node does not create circular dependency
            dataNode.NextNode = null;
            dataNode.PreviousNode = null;

            if (First != null)
            {
                Last.NextNode = dataNode;
                dataNode.PreviousNode = Last;
                Last = dataNode;
            }
            else
            {
                First = Last = dataNode;
            }
            Capacity++;
        }

        public DoublyLinkedListNode<T> Dequeue()
        {
            DoublyLinkedListNode<T> dequeueNode = null;
            if (First != null)
            {
                dequeueNode = First;
                First = First.NextNode;
                First.PreviousNode = null;
                Capacity--;
            }
            return dequeueNode;
        }

        /// <summary>
        /// Deletes the node from the linked list.
        /// If the nodeToDelete is not present then we might decrement the capacity w/o deleting the node from the list
        /// We can have a dictionary of node references to make sure that the node is present in the dicitonary(not done in this method)
        /// </summary>
        /// <param name="nodeToDelete">it is assumed that this node is the reference to the node present in the linked list</param>
        public void Delete(DoublyLinkedListNode<T> nodeToDelete)
        {
            if(nodeToDelete == First)
            {
                First = First.NextNode;
                if (First != null)
                {
                    First.PreviousNode = null;
                }
            }
            if(nodeToDelete == Last)
            {
                Last = Last.PreviousNode;
                if (Last != null)
                {
                    Last.NextNode = null;
                }
            }
            if (nodeToDelete != First && nodeToDelete != Last)  // This guarentees nodeToDelete.PreviousNode and nodeToDelete.NextNode are not null
            {
                // But will check nodeToDelete.PreviousNode and nodeToDelete.NextNode for the case when the nodeToDelete is not present in the linked list
                if (nodeToDelete.PreviousNode != null)
                {
                    nodeToDelete.PreviousNode.NextNode = nodeToDelete.NextNode;
                }
                if (nodeToDelete.NextNode != null)
                {
                    nodeToDelete.NextNode.PreviousNode = nodeToDelete.PreviousNode;
                }
            }
            Capacity--;
        }

        public void PrintQueue()
        {
            DoublyLinkedListNode<T> head = First;
            while (head != null)
            {
                Console.Write(head.Data + " <-> ");
                head = head.NextNode;
            }
            Console.Write("null");
            Console.WriteLine();
        }
    }
}
