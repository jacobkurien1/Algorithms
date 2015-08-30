using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees.TreeHelper
{
    class BinaryTreeNodeWithParent<T>
    {
        public BinaryTreeNodeWithParent<T> Left { get; set; }
        public BinaryTreeNodeWithParent<T> Right { get; set; }
        public BinaryTreeNodeWithParent<T> Parent { get; set; }
        
        public T Data { get; set; }

        public BinaryTreeNodeWithParent(T data)
        {
            Data = data;
        }
    }

    class BinaryTreeWithParent<T> where T:IComparable
    {
        public BinaryTreeNodeWithParent<T> Head { get; set; }

        private Queue<BinaryTreeNodeWithParent<T>> BtQueue { get; set; }

        /// <summary>
        /// Creates a complete binary tree with the array of tree data provided
        /// </summary>
        /// <param name="dataForTree">array of tree data provided</param>
        public BinaryTreeWithParent(T[] dataForTree, bool IsCompleteTree)
        {
            if (IsCompleteTree)
            {
                BtQueue = new Queue<BinaryTreeNodeWithParent<T>>();
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
            if (Head == null)
            {
                Head = new BinaryTreeNodeWithParent<T>(data);
                BtQueue.Enqueue(Head);
            }
            else
            {
                BinaryTreeNodeWithParent<T> nodeToAdd = new BinaryTreeNodeWithParent<T>(data);
                BinaryTreeNodeWithParent<T> nodeFromQueue = BtQueue.Peek();
                if (nodeFromQueue.Left == null)
                {
                    // add the new node to the left if the left node is not present on the nodeFromQueue
                    nodeFromQueue.Left = nodeToAdd;
                    nodeToAdd.Parent = nodeFromQueue;

                }
                else if (nodeFromQueue.Right == null)
                {
                    // add the new node to the right if the right node is not present on the nodeFromQueue
                    nodeFromQueue.Right = nodeToAdd;
                    nodeToAdd.Parent = nodeFromQueue;
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
            if (Head == null)
            {
                Head = new BinaryTreeNodeWithParent<T>(data);
            }
            else
            {
                BinaryTreeNodeWithParent<T> currentNode = Head;
                while (currentNode.Left != null)
                {
                    currentNode = currentNode.Left;
                }
                BinaryTreeNodeWithParent<T> nodeToAdd = new BinaryTreeNodeWithParent<T>(data);
                currentNode.Left = nodeToAdd;
                nodeToAdd.Parent = currentNode;
            }
        }

        public BinaryTreeNodeWithParent<T> Search(T data)
        {
            Queue<BinaryTreeNodeWithParent<T>> queueForTreeNodes = new Queue<BinaryTreeNodeWithParent<T>>();
            queueForTreeNodes.Enqueue(Head);
            while(queueForTreeNodes.Count!=0)
            {
                BinaryTreeNodeWithParent<T> currentNode = queueForTreeNodes.Dequeue();
                if(currentNode.Data.CompareTo(data) == 0)
                {
                    // Found the node
                    return currentNode;
                }
                if(currentNode.Left!=null)
                {
                    queueForTreeNodes.Enqueue(currentNode.Left);
                }
                if(currentNode.Right!=null)
                {
                    queueForTreeNodes.Enqueue(currentNode.Right);
                }
            }
            return null;
        }
    }
}
