using AlgorithmProblems.Linked_List.Linked_List_Helper;
using AlgorithmProblems.Stack_and_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// create tree from the expression
    /// expression: a < b < e < >> c <> d < f <>>>
    /// tree: a -> b -> e
    ///         -> c
    ///         -> d -> f
    /// </summary>
    class TreeFromExpression
    {
        /// <summary>
        /// Algo1: 1. Keep a stack and every time a char(other than < or >) is encountered, make stack.top.child = newelement
        /// Push this new element into the stack.
        /// 2. When a < is encountered, do nothing
        /// 3. When a > is encountered, do a pop()
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static TreeNode<char> CreateTreeFromExpression(char[] expression)
        {
            StackViaLinkedList<TreeNode<char>> stackForTree = new StackViaLinkedList<TreeNode<char>>();
            TreeNode<char> headOfTree = null;
            for(int expIndex = 0; expIndex<expression.Length; expIndex++)
            {
                if(expression[expIndex] == '<' || expression[expIndex] == ' ' )
                {
                    // No-op
                }
                else if (expression[expIndex] == '>')
                {
                    SingleLinkedListNode<TreeNode<char>> top = stackForTree.Pop();
                    if(top == null)
                    {
                        throw new Exception("The expression is not well formed");
                    }
                }
                else
                {
                    SingleLinkedListNode<TreeNode<char>> top = stackForTree.Peek();
                    TreeNode<char> currentNode = new TreeNode<char>(expression[expIndex]);
                    if (top == null)
                    {
                        headOfTree = currentNode;
                    }
                    else
                    {
                        top.Data.Children.Add(currentNode);
                    }
                    stackForTree.Push(currentNode);
                }
            }
            // After this step the stack should be empty for a well formed expression
            if(stackForTree.Top!=null)
            {
                throw new Exception("The expression is not well formed");
            }
            return headOfTree;
        }

        public static void TestCreateTreeFromExpression()
        {
            Console.WriteLine("Test create tree from well formed expression");
            TreeNode<char> treeHead = CreateTreeFromExpression("a<b<e<>>c<>d<f<>>>".ToCharArray());
            TreeHelperGeneral.PrintATree(treeHead);
            Console.WriteLine("Test create tree from non - well formed expression");
            try
            {
                CreateTreeFromExpression("a<b<e<>>c<<>>d<f<>>>".ToCharArray());
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
    }
}
