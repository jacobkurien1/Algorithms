using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Math_Problems
{
    /// <summary>
    /// Get all the prime factors of a given number
    /// </summary>
    class GetAllPrimeFactors
    {
        /// <summary>
        /// 1. Check whether 2 is a factor of num, if yes keep dividing num by 2 till the remainder is 0
        /// 2. Check for all the primes from 3 to sqrt(num) and increment in steps of 2. Keep adding the factors as we see. also keep doing num/=prime till remainder is 0
        /// 3. if num is still not 1, that means we have a prime factor beyond sqrt(num) eg 14 has 2*7 and we will have 7 remaining. So add this to the list
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static List<int> GetAllPrimeFactorsForNum(int num)
        {
            List<int> allPrimes = new List<int>();

            bool isFirst = true;
            //1. Check whether 2 is a factor of num, if yes keep dividing num by 2 till the remainder is 0
            while (num!= 0 && num%2 == 0)
            {
                if (isFirst)
                {
                    allPrimes.Add(2);
                    isFirst = false;
                }
                num /= 2;
            }

            //2. Check for all the primes from 3 to sqrt(num) and increment in steps of 2. 
            //Keep adding the factors as we see. also keep doing num/=prime till remainder is 0
            for(int i=3; i*i<= num; i= i+2)
            {
                isFirst = true;
                while (num != 0 && num % i == 0)
                {
                    if (isFirst)
                    {
                        allPrimes.Add(i);
                        isFirst = false;
                    }
                    num /= i;
                }
            }

            //3. if num is still not 1, that means we have a prime factor beyond sqrt(num)
            if(num>2)
            {
                allPrimes.Add(num);
            }

            return allPrimes;
        }

        public static void TestGetAllPrimeFactors()
        {
            int num = 14;
            Console.WriteLine("All the prime factor of num : {0} are", num);
            PrintAllPrimes(GetAllPrimeFactorsForNum(num));

            num = 315;
            Console.WriteLine("All the prime factor of num : {0} are", num);
            PrintAllPrimes(GetAllPrimeFactorsForNum(num));

            num = 101;
            Console.WriteLine("All the prime factor of num : {0} are", num);
            PrintAllPrimes(GetAllPrimeFactorsForNum(num));


            num = 111;
            Console.WriteLine("All the prime factor of num : {0} are", num);
            PrintAllPrimes(GetAllPrimeFactorsForNum(num));

            num = 121;
            Console.WriteLine("All the prime factor of num : {0} are", num);
            PrintAllPrimes(GetAllPrimeFactorsForNum(num));
        }

        private static void PrintAllPrimes(List<int> allPrimes)
        {
            foreach(int prime in allPrimes)
            {
                Console.Write("{0}, ", prime);
            }
            Console.WriteLine();
        }
    }
}
