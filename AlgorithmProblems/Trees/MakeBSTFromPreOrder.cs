using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// Check whether a BST can be created using a Preorder traversal input of a tree
    /// </summary>
    class MakeBSTFromPreOrder
    {
        public MakeBSTFromPreOrder()
        {
            currentIndex = -1;
        }
        private int currentIndex;

        /// <summary>
        /// This is the recursive subroutine which creates a tree node with data at arr[currentIndex] and sets the left and 
        /// right node appropriately.
        /// </summary>
        /// <param name="arr">array which has the preorder traversal </param>
        /// <param name="min">min value which can be taken by arr[currentIndex]</param>
        /// <param name="max">max value which can be taken by arr[currentIndex]</param>
        /// <returns>tree node with data at arr[currentIndex]</returns>
        private BinaryTreeNode<int> GetBSTRecursive(int[] arr, int min, int max)
        {
            BinaryTreeNode<int> currentNode = null;
            if (currentIndex < arr.Length && arr[currentIndex] <= max && arr[currentIndex] >= min)
            {
                int currentData = arr[currentIndex];
                currentNode = new BinaryTreeNode<int>(currentData);
                currentIndex++;
                currentNode.Left = GetBSTRecursive(arr, min, currentData);
                currentNode.Right = GetBSTRecursive(arr, currentData, max);
            }
            return currentNode;
        }

        /// <summary>
        /// Can the BST be created from the preorder traversal mentioned in arr
        /// The running time here is O(n) as we visit each node once
        /// </summary>
        /// <param name="arr">preorder traversal is present in the arr</param>
        /// <returns>whether the BST be created or not</returns>
        public bool CanBSTBeCreated(int[] arr)
        {
            currentIndex = 0;
            GetBSTRecursive(arr, int.MinValue, int.MaxValue);
            return currentIndex == arr.Length;
        }

        /// <summary>
        /// Returns the BST from the preorder array.
        /// The running time here is O(n) as we visit each node once
        /// </summary>
        /// <param name="arr">preorder traversal is present in the arr</param>
        /// <returns></returns>
        public BinaryTreeNode<int> GetBSTFromPreOrder(int[] arr)
        {
            currentIndex = 0;
            return GetBSTRecursive(arr, int.MinValue, int.MaxValue);
        }


        public static void TestMakeBSTFromPreOrder()
        {
            // Test case 1: complete bst
            MakeBSTFromPreOrder createBST = new MakeBSTFromPreOrder();
            int[] arr = new int[] { 4, 2, 1, 3, 6, 5, 7 };
            Console.WriteLine("The preorder travesal is given by:");
            ArrayHelper.PrintArray(arr);
            if (createBST.CanBSTBeCreated(arr))
            {
                Console.WriteLine("The bst can be created for the preorder traversal: {0}", true);
                Console.WriteLine("the tree looks like as shown below");
                TreeHelperGeneral.PrintATree(createBST.GetBSTFromPreOrder(arr));
            }
            else
            {
                Console.WriteLine("The bst can be created for the preorder traversal: {0}", false);
            }

            // Test case 2: skewed bst
            createBST = new MakeBSTFromPreOrder();
            arr = new int[] { 7, 6, 5, 4, 3, 2, 1, 0 };
            Console.WriteLine("The preorder travesal is given by:");
            ArrayHelper.PrintArray(arr);
            if (createBST.CanBSTBeCreated(arr))
            {
                Console.WriteLine("The bst can be created for the preorder traversal: {0}", true);
                Console.WriteLine("the tree looks like as shown below");
                TreeHelperGeneral.PrintATree(createBST.GetBSTFromPreOrder(arr));
            }
            else
            {
                Console.WriteLine("The bst can be created for the preorder traversal: {0}", false);
            }

            // Test case 4: not bst
            createBST = new MakeBSTFromPreOrder();
            arr = new int[] { 7, 5, 9, 2, 1, 4, 6 };
            Console.WriteLine("The preorder travesal is given by:");
            ArrayHelper.PrintArray(arr);
            if (createBST.CanBSTBeCreated(arr))
            {
                Console.WriteLine("The bst can be created for the preorder traversal: {0}", true);
                Console.WriteLine("the tree looks like as shown below");
                TreeHelperGeneral.PrintATree(createBST.GetBSTFromPreOrder(arr));
            }
            else
            {
                Console.WriteLine("The bst can be created for the preorder traversal: {0}", false);
            }
        }
    }
}
