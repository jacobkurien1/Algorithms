using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    class AncestorOfTwoNodesInBST
    {
        public static BinarySearchTreeNode<int> AncestorOfTwoNodesIterative(int node1Data, int node2Data, BinarySearchTree<int> bst)
        {
            // Make sure that node1 has the smaller of the 2 nodes
            if (node1Data > node2Data)
            {
                int temp = node2Data;
                node2Data = node1Data;
                node1Data = temp;
            }

            // Make sure that node1 and node2 are present in the Binary Search Tree
            BinarySearchTreeNode<int> node1 = bst.SearchBSTIterative(node1Data);
            BinarySearchTreeNode<int> node2 = bst.SearchBSTIterative(node2Data);

            if (node1 == null || node2 == null)
            {
                return null;    // The node is not present in the BST
            }
            BinarySearchTreeNode<int> currentNode = bst.Head;
            while (currentNode != null)
            {
                if (currentNode.Data >= node1.Data && currentNode.Data <= node2.Data)
                {
                    // We have found the ancestor node
                    return currentNode;
                }
                else if (currentNode.Data >= node1.Data && currentNode.Data >= node2.Data)
                {
                    // both the nodes are less than the current node and hence they lie on the left subtree
                    currentNode = currentNode.Left;
                }
                else
                {
                    // both the nodes are on the right subtree
                    currentNode = currentNode.Right;
                }
            }
            return null;
        }

        public static BinarySearchTreeNode<int> AncestorOfTwoNodesRecursive(BinarySearchTreeNode<int> smaller, BinarySearchTreeNode<int> larger, BinarySearchTreeNode<int> currentNode)
        {
            if(smaller == null || larger == null || currentNode == null)
            {
                return null;    // Base condition
            }
            if(smaller.Data <= currentNode.Data && larger.Data >= currentNode.Data)
            {
                // We have found the ancestor node
                return currentNode;
            }
            else if (smaller.Data <= currentNode.Data && larger.Data <= currentNode.Data)
            {
                // both the nodes are less than the current node and hence they lie on the left subtree
                return AncestorOfTwoNodesRecursive(smaller, larger, currentNode.Left);
            }
            else
            {
                // both the nodes are on the right subtree
                return AncestorOfTwoNodesRecursive(smaller, larger, currentNode.Right);
            }
        }

        public static void TestAncestorOfTwoNodesInBST()
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

            Console.WriteLine("Ancestor of {0} and {1} from AncestorOfTwoNodesIterative is : {2}", 1, 7, AncestorOfTwoNodesIterative(1, 7, bst).Data);
            Console.WriteLine("Ancestor of {0} and {1} from AncestorOfTwoNodesRecursive is : {2}", 1, 7, AncestorOfTwoNodesRecursive(bst.SearchBSTIterative(1), bst.SearchBSTIterative(7), bst.Head).Data);
            Console.WriteLine("Ancestor of {0} and {1} from AncestorOfTwoNodesIterative is : {2}", 1, 3, AncestorOfTwoNodesIterative(1, 3, bst).Data);
            Console.WriteLine("Ancestor of {0} and {1} from AncestorOfTwoNodesRecursive is : {2}", 1, 3, AncestorOfTwoNodesRecursive(bst.SearchBSTIterative(1), bst.SearchBSTIterative(3), bst.Head).Data);
        }
    }
}
