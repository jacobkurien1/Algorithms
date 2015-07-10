using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List.Linked_List_Helper
{
    public class SingleLinkedListNode<T>
    {
        public SingleLinkedListNode<T> NextNode { get; set; }
        public T Data { get; set; }

        public SingleLinkedListNode(T data)
        {
            Data = data;
        }

        /// <summary>
        /// Appends a node to the end of the singly linked list
        /// </summary>
        /// <param name="data">data for the node to be appended</param>
        public void AppendToEnd(T data)
        {
            SingleLinkedListNode<T> endNode = new SingleLinkedListNode<T>(data);
            SingleLinkedListNode<T> currentNode = this;
            while(currentNode.NextNode != null)
            {
                currentNode = currentNode.NextNode;
            }
            currentNode.NextNode = endNode;
        }

        public void AppendNext(T data)
        {
            SingleLinkedListNode<T> nextNode = this.NextNode;
            SingleLinkedListNode<T> nodeToAdd = new SingleLinkedListNode<T>(data);
            nodeToAdd.NextNode = nextNode;
            this.NextNode = nodeToAdd;
        }

    }
}
