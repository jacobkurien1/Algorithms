using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.PermutationAndCombination
{
    /// <summary>
    /// Given a binary string of 1's and 0's and "?"
    /// ? means that it can have 1 or 0.
    /// Create all possible combinations of the binary string
    /// </summary>
    public class CombinationsOfBinaryString
    {
        /// <summary>
        /// This is the recursive subroutine which gets all the different combinations of the binary string
        /// </summary>
        /// <param name="allStrings">all string combination till the currentIndex -1 </param>
        /// <param name="currentIndex">the string element at currentIndex is taken into consideration</param>
        /// <param name="originalStr">the binary string whose different combinations need to be evaluated</param>
        /// <returns></returns>
        private List<string> GetAllCombinations(List<string> allStrings, int currentIndex, string originalStr)
        {
            if(allStrings == null)
            {
                allStrings = new List<string> { "" };
            }
            if(currentIndex == originalStr.Length)
            {
                return allStrings;
            }
            if(originalStr[currentIndex] == '?')
            {
                List<string> ret = new List<string>();
                foreach(string sb in allStrings)
                {
                    ret.Add(sb+"1");
                    ret.Add(sb+"0");
                }
                return GetAllCombinations(ret, ++currentIndex, originalStr);
            }
            else
            {
                //originalStr[currentIndex] == 0 or 1, assuming the originalStr is wellformed
                for(int i = 0; i<allStrings.Count; i++)
                {
                    allStrings[i] += (originalStr[currentIndex]);
                }
                return GetAllCombinations(allStrings, ++currentIndex, originalStr);
            }
        }

        public List<string> GetAllCombinationsOfBinaryString(string originalStr)
        {
            return GetAllCombinations(null, 0, originalStr);
        }

        public static void TestCombinationsOfBinaryString()
        {
            CombinationsOfBinaryString combinations = new CombinationsOfBinaryString();

            string originalStr = "1010?1010";
            Console.WriteLine("The different combinations of {0} are as shown below", originalStr);
            PrintCombinations(combinations.GetAllCombinationsOfBinaryString(originalStr));

            originalStr = "??0";
            Console.WriteLine("The different combinations of {0} are as shown below", originalStr);
            PrintCombinations(combinations.GetAllCombinationsOfBinaryString(originalStr));

            originalStr = "1?";
            Console.WriteLine("The different combinations of {0} are as shown below", originalStr);
            PrintCombinations(combinations.GetAllCombinationsOfBinaryString(originalStr));

        }

        private static void PrintCombinations(List<string> combinations)
        {
            foreach(string combination in combinations)
            {
                Console.Write("{0} ", combination);
            }
            Console.WriteLine();
        }
    }
}
