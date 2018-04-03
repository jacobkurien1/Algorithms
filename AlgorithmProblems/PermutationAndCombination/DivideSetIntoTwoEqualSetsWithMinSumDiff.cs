using AlgorithmProblems.Arrays.ArraysHelper;
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
    class DivideSetIntoTwoEqualSetsWithMinSumDiff
    {
        public List<int> OptimizedSet { get; set; }
        private int MinDifference = int.MaxValue;
        private int TotalArraySum = 0;

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
        private void MinimizeTheSumOfEqualLengthSets(int[] arr, int currentIndex, SetWithSum set)
        {
            
            if(set.Count == (arr.Length+1)/2)
            {
                int currentDifference = GetTheDifferenceOfSumOfSets(set);
                if(currentDifference < MinDifference)
                {
                    OptimizedSet = set.currentSet;
                    MinDifference = currentDifference;
                }
                return;
            }
            if (currentIndex >= arr.Length)
            {
                return;
            }
            SetWithSum clone1 = new SetWithSum(set);
            SetWithSum clone2 = new SetWithSum(set);
            clone2.AddElement(arr[currentIndex], currentIndex);
            MinimizeTheSumOfEqualLengthSets(arr, currentIndex + 1, clone1);
            MinimizeTheSumOfEqualLengthSets(arr, currentIndex + 1, clone2);

        }

        /// <summary>
        /// The {sum of elements in set1} - {sum of elements in set2}
        /// = {total sum of all elements} - 2*{sum of elements in set1}
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        private int GetTheDifferenceOfSumOfSets(SetWithSum set)
        {
            return Math.Abs((TotalArraySum - set.currentSum)- set.currentSum);
        }

        /// <summary>
        /// This is the main subroutine which calls the recursive subroutine MinimizeTheSumOfEqualLengthSets
        /// This problem has an optimized solution via Dynamic Programming approach.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public List<List<int>> GetTheEqualLengthSetsWithMinDifference(int[] arr)
        {

            // Get the total sum of all the elements in arr
            for (int i = 0; i < arr.Length; i++)
            {
                TotalArraySum += arr[i];
            }
            MinimizeTheSumOfEqualLengthSets(arr, 0, new SetWithSum());
            List<int> set2 = new List<int>();
            List<int> set1 = new List<int>();
            Dictionary<int, bool> dict = new Dictionary<int, bool>();
            foreach(int index in OptimizedSet)
            {
                dict[index] = true;
            }
            // divide all the elements into set1 and set2
            for (int i = 0; i < arr.Length; i++)
            {
                if(dict.ContainsKey(i))
                {
                    set1.Add(arr[i]);
                }
                else
                {
                    set2.Add(arr[i]);
                }
            }
            List<List<int>> retVal = new List<List<int>>();
            retVal.Add(set1);
            retVal.Add(set2);
            return retVal;
        }

        public static void TestDivideSetIntoTwoEqualSetsWithMinSumDiff()
        {
            int[] arr = new int[] { 1, 2, 1, 2, 1, 2, 1, 2 };
            Console.WriteLine("The intial array is as shown below");
            ArrayHelper.PrintArray(arr);
            DivideSetIntoTwoEqualSetsWithMinSumDiff ds = new DivideSetIntoTwoEqualSetsWithMinSumDiff();
            foreach(List<int> set in ds.GetTheEqualLengthSetsWithMinDifference(arr))
            {
                Console.WriteLine("One of the set is as shown below:");
                PrintTheSet(set);
            }
            Console.WriteLine("The difference between the 2 sets is {0}", ds.MinDifference);

            arr = new int[] { 1,2,3,4,5,6,7,8};
            Console.WriteLine("The intial array is as shown below");
            ArrayHelper.PrintArray(arr);
            ds = new DivideSetIntoTwoEqualSetsWithMinSumDiff();
            foreach (List<int> set in ds.GetTheEqualLengthSetsWithMinDifference(arr))
            {
                Console.WriteLine("One of the set is as shown below:");
                PrintTheSet(set);
            }
            Console.WriteLine("The difference between the 2 sets is {0}", ds.MinDifference);

            arr = new int[] { 1,2,1 };
            Console.WriteLine("The intial array is as shown below");
            ArrayHelper.PrintArray(arr);
            ds = new DivideSetIntoTwoEqualSetsWithMinSumDiff();
            foreach (List<int> set in ds.GetTheEqualLengthSetsWithMinDifference(arr))
            {
                Console.WriteLine("One of the set is as shown below:");
                PrintTheSet(set);
            }
            Console.WriteLine("The difference between the 2 sets is {0}", ds.MinDifference);

            arr = new int[] { 1, 2, 1,2,1,1,1};
            Console.WriteLine("The intial array is as shown below");
            ArrayHelper.PrintArray(arr);
            ds = new DivideSetIntoTwoEqualSetsWithMinSumDiff();
            foreach (List<int> set in ds.GetTheEqualLengthSetsWithMinDifference(arr))
            {
                Console.WriteLine("One of the set is as shown below:");
                PrintTheSet(set);
            }
            Console.WriteLine("The difference between the 2 sets is {0}", ds.MinDifference);
        }
        private static void PrintTheSet(List<int> listOfInt)
        {
            foreach(int i in listOfInt)
            {
                Console.Write("{0} , ", i);
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// This is a data structure which will store a list of index to the array
    /// And also store the total sum of the set formed by the list of indices
    /// </summary>
    public class SetWithSum
    {
        /// <summary>
        /// This will contain the set of indices
        /// </summary>
        public List<int> currentSet { get; set; }

        /// <summary>
        /// This will contain all the sum of numbers in all the indices present in currentSet
        /// </summary>
        public int currentSum { get; set; }

        public SetWithSum()
        {
            currentSet = new List<int>();
            currentSum = 0;
        }

        /// <summary>
        /// Returns the count of all the indices in the currentSet
        /// </summary>
        public int Count
        {
            get
            {
                return (currentSet != null) ? currentSet.Count : 0;
            }
        }

        public SetWithSum(SetWithSum newSet)
        {
            // clone the list here
            currentSet = new List<int>(newSet.currentSet);
            currentSum = newSet.currentSum;
        }

        /// <summary>
        /// Adds the index to the currentSet and adds value to the currentSum
        /// </summary>
        /// <param name="valToAdd">value which needs to be added to the currentSum</param>
        /// <param name="currentIndex">index which needs to be added to the currentSet</param>
        public void AddElement(int valToAdd, int currentIndex)
        {
            currentSet.Add(currentIndex);
            currentSum += valToAdd;
        }
    }
}
