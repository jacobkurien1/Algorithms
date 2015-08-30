using AlgorithmProblems.Trie.TrieHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trie
{
    /// <summary>
    /// Question: Given a search string and a set of strings, find the shortest search string prefix which is not a prefix in the given set of strings
    /// </summary>
    class ShortestPrefix
    {
        public static string GetShortestPrefixNotPresentInStringSet(string searchStr, List<string> knownStringSet)
        {
            // Create Trie with all the known string set
            Trie<char> trieOfKnownStr = new Trie<char>();
            foreach( string knownStr in knownStringSet)
            {
                trieOfKnownStr.Insert(knownStr.ToCharArray());
            }
            
            // Now we need to search the Trie 
            TrieNode<char> currentNode = trieOfKnownStr.Root;

            for(int searchStrIndex =0; searchStrIndex < searchStr.Length; searchStrIndex++)
            {
                if (!currentNode.Children.ContainsKey(searchStr[searchStrIndex]))
                {
                    return searchStr.Substring(0, searchStrIndex + 1);
                }
                else
                {
                    currentNode = currentNode.Children[searchStr[searchStrIndex]];
                }
            }

            // the searchStr is present in the string set and hence the shortest prefix not present is empty
            return "";
        }

        public static void TestGetShortestPrefixNotPresentInStringSet()
        {
            List<string> knownStringSet = new List<string>();
            knownStringSet.Add("dog");
            knownStringSet.Add("cat");
            knownStringSet.Add("drag");
            knownStringSet.Add("drool");

            Console.WriteLine("The smallest prefix of {0} not present in the knownStringSet is {1}", "came", GetShortestPrefixNotPresentInStringSet("came", knownStringSet));
            Console.WriteLine("The smallest prefix of {0} not present in the knownStringSet is {1}", "cat", GetShortestPrefixNotPresentInStringSet("cat", knownStringSet));
            Console.WriteLine("The smallest prefix of {0} not present in the knownStringSet is {1}", "dragged", GetShortestPrefixNotPresentInStringSet("dragged", knownStringSet));
            Console.WriteLine("The smallest prefix of {0} not present in the knownStringSet is {1}", "dro", GetShortestPrefixNotPresentInStringSet("dro", knownStringSet));
        }
    }
}
