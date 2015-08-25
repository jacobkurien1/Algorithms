using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    class LinkedListFromLeavesOfBT
    {
        public LinkedList<BinaryTreeNode<int>> LinkedListOfLeaves { get; set; }
        private void CreateLinkedListFromLeavesOfBTRecursive(BinaryTreeNode<int> treeNode)
        {
            if(treeNode == null)
            {
                // null check
                return;
            }
            if(treeNode.Left == null && treeNode.Right == null)
            {
                // this is a leaf node
                if(LinkedListOfLeaves == null)
                {
                    LinkedListOfLeaves = new LinkedList<BinaryTreeNode<int>>();
                }
                LinkedListOfLeaves.AddLast(treeNode);
            }
            else
            {
                CreateLinkedListFromLeavesOfBTRecursive(treeNode.Left);
                CreateLinkedListFromLeavesOfBTRecursive(treeNode.Right);
            }
        }

        public static void TestLinkedListFromLeavesOfBT()
        {
            LinkedListFromLeavesOfBT llSol = new LinkedListFromLeavesOfBT();
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, true);
            llSol.CreateLinkedListFromLeavesOfBTRecursive(bt1.Head);

            // print the linked list
            LinkedListNode<BinaryTreeNode<int>> currentNode = llSol.LinkedListOfLeaves.First;
            while(currentNode!=null)
            {
                Console.Write(currentNode.Value.Data + " -> ");
                currentNode = currentNode.Next;
            }
            Console.WriteLine(" null");
        }
    }
}
