using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems
{
    class PatternMatching
    {
        private static List<int> BoyerMooreHorsePoolAlgo(string textStr, string searchStr)
        {
            // Preprocess the data to form the bad match table
            Dictionary<char, int> badMatchTable = new Dictionary<char, int>();
            for(int i=0; i<searchStr.Length; i++)
            {
                badMatchTable[searchStr[i]] = searchStr.Length - 1 - i;
            }
            // In default case where the key is not present we need to move the whole search index by the length of the searchStr
            badMatchTable['?'] = searchStr.Length;

            // Now Lets find the different matches of the searchStr in textStr
            List<int> matchIndices = new List<int>();
            int searchIndex = searchStr.Length - 1;
            int textIndex = searchIndex;
            while(textIndex<textStr.Length)
            {
                while(searchIndex >=0)
                {
                    if(textStr[textIndex] == searchStr[searchIndex])
                    {
                        searchIndex--;
                        textIndex--;
                    }
                    else
                    {
                        // We found mismatch
                        if(badMatchTable.ContainsKey(textStr[textIndex]))
                        {
                            textIndex += badMatchTable[textStr[textIndex]];
                        }
                        else
                        {
                            textIndex += badMatchTable['?'];
                        }
                        break;
                    }
                }
                if(searchIndex==-1)
                {
                    // We have a complete match at ++textIndex
                    matchIndices.Add(++textIndex);
                    textIndex += searchStr.Length;
                }
                searchIndex = searchStr.Length-1;
            }

            return matchIndices;
        }

        public static void TestPatternMatching()
        {
            Console.WriteLine("Use the Boyer Moore Horsepool Algorithm");
            List<int> matchIndices = BoyerMooreHorsePoolAlgo("jacob is a cute boy", "cute");
            foreach(int index in matchIndices)
            {
                Console.WriteLine("The pattern is matched at index {0}", index);
            }
        }
    }
}
