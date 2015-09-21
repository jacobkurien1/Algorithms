using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    class LongestPalindromicSubsequence
    {
        /// <summary>
        /// We need to find the longest subsequence in the string str which is a palindrome
        /// For example the LPS("LPASPAL") = LPSPL
        /// We will get the LPS also by using dynamic programming.
        /// 
        /// LPS("LPASPAL") = LPS("PASPA") +2 , since the first and the last char is the same
        /// LPS("PASPA") = max(LPS("ASPA"), LPS("PASP")), since the first and the last char is not the same
        /// 
        /// We will also keep a pointer table
        /// 0 -> there was a match, hence go i+1 and j-1(diagonally opposite)
        /// 1 -> go horizontally(left)
        /// 2 -> go vertically(down)
        /// </summary>
        /// <param name="str">input string</param>
        /// <returns>longest palindromic subsequence of str</returns>
        private static string GetLongestPalindromicSubsequence(string str)
        {
            int[,] LPSValue = new int[str.Length, str.Length];
            int[,] pointerTable = new int[str.Length, str.Length];

            // initialization
            for(int i=0; i< str.Length; i++)
            {
                LPSValue[i, i] = 1;
                pointerTable[i, i] = 0;
            }

            for (int lenOfStrConsidered = 1; lenOfStrConsidered < str.Length; lenOfStrConsidered++)
            {
                for (int i = 0; (i < str.Length) && (i + lenOfStrConsidered< str.Length); i++)
                {
                    if (str[i] == str[i + lenOfStrConsidered])
                    {
                        LPSValue[i, i + lenOfStrConsidered] = 2 + LPSValue[i + 1, i + lenOfStrConsidered - 1];
                        pointerTable[i, i + lenOfStrConsidered] = 0;
                    }
                    else
                    {
                        if(LPSValue[i, i + lenOfStrConsidered] < LPSValue[i, i+ lenOfStrConsidered-1])
                        {
                            LPSValue[i, i + lenOfStrConsidered] = LPSValue[i , i + lenOfStrConsidered - 1];
                            pointerTable[i, i + lenOfStrConsidered] = 1;
                        }
                        if(LPSValue[i, i + lenOfStrConsidered] < LPSValue[i+1, i+ lenOfStrConsidered])
                        {
                            LPSValue[i, i + lenOfStrConsidered] = LPSValue[i + 1, i + lenOfStrConsidered];
                            pointerTable[i, i + lenOfStrConsidered] = 2;
                        }
                    }
                }
            }

            int lengthOfLPS = LPSValue[0, str.Length - 1];
            Console.WriteLine("The longest palindromic subsequence is having a length of {0}", lengthOfLPS);

            // Back track to get the string value of longest palindromic subsequence
            int jIndex = str.Length - 1;
            int iIndex = 0;
            char[] lpsString = new char[lengthOfLPS];
            int lpsStringEndIndex = lengthOfLPS - 1;
            int lpsStringStartIndex = 0;

            while(iIndex <str.Length && jIndex >=0 && iIndex<=jIndex )
            {
                if(pointerTable[iIndex,jIndex] == 0)
                {
                    // Match has been found
                    lpsString[lpsStringStartIndex++] = str[iIndex];
                    lpsString[lpsStringEndIndex--] = str[iIndex];
                    iIndex++;
                    jIndex--;
                }
                else if (pointerTable[iIndex,jIndex] == 1)
                {
                    // go horizontally(left)
                    jIndex--;
                }
                else if (pointerTable[iIndex,jIndex] == 2)
                {
                    // go vertically (down)
                    iIndex++;
                }
            }
            return new string(lpsString);
        }

        public static void TestGetLongestPalindromicSubsequence()
        {
            Console.WriteLine("The LPS of the string {0} is {1}", "LPASPAL", GetLongestPalindromicSubsequence("LPASPAL"));
            Console.WriteLine("The LPS of the string {0} is {1}", "jacobbocaj", GetLongestPalindromicSubsequence("jacobbocaj"));
            Console.WriteLine("The LPS of the string {0} is {1}", "jeakclonbvoccaxjr", GetLongestPalindromicSubsequence("jeakclonbvoccaxjr"));
            Console.WriteLine("The LPS of the string {0} is {1}", "jeakclonbvboccaxjr", GetLongestPalindromicSubsequence("jeakclonbvboccaxjr"));
        }
    }
}
