using AlgorithmProblems.Linked_List.Linked_List_Helper;
using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// Convert each level of a binary tree into a linked list.
    /// Return a List of linkedlists
    /// 
    /// </summary>
    class LinkedListOfTreeNodesAtEachDepth
    {
        /// <summary>
        /// We can use 2 linked list to do the operation in BFS fashion.
        /// 
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static List<SinglyLinkedList<BinaryTreeNode<int>>> CreateLinkedListOfTreeNodesAtEachDepth(BinaryTreeNode<int> head)
        {
            // the list of the linkedlist of nodes at each level
            List<SinglyLinkedList<BinaryTreeNode<int>>> retList = new List<SinglyLinkedList<BinaryTreeNode<int>>>();

            SinglyLinkedList<BinaryTreeNode<int>> lastLinkedList = new SinglyLinkedList<BinaryTreeNode<int>>();
            lastLinkedList.AppendToEnd(head);
            while (lastLinkedList.Head != null)
            {
                SingleLinkedListNode<BinaryTreeNode<int>> lastLinkedListNode = lastLinkedList.Head;
                SinglyLinkedList<BinaryTreeNode<int>> newLinkedList = new SinglyLinkedList<BinaryTreeNode<int>>();

                while (lastLinkedListNode != null)
                {
                    if (lastLinkedListNode.Data.Left != null)
                    {
                        // Add the left child to the linked list
                        newLinkedList.AppendToEnd(lastLinkedListNode.Data.Left);
                    }
                    if (lastLinkedListNode.Data.Right != null)
                    {
                        // Add the right child to the linked list
                        newLinkedList.AppendToEnd(lastLinkedListNode.Data.Right);
                    }

                    lastLinkedListNode = lastLinkedListNode.NextNode;
                }
                retList.Add(lastLinkedList);
                lastLinkedList = newLinkedList;
            }

            return retList;
        }

        private static void PrintListOfNodesAtEachLevel(List<SinglyLinkedList<BinaryTreeNode<int>>> listOfNodesAtEachLevel)
        {
            foreach (SinglyLinkedList<BinaryTreeNode<int>> currentList in listOfNodesAtEachLevel)
            {
                Console.WriteLine("The nodes at this level are:");
                SingleLinkedListNode<BinaryTreeNode<int>> currentNode = currentList.Head;
                while (currentNode != null)
                {
                    Console.Write(currentNode.Data.Data + " -> ");
                    currentNode = currentNode.NextNode;
                }
                Console.WriteLine("null");
            }
        }

        public static void TestCreateLinkedListOfTreeNodesAtEachDepth()
        {
            // test with a complete Binary Tree
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, true);
            PrintListOfNodesAtEachLevel(CreateLinkedListOfTreeNodesAtEachDepth(bt1.Head));

            // test with a skewed binary tree
            BinaryTree<int> bt2 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, false);
            PrintListOfNodesAtEachLevel(CreateLinkedListOfTreeNodesAtEachDepth(bt2.Head));
        }

    }
}
