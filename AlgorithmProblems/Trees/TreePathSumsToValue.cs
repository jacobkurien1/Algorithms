using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// Get the different paths in a tree which sums up to the given value
    /// </summary>
    class TreePathSumsToValue
    {
        /// <summary>
        /// Recursively get each path and keep the current sum that need to be matched and the path.
        /// if the path is not found backtrack by removing the path.
        /// 
        /// The running time of the operation is O(n) since we have to visit all the elements once.
        /// The space complexity will be O(depth of tree) ie O(n) for a  non balanced tree. 
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="sum"></param>
        /// <param name="path"></param>
        public void PrintPathWithSumToValue(BinaryTreeNode<int> currentNode, int sum, List<BinaryTreeNode<int>> path)
        {
            if (currentNode == null)
            {
                // error condition
                return;
            }
            sum -= currentNode.Data;
            path.Add(currentNode);
            if (sum == 0 && currentNode.Left == null && currentNode.Right == null)
            {
                // print path
                PrintPath(path);
            }
            PrintPathWithSumToValue(currentNode.Left, sum, path);
            PrintPathWithSumToValue(currentNode.Right, sum, path);
            path.Remove(currentNode);

        }

        /// <summary>
        /// Prints the path of the tree
        /// </summary>
        /// <param name="path"></param>
        private void PrintPath(List<BinaryTreeNode<int>> path)
        {
            if(path != null)
            {
                foreach(BinaryTreeNode<int> node in path)
                {
                    Console.Write("{0} -> ", node.Data);
                }
                Console.WriteLine();
            }
        }

        #region TestArea
        public static void TestTreePathSumsToValue()
        {
            // create tree with 2 paths that sum to 6
            BinaryTreeNode<int> root = new BinaryTreeNode<int>(1);
            BinaryTreeNode<int> rootLeft = new BinaryTreeNode<int>(2);
            BinaryTreeNode<int> rootRight = new BinaryTreeNode<int>(6);
            root.Left = rootLeft;
            root.Right = rootRight;
            BinaryTreeNode<int> n1Left = new BinaryTreeNode<int>(3);
            rootLeft.Left = n1Left;
            BinaryTreeNode<int> n1Right = new BinaryTreeNode<int>(13);
            rootLeft.Right = n1Right;
            BinaryTreeNode<int> n2Right = new BinaryTreeNode<int>(-1);
            rootRight.Right = n2Right;

            TreePathSumsToValue tp = new TreePathSumsToValue();

            tp.PrintPathWithSumToValue(root, 6, new List<BinaryTreeNode<int>>());

        }
        #endregion
    }
}
