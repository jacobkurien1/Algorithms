using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Misc
{
    /// <summary>
    /// To get the number of zeros in n factorial we need to count 
    /// </summary>
    class ZerosInFactorial
    {
        /// <summary>
        /// To calculate the number of zeros in a factorial, we need to calculate the number of 5's in the numbers
        /// from 1 to n.
        /// Since 2*5 will give a 10. And since there are lot of 2's but less number of 5's, counting 5's should suffice.
        /// 
        /// This can be done by getting num/5^1 + num/5^2 + num/5^3 +.. till num/5^k == 0
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int GetZerosInFactorial(int num)
        {
            if(num == 0)
            {
                return 1;
            }
            int totalNumOfZeros = 0;
            int factor = 5;
            while(num/factor!=0)
            {
                totalNumOfZeros += num/factor;
                factor *= 5;
            }
            return totalNumOfZeros;
        }

        public static void TestZerosInFactorial()
        {
            int num = 5;
            Console.WriteLine("The num of zeros in {0} is {1}", num, GetZerosInFactorial(num));

            num = 55;
            Console.WriteLine("The num of zeros in {0} is {1}", num, GetZerosInFactorial(num));

            num = 98;
            Console.WriteLine("The num of zeros in {0} is {1}", num, GetZerosInFactorial(num));

            num = 135;
            Console.WriteLine("The num of zeros in {0} is {1}", num, GetZerosInFactorial(num));
        }
    }
}
