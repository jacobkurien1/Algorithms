using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List
{
    /// <summary>
    /// Delete a node from singly linked list when the direct reference to only
    /// that node is given.
    /// </summary>
    class DeleteNodeWithDirectReference
    {
        public bool DeleteNode(SingleLinkedListNode<int> delNode)
        {
            // Error check: null node cannot be deleted
            if(delNode == null)
            {
                return false;
            }
            if (delNode.NextNode == null)
            {
                // This is the case which will not be handled by this method
                // if we do delNode = null, we are only changing the reference delNode
                // and it will not affect the node

                // We can mark the last node as dummy node, but the functions calling
                // this method should also know how to handle the dummy node.
                return false;
            }
            else
            {
                SingleLinkedListNode<int> nextNode = delNode.NextNode;
                delNode.Data = nextNode.Data;
                delNode.NextNode = nextNode.NextNode;
                return true;
            }
        }

        public static void TestDeleteNode()
        {
            Console.WriteLine("Delete the linked list node with direct reference");
            DeleteNodeWithDirectReference delNode = new DeleteNodeWithDirectReference();
            
            SingleLinkedListNode<int> ll = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(ll);
            SingleLinkedListNode<int> rndNode = LinkedListHelper.GetRandomNode(ll, 10);

            Console.WriteLine("The random node selected is" + rndNode.Data);
            delNode.DeleteNode(rndNode);
            Console.WriteLine("The linked list after node deletion is");
            LinkedListHelper.PrintSinglyLinkedList(ll);
        }
    }
}
