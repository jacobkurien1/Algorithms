using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Arrays
{
    /// <summary>
    /// Given a histogram, we need to find the water that can get collected in puddles if rain happens
    /// </summary>
    class WaterCollectedInPuddleShownByHistogram
    {
        /// <summary>
        /// 1. Have a left pointer and a right pointer
        /// 2. Also have the largestHieght left to the left pointer, and largest height right to the right pointer
        /// 3. if the leftLargest is less than the rightLargest then add leftLargest - hist[leftIndex] to water area.
        ///     and Update the leftLargest.
        /// 4. Similarly if the leftLargest is greater than largestRight then add rightLargest - hist[rightIndex] to water area
        ///     and update the rightLargest.
        /// 
        /// Do the steps 1-4 while leftIndex<=rightIndex
        /// 
        /// The running time is O(n)
        /// The space requirement is O(1)
        ///     
        /// </summary>
        /// <param name="hist">histogram heights represented by an array</param>
        /// <returns>area in which the water can get collected</returns>
        public static int GetWaterArea(int[] hist)
        {
            int waterArea = 0;

            if(hist.Length>=2)
            {
                int leftLargest = hist[0];
                int rightLargest = hist[hist.Length - 1];
                int leftIndex = 1;
                int rightIndex = hist.Length - 2;
                while(leftIndex<=rightIndex)
                {
                    if(leftLargest<=rightLargest)
                    {
                        int currentArea = leftLargest - hist[leftIndex];
                        if(currentArea>0)
                        {
                            waterArea += currentArea;
                        }
                        if(leftLargest<hist[leftIndex])
                        {
                            leftLargest = hist[leftIndex];
                        }
                        leftIndex++;
                    }
                    else
                    {
                        int currentArea = rightLargest - hist[rightIndex];
                        if (currentArea > 0)
                        {
                            waterArea += currentArea;
                        }
                        if (rightLargest<hist[rightIndex])
                        {
                            rightLargest = hist[rightIndex];
                        }
                        rightIndex--;
                    }
                }

            }
            return waterArea;
        }

        public static void TestWaterCollectedInPuddleShownByHistogram()
        {
            int[] hist = new int[] { 8, 7, 6, 9, 2, 6, 2, 10 };
            Console.WriteLine("The water area in the histogram is {0}. Expected:20", GetWaterArea(hist));

            hist = new int[] { 2, 5, 1, 3, 1, 2, 1, 7, 7, 6 };
            Console.WriteLine("The water area in the histogram is {0}. Expected:17", GetWaterArea(hist));
        }
    }
}
