using System;
using System.Collections;
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
                if (charset[c] == 0)
                {
                    uniqueCharacters++;
                }
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
        /// Step3: Edit the string so that we wont have any duplicates
        /// Running time = O(n)
        /// Space = O(256)
        /// </summary>
        /// <param name="inputCharArray">input char arraylist whose duplicates needs to be removed</param>
        /// <returns>returns the char arraylist w/o any duplicates</returns>
        public ArrayList RemoveDuplicatesFromStringAlgo2(ArrayList inputCharArray)
        {
            int[] charset = new int[256];
            int uniqueCharacters = 0;
            // Step1: Form a character set.
            // Step2: Track the number of occurances
            foreach (char c in inputCharArray)
            {
                if (charset[c] == 0)
                {
                    uniqueCharacters++;
                }
                charset[c]++;
            }
            int index = 0;
            int initialLength = inputCharArray.Count;

            // Step3: Edit the string so that we wont have any duplicates
            for (int i = 0; i < initialLength; i++ )
            {
                if (charset[(char)inputCharArray[i]] >= 1)
                {
                    inputCharArray[index++] = (char)inputCharArray[i];
                    charset[(char)inputCharArray[i]] = 0;
                }
            }
            inputCharArray.RemoveRange(uniqueCharacters, initialLength - uniqueCharacters);
            return inputCharArray;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputCharArray"></param>
        /// <returns></returns>
        public ArrayList RemoveDuplicatesFromStringAlgo3(ArrayList inputCharArray)
        {
            int tail = 0;
            for (int i = 0; i < inputCharArray.Count; i++ )
            {
                bool shouldDelete = false;
                for(int j=0; j<i;j++)
                {
                    if((char)inputCharArray[i] == (char)inputCharArray[j])
                    {
                        shouldDelete = true;
                        break;
                    }
                }
                if(!shouldDelete)
                {
                    inputCharArray[tail++] = inputCharArray[i]; 
                }
            }
            inputCharArray.RemoveRange(tail, inputCharArray.Count - tail);
            return inputCharArray;
        }

        public static void TestRemoveDuplicatesFromString()
        {
            RemoveDuplicates rd = new RemoveDuplicates();
            string inputStr = "jacob jacob should be a good boy";

            Console.WriteLine("The input string is : "+ inputStr);
            Console.WriteLine("The string w/o duplicates from Algo1 is: " + rd.RemoveDuplicatesFromStringAlgo1(inputStr));
            ArrayList al = new ArrayList();
            al.AddRange(inputStr.ToCharArray());
            Console.WriteLine("The string w/o duplicates from Algo2 is: " + new string (rd.RemoveDuplicatesFromStringAlgo2(al).ToArray(typeof(char)) as char[]));
            ArrayList al1 = new ArrayList();
            al1.AddRange(inputStr.ToCharArray());
            Console.WriteLine("The string w/o duplicates from Algo3 is: " + new string(rd.RemoveDuplicatesFromStringAlgo3(al1).ToArray(typeof(char)) as char[]));

        }
    }
}
