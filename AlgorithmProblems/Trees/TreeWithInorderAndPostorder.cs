using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    ///  Construct a tree with its inorder and postorder traversal information
    /// </summary>
    class TreeWithInorderAndPostorder
    {
        /// <summary>
        /// We can construct a unique tree if we have an
        ///     1. inorder and preorder traversals
        ///     2. inorder and postorder traversals
        /// 
        /// having one of the traversal as inorder is a must.
        /// also note that if we have duplicate elements in the tree we cannot uniquely form a tree.
        /// 
        /// Algo:
        /// 1. the last node of postorder is the root node.
        /// 2. find this node in inorder traversal, all the nodes to the left of that index will form the left subtree
        /// and all the nodes to the right of this index will form the right subtree
        /// 3. get the number of nodes in the left and right subtree from 2 and that is the length of the left and right subtree in postorder array.
        /// 4. now get the left node and right node recursively.
        /// 
        /// The recurrance relationship is 
        /// T(n) = T(n-k)+T(k) + O(n)
        /// 
        /// In worst case k=1 and the running time is O(n^2)
        /// </summary>
        /// <param name="inorder">inorder traversal of the bt</param>
        /// <param name="inorderSt">start index in the inorder array</param>
        /// <param name="inorderEnd">end index in the inorder array</param>
        /// <param name="postorder">postorder traversal of the bt</param>
        /// <param name="postorderSt">start index in the postorder array</param>
        /// <param name="postorderEnd">end index in the postorder array</param>
        /// <returns></returns>
        public BinaryTreeNode<int> GetTree(int[] inorder, int inorderSt, int inorderEnd, int[] postorder, int postorderSt, int postorderEnd)
        {
            if(postorderEnd<postorderSt || inorderEnd<inorderSt)
            {
                // error case
                return null;
            }

            BinaryTreeNode<int> currentNode = new BinaryTreeNode<int>(postorder[postorderEnd]);

            // now search for this element in the inorder array
            int inorderIndex = -1;
            for(int i=inorderSt; i<=inorderEnd; i++)
            {
                if(inorder[i] == postorder[postorderEnd])
                {
                    // we found the element index
                    inorderIndex = i;
                }
            }
            if(inorderIndex == -1)
            {
                // we were not able to find the element index
                throw new Exception("The binary tree cannot be formed with these arrays");
            }

            int rightLength = inorderEnd - inorderIndex;
            // get the right node
            currentNode.Right = GetTree(inorder, inorderIndex + 1, inorderEnd, postorder, postorderEnd - rightLength, postorderEnd - 1);
            // get the left node
            currentNode.Left = GetTree(inorder, inorderSt, inorderIndex - 1, postorder, postorderSt, postorderEnd - rightLength - 1);

            return currentNode;
        }

        public static void TestTreeWithInorderAndPostorder()
        {
            int[] postorder = new int[] { 5, 7, 8, 6, 2, 4, 3, 1 };
            int[] inorder = new int[] { 5, 2, 7, 6, 8, 1, 3, 4 };
            TreeWithInorderAndPostorder tree = new TreeWithInorderAndPostorder();
            TreeHelperGeneral.PrintATree(tree.GetTree(inorder, 0, inorder.Length - 1, postorder, 0, postorder.Length - 1));
        }
    }
}
