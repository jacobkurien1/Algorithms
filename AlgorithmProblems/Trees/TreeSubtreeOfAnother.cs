using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    class TreeSubtreeOfAnother
    {
#region Algo1
        /// <summary>
        /// Is tree2 a subtree of tree1
        /// </summary>
        /// <param name="tree1"></param>
        /// <param name="tree2"></param>
        /// <returns></returns>
        private static bool IsSubtree(BinaryTreeNode<int> treeNode1, BinaryTreeNode<int> treeNode2)
        {
            if(treeNode1 == null && treeNode2 == null)
            {
                // base condition
                return true;
            }
            if(treeNode1 == null || treeNode2==null)
            {
                //Error condition check as one of treeNode1 or treeNode2 is null
                return false;
            }
            if(treeNode1.Data == treeNode2.Data)
            {
                return IsSubtree(treeNode1.Left, treeNode2.Left) && IsSubtree(treeNode1.Right, treeNode2.Right);
            }
            return false;
        }

        /// <summary>
        /// This checks whether the subtree is a contained in the mainTree
        /// This is a Recursive alogorithm with a running time of O(n*m)
        /// where n is the number of elements in mainTree and m is the number od elements in subTree
        /// </summary>
        /// <param name="mainTree">MainTree which needs to be traversed</param>
        /// <param name="subTree">subTree which may or maynot be contained in the mainTree</param>
        /// <returns></returns>
        private static bool MatchTree(BinaryTreeNode<int> mainTree, BinaryTreeNode<int> subTree)
        {
            if(IsSubtree(mainTree, subTree))
            {
                // we found our match
                return true;
            }
            else
            {
                // we need to check in the left and right subtree
                if (mainTree != null)
                {
                    return MatchTree(mainTree.Left, subTree) || MatchTree(mainTree.Right, subTree);
                }
            }
            return false;
        }
#endregion
#region Algo2
        /// <summary>
        /// This is an O(n+m) algorithm
        /// Step1: We will do an inorder traversal of mainTree and subTree and check if
        /// inorder traversal of subTree is a substring of inorder traversal of mainTree
        /// Step2: We do a preorder/postorder traversal of both the trees and make sure 
        /// subtree's traversal result in contained in mainTree's traversal result
        /// </summary>
        /// <param name="mainTree"></param>
        /// <param name="subTree"></param>
        /// <returns></returns>
        private static bool MatchTreeAlgo2(BinaryTreeNode<int> mainTree, BinaryTreeNode<int> subTree)
        {
            // Step1: We will do an inorder traversal of mainTree and subTree and check if
            // inorder traversal of subTree is a substring of inorder traversal of mainTree
            if(WalkTheTree.InOrderTraversalReturnAsString(mainTree).Contains(WalkTheTree.InOrderTraversalReturnAsString(subTree)))
            {
                // Step2: We do a preorder/postorder traversal of both the trees and make sure 
                // subtree's traversal result in contained in mainTree's traversal result
                return (WalkTheTree.PreOrderTraversalReturnAsString(mainTree).Contains(WalkTheTree.PreOrderTraversalReturnAsString(subTree)));
            }
            return false;
        }

#endregion

        public static void TestMatchTree()
        {
            // test with a complete Binary Tree
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, true);
            BinaryTree<int> bt2 = new BinaryTree<int>(new int[] {12, 9, 13 }, true);
            Console.WriteLine("b2 is a subtree of b1 from algo1: "+ MatchTree(bt1.Head, bt2.Head));
            Console.WriteLine("b2 is a subtree of b1 from algo2: " + MatchTreeAlgo2(bt1.Head, bt2.Head));

            // test with a skewed binary tree
            BinaryTree<int> bt3 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, false);
            BinaryTree<int> bt4 = new BinaryTree<int>(new int[] { 13, 0, 2 }, false);
            Console.WriteLine("b4 is a subtree of b3 from algo1: " + MatchTree(bt3.Head, bt4.Head));
            Console.WriteLine("b4 is a subtree of b3 from algo2: " + MatchTreeAlgo2(bt3.Head, bt4.Head));

            BinaryTree<int> bt5 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, true);
            BinaryTree<int> bt6 = new BinaryTree<int>(new int[] { 8, 4, 12 }, true);
            Console.WriteLine("b6 is a subtree of b5 from algo1: " + MatchTree(bt5.Head, bt6.Head));
            Console.WriteLine("b6 is a subtree of b5 from algo2: " + MatchTreeAlgo2(bt5.Head, bt6.Head));
        }
    }
}
