using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// Print the binary tree nodes at each level spirally
    /// </summary>
    class PrintBinaryTreeNodeAtEachLevelSpirally
    {
        private static void PrintTreeLevelByLevel(BinaryTreeNode<int> root)
        {
            if (root == null)
            {
                // Error case
                return;
            }
            Stack<BinaryTreeNode<int>> s1 = new Stack<BinaryTreeNode<int>>();
            s1.Push(root);
            Stack<BinaryTreeNode<int>> s2 = new Stack<BinaryTreeNode<int>>();
            bool directionRL = true;
            while (s1.Count > 0)
            {
                while (s1.Count > 0)
                {
                    BinaryTreeNode<int> current = s1.Pop();
                    Console.Write("{0} ", current.Data);
                    if (directionRL)
                    {
                        // for right -> left, push left node first and then right
                        if (current.Left != null)
                        {
                            s2.Push(current.Left);
                        }
                        if (current.Right != null)
                        {
                            s2.Push(current.Right);
                        }
                    }
                    else
                    {
                        // for left -> right, push right node first and then left
                        if (current.Right != null)
                        {
                            s2.Push(current.Right);
                        }
                        if (current.Left != null)
                        {
                            s2.Push(current.Left);
                        }
                    }
                }
                Console.WriteLine();

                s1 = s2;
                s2 = new Stack<BinaryTreeNode<int>>();
                directionRL = !directionRL;
            }
        }

        public static void TestPrintBinaryTreeNodeAtEachLevelSpirally()
        {
            // test with a complete Binary Tree
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, true);
            Console.WriteLine("Result by using 2 stack");
            PrintTreeLevelByLevel(bt1.Head);

            // test with a skewed binary tree
            BinaryTree<int> bt2 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, false);
            Console.WriteLine("Result by using 2 stack");
            PrintTreeLevelByLevel(bt2.Head);
        }
    }
}
