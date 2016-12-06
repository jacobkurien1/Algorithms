using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// Serialize and deserialize a tree.
    /// 
    /// if the given tree is BST then we can save a preorder/postorder traversal
    /// If the given tree is a complete tree(all nodes are present except the last level nodes to the right), we can use a binary heap and store it.
    /// If the given tree is a full binary tree(internal nodes have 2 children and there are leaf nodes), we can store a preorder and store a bit to indicate the leaf nodes.
    /// If the given tree is a general binary tree, we can save a preorder traversal with marker for null nodes.
    /// </summary>
    class SerializeDeserializeBinaryTree
    {

        static void Serialize(BinaryTreeNode<int> node, StringBuilder sb)
        {
            if(node == null)
            {
                sb.Append("x ");
                return;
            }
            sb.Append(string.Format("{0} ", node.Data));
            Serialize(node.Left, sb);
            Serialize(node.Right, sb);
        }

        static BinaryTreeNode<int> DeSerialize(string[] allNodes, ref int index)
        {
            if (index < allNodes.Length && allNodes[index] != "x")
            {
                BinaryTreeNode<int> node = new BinaryTreeNode<int>(int.Parse(allNodes[index]));
                index++;
                node.Left = DeSerialize(allNodes, ref index);
                index++;
                node.Right = DeSerialize(allNodes, ref index);
                return node;
            }
            return null;
        }

        public static void TestSerializeDeserializeBinaryTree()
        {
            string serializedTree = "1 2 x 3 x x 4 x x";
            string[] allNodes = serializedTree.Split(' ');
            int index = 0;
            BinaryTreeNode<int> node = DeSerialize(allNodes, ref index);

            StringBuilder sb = new StringBuilder();
            Serialize(node, sb);
            Console.WriteLine("Expected Serialized tree is {0}. Actual Serialized tree is {1}", serializedTree, sb.ToString());
        }
    }
}
