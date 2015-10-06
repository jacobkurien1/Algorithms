using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    class WordBreaking
    {
        private static string BreakTheWords(string sentence)
        {
            // We will do the initialization
            int[] mat = new int[sentence.Length];
            for(int i=0; i< sentence.Length; i++)
            {
                mat[i] = -1;
            }

            // Now lets do the main algo
            for(int i = 0; i<sentence.Length; i++)
            {
                for(int j = 0; j<=i; j++)
                {
                    if(IsWord(i-j, i, sentence) && (i-j-1<0 || mat[i-j-1] != -1))
                    {
                        mat[i] = j;
                    }
                }
            }

            // Now we need to back track and add the space that is needed in the sentence
            int index = sentence.Length - 1;
            Stack<string> stByBacktracking = new Stack<string>();
            while (index >= 0 && index >= mat[index] && mat[index]!=-1)
            {
                stByBacktracking.Push(sentence.Substring(index - (mat[index]), mat[index] + 1));
                index -= (mat[index] + 1);
            }
            
            // Now form the sentence with the spaces in between
            StringBuilder sb = new StringBuilder();
            while (stByBacktracking.Count>0)
            {
                sb.Append(stByBacktracking.Pop() + " ");
            }
            return (sb.Length==0)? "": sb.ToString(0, sb.Length-1); // subtracting 1 to remove the last space in the string
        }

        private static bool IsWord(int start, int end, string str)
        {
            Dictionary<string, bool> wordDict = new Dictionary<string, bool>();
            wordDict.Add("the", true);
            wordDict.Add("dog", true);
            wordDict.Add("is", true);
            wordDict.Add("in", true);
            wordDict.Add("house", true);
            wordDict.Add("cat", true);
            wordDict.Add("need", true);
            wordDict.Add("to", true);
            wordDict.Add("break", true);
            wordDict.Add("these", true);
            wordDict.Add("words", true);
            string subStr = str.Substring(start, end-start+1);
            return wordDict.ContainsKey(subStr);
        }

        public static void TestBreakTheWords()
        {
            Console.WriteLine("The string after word breaking is :{0}", BreakTheWords("thedogisinthehouse"));
            Console.WriteLine("The string after word breaking is :{0}", BreakTheWords("thecatisinthehouse"));
            Console.WriteLine("The string after word breaking is :{0}", BreakTheWords("needtobreakthesewords"));
            Console.WriteLine("The string after word breaking is :{0}", BreakTheWords("thesewordscannotbebroken"));
        }
    }
}
