using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.PermutationAndCombination
{

    public class AllPermutationsOfString
    {
        /// <summary>
        /// Here we are going from index ==0 to the end
        /// </summary>
        /// <param name="originalStr">original string whose permutations needs to be calculated</param>
        /// <param name="currentIndex">index of originalStr under consideration</param>
        /// <param name="allPerms">allPermutations till currentIndex-1</param>
        /// <returns></returns>
        public List<string> GetAllPermutations(string originalStr, int currentIndex, List<string> allPerms = null)
        {
            if(allPerms == null)
            {
                allPerms = new List<string>() { string.Empty };
            }
            if (currentIndex == originalStr.Length)
            {
                //base condition to stop the recursion
                return allPerms;
            }
            List<string> ret = new List<string>();
            foreach (string str in allPerms)
            {
                ret.AddRange(GetPermutationsForTheCharater(originalStr[currentIndex].ToString(), str));
            }
            return GetAllPermutations(originalStr, ++currentIndex, ret);
        }

        /// <summary>
        /// inserts the singleCharater into each index og the completeString
        /// </summary>
        /// <param name="singleCharacter">the character that needs to be inserted</param>
        /// <param name="completeString">the complete string in which the single character needs to be inserted</param>
        /// <returns></returns>
        private List<string> GetPermutationsForTheCharater(string singleCharacter, string completeString)
        {
            List<string> ret = new List<string>();
            ret.Add(singleCharacter + completeString);
            for(int i = 1; i<completeString.Length; i++)
            {
                ret.Add(completeString.Substring(0, i) + singleCharacter + completeString.Substring(i, completeString.Length - i));
            }
            if (completeString.Length != 0)
            {
                ret.Add(completeString + singleCharacter);
            }
            return ret;
        }
        public static void TestAllPermutationsOfString()
        {
            AllPermutationsOfString allPerms = new AllPermutationsOfString();
            string originalStr = "jacob";
            Console.WriteLine("The different combinations of {0} are as shown below", originalStr);
            PrintPermutations(allPerms.GetAllPermutations(originalStr, 0));

            originalStr = "gak";
            Console.WriteLine("The different combinations of {0} are as shown below", originalStr);
            PrintPermutations(allPerms.GetAllPermutations(originalStr, 0));
        }
        private static void PrintPermutations(List<string> combinations)
        {
            foreach (string combination in combinations)
            {
                Console.Write("{0} ", combination);
            }
            Console.WriteLine();
        }
    }
}
