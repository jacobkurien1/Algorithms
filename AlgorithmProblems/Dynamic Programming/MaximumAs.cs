using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    class MaximumAs
    {
        /// <summary>
        /// If we can press n keys and the different keys are:
        /// A -> Prints A
        /// *A -> ctrl+A selects all
        /// *C -> ctrl+c copy selected
        /// *V -> ctrl+v paste selected
        /// 
        /// Find the maximum number of A's that can be printed on the screen?
        /// if n less than equal to 6, f(n) = n
        /// if n greater than 6, f(n) = max i->1 to n-2 {f(n-1-i)*i}
        /// 
        /// 
        /// for eg: Consider f(7) = we can have A *A *C *V *V *V *V = f(1)*5
        ///                         AA *A *C *V *V *V = f(2)*4
        ///                         AAA *A *C *V *V  = f(3)*3
        ///                         AAAA *A *C *V = f(4)*2
        ///                         AAAAA  = f(5)*1
        /// take the maximum and that will be f(7)
        /// </summary>
        /// <param name="n">number of keystrokes</param>
        /// <returns>maximum number of A's that can be printed using n key strokes</returns>
        private static int GetMaximumAs(int n)
        {
            // we will save f(n) at maxAs[n]
            int[] maxAs = new int[n + 1];

            // initialization
            for (int i = 1; i <= 6 && i<=n; i++)
            {
                maxAs[i] = i;
            }

            if(n>6)
            {
                for(int index=7; index<=n; index++)
                {
                    for(int i= 1 ; i<=index-2; i++)
                    {
                        if(maxAs[index] < maxAs[index-1-i]*i)
                        {
                            maxAs[index] = maxAs[index - 1 - i] * i;
                        }
                    }
                }
            }

            return maxAs[n];
            
        }

        public static void TestGetMaximumAs()
        {
            Console.WriteLine("The maximum number of A's that can be printed when n = {0} is {1}", 7, GetMaximumAs(7));
            Console.WriteLine("The maximum number of A's that can be printed when n = {0} is {1}", 5, GetMaximumAs(5));
            Console.WriteLine("The maximum number of A's that can be printed when n = {0} is {1}", 27, GetMaximumAs(27));
        }
    }
}
