using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// Get the kth smallest element in a BST
    /// </summary>
    public class KthSmallestElementFromBST
    {
        public BinarySearchTreeNode<int> KthSmallestElement { get; set; }

        /// <summary>
        /// We will traverse the BST in inorder traversal and keep track of the currentOrder
        /// which is the order statistic for that node in the ascending order.
        /// 
        /// The running time is O(n)
        /// and the space requrement is O(logn) since we are employing recursion here.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="k"></param>
        /// <param name="currentOrder"></param>
        /// <returns></returns>
        public int GetKthSmallestElement(BinarySearchTreeNode<int> node, int k, int currentOrder)
        {
            if(k == 0)
            {
                // Error condition: K should start from 1
                return -1;
            }
            if(node !=null)
            {
                if (node.Left != null)
                {
                    currentOrder = GetKthSmallestElement(node.Left, k, currentOrder);
                }
                if(++currentOrder == k)
                {
                    //We have found the kth element
                    KthSmallestElement = node;
                }
                if(node.Right != null)
                {
                    currentOrder = GetKthSmallestElement(node.Right, k, currentOrder);
                }
            }
            return currentOrder;

        }

        /// <summary>
        /// Algo2: here we will do the inorder traversal in an iterative fashion.
        /// When we do pop from the stack we can keep a count of the order and when order == k
        /// we can return that element
        /// </summary>
        /// <param name="node"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public BinarySearchTreeNode<int> GetKthSmallestElementIterative(BinarySearchTreeNode<int> node, int k)
        {
            Stack<BinarySearchTreeNode<int>> st = new Stack<BinarySearchTreeNode<int>>();
            st.Push(node);
            int currentOrder = 0;
            bool goLeft = true;

            while (st.Count > 0)
            {
                BinarySearchTreeNode<int> currentNode = st.Peek();
                while (currentNode.Left != null && goLeft)
                {
                    st.Push(currentNode.Left);
                    currentNode = currentNode.Left;
                }
                goLeft = false;
                currentNode = st.Pop();
                if(++currentOrder == k)
                {
                    // We have found the kth smallest element
                    return currentNode;
                }
                if(currentNode.Right!=null)
                {
                    st.Push(currentNode.Right);
                    goLeft = true;
                }

            }
            return null;
        }

        public static void TestKthSmallestElementFromBST()
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>();
            bst.Insert(6);
            bst.Insert(3);
            bst.Insert(2);
            bst.Insert(1);
            bst.Insert(0);
            bst.Insert(8);
            bst.Insert(7);
            bst.Insert(9);
            WalkTheTree.InOrderTraversal(bst.Head);
            Console.WriteLine();

            KthSmallestElementFromBST kObj = new KthSmallestElementFromBST();
            kObj.GetKthSmallestElement(bst.Head, 5, 0);
            Console.WriteLine("The {0}th element in the bst is {1}", 5, kObj.KthSmallestElement.Data);
            BinarySearchTreeNode<int> kthSmallestElement = kObj.GetKthSmallestElementIterative(bst.Head, 5);
            Console.WriteLine("Algo2: The {0}th element in the bst is {1}", 5, kthSmallestElement.Data);
            
            kObj = new KthSmallestElementFromBST();
            kObj.GetKthSmallestElement(bst.Head, 10, 0);
            Console.WriteLine("The {0}th element in the bst is {1}", 10, (kObj.KthSmallestElement==null) ? -1 : kObj.KthSmallestElement.Data);
            kthSmallestElement = kObj.GetKthSmallestElementIterative(bst.Head, 10);
            Console.WriteLine("Algo2: The {0}th element in the bst is {1}", 10, (kthSmallestElement == null) ? -1 : kthSmallestElement.Data);

            kObj = new KthSmallestElementFromBST();
            kObj.GetKthSmallestElement(bst.Head, 0, 0);
            Console.WriteLine("The {0}th element in the bst is {1}", 0, (kObj.KthSmallestElement == null) ? -1 : kObj.KthSmallestElement.Data);
            kthSmallestElement = kObj.GetKthSmallestElementIterative(bst.Head, 0);
            Console.WriteLine("Algo2: The {0}th element in the bst is {1}", 0, (kthSmallestElement == null) ? -1 : kthSmallestElement.Data);

            kObj = new KthSmallestElementFromBST();
            kObj.GetKthSmallestElement(bst.Head, 7, 0);
            Console.WriteLine("The {0}th element in the bst is {1}", 7, (kObj.KthSmallestElement == null) ? -1 : kObj.KthSmallestElement.Data);
            kthSmallestElement = kObj.GetKthSmallestElementIterative(bst.Head, 7);
            Console.WriteLine("Algo2: The {0}th element in the bst is {1}", 7, (kthSmallestElement == null) ? -1 : kthSmallestElement.Data);

            kObj = new KthSmallestElementFromBST();
            kObj.GetKthSmallestElement(bst.Head, 1, 0);
            Console.WriteLine("The {0}th element in the bst is {1}", 1, (kObj.KthSmallestElement == null) ? -1 : kObj.KthSmallestElement.Data);
            kthSmallestElement = kObj.GetKthSmallestElementIterative(bst.Head, 1);
            Console.WriteLine("Algo2: The {0}th element in the bst is {1}", 1, (kthSmallestElement == null) ? -1 : kthSmallestElement.Data);
        }
    }
}
