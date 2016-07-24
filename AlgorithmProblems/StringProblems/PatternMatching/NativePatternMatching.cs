using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.PatternMatching
{
    /// <summary>
    /// Do a pattern search on a text string
    /// </summary>
    class NativePatternMatching
    {

        /// <summary>
        /// This is a native pattern searching algorithm.
        /// Where we check for the pattern in each index of the text.
        /// The running time of this algorithm is O(textLength* patternLength)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static List<int> GetAllMatches(string text, string pattern)
        {
            List<int> ret = new List<int>();

            for(int textIndex =0; textIndex<text.Length; textIndex++)
            {
                int currentTextIndex = textIndex;
                int currentPatternIndex = 0;
                while(currentTextIndex<text.Length && currentPatternIndex<pattern.Length)
                {
                    if(text[currentTextIndex] != pattern[currentPatternIndex])
                    {
                        break;
                    }
                    currentPatternIndex++;
                    currentTextIndex++;
                }
                if(currentPatternIndex== pattern.Length)
                {
                    // We have a match at textIndex
                    ret.Add(textIndex);
                }
            }

            return ret;
        }

        public static void TestNativePatternMatching()
        {
            string text = "Get all textext in the string tex";
            string pattern = "text";
            Console.WriteLine("The pattern:{0} is present in the text:{1}", pattern, text);
            PrintMatches(GetAllMatches(text, pattern));

            text = "Get all textext in the string tex";
            pattern = "textx";
            Console.WriteLine("The pattern:{0} is present in the text:{1}", pattern, text);
            PrintMatches(GetAllMatches(text, pattern));

            text = "tttttttt";
            pattern = "t";
            Console.WriteLine("The pattern:{0} is present in the text:{1}", pattern, text);
            PrintMatches(GetAllMatches(text, pattern));
        }

        private static void PrintMatches(List<int> allMatches)
        {
            foreach(int match in allMatches)
            {
                Console.Write("{0}, ", match);
            }
            Console.WriteLine();
        }
    }
}
