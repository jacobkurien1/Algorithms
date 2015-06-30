using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems
{
    class Anagrams
    {
        /// <summary>
        /// Step1: Add the number of occurances for first string
        /// Step2: Sub the number of occurances for 2nd string
        /// Step3: Check for duplicate corner case 
        /// </summary>
        /// <param name="s1">first string</param>
        /// <param name="s2">second string</param>
        /// <returns>if the strings are anagrams</returns>
        public bool IsAnagramAlgo1(string s1, string s2)
        {
            if(s1.Length != s2.Length)
            {
                return false;
            }
            int[] charset = new int[256];
            Char[] c1 = s1.ToCharArray();
            Char[] c2 = s2.ToCharArray();

            // Step1: Add the number of occurances for first string
            foreach(char c in c1)
            {
                charset[c]++;
            }

            // Step2: Sub the number of occurances for 2nd string
            foreach(char c in c2)
            {
                if(--charset[c] < 0)
                {
                    return false;
                }
            }

            // Step3: Check for duplicate corner case 
            for (int i = 0; i < charset.Length; i++)
            {
                if(charset[i] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// We can keep the 
        /// </summary>
        /// <param name="s1">first string</param>
        /// <param name="s2">second string</param>
        /// <returns>if the strings are anagrams</returns>
        public bool IsAnagramAlgo2(string s1, string s2)
        {
            int numOfUniqueChars = 0;

            if (s1.Length != s2.Length)
            {
                return false;
            }
            int[] charset = new int[256];
            Char[] c1 = s1.ToCharArray();
            Char[] c2 = s2.ToCharArray();

            // Step1: Add the number of occurances for first string
            // and calculate the unique characters
            foreach (char c in c1)
            {
                if (charset[c] == 0)
                {
                    numOfUniqueChars++;
                }
                charset[c]++;
            }

            // Step2: Sub the number of occurances for 2nd string
            // and keep track of the number of unique characters
            foreach (char c in c2)
            {
                charset[c]--;
                if (charset[c] < 0)
                {
                    return false;
                }
                else if(charset[c] == 0)
                {
                    numOfUniqueChars--;
                }
            }

            // Step3: Make sure that the number of unique characters
            // is zero.
            if(numOfUniqueChars != 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Sort the 2 strings and check whether they are same
        /// </summary>
        /// <param name="s1">first string</param>
        /// <param name="s2">second string</param>
        /// <returns>if the strings are anagrams</returns>
        public bool IsAnagramAlgo3(string s1, string s2)
        {
            Char[] c1 = s1.ToCharArray();
            Char[] c2 = s2.ToCharArray();
            Array.Sort(c1);
            Array.Sort(c2);

            return new String(c1) == new String(c2);
        }

        public static void TestIsAnagramAlgo()
        {
            Anagrams angrams = new Anagrams();
            // positive test case
            string inputStr1 = "jacob";
            string inputStr2 = "cabjo";

            Console.WriteLine("the 2 strings are:" + inputStr1 + " and " + inputStr2 + ". And this is anagrams: " + angrams.IsAnagramAlgo1(inputStr1, inputStr2));
            Console.WriteLine("the 2 strings are:" + inputStr1 + " and " + inputStr2 + ". And this is anagrams: " + angrams.IsAnagramAlgo2(inputStr1, inputStr2));
            Console.WriteLine("the 2 strings are:" + inputStr1 + " and " + inputStr2 + ". And this is anagrams: " + angrams.IsAnagramAlgo3(inputStr1, inputStr2));

            // negative test case
            inputStr1 = "jacob";
            inputStr2 = "casjo";

            Console.WriteLine("the 2 strings are:" + inputStr1 + " and " + inputStr2 + ". And this is anagrams: " + angrams.IsAnagramAlgo1(inputStr1, inputStr2));
            Console.WriteLine("the 2 strings are:" + inputStr1 + " and " + inputStr2 + ". And this is anagrams: " + angrams.IsAnagramAlgo2(inputStr1, inputStr2));
            Console.WriteLine("the 2 strings are:" + inputStr1 + " and " + inputStr2 + ". And this is anagrams: " + angrams.IsAnagramAlgo3(inputStr1, inputStr2));

        }
    }
}
