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
        /// LCS(JACOB, JACOX) = 0 , since the last char in both the strings are different
        /// LCS(XJACOB, YJACOB) = 1+ LCS(XJACO, YJACO) , since the last char in both the strings are same
        /// 
        /// </summary>
        /// <param name="str1">input string</param>
        /// <param name="str2">input string</param>
        /// <returns>longest common substring between 2 input strings str1 and str2</returns>
        private static string GetLongestCommonSubString(string str1, string str2)
        {
            int[,] lcsVal = new int[str1.Length, str2.Length];
            int maxLCSVal = 0;
            int maxLCSValIndex = -1;

            for(int i=0; i<str1.Length; i++)
            {
                for(int j=0; j<str2.Length; j++)
                {
                    if(str1[i] == str2[j])
                    {
                        lcsVal[i, j] = 1 + (( i-1<0 || j-1 <0 )? 0: lcsVal[i - 1, j - 1]);
                        if(maxLCSVal < lcsVal[i, j])
                        {
                            maxLCSVal = lcsVal[i, j];
                            maxLCSValIndex = i;
                        }
                    }
                }
            }

            return str1.Substring(maxLCSValIndex - maxLCSVal + 1, maxLCSVal);
        }

        public static void TestGetLongestCommonSubString()
        {
            Console.WriteLine("The longest common substring of strings {0} and {1} is {2}", "XJacobY", "YJacobX", GetLongestCommonSubString("XJacobY", "YJacobX"));
            Console.WriteLine("The longest common substring of strings {0} and {1} is {2}", "XJacob", "YJacob", GetLongestCommonSubString("XJacob", "YJacob"));
            Console.WriteLine("The longest common substring of strings {0} and {1} is {2}", "JacobYL", "JacobXL", GetLongestCommonSubString("JacobYL", "JacobXL"));
            Console.WriteLine("The longest common substring of strings {0} and {1} is {2}", "bbbbJacob", "Jacob", GetLongestCommonSubString("bbbbJacob", "Jacob"));
            Console.WriteLine("The longest common substring of strings {0} and {1} is {2}", "Jacobbbbbb", "Jacob", GetLongestCommonSubString("Jacobbbbbb", "Jacob"));
            Console.WriteLine("The longest common substring of strings {0} and {1} is {2}", "Jacob", "bbbbJacob", GetLongestCommonSubString("Jacob", "bbbbJacob"));
            Console.WriteLine("The longest common substring of strings {0} and {1} is {2}", "Jacob", "Jacobbbbbb", GetLongestCommonSubString("Jacob", "Jacobbbbbb"));
        }
    }
}
