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
        /// 
        /// The running time of this approach is O(2^n)
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

        #region Algo2 - Dynamic Programming approach

        /// <summary>
        /// We can solve the above problem using the Dynamic Programming approach
        /// This is the main subroutine which calls the GetPartitionWithAParticularSum which inturn does the DP approach
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public List<List<int>> GetPartitionAlgo2(int[] arr)
        {
            List<List<int>> ret = new List<List<int>>();

            // Get the sum of all the elements in the array
            int totalSum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                totalSum += arr[i];
            }
            // We can only divide the set into 2 partitions if the total sum of the arr is even
            if (totalSum % 2 == 0)
            {
                // Get all the indicies in the dictionary
                Dictionary<int, bool> dict = new Dictionary<int, bool>();
                foreach (int index in GetPartitionWithAParticularSum(arr, totalSum/2))
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

            return ret;
        }

        /// <summary>
        /// This is the dynamic programming subroutine where we send the input array and the sum
        /// and the method should return a List of indices which represents a set of elements with sum = the parameter sum
        /// 
        /// The dynamic programming formulae is as shown below
        /// M[i,j] = ->if( i-arr[j]<0) {(M[i,j-1].row == i)? M[i,j-1]:new Cell(i,j-1)}
        ///          ->if(i-arr[j]==0) {new Cell(0,0)}// We have got the partition
        ///          ->if(i-arr[j]>0)  {new Cell(i-arr[j], j-1)}
        /// 
        /// Here M[i,j] means in the arr[0,j] can the sum i be achieved
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public List<int> GetPartitionWithAParticularSum(int[] arr, int sum)
        {
            Cell[,] backTrack = new Cell[sum + 1, arr.Length];

            // lets fill the backTrack matrix in a bottoms up manner
            for(int i = 0; i<=sum; i++)
            {
                for(int j=0; j < arr.Length; j++)
                {
                    if (i - arr[j] < 0 )
                    {
                        if (j - 1 >= 0)
                        {
                            if (backTrack[i, j - 1] != null && backTrack[i, j - 1].Row == i)
                            {
                                // get the value at the left of the matrix cell.
                                // This is done for the optimization so that we can jump all the columns whose arr[j] is not present in the subset
                                backTrack[i, j] = backTrack[i, j - 1];
                            }
                            else
                            {
                                // store the cell object which points to the left of the matrix
                                backTrack[i, j] = new Cell(i, j - 1);
                            }
                        }
                    }
                    else if (i - arr[j] == 0)
                    {
                        // We have found the partition
                        backTrack[i, j] = new Cell(0, 0);
                    }
                    else
                    {
                        if(j-1>=0)
                        {
                            // our partition will contain the arr[j] element
                            backTrack[i, j] = new Cell(i - arr[j], j - 1);
                        }
                    }
                }
            }

            // Now lets backtrack to get the partition Indices
            Cell currentCell = backTrack[sum, arr.Length - 1];
            int rowIndex = sum;
            int colIndex = arr.Length - 1;
            List<int> partitionIndexList = new List<int>();
            while (currentCell != null && !(rowIndex == 0 && colIndex == 0))
            {
                if (currentCell.Row != rowIndex)
                {
                    //We need to include this element at the arr[j]
                    // since we are including only the index it is sufficient to add j
                    partitionIndexList.Add(colIndex);
                }
                rowIndex = currentCell.Row;
                colIndex = currentCell.Col;
                currentCell = backTrack[rowIndex, colIndex];

            }
            return partitionIndexList;

        }

        public class Cell
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public Cell(int row, int col)
            {
                Row = row;
                Col = col;
            }
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
            Console.WriteLine("The output from the Dynamic Programming approach is as follows:");
            foreach (List<int> set in ds.GetPartitionAlgo2(arr))
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
            Console.WriteLine("The output from the Dynamic Programming approach is as follows:");
            foreach (List<int> set in ds.GetPartitionAlgo2(arr))
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
            Console.WriteLine("The output from the Dynamic Programming approach is as follows:");
            foreach (List<int> set in ds.GetPartitionAlgo2(arr))
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
            Console.WriteLine("The output from the Dynamic Programming approach is as follows:");
            foreach (List<int> set in ds.GetPartitionAlgo2(arr))
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
            Console.WriteLine("The output from the Dynamic Programming approach is as follows:");
            foreach (List<int> set in ds.GetPartitionAlgo2(arr))
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
