using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems.PatternMatching
{
    /// <summary>
    /// Do Substring match by using Knuth-Morris-Pratt Algorithm
    /// </summary>
    class KMPPatternMatching
    {
        /// <summary>
        /// 1. Create a prefix array
        /// 2. get two pointers textIndex and patternIndex for text and pattern char[] and initialize them to 0
        /// 3. Do the following as long as the patternIndex < pattern.Length
        ///     4. if(text[textIndex] == pattern[patternIndex]) ->{ if(textIndex == text.Length -1) we have a match else textIndex++ and patternIndex++
        ///     5. else -> patternIndex = prefixArr[patternIndex-1]
        /// 
        /// The running time of this approach is O(pattern.Length + text.Length)
        /// The space needed is O(pattern.Length)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static List<int> GetAllMatches(string text, string pattern)
        {
            List<int> allMatches = new List<int>();
            int[] prefixArr = GetPrefixArray(pattern);
            int textIndex = 0;
            int patternIndex = 0;
            while(textIndex < text.Length)
            {
                if(text[textIndex] == pattern[patternIndex])
                {
                    if (patternIndex == pattern.Length - 1)
                    {
                        // We have a match
                        allMatches.Add(textIndex - patternIndex);
                        patternIndex = 0;
                        textIndex++;
                    }
                    else
                    {
                        // Keep incrementing both the indices till we get the complete match
                        patternIndex++;
                        textIndex++;
                    }

                }
                else
                {
                    if (patternIndex == 0)
                    {
                        // We have a mismatch at the first Index itself
                        // need to increment the textIndex here
                        textIndex++;
                    }
                    else
                    {
                        // We dont need to start from patternIndex = 0,
                        // prefixArr will get us the next index from which the match needs to be started
                        patternIndex = prefixArr[patternIndex-1];
                    }
                }
            }

            return allMatches;
        }

        /// <summary>
        /// We have 2 pointers prefixIndex and sufixIndex
        /// 1. if pattern[prefixIndex] == pattern[suffixIndex] -> prefixArr[suffixIndex] = prefixIndex + 1;
        /// and increment both prefixIndex and suffixIndex
        /// 2. if they do not match -> prefixIndex = prefixArr[prefixIndex - 1];
        ///     once prefixIndex == 0 then we should make  prefixArr[suffixIndex] = 0 and increment suffixIndex
        /// 
        /// The running time is O(pattern.Length) 
        /// The space is O(pattern.Length)
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static int[] GetPrefixArray(string pattern)
        {
            int[] prefixArr = new int[pattern.Length];

            int suffixIndex = 1;
            int prefixIndex = 0;

            while(suffixIndex<pattern.Length)
            {
                if(pattern[suffixIndex] == pattern[prefixIndex])
                {
                    prefixArr[suffixIndex] = prefixIndex + 1;
                    prefixIndex++;
                    suffixIndex++;
                }
                else
                {
                    if (prefixIndex == 0)
                    {
                        prefixArr[suffixIndex] = 0;
                        suffixIndex++;
                    }
                    else
                    {
                        prefixIndex = prefixArr[prefixIndex - 1];
                    }
                }
            }

            return prefixArr;
        }

        public static void TestKMPPatternMatching()
        {
            string text = "Get all textext in the string tex";
            string pattern = "text";
            Console.WriteLine("The pattern:{0} is present in the text:{1}", pattern, text);
            PrintMatches(GetAllMatches(text, pattern));

            //text = "Get all textext in the string tex";
            //pattern = "textx";
            //Console.WriteLine("The pattern:{0} is present in the text:{1}", pattern, text);
            //PrintMatches(GetAllMatches(text, pattern));

            //text = "tttttttt";
            //pattern = "t";
            //Console.WriteLine("The pattern:{0} is present in the text:{1}", pattern, text);
            //PrintMatches(GetAllMatches(text, pattern));

            text = "abxabcabcaby";
            pattern = "abcaby";
            Console.WriteLine("The pattern:{0} is present in the text:{1}", pattern, text);
            PrintMatches(GetAllMatches(text, pattern));

            text = "abcxabcdabxabcdabcdabcy";
            pattern = "abcdabcy";
            Console.WriteLine("The pattern:{0} is present in the text:{1}", pattern, text);
            PrintMatches(GetAllMatches(text, pattern));
        }

        private static void PrintMatches(List<int> allMatches)
        {
            foreach (int match in allMatches)
            {
                Console.Write("{0}, ", match);
            }
            Console.WriteLine();
        }
    }
}
