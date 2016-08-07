using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    class TreeHelperGeneral
    {
        public static void PrintATree(TreeNode<char> node)
        {
            Console.WriteLine(PrintANode(node));
        }

        private static string PrintANode(TreeNode<char> node)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(node.Data + " : [ ");
            for(int index = 0; index < node.Children.Count; index++)
            {
                sb.Append(PrintANode(node.Children[index]));
            }
            sb.Append(" ]");
            return sb.ToString();
        }

        public static void PrintATree(BinaryTreeNode<int> node)
        {
            Console.WriteLine(PrintANode(node));
        }

        private static string PrintANode(BinaryTreeNode<int> node)
        {
            
            StringBuilder sb = new StringBuilder();
            if (node != null)
            {
                sb.Append(node.Data + " : [ ");
                for (int index = 0; index < node.Children.Count; index++)
                {
                    sb.Append(PrintANode(node.Children[index]));
                }
                sb.Append(" ]");
            }
            else
            {
                sb.Append("null ");
            }
            return sb.ToString();
        }
    }
}
