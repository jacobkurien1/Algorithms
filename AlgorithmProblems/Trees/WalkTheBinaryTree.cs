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
        /// Post order traversal iteratively is the complicated one among the traversals
        /// due to its non tail recursion
        /// 
        /// We can solve this by using 2 stacks
        /// Steps are as follows:
        /// 1. Push the root to stack1
        /// 2. while stack1 is not empty
        ///     *pop element from stack1
        ///     *push the element to stack 2 and push the left and right child into the stack1
        /// 3. Print the contents of stack2 which will be the postorder
        /// </summary>
        /// <param name="treeNode"></param>
        public static void PostOrderTraversalIterativelyWith2Stacks(BinaryTreeNode<int> treeNode)
        {
            if(treeNode == null)
            {
                // null check
                return;
            }
            
            // intialize the two stacks
            Stack<BinaryTreeNode<int>> st1 = new Stack<BinaryTreeNode<int>>();
            Stack<BinaryTreeNode<int>> st2 = new Stack<BinaryTreeNode<int>>();
            st1.Push(treeNode);
            while(st1.Count > 0)
            {
                st2.Push(st1.Pop());
                if (st2.Peek().Left != null)
                {
                    // if left child is not null, add it
                    st1.Push(st2.Peek().Left);
                }
                if (st2.Peek().Right != null)
                {
                    // if right child is not null, add it
                    st1.Push(st2.Peek().Right);
                }
            }
            // Print st2 for the postorder traversal
            while(st2.Count>0)
            {
                Console.Write(st2.Pop().Data + " ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// We can do the post order traversal with a single stack
        /// 
        /// 1. Set the currentNode as root
        /// 2. push right node and current Node into stack
        /// 3. make the currentNode.left as the new currentNode
        /// 4. if the currentNode == null, pop the node from the stack if stack.Peek() is the right child of the popped node
        ///     make currentNode = right child and push the popped node back into the stack
        /// 5. Else print the currentNode and set the currentNode = null
        /// 
        /// </summary>
        /// <param name="treeNode"></param>
        public static void PostOrderTraversalIterativelySingleStack(BinaryTreeNode<int> treeNode)
        {
            if (treeNode == null)
            {
                // null check
                return;
            }

            BinaryTreeNode<int> currentNode = treeNode;
            // intialize the stack
            Stack<BinaryTreeNode<int>> st = new Stack<BinaryTreeNode<int>>();
            do
            {
                if (currentNode != null)
                {
                    if (currentNode.Right != null)
                    {
                        st.Push(currentNode.Right);
                    }
                    st.Push(currentNode);
                    currentNode = currentNode.Left;
                }
                else
                {
                    currentNode = st.Pop();
                    if (st.Count>0 && currentNode.Right == st.Peek())
                    {
                        BinaryTreeNode<int> currentNodeActual = st.Pop();
                        st.Push(currentNode);
                        currentNode = currentNodeActual;
                    }
                    else
                    {
                        Console.Write(currentNode.Data + " ");

                        currentNode = null;
                    }
                }
            } while (st.Count > 0);
            Console.WriteLine();
        }

        /// <summary>
        /// This is an optimized version of the above algorithm
        /// Here we want to traverse the tree in which we will keep track of whether we visited 
        /// the left child and right child using a Node reference lastvisitedNode
        /// 
        /// Steps: 1. Create a stack and push the tree root into the stack.
        /// 2. while stack is not empty, peek at stacktop and check if the left node is present 
        ///    and it is not already visited. Push left node to stack and update lastvisitednode as this left node
        /// 3. Do step 2 if right node is present and is not visited.
        /// 4. if steps 2 and 3 are not valid( ie either we reached a leaf node or a node which has both left and 
        ///    right child visited), Print the node and update the last visited node
        ///    
        /// The running time is O(n)
        /// The space requirement is O(h) h= height; h=n for a skewed tree 
        /// </summary>
        /// <param name="treeNode"></param>
        public static void PostOrderTraversalIterativelySingleStackCleaner(BinaryTreeNode<int> treeNode)
        {
            Stack<BinaryTreeNode<int>> st = new Stack<BinaryTreeNode<int>>();
            BinaryTreeNode<int> lastVisitedNode = treeNode;
            st.Push(treeNode);

            while(st.Count>0)
            {
                BinaryTreeNode<int> currentNode = st.Peek();
                if (currentNode.Left != null && currentNode.Left != lastVisitedNode && currentNode.Right != lastVisitedNode)
                {
                    // if the left node is present and it is not already visited
                    st.Push(currentNode.Left);
                    lastVisitedNode = currentNode.Left;
                }
                else if (currentNode.Right != null && currentNode.Right != lastVisitedNode)
                {
                    // if the right node is present and it is not already visited
                    st.Push(currentNode.Right);
                    lastVisitedNode = currentNode.Right;
                }
                else
                {
                    //either we reached a leaf node or a node which has both left and right child visited
                    currentNode = st.Pop();
                    Console.Write("{0} ", currentNode.Data);
                    lastVisitedNode = currentNode;
                }
            }
            Console.WriteLine();
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
            // complete BT
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13 }, true);
            PreOrderTraversalIteratively(bt1.Head);
            Console.WriteLine();
            PreOrderTraversal(bt1.Head);
            Console.WriteLine();

            PostOrderTraversalIterativelyWith2Stacks(bt1.Head);
            PostOrderTraversalIterativelySingleStack(bt1.Head);
            PostOrderTraversalIterativelySingleStackCleaner(bt1.Head);
            PostOrderTraversal(bt1.Head);
            Console.WriteLine();

            // skewed BT
            bt1.Head.Left.Left.Right = new BinaryTreeNode<int>(22);
            bt1.Head.Right.Left.Left = new BinaryTreeNode<int>(44);

            PostOrderTraversalIterativelyWith2Stacks(bt1.Head);
            PostOrderTraversalIterativelySingleStack(bt1.Head);
            PostOrderTraversalIterativelySingleStackCleaner(bt1.Head);
            PostOrderTraversal(bt1.Head);
            Console.WriteLine();
        }
    }
}
