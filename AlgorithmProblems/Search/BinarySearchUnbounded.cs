using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AlgorithmProblems.Search
{
    /// <summary>
    /// Given a function f(x) where x>=0 and f(x) is non-decreasing.
    /// We need to find the value of x where f(x) turns positive
    /// </summary>
    public class BinarySearchUnbounded
    {
        /// <summary>
        /// The assumption here is f(x) is monotonically increasing for all x>=0
        /// Algo: 1. Since we dont know the upper bound we will keep checking the function till we hit a positive result
        /// f(0), f(2^0), f(2^1), f(2^2), f(2^3), f(2^4), f(2^5), ..
        /// 2. Then we do binary search for the x where f(x) is positive for the first time between 2^(n-1) to 2^n
        /// 
        /// The running time here will be O(log(n)) assuming the f(x) calculation takes O(1) time
        /// The space requirement is O(1)
        /// </summary>
        /// <param name="monotonicallyIncreasingFunc"></param>
        /// <returns>input to the function where the funtion turns positive for the first time</returns>
        int FindFirstPositive(Func<int, int> monotonicallyIncreasingFunc)
        {
            int funcInput = 0;
            while(monotonicallyIncreasingFunc(funcInput) <= 0)
            {
                funcInput = (funcInput == 0) ? 1 : funcInput * 2;
            }
            return BinarySearch(funcInput / 2, funcInput, monotonicallyIncreasingFunc);
        }

        /// <summary>
        /// Returns the index where the monotonicallyIncreasingFunc gives the first positive number.
        /// Note : we will not consider 0 as positive number here.
        /// </summary>
        /// <param name="low">start of the input to monotonicallyIncreasingFunc</param>
        /// <param name="high">upper end of the input to monotonicallyIncreasingFunc</param>
        /// <param name="monotonicallyIncreasingFunc">delegate monotonicallyIncreasingFunc</param>
        /// <returns></returns>
        private int BinarySearch(int low, int high, Func<int, int> monotoncallyIncreasingFunc)
        {
            while(low <= high)
            {
                int mid = low + ((high - low) / 2);
                if(monotoncallyIncreasingFunc(mid) <= 0)
                {
                    low = mid + 1;
                }
                else if (monotoncallyIncreasingFunc(mid) > 0 && monotoncallyIncreasingFunc(mid - 1) <=0)
                {
                    return mid;
                }
                else
                {
                    high = mid - 1;
                }
            }
            return -1;
        }


        #region TestArea
        public static void TestBinarySearchUnbounded()
        {
            BinarySearchUnbounded bsu = new BinarySearchUnbounded();

            Console.WriteLine("The first positive input is {0}, Expected: {1}", bsu.FindFirstPositive(linearFunc), 21);
            Console.WriteLine("The first positive input is {0}, Expected: {1}", bsu.FindFirstPositive(logFunc), 2);
            Console.WriteLine("The first positive input is {0}, Expected: {1}", bsu.FindFirstPositive(quadraticFunc), 1);
            Console.WriteLine("The first positive input is {0}, Expected: {1}", bsu.FindFirstPositive(customFunc), 100001);
        }

        private static int linearFunc(int x)
        {
            return x - 20;
        }

        private static int logFunc(int x)
        {
            // make sure that the func does not fail when x=0
            return (x == 0) ? -1000000 : (int)Math.Ceiling(Math.Log(x));
        }

        private static int quadraticFunc(int x)
        {
            return x*x;
        }

        private static int customFunc(int x)
        {
            if(x <= 100000)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        #endregion
    }
}
