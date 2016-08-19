using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Math_Problems
{
    /// <summary>
    /// get all factors of a number.
    /// Note the factors need not be only the primes
    /// </summary>
    class AllFactors
    {
        /// <summary>
        /// for each factor from 1 t0 sqrt(num) we get the other factor by doing num/factor
        /// Keep adding both these factors to a list and return it
        /// </summary>
        /// <param name="num">number for which the factors needs to be calculated</param>
        /// <returns>list containing all the factors</returns>
        public static List<int> GetAllFactors(int num)
        {
            List<int> allFactors = new List<int>();
            int sqrtNum = (int)Math.Sqrt(num);
            for(int i=1; i<=sqrtNum; i++)
            {
                if(num%i == 0)
                {
                    // i is a factor
                    allFactors.Add(i);

                    if (num / i != i)
                    {
                        // num/i is also a factor
                        allFactors.Add(num / i);
                    }
                }
            }

            return allFactors;
        }

        public static void TestAllFactors()
        {
            int num = 36;
            Console.WriteLine("All the factors of {0} are:", num);
            PrintAllFactors(GetAllFactors(num));

            num = 14;
            Console.WriteLine("All the factors of {0} are:", num);
            PrintAllFactors(GetAllFactors(num));
        }

        private static void PrintAllFactors(List<int> allFactors)
        {
            foreach (int factor in allFactors)
            {
                Console.Write("{0}, ", factor);
            }
            Console.WriteLine();
        }
    }
}
