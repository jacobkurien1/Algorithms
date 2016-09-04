using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// You are given a set of coin change denomination {1,2,3,5} you need to find
    /// all the different ways you can create finalValue by using different combinations of the given 
    /// denominations. note: the order of the coin change does not matter
    /// Also you can assume that the number of coins in each denomination is unlimited.
    /// </summary>
    class DifferentWaysForCoinChange
    {
        #region Alog1: recursive algorithm
        /// <summary>
        /// we can solve this easily using the recursive algorithm
        /// but note this is not an optimum algorithm as we will be solving the 
        /// same subproblem again and again.
        /// 
        /// The running time here is O(2^n):
        /// T(n, m) = c+ T(n-1, m) + T(n, m-1)
        /// 
        /// </summary>
        /// <param name="denomiations">all the different denominations that could be used</param>
        /// <param name="numOfCoinsOfDifferentDenominations">this array is used to track the number of coins used in each denomination. this will help us to print the different combinations</param>
        /// <param name="finalValue">this is the value for which we need to get the change</param>
        /// <param name="currentIndex">this is a index for the denominations array and help us track which is the current denomination under consideration</param>
        /// <returns></returns>
        public int NumberOfDifferntWaysForCoinChange(int[] denomiations, int[] numOfCoinsOfDifferentDenominations, int finalValue, int currentIndex)
        {
            if (finalValue == 0)
            {
                // this means we have a combination of denominations which gives the correct change for the value
                // hence we need to print the number of coins from each denomination which is used
                PrintCoinsFromEachDenomination(denomiations, numOfCoinsOfDifferentDenominations);
                return 1;
            }
            if (finalValue < 0 || currentIndex < 0)
            {
                // there is no combination here.
                return 0;
            }
            // we will recursively reach the correct combination of change
            int[] newNumOfCoinsOfDifferentDenominations = (int[])numOfCoinsOfDifferentDenominations.Clone();
            int countWithoutCurrentIndex = NumberOfDifferntWaysForCoinChange(denomiations, newNumOfCoinsOfDifferentDenominations, finalValue, currentIndex - 1);
            newNumOfCoinsOfDifferentDenominations[currentIndex]++;
            int countWithCurrentIndex = NumberOfDifferntWaysForCoinChange(denomiations, newNumOfCoinsOfDifferentDenominations, finalValue - denomiations[currentIndex], currentIndex);
            return countWithCurrentIndex + countWithoutCurrentIndex;
        }


        #endregion

        #region common methods
        private void PrintCoinsFromEachDenomination(int[] denomiations, int[] numOfCoinsOfDifferentDenominations)
        {
            for (int i = 0; i < denomiations.Length; i++)
            {
                Console.Write("({0} -> {1})", denomiations[i], numOfCoinsOfDifferentDenominations[i]);
            }
            Console.WriteLine();
        }
        #endregion

        #region Algo2: Dynamic Programming approach
        /// <summary>
        /// We can use the dynamic programming approach to solve this problem
        /// We will have a matrix(changeTable) which will keep track of the different ways in which change can be made.
        /// The changetable will be a matrix of size finalValue+1 * denominations.Length
        /// 
        /// Dynamic programming equation will be as shown below:
        /// changeTable[i,j] = changeTable[i-denomination[j], j] (when we take the denomination at indexj) + 
        ///                     changeTable[i,j-1] (when we dont consider the denomination at index j)
        /// 
        /// combinationsOfChange will keep track of all the different combinations of the change
        /// </summary>
        /// <param name="denominations"></param>
        /// <param name="finalValue"></param>
        /// <returns></returns>
        public int NumberOfDifferntWaysForCoinChangeAgo2(int[] denominations, int finalValue)
        {
            int[,] changeTable = new int[finalValue + 1, denominations.Length+1];
            List<int[]>[,] combinationsOfChange = new List<int[]>[finalValue + 1, denominations.Length + 1];
            // initialization
            for (int i = 0; i < finalValue + 1; i++)
            {
                for (int j = 0; j < denominations.Length + 1; j++)
                {
                    combinationsOfChange[i, j] = new List<int[]>();
                }
            }

            for (int j = 1; j < denominations.Length + 1; j++)
            {
                changeTable[0, j] = 1;
                combinationsOfChange[0, j].Add(new int[denominations.Length]);
            }

            for (int i = 1; i < finalValue+1; i++)
            {
                for (int j = 1; j < denominations.Length+1; j++)
                {
                    int residualVal = i - denominations[j-1];
                    int changeWithJthDenomination = 0;
                    int changeWithoutJthDenomination = 0;
                    if (residualVal >= 0 )
                    {
                        changeWithJthDenomination = changeTable[residualVal, j];
                        if(changeWithJthDenomination>0)
                        {
                            // we are going to create an array which shows that we have taken j-1th element in denominations array
                            int[] count = new int[denominations.Length];
                            count[j - 1] = 1;

                            //Now we are going to add the count array elementwise will all the arrays in the combinationsOfChange[residualVal, j]
                            foreach (int[] previous in combinationsOfChange[residualVal, j])
                            {
                                var summed = GetElementWiseSum(count, previous);
                                combinationsOfChange[i, j].Add(summed);
                            }
                        }
                    }
                    if(j - 1 >= 0)
                    {
                        changeWithoutJthDenomination = changeTable[i, j - 1];
                        if (changeWithoutJthDenomination > 0)
                        {
                            combinationsOfChange[i, j].AddRange(combinationsOfChange[i, j-1]);
                        }
                    }
                    changeTable[i, j] = changeWithJthDenomination + changeWithoutJthDenomination;
                }
            }
            // Print the coins of different denominations
            foreach (int[] singleCombination in combinationsOfChange[finalValue, denominations.Length])
            {
                PrintCoinsFromEachDenomination(denominations, singleCombination);
            }

            return changeTable[finalValue, denominations.Length];
        }

        private int[] GetElementWiseSum(int[] count, int[] previous)
        {
            int[] ret = new int[count.Length];
            if(count==null || previous == null || count.Length != previous.Length)
            {
                throw new ArgumentException();
            }
            for(int i=0; i<count.Length; i++)
            {
                ret[i] = previous[i] + count[i];
            }
            return ret;
        }

        #endregion

        #region Algo3: Dynamic Programming Space Efficient Approach
        /// <summary>
        /// In this approach we have a table array which is a list<string>
        /// Eg: table[5] contains a list<string> such that all the combinations from denominations[0,k] are present which sum upto 5
        /// k goes from 0 to denominations.Length.
        /// 
        /// Dynamic programming formulae is:
        /// table[i] = append denominations[denomIndex] to all elements of table[i-denominations[denomIndex]] when i-denominations[denomIndex]>=0
        /// 
        /// The running time here is O(finalValue*denominations.Length)
        /// The space requirement is O(finalValue*(2^denominations.Length))
        /// </summary>
        /// <param name="denominations"></param>
        /// <param name="finalValue"></param>
        /// <returns></returns>
        public int CoinChangeAlgo3(int[] denominations, int finalValue)
        {
            // initialization
            List<string>[] table = new List<string>[finalValue + 1];
            for(int i=0; i< table.Length; i++)
            {
                table[i] = new List<string>();
            }
            table[0].Add("");


            for (int denomIndex = 0; denomIndex < denominations.Length; denomIndex++)
            {
                for(int i = denominations[denomIndex]; i<=finalValue; i++)
                {
                    if(i - denominations[denomIndex]>= 0)
                    {
                        // Add to table[i] list after appending "denominations[denomIndex]" to all elements of list "table[i - denominations[denomIndex]]"
                        table[i].AddRange(AddDenominationToList(table[i - denominations[denomIndex]], denominations[denomIndex]));
                    }
                }
            }

            // Print all combinations
            PrintAllCombinations(table[finalValue]);

            // return the count of combinations
            return table[finalValue].Count;
        }

        /// <summary>
        /// We need to append a particular denomination to all the combination of denominations in the list
        /// </summary>
        /// <param name="list">all combination of denominations</param>
        /// <param name="denomination">denomination to add</param>
        /// <returns>list which has denomination appeneded to all elements of list</returns>
        private List<string> AddDenominationToList(List<string> list, int denomination)
        {
            // Make clone of the list else the original list will be manipulated cause list<string> is a reference type and not a value type
            List<string> ret = new List<string>(list);

            for (int i = 0; i < ret.Count; i++)
            {
                ret[i] += (denomination.ToString());
            }
            return ret;
        }

        /// <summary>
        /// Print all the different combination which will add up to finalValue
        /// </summary>
        /// <param name="combinations"></param>
        private void PrintAllCombinations(List<string> combinations)
        {
            Console.WriteLine("All the combinations of change is as follows:");
            foreach(string combination in combinations)
            {
                Console.WriteLine(combination);
            }
        }
        #endregion
        public static void TestDifferentWaysForCoinChange()
        {
            DifferentWaysForCoinChange coinChange = new DifferentWaysForCoinChange();
            int[] denominations = new int[] { 1, 2, 3 };
            int[] numOfCoinsOfDifferentDenominations = new int[3];
            int finalValue = 4;

            Console.WriteLine("The different ways in which coin change can be done");
            int totalNumOFDifferentWays = coinChange.NumberOfDifferntWaysForCoinChange(denominations, numOfCoinsOfDifferentDenominations, finalValue, denominations.Length - 1);
            Console.WriteLine("The total number of different ways in which change can be made is {0}", totalNumOFDifferentWays);
            Console.WriteLine("Ago2:The total number of different ways in which change can be made is {0}", coinChange.NumberOfDifferntWaysForCoinChangeAgo2(denominations, finalValue));
            Console.WriteLine("Ago3:The total number of different ways in which change can be made is {0}", coinChange.CoinChangeAlgo3(denominations, finalValue));

            denominations = new int[] { 2,5,3,6};
            numOfCoinsOfDifferentDenominations = new int[4];
            finalValue = 10;

            Console.WriteLine("The different ways in which coin change can be done");
            totalNumOFDifferentWays = coinChange.NumberOfDifferntWaysForCoinChange(denominations, numOfCoinsOfDifferentDenominations, finalValue, denominations.Length - 1);
            Console.WriteLine("The total number of different ways in which change can be made is {0}", totalNumOFDifferentWays);
            Console.WriteLine("Algo2:The total number of different ways in which change can be made is {0}", coinChange.NumberOfDifferntWaysForCoinChangeAgo2(denominations, finalValue));
            Console.WriteLine("Algo3:The total number of different ways in which change can be made is {0}", coinChange.CoinChangeAlgo3(denominations, finalValue));
        }
    }
}
