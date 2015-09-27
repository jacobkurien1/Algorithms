using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    class DepthOfTree
    {
        /// <summary>
        /// Algo1: We have used recursion here.
        /// We can do this recursively by calculating the max depth of the left and right subtree and adding 1
        /// The running time is O(n)
        /// </summary>
        /// <param name="btNode">root node</param>
        /// <returns>maximum depth of the tree</returns>
        private static int GetDepthOfTree(BinaryTreeNode<int> btNode)
        {
            if(btNode!= null)
            {
                return 1 + Math.Max(GetDepthOfTree(btNode.Left), GetDepthOfTree(btNode.Right));
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// To calculate the max depth we need to do a variant of BFS on the root node
        /// We keep a dictionary which tracks the depth of each node
        /// </summary>
        /// <param name="btNode">the root node of the tree</param>
        /// <returns>max depth pf the tree</returns>
        private static int GetDepthOfTreeIteratively(BinaryTreeNode<int> btNode)
        {
            int maxDepth = 0;
            if (btNode != null)
            {
                Queue<BinaryTreeNode<int>> queue = new Queue<BinaryTreeNode<int>>();
                Dictionary<BinaryTreeNode<int>, int> depthDict = new Dictionary<BinaryTreeNode<int>, int>();
                queue.Enqueue(btNode);
                depthDict[btNode] = 1;
                while(queue.Count > 0)
                {
                    BinaryTreeNode<int> currentNode = queue.Dequeue();

                    if(currentNode.Left!=null)
                    {
                        depthDict[currentNode.Left] = depthDict[currentNode] + 1;
                        queue.Enqueue(currentNode.Left);
                        if (depthDict[currentNode.Left]>maxDepth)
                        {
                            maxDepth = depthDict[currentNode.Left];
                        }
                    }
                    if (currentNode.Right != null)
                    {
                        depthDict[currentNode.Right] = depthDict[currentNode] + 1;
                        queue.Enqueue(currentNode.Right);
                        if (depthDict[currentNode.Right] > maxDepth)
                        {
                            maxDepth = depthDict[currentNode.Right];
                        }
                    }

                }
            }
            return maxDepth;
            
        }

        public static void TestGetDepthOfTree()
        {
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13 }, true);
            Console.WriteLine("The depth of the tree from the recursive algo is : {0}", GetDepthOfTree(bt1.Head));
            Console.WriteLine("The depth of the tree from the iterative algo is : {0}", GetDepthOfTreeIteratively(bt1.Head));

            bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13 }, false);
            Console.WriteLine("The depth of the tree from the recursive algo is : {0}", GetDepthOfTree(bt1.Head));
            Console.WriteLine("The depth of the tree from the iterative algo is : {0}", GetDepthOfTreeIteratively(bt1.Head));
        }
    }
}
