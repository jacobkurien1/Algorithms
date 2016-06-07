using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// Given an array, partition it into 2 sets(could be of different size)
    /// such that the sum of the 2 sets is equal.
    /// If such a partion does not exist, return false.
    /// </summary>
    public class PartitionArrayIntoEqualSumSets
    {
        #region Algo1 - Recursive method

        /// <summary>
        /// We can get the Partition using recursion.
        /// This is the main main method which calls the recursive subroutine
        /// Steps
        /// 1. Get the sum of all the elements in the arr
        /// if the sum is even then only we can divide the arr into 2 sets of equal sum
        /// 2. We need to calculate the partition which sums upto sum/2
        /// 3. Call the GetPartitionRecursive and save the resulting partition indices to setOfIndicesCopy
        /// 4. Return the partions back.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public List<List<int>> GetPartitionAlgo1(int[] arr)
        {
            List<List<int>> ret = new List<List<int>>();

            // Get the sum of all the elements in the array
            int totalSum = 0;
            for(int i=0; i<arr.Length; i++)
            {
                totalSum += arr[i];
            }
            // We can only divide the set into 2 partitions if the total sum of the arr is even
            if (totalSum % 2 == 0)
            {
                if (GetPartitionRecursive(arr, 0, totalSum / 2, new List<int>()))
                {
                    // Get all the indicies in the dictionary
                    Dictionary<int, bool> dict = new Dictionary<int, bool>();
                    foreach (int index in setOfIndicesCopy)
                    {
                        dict[index] = true;
                    }

                    //Now partition the array into 2 sets
                    List<int> set1 = new List<int>();
                    List<int> set2 = new List<int>();
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (dict.ContainsKey(i))
                        {
                            set1.Add(arr[i]);
                        }
                        else
                        {
                            set2.Add(arr[i]);
                        }
                    }
                    ret.Add(set1);
                    ret.Add(set2);
                }
            }
            return ret;
        }

        /// <summary>
        /// Saves the copy of the partition(of indices) which sums to totalSum/2
        /// </summary>
        private List<int> setOfIndicesCopy;

        /// <summary>
        /// This is the recursive subroutine which gets the partition which sums upto mainSum
        /// </summary>
        /// <param name="arr">array of integers whose partition needs to be calculated</param>
        /// <param name="index">current index of arr under consideration</param>
        /// <param name="mainSum">Get the partition which sums upto mainSum</param>
        /// <param name="setOfIndices">set of indices already traversed</param>
        /// <returns>true if the Partition where the sum = totalSum/2 is found and false otherwise</returns>
        public bool GetPartitionRecursive(int[] arr, int index, int mainSum, List<int> setOfIndices)
        {
            if(index == arr.Length || mainSum<0)
            {
                // We have reached the end of the array
                // or we the mainSum has become negative and hence going forward would not be fruitful
                return false;
            }
            if(mainSum == 0)
            {
                // We have found a partition where the sum of elements at indices in setOfIndices is equal to the
                // sum of elements in the complimentary set
                setOfIndicesCopy = setOfIndices;
                return true;
            }
            List<int> clone = new List<int>(setOfIndices);
            clone.Add(index);
            return GetPartitionRecursive(arr, index + 1, mainSum, setOfIndices) ||
                GetPartitionRecursive(arr, index + 1, mainSum - arr[index], clone);
        }
        #endregion

        public static void TestPartitionArrayIntoEqualSumSets()
        {
            int[] arr = new int[] { 1, 2, 1, 2, 1, 2, 1, 2 };
            Console.WriteLine("The intial array is as shown below");
            ArrayHelper.PrintArray(arr);
            PartitionArrayIntoEqualSumSets ds = new PartitionArrayIntoEqualSumSets();
            foreach (List<int> set in ds.GetPartitionAlgo1(arr))
            {
                Console.WriteLine("One of the set is as shown below:");
                PrintTheSet(set);
            }

            arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Console.WriteLine("The intial array is as shown below");
            ArrayHelper.PrintArray(arr);
            ds = new PartitionArrayIntoEqualSumSets();
            foreach (List<int> set in ds.GetPartitionAlgo1(arr))
            {
                Console.WriteLine("One of the set is as shown below:");
                PrintTheSet(set);
            }

            arr = new int[] { 1, 2, 1 };
            Console.WriteLine("The intial array is as shown below");
            ArrayHelper.PrintArray(arr);
            ds = new PartitionArrayIntoEqualSumSets();
            foreach (List<int> set in ds.GetPartitionAlgo1(arr))
            {
                Console.WriteLine("One of the set is as shown below:");
                PrintTheSet(set);
            }

            arr = new int[] { 1, 2, 1, 2, 1, 1, 1 };
            Console.WriteLine("The intial array is as shown below");
            ArrayHelper.PrintArray(arr);
            ds = new PartitionArrayIntoEqualSumSets();
            foreach (List<int> set in ds.GetPartitionAlgo1(arr))
            {
                Console.WriteLine("One of the set is as shown below:");
                PrintTheSet(set);
            }

            arr = new int[] {10,1,1,1,1,1,5};
            Console.WriteLine("The intial array is as shown below");
            ArrayHelper.PrintArray(arr);
            ds = new PartitionArrayIntoEqualSumSets();
            foreach (List<int> set in ds.GetPartitionAlgo1(arr))
            {
                Console.WriteLine("One of the set is as shown below:");
                PrintTheSet(set);
            }
        }

        private static void PrintTheSet(List<int> listOfInt)
        {
            foreach (int i in listOfInt)
            {
                Console.Write("{0} , ", i);
            }
            Console.WriteLine();
        }
    }
}
