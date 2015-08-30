using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    class AncestorOfTwoNodesInBT
    {
        #region Algo1

        /// <summary>
        /// Search for a node in a tree
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="nodetoSearch"></param>
        /// <returns></returns>
        private static bool SearchNodeInTree(BinaryTreeNode<int> treeNode, BinaryTreeNode<int> nodetoSearch)
        {
            if(treeNode == null)
            {
                // did not find the node
                return false;
            }
            if(treeNode.Data == nodetoSearch.Data)
            {
                // found the node
                return true;
            }
            else
            {
                return SearchNodeInTree(treeNode.Left, nodetoSearch) || SearchNodeInTree(treeNode.Right, nodetoSearch);
            }
        }

        /// <summary>
        /// Get the ancestor of node1 and node2 in the tree
        /// </summary>
        /// <param name="node1">node for which the ancestor needs to be found</param>
        /// <param name="node2">node for which the ancestor needs to be found</param>
        /// <param name="treeNode"></param>
        /// <returns>ancestor of node1 and node2</returns>
        private static BinaryTreeNode<int> GetAncestorOfTwoNodesInBT(BinaryTreeNode<int> node1, BinaryTreeNode<int> node2, BinaryTreeNode<int> treeNode)
        {
            if(treeNode== null)
            {
                // Null check
                return null;
            }
            bool IsNode1PresentInLeftSubtree = SearchNodeInTree(treeNode.Left, node1);
            bool IsNode2PresentInLeftSubtree = SearchNodeInTree(treeNode.Left, node2);
            bool IsNode1PresentInRightSubtree = SearchNodeInTree(treeNode.Right, node1);
            bool IsNode2PresentInRightSubtree = SearchNodeInTree(treeNode.Right, node2);

            // Check if node1 and node2 are in left and right subtrees(or right/left subtrees)
            if(((node1.Data == treeNode.Data || IsNode1PresentInLeftSubtree) && (node2.Data == treeNode.Data || IsNode2PresentInRightSubtree)) || 
                ((node1.Data== treeNode.Data || IsNode1PresentInRightSubtree) && (node2.Data == treeNode.Data || IsNode2PresentInLeftSubtree)))
            {
                // the current node is the common ancestor
                return treeNode;
            }
            else if(IsNode1PresentInLeftSubtree && IsNode2PresentInLeftSubtree)
            {
                // both nodes are in the left subtree
                return GetAncestorOfTwoNodesInBT(node1, node2, treeNode.Left);
            }
            else if (IsNode1PresentInRightSubtree && IsNode2PresentInRightSubtree)
            {
                // both nodes are in right subtree
                return GetAncestorOfTwoNodesInBT(node1, node2, treeNode.Right);
            }
            return null;
        }
        #endregion

        #region Algo2
        /// <summary>
        /// If the nodes have a knowledge of its parents, then trace the path back to the root
        /// Push the nodes to the dictionary and the first collision will be the ancestor node.
        /// The running time here is max(depthOfNode1-depthOfAncestor, depthOfNode2-depthOfAncestor)
        /// The space requirement is O(h) where h is the height of the tree.
        /// </summary>
        public static BinaryTreeNodeWithParent<int> GetAncestorOfTwoNodesInBTAlgo2(BinaryTreeNodeWithParent<int> node1, BinaryTreeNodeWithParent<int> node2)
        {
            Dictionary<BinaryTreeNodeWithParent<int>, bool> dict = new Dictionary<BinaryTreeNodeWithParent<int>, bool>();

            while(node1 !=null || node2!=null)
            {
                if(node1 !=null)
                {
                    if(dict.ContainsKey(node1))
                    {
                        // we got the ancestor
                        return node1;
                    }
                    dict[node1] = true;
                    node1 = node1.Parent;
                }
                if (node2 != null)
                {
                    if (dict.ContainsKey(node2))
                    {
                        // we got the ancestor
                        return node2;
                    }
                    dict[node2] = true;
                    node2 = node2.Parent;
                }
            }
            return null;
        }
        #endregion

        #region Algo3
        /// <summary>
        /// Step1: Find the depth of node1 and node2
        /// Step2: Compute depth1 - depth2, if node1 is at a greater depth than node2
        /// Step3: Move the pointer for node1 up by depth1-depth2.
        /// Step4: Move both pointer for node1 and node2 till you hit the ancestor node
        /// The running time is O(h), h is the hieght of the tree
        /// The Space requirement is O(1)
        /// </summary>
        public static BinaryTreeNodeWithParent<int> GetAncestorOfTwoNodesInBTAlgo3(BinaryTreeNodeWithParent<int> node1, BinaryTreeNodeWithParent<int> node2)
        {
            //Step1: Find the depth of node1 and node2
            int depth1 = GetDepth(node1);
            int depth2 = GetDepth(node2);

            //Step2: Compute depth1 - depth2, if node1 is at a greater depth than node2
            int differenceOfDepths = 0;
            BinaryTreeNodeWithParent<int> larger = node1;
            BinaryTreeNodeWithParent<int> smaller = node2;
            if(depth1>depth2)
            {
                differenceOfDepths = depth1 - depth2;
            }
            else
            {
                differenceOfDepths = depth2 - depth1;
                larger = node2;
                smaller = node1;
            }

            //Step3: Move the pointer for node1 up by depth1-depth2.
            while(differenceOfDepths!=0)
            {
                larger = larger.Parent;
                differenceOfDepths--;
            }

            //Step4: Move both pointer for node1 and node2 till you hit the ancestor node
            while(larger != smaller )
            {
                larger = larger.Parent;
                smaller = smaller.Parent;
            }
            return larger;
        }

        private static int GetDepth(BinaryTreeNodeWithParent<int> treeNode)
        {
            int depth = 0;
            while(treeNode!=null)
            {
                depth++;
                treeNode = treeNode.Parent;
            }
            return depth;
        }

        #endregion

        public static void TestGetAncestorOfTwoNodesInBT()
        {
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, true);
            Console.WriteLine("The common ancestor of 4,2 from Algo1 is : " + GetAncestorOfTwoNodesInBT(new BinaryTreeNode<int>(4), new BinaryTreeNode<int>(2), bt1.Head).Data);
            Console.WriteLine("The common ancestor of 1,13 from Algo1 is : " + GetAncestorOfTwoNodesInBT(new BinaryTreeNode<int>(1), new BinaryTreeNode<int>(13), bt1.Head).Data);
            Console.WriteLine("The common ancestor of 9,13 from Algo1 is : " + GetAncestorOfTwoNodesInBT(new BinaryTreeNode<int>(9), new BinaryTreeNode<int>(13), bt1.Head).Data);
            BinaryTreeNode<int> ancestor = GetAncestorOfTwoNodesInBT(new BinaryTreeNode<int>(4), new BinaryTreeNode<int>(14), bt1.Head);
            Console.WriteLine("The common ancestor of 4,14 from Algo1 is : {0}", ancestor);

            //Algo2
            BinaryTreeWithParent<int> bt2 = new BinaryTreeWithParent<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, true);
            Console.WriteLine("The common ancestor of 4,2 from Algo3 is : " + GetAncestorOfTwoNodesInBTAlgo3(bt2.Search(4), bt2.Search(2)).Data);
            Console.WriteLine("The common ancestor of 1,13 from Algo3 is : " + GetAncestorOfTwoNodesInBTAlgo3(bt2.Search(1), bt2.Search(13)).Data);
            Console.WriteLine("The common ancestor of 9,13 from Algo3 is : " + GetAncestorOfTwoNodesInBTAlgo3(bt2.Search(9), bt2.Search(13)).Data);
            BinaryTreeNodeWithParent<int> ancestor2 = GetAncestorOfTwoNodesInBTAlgo3(bt2.Search(4), bt2.Search(14));
            Console.WriteLine("The common ancestor of 4,14 from Algo3 is : {0}", ancestor2);

            // Algo3
            BinaryTreeWithParent<int> bt3 = new BinaryTreeWithParent<int>(new int[] { 8, 4, 12, 1, 5, 9, 13, 0, 2 }, true);
            Console.WriteLine("The common ancestor of 4,2 from Algo3 is : " + GetAncestorOfTwoNodesInBTAlgo3(bt3.Search(4), bt3.Search(2)).Data);
            Console.WriteLine("The common ancestor of 1,13 from Algo3 is : " + GetAncestorOfTwoNodesInBTAlgo3(bt3.Search(1), bt3.Search(13)).Data);
            Console.WriteLine("The common ancestor of 9,13 from Algo3 is : " + GetAncestorOfTwoNodesInBTAlgo3(bt3.Search(9), bt3.Search(13)).Data);
            BinaryTreeNodeWithParent<int>  ancestor3 = GetAncestorOfTwoNodesInBTAlgo3(bt3.Search(4), bt3.Search(14));
            Console.WriteLine("The common ancestor of 4,14 from Algo3 is : {0}", ancestor3);
        }
    }
}
