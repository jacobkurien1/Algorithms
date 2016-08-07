using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    public class BinaryTreeNode<T>
    {
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public List<BinaryTreeNode<T>> Children
        {
            get
            {
                return new List<BinaryTreeNode<T>>() { Left, Right };
            }
        }
        
        public T Data { get; set; }

        public BinaryTreeNode(T data)
        {
            Data = data;
        }
    }
}
