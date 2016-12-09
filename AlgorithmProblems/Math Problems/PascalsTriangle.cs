using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Math_Problems
{
    /// <summary>
    /// Get Binomial Coefficients using pascals triangle
    /// 
    /// Pascals triangle is the triangular array of binary coefficients.
    /// An ith value in the pascals triangle at line index = line(C)i = line!/((line-i)!*i!)
    /// 
    /// The values in the pascal's triangle gives binomial coefficients due to the following property
    /// nCr = (n-1)C(r-1) + (n-1)C(r)
    /// </summary>
    class PascalsTriangle
    {
        /// <summary>
        /// The space requirement here is O(n)
        /// and the running time is O(n^2)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int[] GetBinomialCoefficients(int n)
        {
            int[] prevBinomialCoeff = new int[] { 1 };
            for(int i=2; i<= n; i++)
            {
                int[] newBinomialCoeff = new int[prevBinomialCoeff.Length + 1];
                for(int j = 0; j<newBinomialCoeff.Length; j++)
                {
                    // val1 is (n-1)C(r-1)
                    int val1 = (j - 1) >= 0 ? prevBinomialCoeff[j - 1] : 0;
                    // val2 is (n-1)C(r)
                    int val2 = (j < prevBinomialCoeff.Length) ? prevBinomialCoeff[j] : 0;
                    newBinomialCoeff[j] = val1 + val2;
                }
                prevBinomialCoeff = newBinomialCoeff;
            }
            return prevBinomialCoeff;
        }

        public static void TestPascalsTriangle()
        {
            int n = 6;
            Console.WriteLine("The binomial coefficients of n : {0} is as shown below", n);
            ArrayHelper.PrintArray(GetBinomialCoefficients(n));

            n = 7;
            Console.WriteLine("The binomial coefficients of n : {0} is as shown below", n);
            ArrayHelper.PrintArray(GetBinomialCoefficients(n));
        }
    }
}
