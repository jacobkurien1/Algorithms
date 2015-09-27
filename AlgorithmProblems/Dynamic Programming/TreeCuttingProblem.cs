using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    class TreeCuttingProblem
    {
        /// <summary>
        /// Get the cuts for the log to maximize the profits
        /// We need to take the dynamic programming approach to solve this problem.
        /// 
        /// MaxProfit(logLength) = max i -> 1 to loglength{price[i] + MaxProfit(logLength - i)}
        /// Here i is the length of the log after it is cut.
        /// 
        /// We also need another array to store the log lengths to which the log needs to be cut.
        /// So that we can backtrack and get information about how many cuts at different length to maximize profit
        /// 
        /// </summary>
        /// <param name="price"></param>
        /// <param name="logLength"></param>
        /// <returns></returns>
        private static List<int> GetTreeCuttingToMaximizeProfits(int[] price, int logLength)
        {

            int[] maxProfit = new int[price.Length];
            int[] cutsOnLog = new int[price.Length];

            for (int logLen = 1; logLen < price.Length && logLen <= logLength; logLen++)
            {
                for (int i = 1; i <= logLen; i++)
                {
                    if (maxProfit[logLen] < price[i] + maxProfit[logLen - i])
                    {
                        maxProfit[logLen] = price[i] + maxProfit[logLen - i];
                        cutsOnLog[logLen] = i;
                    }
                }
            }

            Console.WriteLine("The maximum profit from the cuts are {0}", maxProfit[logLength]);

            // Get the actual lengths of the tree that needs to be cut to maximize the profits
            int iIndex = logLength;
            List<int> cutsToMaxProfits = new List<int>();
            while(iIndex>0)
            {
                cutsToMaxProfits.Add(cutsOnLog[iIndex]);
                iIndex -= cutsOnLog[iIndex];
            }
            return cutsToMaxProfits;
        }

        public static void TestGetTreeCuttingToMaximizeProfits()
        {
            int[] prices = new int[9] { 0, 1, 5, 8, 9, 10, 17, 17, 20 };
            ArrayHelper.PrintArray(prices);
            List<int> cutsToMaxProfits = GetTreeCuttingToMaximizeProfits(prices, 4);
            foreach(int cut in cutsToMaxProfits)
            {
                Console.Write(cut + " ");
            }
            Console.WriteLine();

            cutsToMaxProfits = GetTreeCuttingToMaximizeProfits(prices, 7);
            foreach (int cut in cutsToMaxProfits)
            {
                Console.Write(cut + " ");
            }
            Console.WriteLine();

            cutsToMaxProfits = GetTreeCuttingToMaximizeProfits(prices, 8);
            foreach (int cut in cutsToMaxProfits)
            {
                Console.Write(cut + " ");
            }
            Console.WriteLine();
        }
    }
}
