using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// We need an effective way to calculate nCr
    /// </summary>
    public class BinomialCoefficient
    {
        /// <summary>
        /// We can use dynamic programming 
        /// C[n, r] = C[n-1, r-1] + C[n-1, r]
        /// and C[n,n] =1 and C[n,0] = 1
        /// 
        /// n should be greater than equal to r
        /// Hence the matrix C will be only filled in the left bottom part as the n is denoted by rows
        /// and the r is denoted by the columns
        /// </summary>
        /// <param name="n"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public long Calculate(int n, int r)
        {
            int[,] C = new int[n + 1, r + 1];
            //initialization
            for (int i = 0; i <= n; i++)
            {
                C[i, 0] = 1;
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= r; j++)
                {
                    if(i<j)
                    {
                        break;
                    }
                    C[i, j] = ((i - 1 >= 0 && j - 1 >= 0) ? C[i - 1, j - 1] : 0) + ((i - 1 >= 0 && i - 1 >= j) ? C[i - 1, j] : 0);
                }
            }
            return C[n, r];
        }

        public static void TestBinomialCoefficient()
        {
            BinomialCoefficient bc = new BinomialCoefficient();
            Console.WriteLine("The binomial coeff of {0}C{1} is {2} and expeceted value is {3}", 20, 5, bc.Calculate(20, 5), 15504);
            Console.WriteLine("The binomial coeff of {0}C{1} is {2} and expeceted value is {3}", 23, 20, bc.Calculate(23, 20), 1771);
            Console.WriteLine("The binomial coeff of {0}C{1} is {2} and expeceted value is {3}", 33, 8, bc.Calculate(33,8), 13884156);
            Console.WriteLine("The binomial coeff of {0}C{1} is {2} and expeceted value is {3}", 5, 2, bc.Calculate(5,2), 10);
        }
    }
}
