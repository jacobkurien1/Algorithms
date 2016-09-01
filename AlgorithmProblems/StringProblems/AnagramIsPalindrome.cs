using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems
{
    /// <summary>
    /// Check whether any anagram of a given string is palindrome or not.
    /// Also note: the string only has characters from a-z
    /// </summary>
    class AnagramIsPalindrome
    {
        /// <summary>
        /// Here for every char in the string we will get the count of occurance.
        /// We will keep track of the number of odd occurance of the character.
        /// A palindrome can have atmost one odd occurance.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>whether the given string has any anagram which is a palindrome</returns>
        public static bool IsAnagramAPalindrome(string str)
        {
            int[] charCount = new int[26];
            int numOfOdd = 0;
            for (int i = 0; i < str.Length; i++)
            {
                ++charCount[str[i] - 'a'];
                if (charCount[str[i] - 'a'] % 2 != 0)
                {
                    numOfOdd++;
                }
                else
                {
                    numOfOdd--;
                }
            }
            return numOfOdd <= 1;
        }
        public static void TestAnagramIsPalindrome()
        {
            string str = "aabsbuudccd";
            Console.WriteLine("The string : {0} has an anagram which is a palindrome : {1}", str, IsAnagramAPalindrome(str));

            str = "jacob";
            Console.WriteLine("The string : {0} has an anagram which is a palindrome : {1}", str, IsAnagramAPalindrome(str));

            str = "aabsbuudccdx";
            Console.WriteLine("The string : {0} has an anagram which is a palindrome : {1}", str, IsAnagramAPalindrome(str));
        }
    }
}
