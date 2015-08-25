using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List.Linked_List_Helper
{
    class DoublyLinkedListNode<T>
    {
        public DoublyLinkedListNode<T> NextNode { get; set; }
        public DoublyLinkedListNode<T> PreviousNode { get; set; }
        public T Data { get; set; }

        public DoublyLinkedListNode(T data)
        {
            Data = data;
        }

        /// <summary>
        /// Appends a node to the end of the doubly linked list
        /// </summary>
        /// <param name="data">data for the node to be appended</param>
        public void AppendToEnd(T data)
        {
            DoublyLinkedListNode<T> endNode = new DoublyLinkedListNode<T>(data);
            DoublyLinkedListNode<T> currentNode = this;
            while(currentNode.NextNode != null)
            {
                currentNode = currentNode.NextNode;
            }
            currentNode.NextNode = endNode;
            endNode.PreviousNode = currentNode;
        }

        /// <summary>
        /// Appends a node in between this node and its next node
        /// </summary>
        /// <param name="data">data for the node to be appended</param>
        public void AppendNext(T data)
        {
            DoublyLinkedListNode<T> nextNode = this.NextNode;
            DoublyLinkedListNode<T> nodeToAdd = new DoublyLinkedListNode<T>(data);
            nodeToAdd.NextNode = nextNode;
            nextNode.PreviousNode = nodeToAdd;
            this.NextNode = nodeToAdd;
            nodeToAdd.PreviousNode = this;
        }
    }
}
