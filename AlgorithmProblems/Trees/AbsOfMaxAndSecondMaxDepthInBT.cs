using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// Given a very long Binary tree(the width of the tree << height of tree).
    /// Find the max depth - second largest depth in the tree.
    /// 
    /// Since the height of the tree is >>width of tree, we should not do a DFS as there is a chance of StackOverflow exception.
    /// We should do a level by level traversal and keep storing the max depth and second largest depth
    /// </summary>
    class AbsOfMaxAndSecondMaxDepthInBT
    {
        /// <summary>
        /// We will do level by level traversal using the 2 Queues.
        /// 
        /// The running time of this algo is O(n)
        /// The space requirement in worst case is O(width). Note: the width is mentioned to be the very less.
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static int GetAbsOfMaxAndSecondMaxDepthInBT(BinaryTreeNode<int> head)
        {
            if (head == null)
            {
                // Error condition
                throw new ArgumentException("The root is null for the tree");
            }

            Queue<BinaryTreeNode<int>> q1 = new Queue<BinaryTreeNode<int>>();
            Queue<BinaryTreeNode<int>> q2 = null;
            q1.Enqueue(head);
            int maxDepth = 0;
            int secondMaxDepth = 0;
            int currentDepth = 0;

            while (q1.Count != 0)
            {
                q2 = new Queue<BinaryTreeNode<int>>();
                while (q1.Count != 0)
                {
                    BinaryTreeNode<int> current = q1.Dequeue();
                    if (current.IsLeaf())
                    {
                        secondMaxDepth = maxDepth;
                        maxDepth = currentDepth;
                    }
                    if(current.Left != null)
                    {
                        q2.Enqueue(current.Left);
                    }
                    if (current.Right != null)
                    {
                        q2.Enqueue(current.Right);
                    }

                }
                q1 = q2;
                currentDepth++;
            }
            return maxDepth - secondMaxDepth;
        }

        public static void TestGetAbsOfMaxAndSecondMaxDepthInBT()
        {
            BinaryTreeNode<int> n1 = new BinaryTreeNode<int>(1);
            BinaryTreeNode<int> n2 = new BinaryTreeNode<int>(2);
            BinaryTreeNode<int> n3 = new BinaryTreeNode<int>(3);
            BinaryTreeNode<int> n4 = new BinaryTreeNode<int>(4);
            BinaryTreeNode<int> n5 = new BinaryTreeNode<int>(5);
            BinaryTreeNode<int> n6 = new BinaryTreeNode<int>(6);
            BinaryTreeNode<int> n7 = new BinaryTreeNode<int>(7);
            BinaryTreeNode<int> n8 = new BinaryTreeNode<int>(8);
            BinaryTreeNode<int> n9 = new BinaryTreeNode<int>(9);
            BinaryTreeNode<int> n10 = new BinaryTreeNode<int>(10);
            BinaryTreeNode<int> n11 = new BinaryTreeNode<int>(11);
            n1.Left = n2;
            n2.Left = n3;
            n3.Right = n4;
            n4.Right = n5;
            n5.Left = n6;
            n6.Left = n7;
            n7.Left = n8;
            n8.Left = n9;
            n9.Left = n10;
            n6.Right = n11;

            Console.WriteLine("The depth difference is {0}. Expected:3", GetAbsOfMaxAndSecondMaxDepthInBT(n1));
            n9.Right = new BinaryTreeNode<int>(12);
            Console.WriteLine("The depth difference is {0}. Expected:0", GetAbsOfMaxAndSecondMaxDepthInBT(n1));

        }
    }
}
