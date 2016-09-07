using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// Get the mirror image of a tree.
    /// </summary>
    class MirrorATree
    {
        /// <summary>
        /// To get the mirror image we need to make the left child as the right child and the right child as the left child
        /// for each node recursively.
        /// 
        /// The running time is O(n) as we visit all the nodes and the space is O(1)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static BinaryTreeNode<int> GetNodeAfterMirroring(BinaryTreeNode<int> node)
        {
            if(node == null)
            {
                return null;
            }
            BinaryTreeNode<int> leftNode = GetNodeAfterMirroring(node.Left);
            BinaryTreeNode<int> rightNode = GetNodeAfterMirroring(node.Right);
            node.Left = rightNode;
            node.Right = leftNode;
            return node;
        }

        public static void TestMirrorATree()
        {
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, true);
            Console.WriteLine("Initial tree:");
            TreeHelperGeneral.PrintATree(bt1.Head);
            Console.WriteLine("Tree after mirroring");
            TreeHelperGeneral.PrintATree(GetNodeAfterMirroring(bt1.Head));
        }
    }
}
