using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems
{
    /// <summary>
    /// Check whether one of the different permutations of a string is a palindrome
    /// </summary>
    class PalindromeInStringPermutation
    {
        /// <summary>
        /// We need to make sure all the elements are having even number of occurance and
        /// atmost one element can have an odd number of occurance
        /// </summary>
        /// <param name="inputStr">the input string</param>
        /// <returns>whether one of the permutations of the string is a palindrome</returns>
        public static bool IsPalindromeInStringPermutation(string inputStr)
        {
            Dictionary<char, int> occuranceDict = new Dictionary<char, int>();
            for(int i=0; i<inputStr.Length; i++)
            {
                if(!occuranceDict.ContainsKey(inputStr[i]))
                {
                    occuranceDict[inputStr[i]] = 1;
                }
                else
                {
                    occuranceDict[inputStr[i]] += 1;
                }
            }

            int totalOddOccurances = 0;
            int totalEvenOccurances = 0;
            foreach (KeyValuePair<char, int> record in occuranceDict)
            {
                if (record.Value % 2 == 0)
                {
                    totalEvenOccurances++;
                }
                else
                {
                    totalOddOccurances++;
                    if(totalOddOccurances>1)
                    {
                        // odd occurances should not be greater than one for palindrome
                        return false;
                    }
                }
            }
            if(totalEvenOccurances<1)
            {
                //total even occurances should atleast be greater or equal to one
                return false;
            }
            return true;
        }
        public static void TestPalindromeInStringPermutation()
        {
            string inputStr = "ababcbb";
            Console.WriteLine("Is the string : {0} have a permutation which is a palindrome: {1}", inputStr, IsPalindromeInStringPermutation(inputStr));

            inputStr = "a";
            Console.WriteLine("Is the string : {0} have a permutation which is a palindrome: {1}", inputStr, IsPalindromeInStringPermutation(inputStr));

            inputStr = "ababcbbx";
            Console.WriteLine("Is the string : {0} have a permutation which is a palindrome: {1}", inputStr, IsPalindromeInStringPermutation(inputStr));

            inputStr = "aaaabbccccccc";
            Console.WriteLine("Is the string : {0} have a permutation which is a palindrome: {1}", inputStr, IsPalindromeInStringPermutation(inputStr));

            inputStr = "civic";
            Console.WriteLine("Is the string : {0} have a permutation which is a palindrome: {1}", inputStr, IsPalindromeInStringPermutation(inputStr));
        }
    }
}
