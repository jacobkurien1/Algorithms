using AlgorithmProblems.Stack_and_Queue;
using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    class CheckBTisBST
    {
        /// <summary>
        /// This is the recursive algo to check if the binary tree is a BST
        /// </summary>
        /// <param name="currentNode">the current node</param>
        /// <param name="minVal">minimum value that the current node can have</param>
        /// <param name="maxVal">maximum value that the current node can have</param>
        /// <returns></returns>
        public static bool IsBSTAlgo1(BinaryTreeNode<int> currentNode, int minVal, int maxVal)
        {
            if(currentNode == null)
            {
                return true;
            }
            if(currentNode.Data >= minVal && currentNode.Data <= maxVal)
            {
                return IsBSTAlgo1(currentNode.Left, minVal, currentNode.Data) && IsBSTAlgo1(currentNode.Right, currentNode.Data, maxVal);
            }
            return false;
        }

        /// <summary>
        /// This is the iterative algo to check if the binary tree is a BST
        /// this is similar to doing an inorder traversal and making sure that the series is a non decreasing one.
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="minVal"></param>
        /// <param name="maxVal"></param>
        /// <returns></returns>
        public static bool IsBSTAlgo2(BinaryTreeNode<int> head)
        {
            Stack<BinaryTreeNode<int>> stack = new Stack<BinaryTreeNode<int>>();
            stack.Push(head);
            int previousNodeVal = int.MinValue;
            BinaryTreeNode<int> currentNode = head;
            bool shouldCheckLeft = true;
            while(stack.Count != 0)
            {
                while (currentNode.Left != null && shouldCheckLeft)
                {
                    stack.Push(currentNode.Left);
                    currentNode = currentNode.Left;
                }
                shouldCheckLeft = false;
                currentNode = stack.Pop();
                if(previousNodeVal > currentNode.Data)
                {
                    return false;
                }
                previousNodeVal = currentNode.Data;
                if (currentNode.Right != null)
                {
                    stack.Push(currentNode.Right);
                    currentNode = currentNode.Right;
                    shouldCheckLeft = true;
                }
                
            }
            return true;
        }

        /// <summary>
        /// Do inorder traversal and make sure that the series is an increasing series
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="minVal"></param>
        /// <param name="maxVal"></param>
        /// <returns></returns>
        public static bool IsBSTAlgo3(BinaryTreeNode<int> currentNode)
        {
            // Do Inorder traversal and save all the values in an arraylist
            List<int> listOfTree = new List<int>();

            // stack for inorder traversal
            Stack<BinaryTreeNode<int>> st = new Stack<BinaryTreeNode<int>>();
            st.Push(currentNode);
            bool shouldCheckLeft = true;
            while(st.Count!=0)
            {
                while (currentNode.Left != null && shouldCheckLeft)
                {
                    st.Push(currentNode.Left);
                    currentNode = currentNode.Left;
                }
                shouldCheckLeft = false;
                currentNode = st.Pop();
                // Add the node to the list here
                listOfTree.Add(currentNode.Data);
                if(currentNode.Right!=null)
                {
                    currentNode = currentNode.Right;
                    st.Push(currentNode);
                    shouldCheckLeft = true;
                }
            }

            // Check whether the list of integers is non-decreasing
            int previousVal = int.MinValue;
            for (int index = 0; index < listOfTree.Count; index++)
            {
                if(previousVal > listOfTree[index])
                {
                    return false;
                }
                previousVal = listOfTree[index];
            }
            return true;
        }

        public static void TestCheckBTisBST()
        {
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13 }, true);
            Console.WriteLine("Is the bt1 complete binary tree: {0}", IsBSTAlgo1(bt1.Head, int.MinValue, int.MaxValue));
            Console.WriteLine("Is the bt1 complete binary tree: {0}", IsBSTAlgo2(bt1.Head));
            Console.WriteLine("Is the bt1 complete binary tree: {0}", IsBSTAlgo3(bt1.Head));

            BinaryTree<int> bt2 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, true);
            Console.WriteLine("Is the bt2 complete binary tree: {0}", IsBSTAlgo1(bt2.Head, int.MinValue, int.MaxValue));
            Console.WriteLine("Is the bt2 complete binary tree: {0}", IsBSTAlgo2(bt2.Head));
            Console.WriteLine("Is the bt2 complete binary tree: {0}", IsBSTAlgo3(bt2.Head));

            BinaryTree<int> bt3 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 5, 4 }, true);
            Console.WriteLine("Is the bt3 complete binary tree: {0}", IsBSTAlgo1(bt3.Head, int.MinValue, int.MaxValue));
            Console.WriteLine("Is the bt3 complete binary tree: {0}", IsBSTAlgo2(bt3.Head));
            Console.WriteLine("Is the bt3 complete binary tree: {0}", IsBSTAlgo3(bt3.Head));
        }

    }
}
