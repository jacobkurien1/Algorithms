using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Bit_Algorithms
{
    class IntHaveOppositeSigns
    {
        /// <summary>
        /// We can use comparison operator to check whether the signs are opposite
        /// But a faster algo would be to do bit wise XOR and if the numbers are having opposite sign the result will be less than 0
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static bool DoesIntegersHaveOppositeSigns(int num1, int num2)
        {
            return ((num1 ^ num2) < 0);
        }

        public static void TestIntHaveOppositeSigns()
        {
            int num1 = 4;
            int num2 = -3;
            Console.WriteLine("The following numbers {0} and {1} have opposite signs: {2}", num1, num2, DoesIntegersHaveOppositeSigns(num1, num2));

            num1 = 4;
            num2 = 3;
            Console.WriteLine("The following numbers {0} and {1} have opposite signs: {2}", num1, num2, DoesIntegersHaveOppositeSigns(num1, num2));

            num1 = -4;
            num2 = -3;
            Console.WriteLine("The following numbers {0} and {1} have opposite signs: {2}", num1, num2, DoesIntegersHaveOppositeSigns(num1, num2));
        }

    }
}
