using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Misc
{
    /// <summary>
    /// Calculate 1^2 + 2^2 + 3^2 + ... + n^2
    /// Solution: 1^2 + (1^2 +3) + (2^2+5) + ... 
    /// </summary>
    class SumOfSquaresWithoutMultiplication
    {
        public static long SumOfSquares(int n)
        {
            long retVal = 0;
            long sum = 0;
            int a = 1;
            for (int i = 1; i<=n; i++)
            {
                sum = (sum + a);
                a += 2;
                retVal += sum;
            }

            return retVal;
        }

        public static void TestSumOfSquares()
        {
            Console.WriteLine("the sum of squares for n = {0} is {1}", 2, SumOfSquares(2));
            Console.WriteLine("the sum of squares for n = {0} is {1}", 3, SumOfSquares(3));
            Console.WriteLine("the sum of squares for n = {0} is {1}", 10, SumOfSquares(10));
        }
    }
}
