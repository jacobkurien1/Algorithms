using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// Print the binary tree nodes at each level.
    /// </summary>
    class PrintBinaryTreeNodeAtEachLevel
    {
        /// <summary>
        /// Algo: use 2 queues to print the binary tree nodes level by level
        /// 
        /// The running time is O(n)
        /// </summary>
        /// <param name="root"></param>
        public static void PrintTreeLevelByLevel(BinaryTreeNode<int> root)
        {
            if (root == null)
            {
                // Error case
                return;
            }
            Queue<BinaryTreeNode<int>> q1 = new Queue<BinaryTreeNode<int>>();
            q1.Enqueue(root);
            Queue<BinaryTreeNode<int>> q2 = new Queue<BinaryTreeNode<int>>();
            while (q1.Count > 0)
            {
                while (q1.Count > 0)
                {
                    BinaryTreeNode<int> current = q1.Dequeue();
                    Console.Write("{0} ", current.Data);
                    if (current.Left != null)
                    {
                        q2.Enqueue(current.Left);
                    }
                    if (current.Right != null)
                    {
                        q2.Enqueue(current.Right);
                    }
                }
                Console.WriteLine();

                q1 = q2;
                q2 = new Queue<BinaryTreeNode<int>>();
            }
        }
        public static void TestPrintBinaryTreeNodeAtEachLevel()
        {
            // test with a complete Binary Tree
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, true);
            Console.WriteLine("Result by using 2 queues");
            PrintTreeLevelByLevel(bt1.Head);

            // test with a skewed binary tree
            BinaryTree<int> bt2 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, false);
            Console.WriteLine("Result by using 2 queues");
            PrintTreeLevelByLevel(bt2.Head);
        }
    }
}
