using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    class AncestorOfTwoNodesInBT
    {
        #region Algo1

        /// <summary>
        /// Search for a node in a tree
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="nodetoSearch"></param>
        /// <returns></returns>
        private static bool SearchNodeInTree(BinaryTreeNode<int> treeNode, BinaryTreeNode<int> nodetoSearch)
        {
            if(treeNode == null)
            {
                // did not find the node
                return false;
            }
            if(treeNode.Data == nodetoSearch.Data)
            {
                // found the node
                return true;
            }
            else
            {
                return SearchNodeInTree(treeNode.Left, nodetoSearch) || SearchNodeInTree(treeNode.Right, nodetoSearch);
            }
        }

        /// <summary>
        /// Get the ancestor of node1 and node2 in the tree
        /// </summary>
        /// <param name="node1">node for which the ancestor needs to be found</param>
        /// <param name="node2">node for which the ancestor needs to be found</param>
        /// <param name="treeNode"></param>
        /// <returns>ancestor of node1 and node2</returns>
        private static BinaryTreeNode<int> GetAncestorOfTwoNodesInBT(BinaryTreeNode<int> node1, BinaryTreeNode<int> node2, BinaryTreeNode<int> treeNode)
        {
            if(treeNode== null)
            {
                // Null check
                return null;
            }
            bool IsNode1PresentInLeftSubtree = SearchNodeInTree(treeNode.Left, node1);
            bool IsNode2PresentInLeftSubtree = SearchNodeInTree(treeNode.Left, node2);
            bool IsNode1PresentInRightSubtree = SearchNodeInTree(treeNode.Right, node1);
            bool IsNode2PresentInRightSubtree = SearchNodeInTree(treeNode.Right, node2);

            // Check if node1 and node2 are in left and right subtrees(or right/left subtrees)
            if(((node1.Data == treeNode.Data || IsNode1PresentInLeftSubtree) && (node2.Data == treeNode.Data || IsNode2PresentInRightSubtree)) || 
                ((node1.Data== treeNode.Data || IsNode1PresentInRightSubtree) && (node2.Data == treeNode.Data || IsNode2PresentInLeftSubtree)))
            {
                // the current node is the common ancestor
                return treeNode;
            }
            else if(IsNode1PresentInLeftSubtree && IsNode2PresentInLeftSubtree)
            {
                // both nodes are in the left subtree
                return GetAncestorOfTwoNodesInBT(node1, node2, treeNode.Left);
            }
            else if (IsNode1PresentInRightSubtree && IsNode2PresentInRightSubtree)
            {
                // both nodes are in right subtree
                return GetAncestorOfTwoNodesInBT(node1, node2, treeNode.Right);
            }
            return null;
        }
        #endregion

        #region Algo2
        /// <summary>
        /// If the nodes have a knowledge of its parents, then trace the path back to the root
        /// Push the nodes into 2 stacks and keep popping till we see a different node
        /// and get the nodes popped just before the last one and that is the common ancestor.
        /// </summary>
        #endregion

        public static void TestGetAncestorOfTwoNodesInBT()
        {
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, true);
            Console.WriteLine("The common ancestor of 4,2 is : " + GetAncestorOfTwoNodesInBT(new BinaryTreeNode<int>(4), new BinaryTreeNode<int>(2), bt1.Head).Data);
            Console.WriteLine("The common ancestor of 1,13 is : " + GetAncestorOfTwoNodesInBT(new BinaryTreeNode<int>(1), new BinaryTreeNode<int>(13), bt1.Head).Data);
            Console.WriteLine("The common ancestor of 9,13 is : " + GetAncestorOfTwoNodesInBT(new BinaryTreeNode<int>(9), new BinaryTreeNode<int>(13), bt1.Head).Data);
            BinaryTreeNode<int> ancestor = GetAncestorOfTwoNodesInBT(new BinaryTreeNode<int>(4), new BinaryTreeNode<int>(14), bt1.Head);
            Console.WriteLine("The common ancestor of 4,14 is : {0}" , ancestor );
        }
    }
}
