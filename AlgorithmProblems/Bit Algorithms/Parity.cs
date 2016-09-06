using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Bit_Algorithms
{
    /// <summary>
    /// Get the parity of the num. Parity is 1 if the number of bits are odd
    /// Parity is 0 if the number of bits are even
    /// </summary>
    class Parity
    {
        /// <summary>
        /// keep checking the LSB and if its 1 keep togelling paritybit
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int GetParity(int num)
        {
            int parityBit = 0;
            while(num>0)
            {
                parityBit ^= num & 1;
                num >>= 1;
            }
            return parityBit;
        }

        public static void TestParity()
        {
            int num = 15;
            Console.WriteLine("The parity for {0} is {1}", num, GetParity(num));

            num = 14;
            Console.WriteLine("The parity for {0} is {1}", num, GetParity(num));
        }
    }
}
