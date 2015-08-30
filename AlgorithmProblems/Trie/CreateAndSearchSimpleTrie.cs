using AlgorithmProblems.Trie.TrieHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trie
{
    class CreateAndSearchSimpleTrie
    {
        public static void TestCreateAndSearchSimpleTrie()
        {
            Trie<char> trie = new Trie<char>();
            List<char[]> listOfCharArray = new List<char[]>();
            listOfCharArray.Add("dog".ToCharArray());
            listOfCharArray.Add("cat".ToCharArray());
            listOfCharArray.Add("drag".ToCharArray());
            listOfCharArray.Add("drool".ToCharArray());

            // Populate the trie
            foreach(char[] charArray in listOfCharArray)
            {
                trie.Insert(charArray);
            }

            Console.WriteLine("cat is present fully: {0}", trie.Search("cat".ToCharArray(), false));
            Console.WriteLine("drag is present fully: {0}", trie.Search("drag".ToCharArray(), false));
            Console.WriteLine("dra is present fully: {0}", trie.Search("dra".ToCharArray(), false));
            Console.WriteLine("dra is present partially: {0}", trie.Search("dra".ToCharArray(), true));
            Console.WriteLine("dude is present partially: {0}", trie.Search("dude".ToCharArray(), true));
        }
    }
}
