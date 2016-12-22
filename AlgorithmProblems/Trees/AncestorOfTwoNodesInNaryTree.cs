using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// Find the lowest common ancestor in an N ary tree
    /// </summary>
    class AncestorOfTwoNodesInNaryTree
    {
        /// <summary>
        /// This is a recursive subroutine to get the LCA in an Nary tree.
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
        /// 
        /// The running time is O(n)
        /// The space requirement is also O(n) for an unbalanced tree.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns></returns>
        static NaryTreeNode<char> GetLCA(NaryTreeNode<char> root, NaryTreeNode<char> node1, NaryTreeNode<char> node2)
        {
            if(root == null)
            {
                return null;
            }
            if(root == node1 || root == node2)
            {
                return root;
            }

            NaryTreeNode<char> lastLCA = null;
            int numOfElementFound = 0;
            foreach (NaryTreeNode<char> n in root.Children)
            {
                NaryTreeNode<char> lca = GetLCA(n, node1, node2);
                if(lca != null)
                {
                    numOfElementFound++;
                    if(lastLCA == null)
                    {
                        // We have already found the LCA and we need to return the last found LCA
                        lastLCA = lca;
                    }
                }
            }
            if (numOfElementFound == 2)
            {
                // root in this case is the LCA
                return root;
            }
            return lastLCA;
        }

        public static void TestAncestorOfTwoNodesInNaryTree()
        {
            NaryTreeNode<char> root = new NaryTreeNode<char>('A');
            NaryTreeNode<char> child1 = new NaryTreeNode<char>('B');
            NaryTreeNode<char> child2 = new NaryTreeNode<char>('C');
            NaryTreeNode<char> child3 = new NaryTreeNode<char>('D');
            NaryTreeNode<char> child4 = new NaryTreeNode<char>('E');
            root.Children.Add(child1);
            root.Children.Add(child2);
            root.Children.Add(child3);
            root.Children.Add(child4);

            NaryTreeNode<char> child5 = new NaryTreeNode<char>('F');
            NaryTreeNode<char> child6 = new NaryTreeNode<char>('G');
            child2.Children.Add(child5);
            child2.Children.Add(child6);
            NaryTreeNode<char> child7 = new NaryTreeNode<char>('H');
            child3.Children.Add(child7);

            Console.WriteLine("The LCA of {0} and {1} is {2}. Expected:{3}", child5, child6, GetLCA(root, child5, child6), child2);
            Console.WriteLine("The LCA of {0} and {1} is {2}. Expected:{3}", child5, child3, GetLCA(root, child5, child3), root);
            Console.WriteLine("The LCA of {0} and {1} is {2}. Expected:{3}", child2, child6, GetLCA(root, child2, child6), child2);
            Console.WriteLine("The LCA of {0} and {1} is {2}. Expected:{3}", child1, child4, GetLCA(root, child1, child4), root);

        }
    }
}
