using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// Get the ancestor of a list of nodes in a binary tree
    /// </summary>
    class AncestorOfMultiNodesInBT
    {
        /// <summary>
        /// For each node we will get the LCA and save it in index 0 of the list
        /// The running time of this algo is O(n^2)
        /// </summary>
        /// <param name="allNodes"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static BinaryTreeNode<int> GetLCAForMultiNodes(List<BinaryTreeNode<int>> allNodes, BinaryTreeNode<int> root)
        {
            for(int index = 1; index<allNodes.Count; index++)
            {
                BinaryTreeNode<int> node1 = allNodes[0];
                BinaryTreeNode<int> node2 = allNodes[index];
                allNodes[0] = AncestorOfTwoNodesInBT.GetAncestorOfTwoNodesInBTAlgo4(node1, node2, root);
            }
            return allNodes[0];
        }

        public static void TestAncestorOfMultiNodesInBT()
        {
            BinaryTree<int> bt4 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, true);
            Console.WriteLine("The common ancestor of 4,2,9,13 from Algo4 is : " + GetLCAForMultiNodes(new List<BinaryTreeNode<int>>() { new BinaryTreeNode<int>(4), new BinaryTreeNode<int>(2), new BinaryTreeNode<int>(9), new BinaryTreeNode<int>(13) }, bt4.Head).Data);
            Console.WriteLine("The common ancestor of 0,1,2 from Algo4 is : " + GetLCAForMultiNodes(new List<BinaryTreeNode<int>>() { new BinaryTreeNode<int>(0), new BinaryTreeNode<int>(2), new BinaryTreeNode<int>(1)}, bt4.Head).Data);
            Console.WriteLine("The common ancestor of 0,1,2,55 from Algo4 is : " + GetLCAForMultiNodes(new List<BinaryTreeNode<int>>() { new BinaryTreeNode<int>(0), new BinaryTreeNode<int>(2), new BinaryTreeNode<int>(1), new BinaryTreeNode<int>(55) }, bt4.Head));
        }
    }
}
