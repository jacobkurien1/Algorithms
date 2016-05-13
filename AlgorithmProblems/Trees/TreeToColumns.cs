using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// you are given a tree and you are required to seperate the tree into columns
    /// for example a complete tree given by the preorder traversal 8,5,3,9,10,1,2
    /// needs to be divided into the following columns
    /// col1 : 9
    /// col2 : 5
    /// col3 : 8, 10, 1
    /// col4 : 3
    /// col5 : 2
    /// 
    /// </summary>
    public class TreeToColumns
    {
        /// <summary>
        /// We can do this by assigning order = 0 to the root node
        /// whenever we go to the left node we make the order of that node order-1
        /// and whenever we go to the right node we make the order of that node order+1
        /// 
        /// We save all the nodes of a particular order in a dictionary where the key is the order and
        /// the value is the list of all the elements having that order
        /// 
        /// We will also save the minOrder and maxOrder while traversing and that will give us the size of the array of List<BinaryNodes>
        /// we want to return
        /// And then we can populate this array using the dictionary
        /// 
        /// Note: We are saving the min and max Order so that we know how long the index should be and wont have to sort all the orders
        /// otherwise the running time will become O(nlog(n))
        /// 
        /// Hence the running time of this algorithm is O(n)
        /// and the space requirement for this algorithm is O(n)
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static List<BinaryTreeNode<int>>[] ConvertTreeToColumns(BinaryTreeNode<int> root)
        {
            int minOrder = 0;
            int maxOrder = 0;
            Dictionary<int, List<BinaryTreeNode<int>>> dict = new Dictionary<int, List<BinaryTreeNode<int>>>();
            Stack<BinaryTreeNodeWithOrder> st = new Stack<BinaryTreeNodeWithOrder>();
            st.Push(new BinaryTreeNodeWithOrder(root, 0));

            // Traverse the tree and add the elements for a particular column in dict
            // Also make sure that we have the minimum index of the column and maximum index of the columns
            while(st.Count>0)
            {
                BinaryTreeNodeWithOrder current = st.Pop();
                if(!dict.ContainsKey(current.Order))
                {
                    dict[current.Order] = new List<BinaryTreeNode<int>>();
                }
                dict[current.Order].Add(current.Node);
                if(current.Node.Left != null)
                {
                    int order = current.Order - 1;
                    if(order<minOrder)
                    {
                        minOrder = order;
                    }
                    st.Push(new BinaryTreeNodeWithOrder(current.Node.Left, order));
                }
                if (current.Node.Right != null)
                {
                    int order = current.Order + 1;
                    if (order > maxOrder)
                    {
                        maxOrder = order;
                    }
                    st.Push(new BinaryTreeNodeWithOrder(current.Node.Right, order));
                }
            }

            // Now create an array where each index will represent the column and will have the list of nodes
            int range = maxOrder - minOrder + 1;
            List<BinaryTreeNode<int>>[] treeAsColumns = new List<BinaryTreeNode<int>>[range];
            for(int i = 0; i< range; i++)
            {
                treeAsColumns[i] = dict[i + minOrder];
            }
            return treeAsColumns;
        }

        public class BinaryTreeNodeWithOrder
        {
            public BinaryTreeNode<int> Node { get; set; }
            public int Order { get; set; }

            public BinaryTreeNodeWithOrder(BinaryTreeNode<int> node, int order)
            {
                Node = node;
                Order = order;
            }
        }

        public static void TestTreeToColumns()
        {
            BinaryTree<int> bt1 = new BinaryTree<int>(new int[] { 8, 4, 12, 1, 5, 9, 13 }, true);
            List<BinaryTreeNode<int>>[] treeAsColumns = ConvertTreeToColumns(bt1.Head);
            Console.WriteLine("The tree as column can be shown as:");
            PrintTreeAsColumns(treeAsColumns);
        }
        private static void PrintTreeAsColumns(List<BinaryTreeNode<int>>[] treeAsColumns)
        {
            for(int i=0; i<treeAsColumns.Length; i++)
            {
                Console.Write("{0} : ", i);
                foreach(BinaryTreeNode<int> node in treeAsColumns[i])
                {
                    Console.Write("{0}, ", node.Data);
                }
                Console.WriteLine();
            }
        }
    }
}
