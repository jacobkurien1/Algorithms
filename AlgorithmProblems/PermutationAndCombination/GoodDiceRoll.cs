﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.PermutationAndCombination
{
    /// <summary>
    /// Find the count of all combinations of a dice roll n times and get the count of combinations where sum%mod == product%mod.
    /// </summary>
    public class GoodDiceRoll
    {
        /// <summary>
        /// Algo: 1. Get all the combinations of n dice rolls.
        /// 2. Filter the list by the combinations which satisfy sum%mod == product%mod
        /// 3. return the count
        /// 
        /// The running time is O(n^6) where the dice has 6 faces
        /// The space requirement is O(n^6)
        /// </summary>
        /// <param name="n">number of dice rolls</param>
        /// <param name="mod">modulus value</param>
        /// <returns></returns>
        public static int GetGoodDiceRoll(int n, int mod)
        {
            List<List<int>> allComb = new List<List<int>>();
            allComb.Add(new List<int>());
            for (int i = 0; i < n; i++)
            {
                var newComb = new List<List<int>>();
                for (int j = 1; j <= 6; j++)
                {
                    foreach (var comb in allComb)
                    {
                        var combClone = new List<int>(comb);
                        combClone.Add(j);
                        newComb.Add(combClone);

                    }
                }
                allComb = newComb;
            }
            return allComb.Where(p => sumMod(p, mod) == prodMod(p, mod)).ToList().Count;

        }

        /// <summary>
        /// Get the sum of all elements in the list and then get modulus without overflow
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        private static int sumMod(List<int> arr, int mod)
        {
            int sum = 0;
            foreach (int i in arr)
            {
                sum += i;
                sum %= mod;
            }
            return sum;
        }

        /// <summary>
        /// Get the product of all elements in the list and then get modulus without overflow
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        private static int prodMod(List<int> arr, int mod)
        {
            int prod = 1;
            foreach (int i in arr)
            {
                prod *= i;
                prod %= mod;
            }
            return prod;
        }

        /// <summary>
        /// Test method
        /// </summary>
        public static void TestGoodDiceRoll()
        {
            int n = 2;
            int mod = 5;
            Console.WriteLine("The good dice for {2} rolls with mod value as {3} are {0}. Expected: {1}",
                GetGoodDiceRoll(2, 5),
                4,
                n,
                mod);
        }
    }
}