using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    class CheckIfTheTreeIsBalanced
    {
        /// <summary>
        /// Balanced tree can have all leaves nodes at a level defference max of 1
        /// </summary>
        /// <param name="btNode"></param>
        /// <returns></returns>
        public static bool IsTreeBalanced(BinaryTreeNode<int> btNode)
        {
            return (MaxDepth(btNode) - MinDepth(btNode) <= 1);
        }

        private static int MinDepth(BinaryTreeNode<int> btNode)
        {
            if(btNode == null)
            {
                return 0;
            }
            return 1 + Math.Min(MinDepth(btNode.Left), MinDepth(btNode.Right));
        }

        private static int MaxDepth(BinaryTreeNode<int> btNode)
        {
            if (btNode == null)
            {
                return 0;
            }
            return 1 + Math.Max(MaxDepth(btNode.Left), MaxDepth(btNode.Right));
        }

        public static void TestIsTreeBalanced()
        {
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13 }, true);
            Console.WriteLine("The bt1 is a balanced tree : " + IsTreeBalanced(bt1.Head));

            BinaryTree<int> bt2 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, true);
            Console.WriteLine("The bt2 is a balanced tree : " + IsTreeBalanced(bt2.Head));

            BinaryTree<int> bt3 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, false);
            Console.WriteLine("The bt3 is a balanced tree : " + IsTreeBalanced(bt3.Head));
        }
    }
}
