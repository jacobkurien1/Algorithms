using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// Given a path of Size 4*width.
    /// And an infinite supply of brick of size 1x4.
    /// The brick can be layed horizontally or vertically.
    /// Find the total number of ways in which the bricks can be layed
    /// </summary>
    class BrickLaying
    {
        /// <summary>
        /// We do this using the dynamic programming approach.
        /// We consider the case in which we lay the brick vertically and horizontally.
        /// And we do this in a bottoms up manner.
        /// The space required is O(width)
        /// The running time is also O(width)
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public static int DifferentWaysOfLayingBricks(int width)
        {
            int[] arr = new int[width+1];
            arr[0] = 1;
            for(int i=1;i<= width; i++)
            {
                // Lay a brick vertically
                arr[i] += arr[i - 1];

                // Lay 4 bricks horizontally
                if(i-4>=0)
                {
                    arr[i] += arr[i - 4];
                }
            }

            return arr[width];
        }

        public static void TestBrickLaying()
        {
            int width = 7;
            Console.WriteLine("The different ways in which bricks can be laid for width {0} is {1} ways", width, DifferentWaysOfLayingBricks(width));
        }
    }
}
