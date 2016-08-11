using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Misc
{
    class IsPrime
    {
        public static bool CheckPrime(int num)
        {
            for(int i=2; i <= (int)Math.Sqrt(num); i++)
            {
                if(num%i==0)
                {
                    return false;
                }
            }
            return true;
        }

        public static void TestCheckPrime()
        {
            Console.WriteLine("The number {0} is prime {1}", 29, CheckPrime(29));
            Console.WriteLine("The number {0} is prime {1}", 36, CheckPrime(36));
            Console.WriteLine("The number {0} is prime {1}", 11, CheckPrime(11));
        }
    }
}
