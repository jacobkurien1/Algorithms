using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.PermutationAndCombination
{
    /// <summary>
    /// Find the different combinations of character in the string
    /// Note: the order of the characters in the string does not matter
    /// </summary>
    public class AllCombinationsOfString
    {
        /// <summary>
        /// This subroutine is recursively called to get all the different combinations
        /// </summary>
        /// <param name="currentIndex">currentIndex of the originalStr which is under consideration for the current subroutine</param>
        /// <param name="originalStr">the string whose combinations need to be calculated</param>
        /// <param name="allCombinations">all the different combinations till the currentIndex-1</param>
        /// <returns></returns>
        public List<string> GetAllCombinations(int currentIndex, string originalStr, List<string> allCombinations = null)
        {
            if(allCombinations == null)
            {
                // intialization
                // we need to add the empty string to signify the null set
                allCombinations = new List<string> { string.Empty };
            }
            if(currentIndex==originalStr.Length)
            {
                // base condition to terminate recursion
                return allCombinations;
            }
            List<string> cloneList = CloneList(allCombinations);
            foreach(string str in cloneList)
            {
                allCombinations.Add(str + originalStr[currentIndex]);
            }
            return GetAllCombinations(++currentIndex, originalStr, allCombinations);
        }

        private List<string> CloneList(List<string> originalList)
        {
            List<string> cloneList = new List<string>();
            foreach(string listItem in originalList)
            {
                cloneList.Add(listItem);
            }
            return cloneList;
        }

        public static void TestAllCombinationsOfString()
        {
            AllCombinationsOfString allCombs = new AllCombinationsOfString();
            string originalStr = "jacob";
            Console.WriteLine("The different combinations of {0} are as shown below", originalStr);
            PrintCombinations(allCombs.GetAllCombinations(0,originalStr));
        }
        private static void PrintCombinations(List<string> combinations)
        {
            foreach (string combination in combinations)
            {
                Console.Write("{0} ", combination);
            }
            Console.WriteLine();
        }
    }
}
