using System;
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

        #region iterative solution - the sum and prod mod are calculated at end
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

        #endregion

        #region Iterative solution - the sum and prod mod are calculated while adding dice roll
        /// <summary>
        /// 1. Get all dice rolls iteratively while also keeping track of sum and prod mod
        /// 2. filter the elements having equal sum and prod mod values.
        /// The running time is O(6^n)
        /// The space requriement is O(6^n) 
        /// 
        /// This is not an optimal solution as space requirement is more here as we are tackling the problem as BFS
        /// </summary>
        /// <param name="mod">mod value so that we dont have overflow for sum and product</param>
        /// <param name="maxDiceRolls">max number of dice rolls that needs to be performed</param>
        /// <returns></returns>
        public static int GetCountOfGoodDiceRolls(int mod, int maxDiceRolls)
        {
            //1. initialization
            List<DiceRolls> previous = new List<DiceRolls>();
            List<DiceRolls> current = new List<DiceRolls>();
            previous.Add(new DiceRolls(mod));

            //2. get all the dice rolls
            for(int i =  1; i<=maxDiceRolls; i++)
            {
                foreach(DiceRolls dr in previous)
                {
                    for (int diceVal = 1; diceVal <= 6; diceVal++)
                    {
                        DiceRolls drClone = dr.Clone();
                        drClone.Add(diceVal);
                        current.Add(drClone);
                    }
                }
                previous = current;
                current = new List<DiceRolls>();
            }

            //3. Get all the good rolls
            return previous.Where(x => x.ProdMod == x.SumMod).Count();
        }

        /// <summary>
        /// Represents a list of dice rolls with mod of sum of all elements
        /// and mod of product of all elements
        /// </summary>
        class DiceRolls
        {
            private List<int> AllDiceRolls;
            private int Mod;
            public int SumMod { get; set; }
            public int ProdMod { get; set; }

            public DiceRolls(int mod)
            {
                AllDiceRolls = new List<int>();
                Mod = mod;
                ProdMod = 1;
            }

            public void Add(int diceVal)
            {
                AllDiceRolls.Add(diceVal);
                SumMod = ((diceVal % Mod) + SumMod) % Mod;
                ProdMod = ((diceVal % Mod) * ProdMod) % Mod;
            }

            public DiceRolls Clone()
            {
                DiceRolls clone = new DiceRolls(Mod);
                clone.AllDiceRolls = new List<int>(AllDiceRolls);
                clone.SumMod = SumMod;
                clone.ProdMod = ProdMod;
                return clone;
            }
        }

        #endregion

        #region Recursive solution - better space requirement 

        private static int CountOfGoodDiceRolls { get; set; }
        private static void GoodDiceRollsRecur(int mod, int maxRolls, List<int> roll)
        {
            if(roll.Count == maxRolls)
            {
                // check whether the sum mod and prod mod is same
                if(sumMod(roll, mod) == prodMod(roll, mod))
                {
                    CountOfGoodDiceRolls++;
                }
                return;
            }
            for(int dicethrow = 1; dicethrow<=6; dicethrow++)
            {
                roll.Add(dicethrow);
                GoodDiceRollsRecur(mod, maxRolls, roll);
                roll.RemoveAt(roll.Count - 1); // backtracking
            }
        }

        /// <summary>
        /// Main method which calls into the recursive subroutine
        /// The space requriement is the stack requriement for the recursive subroutine as only 1 set of rolls 
        /// is targetted at a time. The space requirement is O(maxRolls) as maxRolls is the height of the tree formed.
        /// 
        /// The time taken is same O(6^maxRolls)
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="maxRolls"></param>
        /// <returns></returns>
        public static int GetCountOfGoodDiceRollsRecur(int mod, int maxRolls)
        {
            GoodDiceRollsRecur(mod, maxRolls, new List<int>());
            return CountOfGoodDiceRolls;
        }

        #endregion

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
            Console.WriteLine("The good dice for {2} rolls with mod value as {3} are {0}. Expected: {1}",
                GetCountOfGoodDiceRolls(mod, n),
                4,
                n,
                mod);
            Console.WriteLine("The good dice for {2} rolls with mod value as {3} for recursive approach are {0}. Expected: {1}",
                GetCountOfGoodDiceRollsRecur(mod, n),
                4,
                n,
                mod);
        }
    }
}
