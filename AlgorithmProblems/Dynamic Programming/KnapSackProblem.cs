using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    class KnapSackProblem
    {
        /// <summary>
        /// We need to find the maximum value items that a robber can put in his knapsack
        /// With native algo the running time is O(2^n)
        /// We will use dynamic programming for solving this.
        /// 
        /// if price array is given as p1,p2, .. pn
        /// and weight array is given as w1,w2, .. , wn
        /// then to find knapsack(n, W) = max{pn + knapsack(n-1, W-wn), knapsack(n-1, W)}
        /// 
        /// We will also use a pointer table to keep track of the elements we are putting in the knap sack
        /// true in pointer table means this element is in knapsack
        /// false means it is not present
        /// 
        /// Note: the weight and price array start from 0 index
        /// the knapsack and pointertable matrix start from 1.
        /// </summary>
        /// <param name="weight">weights of the different items</param>
        /// <param name="price">price of the different items</param>
        /// <param name="WeightOfKnapsack">maximum weight the knapsack can hold</param>
        /// <returns>the items that need to carried in knapsack</returns>
        private static List<int> GetTheMaximumValueWithLimitedWeight(int[] weight, int[] price, int weightOfKnapsack)
        {
            int numOfItems = weight.Length+1;
            int[,] knapsack = new int[numOfItems, weightOfKnapsack+1];
            bool[,] pointerTable = new bool[numOfItems, weightOfKnapsack + 1];

            // initialization
            for(int i=0; i<numOfItems; i++)
            {
                knapsack[i, 0] = 0;
            }
            for (int j = 0; j < weightOfKnapsack+1; j++)
            {
                knapsack[0, j] = 0;
            }

            for(int row = 1; row < numOfItems; row++)
            {
                for(int col = 1; col <=weightOfKnapsack; col++)
                {
                    int priceIfItemIsKnapsacked = ((row > 0 && col - weight[row-1] >= 0) ? price[row-1] + knapsack[row-1, col - weight[row-1]] : 0);
                    if (knapsack[row, col] < priceIfItemIsKnapsacked)
                    {
                        knapsack[row, col] = priceIfItemIsKnapsacked;
                        pointerTable[row, col] = true;
                    }
                    if(row>0 && knapsack[row, col] < knapsack[row - 1, col])
                    {
                        knapsack[row, col] = knapsack[row - 1, col];
                        pointerTable[row, col] = false;
                    }
                }
            }

            // Get the items in knapsack and return it
            int iIndex = numOfItems - 1;
            int jIndex = weightOfKnapsack;
            List<int> itemsKnapsacked = new List<int>();

            while(iIndex >0 && jIndex>0)
            {
                if(pointerTable[iIndex,jIndex])
                {
                    itemsKnapsacked.Add(iIndex);
                    jIndex -= weight[iIndex-1];
                    iIndex--;
                }
                else
                {
                    iIndex--;
                }
            }

            return itemsKnapsacked;

        }

        public static void TestGetTheMaximumValueWithLimitedWeight()
        {
            int[] price = new int[4] { 2, 3, 4, 5 };
            int[] weight = new int[4] { 3, 4, 5, 6 };
            int maxWeight = 5;
            Console.WriteLine("after solving knapsack problem, we have the following item in the knapsack");
            List<int> items = GetTheMaximumValueWithLimitedWeight(weight, price, maxWeight);
            foreach(int item in items)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            price = new int[5] { 1, 6, 18, 22, 28 };
            weight = new int[5] { 1, 2, 5, 6, 7 };
            maxWeight = 11;
            Console.WriteLine("after solving knapsack problem, we have the following item in the knapsack");
            items = GetTheMaximumValueWithLimitedWeight(weight, price, maxWeight);
            foreach (int item in items)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
    }
}
