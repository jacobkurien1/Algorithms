using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    class LongestCommonSubString
    {
        /// <summary>
        /// Get the longest common substring between 2 input strings str1 and str2
        /// 
        /// We will solve this problem using dynamic programming
        /// LCS(JACOB, JACOX) = max{LCS(JACOB, JACO), LCS(JACO, JACOX)} , since the last char in both the strings are different
        /// LCS(XJACOB, YJACOB) = 1+ LCS(XJACO, YJACO) , since the last char in both the strings are same
        /// 
        /// We will also use a pointer table to help us back track and find the longest substring
        /// The different values in the pointer table are:
        /// 0-> match
        /// 1-> horizontal(go left while backtracking)
        /// 2-> vertical(go up while backtracking)
        /// 
        /// </summary>
        /// <param name="str1">input string</param>
        /// <param name="str2">input string</param>
        /// <returns>longest common substring between 2 input strings str1 and str2</returns>
        private static string GetLongestCommonSubString(string str1, string str2)
        {
            int[,] lcsVal = new int[str1.Length, str2.Length];
            int[,] pointerTable = new int[str1.Length, str2.Length];

            //initialization
            for(int i=0; i<str1.Length; i++)
            {
                pointerTable[i, 0] = 1;
            }
            for (int j = 0; j < str1.Length; j++)
            {
                pointerTable[0, j] = 2;
            }

            for(int i=0; i<str1.Length; i++)
            {
                for(int j=0; j<str2.Length; j++)
                {
                    if(str1[i] == str2[j])
                    {
                        lcsVal[i, j] = 1 + ((i==0||j==0)? 0: lcsVal[i - 1, j - 1]);
                        pointerTable[i, j] = 0; // we have a one char match here
                    }
                    else
                    {
                        if(j>0 && lcsVal[i, j] < lcsVal[i, j-1])
                        {
                            lcsVal[i, j] = lcsVal[i, j - 1];
                            pointerTable[i, j] = 1; // go left while backtracking
                        }
                        if (i>0 && lcsVal[i, j] < lcsVal[i-1, j])
                        {
                            lcsVal[i, j] = lcsVal[i - 1, j];
                            pointerTable[i, j] = 2; // go up while backtracking
                        }
                    }
                }
            }

            int iIndex = str1.Length-1;
            int jIndex = str2.Length-1;
            Console.WriteLine("The longest common substring is having a length of {0}", lcsVal[iIndex, jIndex]);
            char[] lcsString = new char[lcsVal[iIndex, jIndex]];
            int lcsStringIndex = lcsVal[iIndex, jIndex]-1;
            while(iIndex>=0 && jIndex>=0)
            {
                if(pointerTable[iIndex, jIndex] == 0)
                {
                    // We have a match here
                    lcsString[lcsStringIndex--] = str1[iIndex];
                    iIndex--;
                    jIndex--;
                }
                else if(pointerTable[iIndex, jIndex] == 1)
                {
                    jIndex--;
                }
                else // if (pointerTable[iIndex, jIndex] == 2)
                {
                    iIndex--;
                }
            }
            return new string(lcsString);
        }

        public static void TestGetLongestCommonSubString()
        {
            Console.WriteLine("The longest common substring of strings {0} and {1} is {2}", "XJacobY", "YJacobX", GetLongestCommonSubString("XJacobY", "YJacobX"));
            Console.WriteLine("The longest common substring of strings {0} and {1} is {2}", "XJacob", "YJacob", GetLongestCommonSubString("XJacob", "YJacob"));
            Console.WriteLine("The longest common substring of strings {0} and {1} is {2}", "JacobY", "JacobX", GetLongestCommonSubString("JacobY", "JacobX"));
        }
    }
}
