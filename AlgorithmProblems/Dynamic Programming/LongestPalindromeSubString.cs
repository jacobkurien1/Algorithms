using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    class LongestPalindromeSubString
    {
        /// <summary>
        /// We need to find the longest palindrome in a string.
        /// The brute force approach will be to get all the substrings of the str and find the longest palindrome.
        /// We will be recomputing a whehter a substring is palindrome or not many times.
        /// Hence we can store the results and use dynamic programming for optimization.
        /// 
        /// We will have an IsPalindrome boolean matrix where IsPalindrome[i,j] means whether the string[i->j] is palindrome or not.
        /// IsPalindrome[i,j] = IsPalindrome[i+1,j-1], if(str[i] == str[j])
        ///                     false , if(str[i] != str[j])
        ///                     
        /// initialization: all the IsPalindrome[i,j] where i==j is true
        /// </summary>
        /// <param name="str">input string</param>
        /// <returns>longest palindrome in the string str</returns>
        private static string GetLongestPalindromeSubString(string str)
        {
            int longestPalindromeLength = 0;
            int longestPalindromeStartIndex = 0;

            bool[,] IsPalindrome = new bool[str.Length, str.Length];

            // Initialization
            for (int i=0; i< str.Length; i++)
            {
                IsPalindrome[i, i] = true;
            }

            for (int lenOfStrConsidered = 1; lenOfStrConsidered < str.Length; lenOfStrConsidered++)
            {
                for (int i = 0; (i < str.Length) && (i + lenOfStrConsidered<str.Length); i++)
                {
                    if(str[i] == str[i+lenOfStrConsidered])
                    {
                        IsPalindrome[i, i + lenOfStrConsidered] = IsPalindrome[i + 1, i + lenOfStrConsidered - 1];
                        if(IsPalindrome[i, i + lenOfStrConsidered])
                        {
                            // We need to check whether this is the longest palindrome and save the indices
                            if(lenOfStrConsidered+1>longestPalindromeLength)
                            {
                                longestPalindromeLength = lenOfStrConsidered + 1;
                                longestPalindromeStartIndex = i;
                            }
                        }
                    }
                    else
                    {
                        // Since the bool array has the default we dont need to initialize it to false
                    }
                }
            }

            Console.WriteLine("The length of the longest palindrome is {0}", longestPalindromeLength);
            return str.Substring(longestPalindromeStartIndex, longestPalindromeLength);
        }

        public static void TestGetLongestPalindromeSubString()
        {
            Console.WriteLine("The longest palindrome of the string {0} is {1}", "bananas", GetLongestPalindromeSubString("bananas"));
        }
    }
}
