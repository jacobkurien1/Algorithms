using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Misc
{
    /// <summary>
    /// 
    /// </summary>
    class MinNumOfCoins
    {
        public int Amount { get; set; }
        public int[] Denominations { get; set; }
        public Dictionary<int,int> CoinsPerDenomination { get; set; }
        MinNumOfCoins(int Amt, int[] denominations)
        {
            // Sort the denominations array in descending order
            Array.Sort(denominations,new Comparison<int>(
                (i1,i2)=>i2.CompareTo(i1)
                ));
            Amount = Amt;
            Denominations = denominations;
            CoinsPerDenomination = new Dictionary<int, int>();
            FindCoinsPerDenomination();
        }

        private void FindCoinsPerDenomination()
        {
            int amt = Amount;
            foreach(int denomination in Denominations)
            {
                int quotient = amt / denomination;
                int reminder = amt % denomination;
                CoinsPerDenomination.Add(denomination, quotient);
                amt = reminder;
            }
            if(amt!=0)
            {
                throw new Exception("The amount cannot be fully covered by the denominations we have");
            }
        }
        public static void TestMinNumOfCoins()
        {
            Console.WriteLine("Test the minimum number of coins needed for an Amount");
            TestDifferentDenomination(51, new int[] { 5, 2, 1 });
            TestDifferentDenomination(51, new int[] { 21 });
            TestDifferentDenomination(102, new int[] { 5, 3, 1 });
            TestDifferentDenomination(51, new int[] { 5 });
            
        }

        private static void TestDifferentDenomination(int amt, int[] denomination)
        {
            MinNumOfCoins minCoins = null;
            try
            {
                minCoins = new MinNumOfCoins(amt, denomination);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            if (minCoins != null)
            {
                PrintCoinsPerDenomination(minCoins.CoinsPerDenomination);
            }
        }

        private static void PrintCoinsPerDenomination(Dictionary<int,int> coinsPerDenomination)
        {
            foreach (KeyValuePair<int, int> entry in coinsPerDenomination)
            {
                Console.WriteLine("The denominations{0} has frequency of {1}", entry.Key, entry.Value);
            }
        }
    }
}
