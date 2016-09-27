using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Math_Problems
{
    /// <summary>
    /// Multiply 2 numbers which are long and are represented in a string.
    /// </summary>
    class MultiplyLongNumbers
    {
        /// <summary>
        /// Here get each digit from num2 and multiply it to all the digits in num1(individually)
        /// and store the results in a result array.
        /// Do the same for all the digits of num2 and keep adding the value in the result array.
        /// Now traverse the results array and store result[i]%10 as the value of that index
        /// and carry forward result[i]/10
        /// 
        /// the running time of this algorithm is O(num1.Length *num2.Length)
        /// and the space requirement is O(num1.Length + num2.Length)
        /// it is assumed that the 2 numbers are valid number strings and are positive
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static string GetResultForMultiplication(string num1, string num2)
        {
            int[] result = new int[num1.Length + num2.Length];
            for(int index2= num2.Length-1; index2>=0; index2--)
            {
                int num2Digit = int.Parse(num2[index2].ToString());
                for (int index1 = num1.Length-1; index1>=0; index1--)
                {
                    int num1Digit = int.Parse(num1[index1].ToString());
                    // get each number digit and multiply it and save it in the correct index of result array
                    result[num1.Length + num2.Length - 2 - (index1 + index2)] += num1Digit * num2Digit;
                }
            }

            StringBuilder sb = new StringBuilder();

            // Now traverse the results array and store result[i]% 10 as the value of that index
            // and carry forward result[i]/10
            for (int i=0; i<result.Length-1; i++)
            {
                int quotient = result[i] / 10;
                int remainder = result[i] % 10;
                sb.Insert(0, remainder);
                result[i + 1] += quotient;
            }
            // take care of the most significant digit
            if(result[result.Length - 1] >0)
            {
                sb.Insert(0, result[result.Length - 1]);
            }
            return sb.ToString();
        }

        public static void TestMultiplyLongNumbers()
        {
            string num1 = "123";
            string num2 = "144";
            Console.WriteLine("The product of {0} and {1} is {2}", num1, num2, GetResultForMultiplication(num1, num2));

            num1 = "566699999";
            num2 = "354";
            Console.WriteLine("The product of {0} and {1} is {2}", num1, num2, GetResultForMultiplication(num1, num2));

            num1 = "100000";
            num2 = "100";
            Console.WriteLine("The product of {0} and {1} is {2}", num1, num2, GetResultForMultiplication(num1, num2));
        }
    }
}
