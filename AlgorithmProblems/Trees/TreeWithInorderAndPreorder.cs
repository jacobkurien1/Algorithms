using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    ///  Construct a tree with its inorder and preorder traversal information
    /// </summary>
    class TreeWithInorderAndPreorder
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
        /// 1. the first node of preorder is the root node.
        /// 2. find this node in inorder traversal, all the nodes to the left of that index will form the left subtree
        /// and all the nodes to the right of this index will form the right subtree
        /// 3. get the number of nodes in the left and right subtree from 2 and that is the length of the left and right subtree in preorder array.
        /// 4. now get the left node and right node recursively.
        /// 
        /// The recurrance relationship is 
        /// T(n) = T(n-k)+T(k) + O(n)
        /// 
        /// In worst case k=1 and the running time is O(n^2)
        /// </summary>
        /// <param name="inorder">array having the inorder traversal of the tree</param>
        /// <param name="inorderSt">start index in inorder array</param>
        /// <param name="inorderEnd">end index in inorder array</param>
        /// <param name="preorder">array having the preorder traversal of the tree</param>
        /// <param name="preorderSt">start index in preorder array</param>
        /// <param name="preorderEnd">end index in preorder array</param>
        /// <returns></returns>
        public BinaryTreeNode<int> GetTree(int[] inorder, int inorderSt, int inorderEnd, int[] preorder, int preorderSt, int preorderEnd)
        {
            if(inorderSt>inorderEnd || preorderSt > preorderEnd)
            {
                return null;
            }
            BinaryTreeNode<int> currentNode = new BinaryTreeNode<int>(preorder[preorderSt]);

            // get the index of preorder[preorderSt] in the inorder[st->end] array
            int inorderIndex = -1;
            for(int i= inorderSt; i<= inorderEnd; i++)
            {
                if(inorder[i] == preorder[preorderSt])
                {
                    // we have found the currentNode in inorder array
                    inorderIndex = i;
                }
            }
            if(inorderIndex == -1)
            {
                // we have not found the currentNode in inorder array
                throw new Exception("The tree cannot be formed");
            }

            // get the left subtree
            currentNode.Left = GetTree(inorder, inorderSt, inorderIndex - 1, preorder, preorderSt + 1, inorderIndex - inorderSt + preorderSt);
            // get the right subtree
            currentNode.Right = GetTree(inorder, inorderIndex + 1, inorderEnd, preorder, inorderIndex - inorderSt + preorderSt + 1, preorderEnd);

            return currentNode;
        }

        public static void TestTreeWithInorderAndPreorder()
        {
            int[] preorder = new int[] { 1, 2, 4, 5, 6, 7, 3, 8 };
            int[] inorder = new int[] { 4, 2, 6, 5, 7, 1, 3, 8 };
            TreeWithInorderAndPreorder tree = new TreeWithInorderAndPreorder();
            TreeHelperGeneral.PrintATree(tree.GetTree(inorder, 0, inorder.Length - 1, preorder, 0, preorder.Length - 1));

        }
    }
}
