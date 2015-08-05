using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    class TreeNode<T>
    {
        public T Data { get; set; }
        public List<TreeNode<T>> Children { get; set; }

        public TreeNode(T data)
        {
            Data = data;
            Children = new List<TreeNode<T>>();
        }
    }
}
