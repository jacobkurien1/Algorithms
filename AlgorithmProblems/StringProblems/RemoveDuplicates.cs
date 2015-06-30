using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems
{
    class RemoveDuplicates
    {
        /// <summary>
        /// Step1: Form a character set.
        /// Step2: Track the number of occurances
        /// Step3: Create a new string w/o duplicates
        /// Running time = O(n)
        /// Space = O(256)
        /// </summary>
        /// <param name="inputStr">input string whose duplicates needs to be removed</param>
        /// <returns>returns the string w/o any duplicates</returns>
        public string RemoveDuplicatesFromStringAlgo1(string inputStr)
        {
            char[] inputCharArray = inputStr.ToCharArray();
            int[] charset = new int[256];
            int uniqueCharacters = 0;
            // Step1: Form a character set.
            // Step2: Track the number of occurances
            foreach(char c in inputCharArray)
            {
                uniqueCharacters++;
                charset[c]++;
            }
            char[] noDuplicateChars = new char[uniqueCharacters];
            int index = 0;
            // Step3: Create a new string w/o duplicates
            foreach(char c in inputCharArray)
            {
                if(charset[c]>=1)
                {
                    noDuplicateChars[index++] = c;
                    charset[c] = 0;
                }
            }
            return new String(noDuplicateChars);
        }

        /// <summary>
        /// Step1: Form a character set.
        /// Step2: Track the number of occurances
        /// Step3: Create a new string w/o duplicates
        /// Running time = O(n)
        /// Space = O(256)
        /// </summary>
        /// <param name="inputStr">input string whose duplicates needs to be removed</param>
        /// <returns>returns the string w/o any duplicates</returns>
        public string RemoveDuplicatesFromStringAlgo2(string inputStr)
        {
        }

        public static void TestRemoveDuplicatesFromString()
        {
            RemoveDuplicates rd = new RemoveDuplicates();
            string inputStr = "jacob jacob should be a good boy";

            Console.WriteLine("The input string is : "+ inputStr);
            Console.WriteLine("The string w/o duplicates from Algo1 is: " + rd.RemoveDuplicatesFromStringAlgo1(inputStr));
        }
    }
}
