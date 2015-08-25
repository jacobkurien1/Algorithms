using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    class ExteriorOfBT
    {
        /// <summary>
        /// Prints the exterior of the Binary Tree in the anti clockwise direction
        /// Step1: Do Preorder of the left boundary of the BT
        /// Step2: Print all the leaf nodes
        /// Step3: Do Post order print of the right boundary of the BT
        /// </summary>
        public static void PrintExteriorOfBT(BinaryTreeNode<int> treeNode)
        {
            PrintLeftBoundaryPreOrder(treeNode, true);
            PrintLeaves(treeNode);
            PrintRightBoundaryPostOrder(treeNode, true);
            Console.WriteLine();
        }

        /// <summary>
        /// Step1: Do Preorder of the left boundary of the BT
        /// Make sure you dont print the leaves
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="IsLeftmostBoundary"></param>
        private static void PrintLeftBoundaryPreOrder(BinaryTreeNode<int> treeNode, bool IsLeftmostBoundary)
        {
            if(treeNode == null || !IsLeftmostBoundary)
            {
                // null check and make sure that the node has the possibility to be the leftmost boundary
                return;
            }
            if(IsLeftmostBoundary && !(treeNode.Left == null && treeNode.Right == null))
            {
                // this node is the left boundary but not the leaf node
                Console.Write(treeNode.Data + " -> ");
            }
            PrintLeftBoundaryPreOrder(treeNode.Left, IsLeftmostBoundary);
            PrintLeftBoundaryPreOrder(treeNode.Right, IsLeftmostBoundary && treeNode.Left == null);
        }

        /// <summary>
        /// Print all the leaf nodes
        /// </summary>
        /// <param name="treeNode"></param>
        private static void PrintLeaves(BinaryTreeNode<int> treeNode)
        {
            if(treeNode == null)
            {
                // null check
                return;
            }
            if(treeNode.Left ==null && treeNode.Right == null)
            {
                // We have found a leaf node
                Console.Write(treeNode.Data + " -> ");
            }
            PrintLeaves(treeNode.Left);
            PrintLeaves(treeNode.Right);
        }

        /// <summary>
        /// Step3: Do Post order print of the right boundary of the BT
        /// Make sure you dont print the leaf nodes
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="IsRightmostBoundary"></param>
        private static void PrintRightBoundaryPostOrder(BinaryTreeNode<int> treeNode, bool IsRightmostBoundary)
        {
            if (treeNode == null || !IsRightmostBoundary)
            {
                // null check and make sure the node has a possibility of the being the rightmost boundary
                return;
            }
            PrintRightBoundaryPostOrder(treeNode.Right, IsRightmostBoundary);
            PrintRightBoundaryPostOrder(treeNode.Left, IsRightmostBoundary && treeNode.Right == null);
            if(IsRightmostBoundary && !(treeNode.Right==null && treeNode.Left == null))
            {
                Console.Write(treeNode.Data + " -> ");
            }
        }

        public static void TestPrintExteriorOfBT()
        {
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, true);
            PrintExteriorOfBT(bt1.Head);
        }
    }
}
