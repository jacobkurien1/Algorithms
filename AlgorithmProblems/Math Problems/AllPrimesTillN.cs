using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Math_Problems
{
    /// <summary>
    /// Get all the prime numbers till N
    /// </summary>
    class AllPrimesTillN
    {
        /// <summary>
        /// This is the Sieve of Eratosthenes algorithm.
        /// We create a bool array till of size n+1.
        /// Start from 2, and whereever you find isNotPrime false add it to the prime number
        /// And make all the multiples of that number notPrimes by updating the array.
        /// 
        /// The time complexity is O(nlog(logn))
        /// The space reqirement is O(n)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<int> GetAllPrimes(int n)
        {
            List<int> allPrimes = new List<int>();
            bool[] isNotPrime = new bool[n + 1];

            for(int i=2; i<= n;i++)
            {
                if(!isNotPrime[i])
                {
                    allPrimes.Add(i);
                    int num = i;
                    while (num + i <= n)
                    {
                        num += i;
                        isNotPrime[num] = true;
                    }
                }
                
            }

            return allPrimes;
        }

        public static void TestAllPrimesTillN()
        {
            int n = 5;
            Console.WriteLine("The count of all primes till {0} is {1}", n, GetAllPrimes(n).Count);
        }
    }
}
