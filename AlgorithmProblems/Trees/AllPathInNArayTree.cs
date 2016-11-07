using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// Print all the paths in an n-ary tree
    /// </summary>
    class AllPathInNArayTree
    {
        /// <summary>
        /// Stores the path as the traversal is done
        /// </summary>
        List<Node> path = new List<Node>();

        /// <summary>
        /// The recursive subroutine which does DFS and the base condition is hit
        /// once the leaf node is hit.
        /// </summary>
        /// <param name="n"></param>
        void PrintAllPath(Node n)
        {
            if(n.Children.Count == 0)
            {
                // this is a leaf node we need to print the path
                path.Add(n);
                PrintPath();
                path.Remove(n);
                return;
            }
            path.Add(n);
            foreach(Node child in n.Children)
            {
                PrintAllPath(child);
            }
            path.Remove(n);
        }

        /// <summary>
        /// Prints the current path till the leaf node
        /// </summary>
        private void PrintPath()
        {
            foreach(Node n in path)
            {
                Console.Write("{0} -> ", n.Data);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Represents the node of an n_ary tree
        /// </summary>
        class Node
        {
            public char Data { get; set; }
            public List<Node> Children { get; set; }
            public Node(char data)
            {
                Data = data;
                Children = new List<Node>();
            }
        }

        #region TestArea
        public static void TestAllPathInNArayTree()
        {
            Node nodeA = new Node('A');
            Node nodeB = new Node('B');
            Node nodeC = new Node('C');
            Node nodeD = new Node('D');
            Node nodeE = new Node('E');
            Node nodeF = new Node('F');
            Node nodeG = new Node('G');
            Node nodeH = new Node('H');
            Node nodeI = new Node('I');
            nodeA.Children = new List<Node>() { nodeB, nodeC, nodeD };
            nodeB.Children = new List<Node>() { nodeE, nodeF };
            nodeC.Children = new List<Node>() { nodeG };
            nodeD.Children = new List<Node>() { nodeH, nodeI };

            AllPathInNArayTree allPath = new AllPathInNArayTree();
            allPath.PrintAllPath(nodeA);

        }
        #endregion
    }
}
