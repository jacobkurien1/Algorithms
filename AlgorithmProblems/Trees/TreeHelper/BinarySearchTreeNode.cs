using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees.TreeHelper
{
    class BinarySearchTreeNode<T>
    {
        public BinarySearchTreeNode<T> Left { get; set; }
        public BinarySearchTreeNode<T> Right { get; set; }
        public BinarySearchTreeNode<T> Parent { get; set; }
        public T Data { get; set; }

        public BinarySearchTreeNode(T data)
        {
            Data = data;
        }
    }
}
