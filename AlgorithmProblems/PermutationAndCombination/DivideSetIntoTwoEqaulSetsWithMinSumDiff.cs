using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.PermutationAndCombination
{
    /// <summary>
    /// Given an array, divide it into 2 arrays of equal size such that the difference of the 
    /// sum of all elements in both the subset is minimized
    /// </summary>
    class DivideSetIntoTwoEqaulSetsWithMinSumDiff
    {
        public List<int> OptimizedSet { get; set; }
        private int MinDifference { get; set; }

        /// <summary>
        /// This is a recursive subroutine which will be calculating all possible combinations
        /// of the elements present in arr.
        /// The current combination will be saved in currentSet and once its length reaches arr.Length/2
        /// we will check the difference between sum of elements in the currentSet and the sum of elements in
        /// the set compliment to currentSet.
        /// If this difference is less than MinDifference we will replace MinDifference with the currentDifference
        /// and also replace the OptimizedSet with the currentSet.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="currentIndex"></param>
        /// <param name="currentSet"></param>
        public void MinimizeTheSumOfEqualLengthSets(int[] arr, int currentIndex, List<int> currentSet)
        {
            if(currentSet.Count == arr.Length/2)
            {
                int currentDifference = GetTheDifferenceOfSumOfSets(arr, currentSet);
                if(currentDifference<MinDifference)
                {
                    OptimizedSet = currentSet;
                }
            }

        }

        /// <summary>
        /// We can get the difference between sum of elements in currentSet and 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="currentSet"></param>
        /// <returns></returns>
        private int GetTheDifferenceOfSumOfSets(int[] arr, List<int> currentSet)
        {
            
        }

        public static void TestDivideSetIntoTwoEqaulSetsWithMinSumDiff()
        {

        }
    }
}
