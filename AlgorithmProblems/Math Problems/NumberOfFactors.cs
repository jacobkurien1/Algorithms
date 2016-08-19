using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Math_Problems
{
    /// <summary>
    /// Get the total number of factors of a number num
    /// </summary>
    class NumberOfFactors
    {
        /// <summary>
        /// Get the total number of factors of a number.
        /// We can get this by breaking the number into the primes and check the factor of primes
        /// num = prime1^n1 * prime2^n2 * prime3^n3 ..
        /// total num of factors = (n1+1) * (n2+1) * (n3+1) ..
        /// 
        /// We will use some tactics to get the prime numbers from  AlgorithmProblems.Math_Problems.GetAllPrimeFactors
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int GetNumberOfFactors(int num)
        {
            int totalNumOfFactors = 1;

            for (int i = 2; i * i <= num; i++)
            {
                int power = 0;
                while(num%i == 0)
                {
                    power++;
                    num /= i;
                }
                totalNumOfFactors *= (power + 1);
            }

            // case where we have a prime number after the sqrt(num) like 14 ; 14 = 7*2 so we will still have 7 in num
            if(num>2)
            {
                // cause we need to do (power+1) and power in this case is 1
                totalNumOfFactors *= 2;
            }
            return totalNumOfFactors;
        }
        public static void TestNumberOfFactors()
        {
            int num = 14;
            Console.WriteLine("The total number of factors of {0} is {1}", num, GetNumberOfFactors(num));

            num = 121;
            Console.WriteLine("The total number of factors of {0} is {1}", num, GetNumberOfFactors(num));

            num = 3351;
            Console.WriteLine("The total number of factors of {0} is {1}", num, GetNumberOfFactors(num));

            num = 18;
            Console.WriteLine("The total number of factors of {0} is {1}", num, GetNumberOfFactors(num));
        }
    }
}
