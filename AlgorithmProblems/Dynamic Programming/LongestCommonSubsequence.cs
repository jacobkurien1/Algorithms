using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    class LongestCommonSubsequence
    {
        /// <summary>
        /// We can find longest common subsequence using recursion. But the running time will be exponential.
        /// So we use the dynamic programming approach.
        /// Given 2 string "ACBEA" and "ADCA", the LCS is "ACA"
        /// Algo: 1. What will be the last step?
        /// if the element Matches then do LCS(ACBEA, ADCA) = 1+ LCS(ACBE, ADC)
        /// if the element does not match, LCS(ACBE, ADC) = max(LCS(ACB,ADC) , LCS(ACBE, AD))
        /// </summary>
        /// <param name="str1">string 1</param>
        /// <param name="str2">string 2</param>
        /// <returns></returns>
        private static char[] GetLongestCommonSubsequence(char[] str1, char[] str2)
        {
            int[,] lcsValue = new int[str1.Length + 1, str2.Length + 1];

            // Pointer table will help us get the subsequence value(by backtracking the path)
            //it can have 3 values:
            //0-> Match
            //1-> Came horizontally
            //2-> Came vertically
            int[,] pointerTable = new int[str1.Length + 1, str2.Length + 1];
            
            // Populate the lcsValue table and the pointer table
            for(int i=1; i<=str1.Length; i++)
            {
                for(int j=1; j<=str2.Length; j++)
                {
                    if(str1[i-1] == str2[j-1])
                    {
                        //Case where we have the match between the 2 characters of the string
                        lcsValue[i, j] = 1 + lcsValue[i - 1, j - 1];
                        pointerTable[i, j] = 0;
                    }
                    else
                    {
                        //Case where We dont have the match
                        if(lcsValue[i,j]<lcsValue[i-1,j])
                        {
                            lcsValue[i, j] = lcsValue[i - 1, j];
                            pointerTable[i, j] = 2;
                        }
                        if(lcsValue[i,j]<lcsValue[i,j-1])
                        {
                            lcsValue[i, j] = lcsValue[i, j - 1];
                            pointerTable[i, j] = 1;
                        }

                    }
                }
            }

            Console.WriteLine("The longest common subsequence is having {0} elements", lcsValue[str1.Length, str2.Length]);

            // Get the actual value of the LCS by back tracking
            char[] lcsOfBothStrings = new char[lcsValue[str1.Length, str2.Length]];
            int iIndex= str1.Length;
            int jIndex = str2.Length;
            int lcsOfBothStringsIndex = lcsOfBothStrings.Length - 1;
            while(iIndex>0 && jIndex>0)
            {
                if(pointerTable[iIndex,jIndex] == 0)
                {
                    // Its a match, hence this will be part of the lcs
                    lcsOfBothStrings[lcsOfBothStringsIndex--] = str1[iIndex - 1];
                    iIndex--;
                    jIndex--;
                }
                else if (pointerTable[iIndex,jIndex] == 1)
                {
                    // Came horizontally, hence go back horizontally
                    jIndex--;
                }
                else
                {
                    // Came vertically, hence go back vertically
                    iIndex--;
                }
            }

            return lcsOfBothStrings;

        }

        public static void TestGetLongestCommonSubsequence()
        {
            Console.WriteLine("The longest subsequnce of {0} and {1} is {2}", "ACBEA", "ADCA", new string(GetLongestCommonSubsequence("ACBEA".ToCharArray(), "ADCA".ToCharArray())));
            Console.WriteLine("The longest subsequnce of {0} and {1} is {2}", "stewie", "swathi", new string(GetLongestCommonSubsequence("stewie".ToCharArray(), "swathi".ToCharArray())));
            Console.WriteLine("The longest subsequnce of {0} and {1} is {2}", "malayalam", "malaysia", new string(GetLongestCommonSubsequence("malayalam".ToCharArray(), "malaysia".ToCharArray())));
        }
    }
}
