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
        /// This is the same recursive algo same as above but here we start from the index originalStr.Length-1
        /// Hence we will not have to send the perms as parameters
        /// </summary>
        /// <param name="originalStr"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        public List<string> GetAllPermutationsFromEndIndex(string originalStr, int currentIndex)
        {
            if (currentIndex < 0)
            {
                //base condition to stop the recursion
                return new List<string>() { string.Empty };
            }
            List<string> ret = new List<string>();
            List<string> allPerms = GetAllPermutationsFromEndIndex(originalStr, currentIndex-1);
            foreach (string str in allPerms)
            {
                ret.AddRange(GetPermutationsForTheCharater(originalStr[currentIndex].ToString(), str));
            }
            return ret;
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
            Console.WriteLine("The different permutations of {0} are as shown below", originalStr);
            PrintPermutations(allPerms.GetAllPermutations(originalStr, 0));
            Console.WriteLine("The different permutations are as shown below");
            PrintPermutations(allPerms.GetAllPermutationsFromEndIndex(originalStr, originalStr.Length-1));

            originalStr = "gak";
            Console.WriteLine("The different permutations of {0} are as shown below", originalStr);
            PrintPermutations(allPerms.GetAllPermutations(originalStr, 0));
            Console.WriteLine("The different permutations are as shown below");
            PrintPermutations(allPerms.GetAllPermutationsFromEndIndex(originalStr, originalStr.Length - 1));
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
