using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    class SkewedBSTToCompleteBST
    {
        /// <summary>
        /// Do an inorder travesal of the skewed BST and save the result to an array/list
        /// Start creating the Complete BST by taking the mid element of the array as the current node
        /// all the nodes to the left form the left subtree and the ones to the right form the right sub tree
        /// </summary>
        /// <param name="skewedBST">skewed BST</param>
        /// <returns>balanced/complete BST</returns>
        private static BinarySearchTree<int> ConvertSkewedBSTToCompleteBST(BinarySearchTree<int> skewedBST)
        {
            // Do an inorder travesal of the skewed BST and save the result to a list/array
            List<int> listOfElements = new List<int>();
            BinarySearchTreeNode<int> currentNode = skewedBST.Head;
            Stack<BinarySearchTreeNode<int>> st = new Stack<BinarySearchTreeNode<int>>();
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
                listOfElements.Add(currentNode.Data);
                if(currentNode.Right!=null)
                {
                    st.Push(currentNode.Right);
                    currentNode = currentNode.Right;
                    shouldCheckLeft = true;
                }
            }

            // Create the complete BST from the sorted list
            BinarySearchTree<int> bst = new BinarySearchTree<int>();
            bst.Head = CreateCompleteBSTFromSortedList(listOfElements, 0, listOfElements.Count - 1);
            return bst;
        }

        /// <summary>
        /// Creates a complete BST from a sorted list 
        /// Algo: Take the mid element from the list and create a BSTNode
        /// All the elements to left of this element forms the left subtree 
        /// and all the elements to the right of this element forms the right subtree
        /// </summary>
        /// <param name="listOfElements">sorted list of elements from which the BST needs to be created</param>
        /// <param name="startIndex">start Index</param>
        /// <param name="finishIndex">finish Index</param>
        /// <returns></returns>
        private static BinarySearchTreeNode<int> CreateCompleteBSTFromSortedList(List<int> listOfElements, int startIndex, int finishIndex)
        {
            if(finishIndex< startIndex)
            {
                // Base condition which creates the left and right child of the leaf nodes
                return null;
            }
            int midIndex = (startIndex + finishIndex)/2;
            BinarySearchTreeNode<int> midNode = new BinarySearchTreeNode<int>(listOfElements[midIndex]);
            midNode.Left = CreateCompleteBSTFromSortedList(listOfElements, startIndex, midIndex - 1);
            midNode.Right = CreateCompleteBSTFromSortedList(listOfElements, midIndex + 1, finishIndex);
            return midNode;
        }

        public static void TestConvertSkewedBSTToCompleteBST()
        {
            Console.WriteLine("Test conversion from skewed BST to the complete BST");
            BinarySearchTree<int> bst = new BinarySearchTree<int>();
            bst.Insert(6);
            bst.Insert(3);
            bst.Insert(2);
            bst.Insert(1);
            bst.Insert(0);
            bst.Insert(8);
            bst.Insert(7);
            bst.Insert(9);
            BinarySearchTree<int> completeBst = ConvertSkewedBSTToCompleteBST(bst);
            WalkTheTree.InOrderTraversal(completeBst.Head);
            Console.WriteLine();
        }


    }
}
