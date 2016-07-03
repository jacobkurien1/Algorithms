using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.RandomizedAlgo
{
    public class RandomGeneration
    {
        /// <summary>
        /// We can get a generator of a smaller number from a generator of a bigger number
        /// 
        /// For example:
        /// if We want to get rand 5 from rand 13. we can get rand 5 from rand 10 
        /// and whenever we get numbers from 10-12 we ignore it and do the operation again
        /// </summary>
        /// <param name="smaller"></param>
        /// <param name="larger"></param>
        /// <returns></returns>
        public static int RandomGenerationOfSmallerNumFromLargerNum(Random rand, int smaller, int larger)
        {
            if(smaller<=0|| larger<=0 || smaller>larger)
            {
                // Error condition
                throw new ArgumentException("smaller and larger parameter is not valid");
            }
            int factor = larger / smaller;
            int randLarger;
            do
            {
                randLarger = rand.Next(larger);

            } while (randLarger > smaller * factor);
            return randLarger % smaller;
        }

        /// <summary>
        /// We can get a rand(larger) from a rand(smaller)
        /// get rand(smaller^k) from rand(smaller) such that smaller^(k-1) < larger < smaller^(k)
        /// and then convert rand(smaller^k) back to rand(larger) using the subroutine RandomGenerationOfSmallerNumFromLargerNum
        /// 
        /// Now we need to think how to convert rand(smaller) to rand(smaller^k)
        /// rand(smaller^k) = Summation{smaller^(k) * rand(smaller)} + smaller - rand(smaller)
        ///                   k = k to 1
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="smaller"></param>
        /// <param name="larger"></param>
        /// <returns></returns>
        public static int RandomGenerationOfLargerNumFromSmallerNum(Random rand, int smaller, int larger)
        {
            if (smaller <= 0 || larger <= 0 || smaller > larger)
            {
                // Error condition
                throw new ArgumentException("smaller and larger parameter is not valid");
            }
            int randVal = 0;
            do
            {
                randVal = 0;
                int k = 1;
                while (larger > Math.Pow(smaller, k))
                {
                    randVal += (int)Math.Pow(smaller, k) * rand.Next(smaller);
                    k++;
                }
                randVal += (int)Math.Pow(smaller, k) * rand.Next(smaller);
                randVal += smaller - rand.Next(smaller) -1;
            } while (randVal >= larger);

            return randVal;
        }

        public static void TestRandomGeneration()
        {
            // Test RandomGenerationOfSmallerNumFromLargerNum
            int smaller = 5;
            int larger = 13;
            Console.WriteLine("The rand({0}) from rand({1})", smaller, larger);
            Print25RandomGen(smaller, larger, RandomGenerationOfSmallerNumFromLargerNum);

            smaller = 5;
            larger = 7;
            Console.WriteLine("The rand({0}) from rand({1})", smaller, larger);
            Print25RandomGen(smaller, larger, RandomGenerationOfSmallerNumFromLargerNum);

            smaller = 5;
            larger = 5;
            Console.WriteLine("The rand({0}) from rand({1})", smaller, larger);
            Print25RandomGen(smaller, larger, RandomGenerationOfSmallerNumFromLargerNum);

            //------------------------------------------------------------
            // Test RandomGenerationOfLargerNumFromSmallerNum
            smaller = 5;
            larger = 7;
            Console.WriteLine("The rand({0}) from rand({1})", larger, smaller);
            Print25RandomGen(smaller, larger, RandomGenerationOfLargerNumFromSmallerNum);

            smaller = 5;
            larger = 135;
            Console.WriteLine("The rand({0}) from rand({1})", larger, smaller);
            Print25RandomGen(smaller, larger, RandomGenerationOfLargerNumFromSmallerNum);

            smaller = 5;
            larger = 27;
            Console.WriteLine("The rand({0}) from rand({1})", larger, smaller);
            Print25RandomGen(smaller, larger, RandomGenerationOfLargerNumFromSmallerNum);
        }

        public static void Print25RandomGen(int smaller, int larger, Func<Random, int, int, int> func)
        {
            int index = 25;
            Random rand = new Random();
            while (index >= 0)
            {
                Console.Write("{0}, ", func(rand, smaller, larger));
                index--;
            }
            Console.WriteLine();
        }
    }
}
