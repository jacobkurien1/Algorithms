using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees.TreeHelper
{
    class BinaryTree<T>
    {
        public BinaryTreeNode<T> Head { get; set; }

        private Queue<BinaryTreeNode<T>> BtQueue {get; set;}

        /// <summary>
        /// Creates a complete binary tree with the array of tree data provided
        /// </summary>
        /// <param name="dataForTree">array of tree data provided</param>
        public BinaryTree(T[] dataForTree, bool IsCompleteTree)
        {
            if (IsCompleteTree)
            {
                BtQueue = new Queue<BinaryTreeNode<T>>();
                for (int index = 0; index < dataForTree.Length; index++)
                {
                    InsertToCreateCompleteBT(dataForTree[index]);
                }
            }
            else
            {
                for (int index = 0; index < dataForTree.Length; index++)
                {
                    InsertSkewed(dataForTree[index]);
                }
            }
        }

        public void InsertToCreateCompleteBT(T data)
        {
            if(Head == null)
            {
                Head = new BinaryTreeNode<T>(data);
                BtQueue.Enqueue(Head);
            }
            else
            {
                BinaryTreeNode<T> nodeToAdd = new BinaryTreeNode<T>(data);
                BinaryTreeNode<T> nodeFromQueue = BtQueue.Peek();
                if(nodeFromQueue.Left == null)
                {
                    // add the new node to the left if the left node is not present on the nodeFromQueue
                    nodeFromQueue.Left = nodeToAdd;

                }
                else if(nodeFromQueue.Right == null)
                {
                    // add the new node to the right if the right node is not present on the nodeFromQueue
                    nodeFromQueue.Right = nodeToAdd;
                    // Now both the left and right nodes of the nodeFromQueue is full, so we can dequeue it
                    BtQueue.Dequeue();
                }
                else
                {
                    // we should never hit this condition, as we are dequeueing the Binary Tree node in the condition before
                    throw new Exception("The node with both left and right nodes notqual to null is present in the queue");
                }
                BtQueue.Enqueue(nodeToAdd);
            }
        }

        public void InsertSkewed(T data)
        {
            if(Head == null)
            {
                Head = new BinaryTreeNode<T>(data);
            }
            else
            {
                BinaryTreeNode<T> currentNode = Head;
                while(currentNode.Left!=null)
                {
                    currentNode = currentNode.Left;
                }
                currentNode.Left = new BinaryTreeNode<T>(data);
            }
        }
    }
}
