using AlgorithmProblems.Stack_and_Queue;
using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    class WalkTheTree
    {
        /// <summary>
        /// Print the node before its children
        /// </summary>
        /// <param name="treeNode"></param>
        public static void PreOrderTraversal(BinaryTreeNode<int> treeNode)
        {
            if(treeNode!=null)
            {
                Console.Write(treeNode.Data + " ");
                PreOrderTraversal(treeNode.Left);
                PreOrderTraversal(treeNode.Right);
            }
        }

        /// <summary>
        /// Print the node before its children iteratively
        /// </summary>
        /// <param name="treeNode"></param>
        public static void PreOrderTraversalIteratively(BinaryTreeNode<int> treeNode)
        {
            Stack<BinaryTreeNode<int>> st = new Stack<BinaryTreeNode<int>>();
            st.Push(treeNode);
            while(st.Count != 0)
            {
                BinaryTreeNode<int> currentNode = st.Pop();
                Console.Write(currentNode.Data + " ");
                if(currentNode.Right!=null)
                {
                    st.Push(currentNode.Right);
                }
                if(currentNode.Left!=null)
                {
                    st.Push(currentNode.Left);
                }
            }
        }

        public static string PreOrderTraversalReturnAsString(BinaryTreeNode<int> treeNode)
        {
            StringBuilder sb = new StringBuilder();
            Stack<BinaryTreeNode<int>> st = new Stack<BinaryTreeNode<int>>();
            st.Push(treeNode);
            while (st.Count != 0)
            {
                BinaryTreeNode<int> currentNode = st.Pop();
                sb.Append(currentNode.Data + " ");
                if (currentNode.Right != null)
                {
                    st.Push(currentNode.Right);
                }
                if (currentNode.Left != null)
                {
                    st.Push(currentNode.Left);
                }
            }
            return sb.ToString();
        }

        public static void PostOrderTraversal(BinaryTreeNode<int> treeNode)
        {
            if (treeNode != null)
            {
                PostOrderTraversal(treeNode.Left);
                PostOrderTraversal(treeNode.Right); 
                Console.Write(treeNode.Data + " ");
            }
        }

        /// <summary>
        /// TBD
        /// </summary>
        /// <param name="treeNode"></param>
        public static void PostOrderTraversalIteratively(BinaryTreeNode<int> treeNode)
        {
            if(treeNode == null)
            {
                // null check
                return;
            }
            
            // intialize the stack and populate it with the root->leftNode and root
            Stack<BinaryTreeNode<int>> st = new Stack<BinaryTreeNode<int>>();
            
            while(st.Count > 0)
            {
                if (treeNode.Right != null)
                {
                    st.Push(treeNode.Right);
                }
                st.Push(treeNode);
                treeNode = treeNode.Left;

            }

        }

        public static void InOrderTraversal(BinaryTreeNode<int> treeNode)
        {
            if (treeNode != null)
            {
                InOrderTraversal(treeNode.Left);
                Console.Write(treeNode.Data + " ");
                InOrderTraversal(treeNode.Right);
            }
        }

        public static void InOrderTraversal(BinarySearchTreeNode<int> treeNode)
        {
            if (treeNode != null)
            {
                InOrderTraversal(treeNode.Left);
                Console.Write(treeNode.Data + " ");
                InOrderTraversal(treeNode.Right);
            }
        }

        public static void InOrderTraversalIterative(BinarySearchTreeNode<int> treeNode)
        {
            StackViaLinkedList<BinarySearchTreeNode<int>> st = new StackViaLinkedList<BinarySearchTreeNode<int>>();
            st.Push(treeNode);
            BinarySearchTreeNode<int> currentNode = treeNode;
            bool shouldCheckLeft = true;
            while(!st.IsEmpty())
            {
                while (currentNode.Left != null && shouldCheckLeft)
                {
                    st.Push(currentNode.Left);
                    currentNode = currentNode.Left;
                }
                currentNode = st.Pop().Data;
                shouldCheckLeft = false;
                Console.Write(currentNode.Data + " ");
                if (currentNode.Right != null)
                {
                    st.Push(currentNode.Right);
                    currentNode = currentNode.Right;
                    shouldCheckLeft = true;
                }
            }
        }

        public static string InOrderTraversalReturnAsString(BinaryTreeNode<int> treeNode)
        {
            StringBuilder returnString = new StringBuilder();
            StackViaLinkedList<BinaryTreeNode<int>> st = new StackViaLinkedList<BinaryTreeNode<int>>();
            st.Push(treeNode);
            BinaryTreeNode<int> currentNode = treeNode;
            bool shouldCheckLeft = true;
            while (!st.IsEmpty())
            {
                while (currentNode.Left != null && shouldCheckLeft)
                {
                    st.Push(currentNode.Left);
                    currentNode = currentNode.Left;
                }
                currentNode = st.Pop().Data;
                shouldCheckLeft = false;
                returnString.Append(currentNode.Data + " ");
                if (currentNode.Right != null)
                {
                    st.Push(currentNode.Right);
                    currentNode = currentNode.Right;
                    shouldCheckLeft = true;
                }
            }
            return returnString.ToString();
        }

        public static void TestWalkTheTree()
        {
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13 }, true);
            PreOrderTraversalIteratively(bt1.Head);
            Console.WriteLine();
            PreOrderTraversal(bt1.Head);
            Console.WriteLine();

        }
    }
}
