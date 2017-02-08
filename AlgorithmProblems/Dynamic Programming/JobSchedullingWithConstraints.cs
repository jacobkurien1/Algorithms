using AlgorithmProblems.matrix_problems.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// Given the times taken by the jobs in an array and also the number of people to whom these jobs can be assigned. 
    /// A group of consecutive jobs can be assigned to a person. 
    /// Find the min time taken to finish all the jobs in the input array, given k people.
    /// 
    /// Basically this problem can be restated as follows:
    /// Given the arr, divide the array into k partitions such that the max sum in any partion is the minimum.
    /// </summary>
    class JobSchedullingWithConstraints
    {
        #region RecursiveSolution with memoization
        public static int GetMinTimeTaken(ref int[] arr, int k, int currentIndex, ref int[,] memoization)
        {
            if(currentIndex >= arr.Length && k>=0)
            {
                // People are left but the work is finished
                return 0;
            }
            else if(currentIndex <arr.Length && k<=0)
            {
                // No people, but the work is left
                return int.MaxValue;
            }
            if(memoization[k, currentIndex] != -1)
            {
                return memoization[k, currentIndex];
            }

            int timeIfKthPersonWorks = 0;
            int minTimeTaken = int.MaxValue;
            for(int i= currentIndex; i<arr.Length; i++)
            {
                timeIfKthPersonWorks += arr[i];
                int minTimeFromRemainigWork = GetMinTimeTaken(ref arr, k - 1, i+1, ref memoization);
                if(minTimeTaken > Math.Max(timeIfKthPersonWorks, minTimeFromRemainigWork))
                {
                    minTimeTaken = Math.Max(timeIfKthPersonWorks, minTimeFromRemainigWork);
                }
            }
            memoization[k, currentIndex] = minTimeTaken;
            return minTimeTaken;
        }
        #endregion

        public static void TestJobSchedullingWithConstraints()
        {
            int[] arr = new int[] { 1, 3, 5 };
            int k = 2;
            int[,] memoization = new int[k+1, arr.Length];
            memoization.Populate(-1);
            Console.WriteLine("Recursive method: The min time taken is {0}", GetMinTimeTaken(ref arr, k, 0, ref memoization));
        }
    }
}
