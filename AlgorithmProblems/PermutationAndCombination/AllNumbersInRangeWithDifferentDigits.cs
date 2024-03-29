﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.PermutationAndCombination
{
    /// <summary>
    /// Find all the numbers from 0 to 1000000 where none of the numbers have the same set of digits in them
    /// 
    /// Hint: if you look closely at the wording of the problem you will realize that this is a problem to get 
    /// all combinations from 0-999999 (where the digits are different from each other)
    /// </summary>
    public class AllNumbersInRangeWithDifferentDigits
    {

        #region Recursive approach
        /// <summary>
        /// This list will be used to store all the different combinations with different digits
        /// </summary>
        public List<long> AllCombinations { get; set; }
        public AllNumbersInRangeWithDifferentDigits(int maxVal)
        {
            AllCombinations = new List<long>();
            GetAllCombinations(maxVal, 0);
        }

        /// <summary>
        /// We will recursively call this function to generate all the combinations
        /// start with 0 -> 1,2,3,4,..,9 
        /// and 1-> 11,12,13,..19
        /// 2 -> 22,23,24,..29
        /// 3 -> 33,34,35,..,39
        /// 
        /// The running time is 9^(log(base9){maxVal})
        /// you can calculate the same if you think of a 9-ary tree and the height of the tree is log(base9){maxVal}
        /// </summary>
        /// <param name="maxVal"></param>
        /// <param name="currentVal"></param>
        private void GetAllCombinations(long maxVal, long currentVal)
        {
            if (currentVal < maxVal)
            {
                AllCombinations.Add(currentVal);
                int currentValLastDigit = (int)(currentVal % 10);
                
                for (int i = currentValLastDigit; i <=9; i++)
                {
                    long newCurrentValue = currentVal * 10 + i;
                    if(newCurrentValue!=currentVal)
                    {
                        GetAllCombinations(maxVal, newCurrentValue);
                    }
                }
            }
        }
        #endregion

        #region Iterative approach
        /// <summary>
        /// We can do the same iteratively by using 2 lists.
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public List<long> GetAllNumsInRangeWithDifferentDigitsIter(int maxValue)
        {
            List<long> previous = new List<long>();
            List<long> current = new List<long>();
            bool isMaxReached = false;
            previous.Add(0);

            while(!isMaxReached)
            {
                foreach (long val in previous)
                {
                    int lastdigit = (int)val%10;
                    for (int i = lastdigit; i <= 9; i++) 
                    {
                        long newVal = val * 10 + i;
                        if(newVal>=maxValue)
                        {
                            isMaxReached = true;
                            break; // break all loops
                        }
                        current.Add(newVal);
                    }
                    if (isMaxReached)
                    {
                        break;  // break all loops
                    }
                }
                previous = current;
                current = new List<long>();
            }
            return previous;
        }

        #endregion

        public static void TestAllNumbersInRangeWithDifferentDigits()
        {
            AllNumbersInRangeWithDifferentDigits allNums = new AllNumbersInRangeWithDifferentDigits(100);
            PrintCombinations(allNums.AllCombinations);
            PrintCombinations(allNums.GetAllNumsInRangeWithDifferentDigitsIter(100));
        }

        private static void PrintCombinations(List<long> combinations)
        {
            Console.WriteLine("The total number of different combinations are {0}", combinations.Count);
            foreach (long combination in combinations)
            {
                Console.Write("{0} ", combination);
            }
            Console.WriteLine();
        }
    }
}
