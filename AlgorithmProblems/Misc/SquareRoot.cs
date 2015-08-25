using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Misc
{
    class SquareRoot
    {
        /// <summary>
        /// This algo has the running time of O(N)
        /// The square root of a number will always lie between 1 and N/2
        /// We will iterate from 1 to N/2 and sqaure it and check if it is equal or greater to num
        /// if it is equal we return that number k but if it is greater we return k-1
        /// </summary>
        /// <param name="num">number for which the square root needs to be found</param>
        /// <returns>the squareroot of the number</returns>
        private static int CalculateSquareRootAlgo1(int num)
        {
            int sqrootVal = 1;
            while(sqrootVal*sqrootVal <= num)
            {
                sqrootVal++;
            }
            // we are doing sqrootVal-1 to round the square root result down
            return sqrootVal - 1;
        }

        /// <summary>
        /// We can reduce this running time to O(log(N)) if we do a binary search style to get
        /// the square root for the number
        /// </summary>
        /// <param name="num">number for which the square root needs to be found</param>
        /// <returns>the squareroot of the number</returns>
        private static int CalculateSquareRootAlgo2(int num)
        {
            // Since the square root value lies between 1 and num/2
            int low = 1;
            int high = num / 2;

            while(low<=high)
            {
                // find the mid value
                int mid = (high + low) / 2;

                // We need to find whether the sqrootVal lies between[low,mid-1] or [mid+1, high] 
                int sqVal = mid * mid;
                if(sqVal == num)    // found the sqrootVal
                {
                    return mid;
                }
                else if(sqVal<num) // the sqrootVal lies between [mid+1, high]
                {
                    low = mid + 1;
                }
                else // the sqrootVal lies between [low, mid-1]
                {
                    high = mid - 1;
                }
            }
            return low - 1;
        }

        /// <summary>
        /// This algo has the running time of O(1)
        /// N^0.5 = 2^log2(N^0.5) = 2^(0.5*log2(N))
        /// </summary>
        /// <param name="num">number for which the square root needs to be found</param>
        /// <returns>the squareroot of the number</returns>
        private static double CalculateSquareRootAlgo3(int num)
        {
            return Math.Pow(2, (0.5 * Math.Log(num, 2)));
        }

        public static void TestCalculateSquareRoot()
        {
            Console.WriteLine("Square root using algo1 of {0} is {1}", 32, CalculateSquareRootAlgo1(32));
            Console.WriteLine("Square root using algo1 of {0} is {1}", 36, CalculateSquareRootAlgo1(36));
            Console.WriteLine("Square root using algo1 of {0} is {1}", 24, CalculateSquareRootAlgo1(24));
            Console.WriteLine("Square root using algo1 of {0} is {1}", 25, CalculateSquareRootAlgo1(25));

            Console.WriteLine("Square root using algo2 of {0} is {1}", 32, CalculateSquareRootAlgo2(32));
            Console.WriteLine("Square root using algo2 of {0} is {1}", 36, CalculateSquareRootAlgo2(36));
            Console.WriteLine("Square root using algo2 of {0} is {1}", 24, CalculateSquareRootAlgo2(24));
            Console.WriteLine("Square root using algo2 of {0} is {1}", 25, CalculateSquareRootAlgo2(25));

            Console.WriteLine("Square root using algo3 of {0} is {1}", 32, CalculateSquareRootAlgo3(32));
            Console.WriteLine("Square root using algo3 of {0} is {1}", 36, CalculateSquareRootAlgo3(36));
            Console.WriteLine("Square root using algo3 of {0} is {1}", 24, CalculateSquareRootAlgo3(24));
            Console.WriteLine("Square root using algo3 of {0} is {1}", 25, CalculateSquareRootAlgo3(25));
        }
    }
}
