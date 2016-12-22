using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// Find a way to serialize/ deserialize an Nary tree.
    /// 
    /// For example the following nary tree
    ///           A
    ///			  |
    ///---------------------------------
    ///|        |              |       |
    ///B        C              D       E
    ///         |              |
    ///	  ----------		   H
    ///	 |         |
    ///  F         G
    /// 
    /// this tree will be serilaized as 
    /// A B ) C F ) G ) ) D H ) ) E ) )
    /// </summary>
    public class SerializeDeserializeAnNaryTree
    {
        public static void Serialize(NaryTreeNode<int> node, StringBuilder sb)
        {
            if (node == null)
            {
                return;
            }
            sb.Append(string.Format("{0} ", node.Data));
            foreach(NaryTreeNode<int> child in node.Children)
            {
                Serialize(child, sb);
            }
            sb.Append(") ");
        }

        public static NaryTreeNode<int> DeSerialize(string[] allNodes)
        {
            Stack<NaryTreeNode<int>> st = new Stack<NaryTreeNode<int>>();
            st.Push(new NaryTreeNode<int>(int.Parse(allNodes[0])));
            NaryTreeNode<int> root = null;

            for (int index = 1; index<allNodes.Length; index++)
            {
                if(allNodes[index] == ")")
                {
                    root = st.Pop();
                }
                else
                {
                    NaryTreeNode<int> currentNode = new NaryTreeNode<int>(int.Parse(allNodes[index]));
                    st.Peek().Children.Add(currentNode);
                    st.Push(currentNode);
                }
            }
            
            return root;
        }

        public static void TestSerializeDeserializeAnNaryTree()
        {
            string serializedTree = "1 2 ) 3 6 ) 7 ) ) 4 8 ) ) 5 ) )";
            string[] allNodes = serializedTree.Split(' ');
            NaryTreeNode<int> node = DeSerialize(allNodes);

            StringBuilder sb = new StringBuilder();
            Serialize(node, sb);
            Console.WriteLine("Expected Serialized tree is {0}. Actual Serialized tree is {1}", serializedTree, sb.ToString());
        }
    }
}
