using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trie.TrieHelper
{
    class Trie<T>
    {
        public TrieNode<T> Root { get; set; }
        public Trie()
        {
            Root = new TrieNode<T>(default(T));
        }

        public void Insert(T[] datas)
        {
            TrieNode<T> currentNode = Root;
            foreach(T data in datas)
            {
                if (!currentNode.Children.ContainsKey(data))
                {
                    currentNode.Children[data] = new TrieNode<T>(data);
                }
                currentNode = currentNode.Children[data];

            }
            currentNode.IsLast = true;
        }

        public bool Search(T[] dataToSearch, bool partialMatchOk)
        {
            TrieNode<T> currentNode = Root;
            foreach(T data in dataToSearch)
            {
                if (!currentNode.Children.ContainsKey(data))
                {
                    return false;
                }
                currentNode = currentNode.Children[data];
            }
            if(partialMatchOk)
            {
                // we found the partial match
                return true;
            }
            else
            {
                // if the node is the last node then we found the match else return false
                return currentNode.IsLast;
            }
        }
    }
}
