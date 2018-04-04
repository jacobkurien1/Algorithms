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
        /// The current combination will be saved in set and once its length reaches arr.Length/2
        /// we will check the difference between sum of elements in the currentSet and the sum of elements in
        /// the set compliment to currentSet.
        /// If this difference is less than MinDifference we will replace MinDifference with the currentDifference
        /// and also replace the OptimizedSet with the currentSet.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="currentIndex"></param>
        /// <param name="currentSet"></param>
        private void MinimizeTheSumOfEqualLengthSets(int[] arr, int currentIndex, List<int> set, int setSum)
        {
            if(set.Count == (arr.Length+1)/2)
            {
                int currentDifference = Math.Abs(Math.Abs(TotalArraySum - setSum) - setSum);
                if(currentDifference < MinDifference)
                {
                    OptimizedSet = new List<int>(set);
                    MinDifference = currentDifference;
                }
                return;
            }
            if (currentIndex >= arr.Length)
            {
                return;
            }
            set.Add(currentIndex);
            MinimizeTheSumOfEqualLengthSets(arr, currentIndex + 1, set, setSum+arr[currentIndex]);// with arr[currentIndex]
            set.RemoveAt(set.Count - 1); // backtrack
            MinimizeTheSumOfEqualLengthSets(arr, currentIndex + 1, set, setSum); // without arr[currentIndex]

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
            MinimizeTheSumOfEqualLengthSets(arr, 0, new List<int>(), 0);
            List<int> set2 = new List<int>();
            List<int> set1 = new List<int>();
            HashSet<int> hashset = new HashSet<int>();
            foreach(int index in OptimizedSet)
            {
                hashset.Add(index);
            }
            // divide all the elements into set1 and set2
            for (int i = 0; i < arr.Length; i++)
            {
                if(hashset.Contains(i))
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
}
