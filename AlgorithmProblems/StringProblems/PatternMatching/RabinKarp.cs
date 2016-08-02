using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems.PatternMatching
{
    /// <summary>
    /// Do the substring search using the Rabin Karp pattern matching algorithm
    /// </summary>
    class RabinKarp
    {
        public int PrimeNum { get; set; }
        public RabinKarp(int primeNum)
        {
            PrimeNum = primeNum;
        }

        /// <summary>
        /// 1. Get the hash of the pattern
        /// 2. Get the hash of the part of text with length same as the pattern length and try to match it to patternHash
        /// 3. If there is a hash match, do the actual string match at that location
        /// 4. If there is a string match too, we have a match.
        /// 
        /// The worst case running time is O(pattern.Length*text.Length)
        /// Cause we can take a bad hash function and keep checking the whole pattern against the text at all textIndices
        /// Which will make it equivalent to the naive algo and hence we have its running time.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private List<int> GetAllMatches(string text, string pattern)
        {
            List<int> allMatches = new List<int>();
            long patternHash = GetHash(pattern);
            long textHash = -1;
            for (int textIndex = 0; textIndex<=text.Length-pattern.Length; textIndex++)
            {
                if(textHash == -1)
                {
                    // Get the hash at textIndex ==0
                    textHash = GetHash(text.Substring(textIndex, pattern.Length));
                }
                else
                {
                    // use the efficient way to get the hash once the textIndex != 0
                    textHash = RecalculateHash(text.Substring(textIndex - 1, pattern.Length), text.Substring(textIndex, pattern.Length), textHash);
                }
                if(textHash == patternHash)
                {
                    // We might have a match
                    // Check all the characters now
                    if(MatchCharsAtIndex0(text.Substring(textIndex, pattern.Length), pattern))
                    {
                        // We have a final match here
                        allMatches.Add(textIndex);
                    }
                }
            }
            return allMatches;
        }

        /// <summary>
        /// Match the characters in text and pattern at index 0
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <returns>returns whether there is a match between all characters of text and pattern at index 0</returns>
        private bool MatchCharsAtIndex0(string text, string pattern)
        {
            if(string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern) || text.Length != pattern.Length)
            {
                throw new ArgumentException();
            }
            for(int index = 0; index < text.Length; index++)
            {
                if(text[index] != pattern[index])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Get the hash value from a string using the following hash function
        /// H("abc") = (ascii(a)* prime^0) + (ascii(b)* prime^1) + (ascii(c)* prime^2)
        /// </summary>
        /// <param name="hashStr">string for which the hash needs to be calculated</param>
        /// <returns>hash value of the hashStr</returns>
        private long GetHash(string hashStr)
        {
            long hashValue = 0;
            int currentPowerMultiple = 1;
            for(int index = 0; index<hashStr.Length; index++)
            {
                hashValue += (long)(hashStr[index]) * currentPowerMultiple;
                currentPowerMultiple *= PrimeNum;
            }
            return hashValue;
        }

        /// <summary>
        /// We need to recalculate the hash when we have the first character of oldStr removed and last char of newStr added
        /// in an effective manner
        /// </summary>
        /// <param name="oldStr">old string</param>
        /// <param name="newStr">new string</param>
        /// <param name="oldHash">hash value of the oldStr</param>
        /// <returns></returns>
        private long RecalculateHash(string oldStr, string newStr, long oldHash)
        {
            if(string.IsNullOrEmpty(oldStr) || string.IsNullOrEmpty(newStr))
            {
                throw new ArgumentException();
            }
            int lastIndex = newStr.Length - 1;
            return ((oldHash - (oldStr[0])) / PrimeNum) + (newStr[lastIndex] * (long)Math.Pow(PrimeNum, lastIndex));
        }

        public static void TestRabinKarp()
        {
            RabinKarp rk = new RabinKarp(3);
            string text = "Get all textext in the string tex";
            string pattern = "text";
            Console.WriteLine("The pattern:{0} is present in the text:{1}", pattern, text);
            PrintMatches(rk.GetAllMatches(text, pattern));

            text = "Get all textext in the string tex";
            pattern = "textx";
            Console.WriteLine("The pattern:{0} is present in the text:{1}", pattern, text);
            PrintMatches(rk.GetAllMatches(text, pattern));

            text = "tttttttt";
            pattern = "t";
            Console.WriteLine("The pattern:{0} is present in the text:{1}", pattern, text);
            PrintMatches(rk.GetAllMatches(text, pattern));

            text = "abxabcabcaby";
            pattern = "abcaby";
            Console.WriteLine("The pattern:{0} is present in the text:{1}", pattern, text);
            PrintMatches(rk.GetAllMatches(text, pattern));

            text = "abcxabcdabxabcdabcdabcy";
            pattern = "abcdabcy";
            Console.WriteLine("The pattern:{0} is present in the text:{1}", pattern, text);
            PrintMatches(rk.GetAllMatches(text, pattern));
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
