using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees.TreeHelper
{
    public class NaryTreeNode<T>
    {
        public List<NaryTreeNode<T>> Children { get; set; }

        public T Data { get; set; }

        public NaryTreeNode(T data)
        {
            Data = data;
            Children = new List<NaryTreeNode<T>>();
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
