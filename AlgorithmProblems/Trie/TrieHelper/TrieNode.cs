using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trie.TrieHelper
{
    class TrieNode<T>
    {
        public T Data { get; set; }
        public bool IsLast { get; set; }
        public Dictionary<T, TrieNode<T>> Children { get; set; }

        public TrieNode(T data, bool isLast = false)
        {
            this.Data = data;
            this.Children = new Dictionary<T, TrieNode<T>>();
            this.IsLast = isLast;
        }
    }
}
