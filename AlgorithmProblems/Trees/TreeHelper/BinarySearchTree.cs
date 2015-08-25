using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees.TreeHelper
{
    /// <summary>
    /// The left subtree will contain all nodes less than the current node
    /// The right subtree will contain all nodes greater or equal to the current node
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class BinarySearchTree<T> where T:IComparable<T>
    {
        public BinarySearchTreeNode<T> Head { get; set; }

        /// <summary>
        /// Insert a node in the Binary Search Tree
        /// </summary>
        /// <param name="data">data of the new tree node that needs to be inserted</param>
        public void Insert(T data)
        {
            BinarySearchTreeNode<T> nodeToInsert = new BinarySearchTreeNode<T>(data);
            // Case where the tree is empty, add the node as the head
            if(Head == null)
            {
                Head = nodeToInsert;
            }
            else
            {
                BinarySearchTreeNode<T> currentNode = Head;
                BinarySearchTreeNode<T> previousNode = null;

                // Find the location where the node needs to be inserted
                while(currentNode != null)
                {
                    previousNode = currentNode;
                    if(currentNode.Data.CompareTo(data)<=0)
                    {
                        currentNode = currentNode.Right;
                    }
                    else
                    {
                        currentNode = currentNode.Left;
                    }
                }

                // Insert the node to the correct location
                if (previousNode.Data.CompareTo(data) <= 0)
                {
                    previousNode.Right = nodeToInsert;
                }
                else
                {
                    previousNode.Left = nodeToInsert;
                }
                nodeToInsert.Parent = previousNode;

            }
        }

        public void Delete(T dataToDelete)
        {
            BinarySearchTreeNode<T> nodeToDelete = SearchBSTIterative(dataToDelete);

            //Case: When the node to delete is not found in the BST
            if(nodeToDelete == null)
            {
                return;
            }

            // Case 1: When the node to be deleted does not have a right node
            if(nodeToDelete.Right == null)
            {
                if (nodeToDelete.Parent == null)
                {
                    // This is the head node
                    Head = nodeToDelete.Left;
                    nodeToDelete.Left.Parent = null;
                }
                else
                {
                    if (nodeToDelete.Parent.Left == nodeToDelete)
                    {
                        nodeToDelete.Parent.Left = nodeToDelete.Left;
                        if (nodeToDelete.Left != null)
                        {
                            nodeToDelete.Left.Parent = nodeToDelete.Parent.Left;
                        }
                    }
                    else if (nodeToDelete.Parent.Right == nodeToDelete)
                    {
                        nodeToDelete.Parent.Right = nodeToDelete.Left;
                        if (nodeToDelete.Left != null)
                        {
                            nodeToDelete.Left.Parent = nodeToDelete.Parent.Right;
                        }
                    }
                }
            }
            // Case 2: When the node to be deleted has right node, but its right node does not have a left node
            else if (nodeToDelete.Right.Left == null)
            {
                if (nodeToDelete.Parent == null)
                {
                    // This is the head node
                    Head = nodeToDelete.Right;
                    nodeToDelete.Right.Parent = null;
                    nodeToDelete.Right.Left = nodeToDelete.Left;
                }
                else
                {
                    if (nodeToDelete.Parent.Left == nodeToDelete)
                    {
                        nodeToDelete.Parent.Left = nodeToDelete.Right;
                        if (nodeToDelete.Right != null)
                        {
                            nodeToDelete.Right.Parent = nodeToDelete.Parent;
                            nodeToDelete.Right.Left = nodeToDelete.Left;
                        }
                    }
                    else if (nodeToDelete.Parent.Right == nodeToDelete)
                    {
                        nodeToDelete.Parent.Right = nodeToDelete.Right;
                        if (nodeToDelete.Right != null)
                        {
                            nodeToDelete.Right.Parent = nodeToDelete.Parent;
                            nodeToDelete.Right.Left = nodeToDelete.Left;
                        }
                    }
                }
            }
            // Case 3: When the node to be deleted has right node and the right node has a left node, go to the 
            // right node and get the leftmost node and swap node to be deleted and the leftmost node.
            // then delete the leftmost node
            else 
            {
                BinarySearchTreeNode<T> currentNode = nodeToDelete.Right;

                // Get the left most node of the right node
                while (currentNode.Left != null)
                {
                    currentNode = currentNode.Left;
                }

                // swap the leftmost node and nodetodelete
                nodeToDelete.Data = currentNode.Data;

                // delete the leftmost node
                if(currentNode.Parent.Left == currentNode)
                {
                    currentNode.Parent.Left = null;
                }
                else    // currentNode.Parent.Right == currentNode
                {
                    currentNode.Parent.Right = null;
                }
            }
        }

        /// <summary>
        /// Search a BST in recursive manner.
        /// The Running time should be O(h) where h is the height of the tree ~= O(log(n)) where n is the total number of nodes
        /// O(h) ~= O(log(n)) is true only when the BST is balanced
        /// </summary>
        /// <param name="data">data which needs to be searched</param>
        /// <param name="currentTreeNode">The node reference, initially head of the tree will be passed to the function</param>
        /// <returns></returns>
        public BinarySearchTreeNode<T> SearchBSTRecursive(T data, BinarySearchTreeNode<T> currentTreeNode)
        {
            if(currentTreeNode == null)
            {
                // base  and error condition
                return null;
            }
            if(currentTreeNode.Data.CompareTo(data) == 0)
            {
                return currentTreeNode; // The node is found
            }
            else if (currentTreeNode.Data.CompareTo(data) > 0)
            {
                // go search in left subtree
                return SearchBSTRecursive(data, currentTreeNode.Left);
            }
            else
            {
                // go search the right subtree
                return SearchBSTRecursive(data, currentTreeNode.Right);
            }
        }

        public BinarySearchTreeNode<T> SearchBSTIterative(T data)
        {
            BinarySearchTreeNode<T> currentTreeNode = Head;
            while(currentTreeNode!=null && currentTreeNode.Data.CompareTo(data) != 0)
            {
                if(currentTreeNode.Data.CompareTo(data)>0)
                {
                    currentTreeNode = currentTreeNode.Left;
                }
                else
                {
                    currentTreeNode = currentTreeNode.Right;
                }
            }
            return currentTreeNode;
        }

        
        
    }
    #region Test Methods
    class TestBinarySearchTree
    {
        
        public static void TestDifferentOperationsOnBST()
        {
            Console.WriteLine("Test insertion in a BST");
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

            Console.WriteLine("Test search in a BST");
            Console.WriteLine("Was seaching {0} and found {1}", 1, bst.SearchBSTIterative(1).Data);
            Console.WriteLine("Was seaching {0} and found {1}", 9, bst.SearchBSTIterative(9).Data);
            Console.WriteLine("Was seaching {0} and found {1}", 6, bst.SearchBSTIterative(6).Data);
            Console.WriteLine("Was seaching {0} and found {1}", 5, bst.SearchBSTIterative(5));
            Console.WriteLine("Was seaching {0} and found {1}", 1, bst.SearchBSTRecursive(1, bst.Head).Data);
            Console.WriteLine("Was seaching {0} and found {1}", 9, bst.SearchBSTRecursive(9, bst.Head).Data);
            Console.WriteLine("Was seaching {0} and found {1}", 6, bst.SearchBSTRecursive(6, bst.Head).Data);
            Console.WriteLine("Was seaching {0} and found {1}", 5, bst.SearchBSTRecursive(5, bst.Head));

            Console.WriteLine("Test delete from a BST");
            // -------------------------------------------
            Console.WriteLine("Deletion case 1");

            bst = new BinarySearchTree<int>();
            bst.Insert(6);
            bst.Insert(3);
            bst.Insert(8);
            bst.Insert(2);
            bst.Insert(4);
            bst.Insert(7);

            bst.Delete(8);
            WalkTheTree.InOrderTraversal(bst.Head);
            Console.WriteLine();
            //---------------------------------------------
            bst = new BinarySearchTree<int>();
            bst.Insert(6);
            bst.Insert(3);
            bst.Insert(8);
            bst.Insert(2);
            bst.Insert(4);
            bst.Insert(7);
            bst.Insert(12);
            bst.Insert(13);
            bst.Delete(8);
            WalkTheTree.InOrderTraversal(bst.Head);
            Console.WriteLine();
            //---------------------------------------------
            bst = new BinarySearchTree<int>();
            bst.Insert(6);
            bst.Insert(3);
            bst.Insert(8);
            bst.Insert(2);
            bst.Insert(4);
            bst.Insert(7);
            bst.Insert(12);
            bst.Insert(13);
            bst.Insert(10);
            bst.Insert(9);
            bst.Insert(11);
            bst.Delete(8);
            WalkTheTree.InOrderTraversal(bst.Head);
            Console.WriteLine();
            //-----------------------------------------------
            bst.Delete(9);
            WalkTheTree.InOrderTraversal(bst.Head);
            Console.WriteLine();
            //-------------------------------------------------
            bst = new BinarySearchTree<int>();
            bst.Insert(6);
            bst.Insert(3);
            bst.Insert(2);
            bst.Insert(4);
            bst.Delete(6);
            WalkTheTree.InOrderTraversal(bst.Head);
            Console.WriteLine();
            //----------------------------------------------------
            bst = new BinarySearchTree<int>();
            bst.Insert(6);
            bst.Insert(3);
            bst.Insert(2);
            bst.Insert(4);
            bst.Insert(8);
            bst.Insert(12);
            bst.Delete(6);
            WalkTheTree.InOrderTraversal(bst.Head);
            Console.WriteLine();
            //----------------------------------------------------
            bst = new BinarySearchTree<int>();
            bst.Insert(6);
            bst.Insert(3);
            bst.Insert(2);
            bst.Insert(4);
            bst.Insert(8);
            bst.Insert(12);
            bst.Insert(7);
            bst.Delete(6);
            WalkTheTree.InOrderTraversal(bst.Head);
            Console.WriteLine();
            Console.WriteLine("Test the iterative inorder traversal");
            WalkTheTree.InOrderTraversalIterative(bst.Head);
            Console.WriteLine();
            //----------------------------------------------------
        }
    }
    #endregion

}
