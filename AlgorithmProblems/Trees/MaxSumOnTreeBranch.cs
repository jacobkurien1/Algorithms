using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    class MaxSumOnTreeBranch
    {
        public static int MaxSum(BinaryTreeNode<int> currentNode)
        {
            if(currentNode == null)
            {
                return 0;
            }
            else
            {
                return Math.Max(MaxSum(currentNode.Left), MaxSum(currentNode.Right)) + currentNode.Data;
            }
        }

        public static void TestMaxSum()
        {
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13 }, true);
            Console.WriteLine("Max sum of a branch in the binary tree is {0}", MaxSum(bt1.Head));
        }
    }
}
