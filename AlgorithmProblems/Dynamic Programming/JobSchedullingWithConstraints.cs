using AlgorithmProblems.Arrays.ArraysHelper;
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
        /// <summary>
        /// In the recursive solution, We first assign task1 to p1 and call the recursive function with tasklist - task1 and personList - p1.
        /// Then we assign t1 and t2 to p1 and call the recursive function with tasklist - (t1,t2) and personList - (p1).
        /// We keep doing this till we assign all the tasks to person p1.
        /// Find the min and that would be the time taken to complete all the tasks with k people.
        /// 
        /// Also use memoization for not calculating the same subproblems again and again.
        /// 
        /// The running time is O(n^2*k) where n is the total number of jobs and k is the total number of people
        /// The space requirement is O(n*k) needed to save the matrix
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="k"></param>
        /// <param name="currentIndex"></param>
        /// <param name="memoization"></param>
        /// <returns></returns>
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

        #region Dynamic Programming
        /// <summary>
        /// In the dynamic programming approach:
        /// Initialize for case when we have only person.
        /// Then loop till we have considered all k people.
        /// Foreach person assign him tasks t1 to tn and get the min time taken and put it in the currentArr
        /// 
        /// The running time is O(n^2*k) where n is the total number of jobs and k is the total number of people
        /// The space requirement is O(n) needed to save the pevious row for the p-1 people case.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int GetMinTimeTakenDP(int[] arr, int k)
        {
            if(arr == null || arr.Length ==0)
            {
                // When we have no jobs
                return 0;
            }
            if (k <= 0)
            {
                // When we have no people to assign the job
                return int.MaxValue;
            }

            int p = 1;
            int[] arrForPPeople = new int[arr.Length];
            for(int i=0; i<arr.Length; i++)
            {
                arrForPPeople[i] = (i - 1 >= 0) ? arrForPPeople[i - 1] + arr[i] : arr[i];
            }
            while (p <= k)
            {
                int[] currentArr = new int[arr.Length];
                currentArr.Populate(int.MaxValue);
                for(int i=0; i< arr.Length; i++)
                {
                    int currentWork = 0;
                    for(int j= i; j>=0; j--)
                    {
                        currentWork += arr[j];
                        int restWorkDoneByPreviousPeople = (j - 1 >= 0) ? arrForPPeople[j - 1] : 0;
                        if(currentArr[i] > Math.Max(currentWork, restWorkDoneByPreviousPeople))
                        {
                            currentArr[i] = Math.Max(currentWork, restWorkDoneByPreviousPeople);
                        }
                    }
                }
                arrForPPeople = currentArr;
                p++;
            }
            return arrForPPeople[arr.Length - 1];

        }
        #endregion

        public static void TestJobSchedullingWithConstraints()
        {
            int[] arr = new int[] { 1, 3, 5 };
            int k = 2;
            int[,] memoization = new int[k+1, arr.Length];
            memoization.Populate(-1);
            Console.WriteLine("Recursive method: The min time taken is {0}. Expected:5", GetMinTimeTaken(ref arr, k, 0, ref memoization));
            Console.WriteLine("DP method: The min time taken is {0}. Expected:5", GetMinTimeTakenDP(arr, k));

            arr = new int[] { 1, 2, 3, 4, 5, 9 };
            k = 3;
            memoization = new int[k + 1, arr.Length];
            memoization.Populate(-1);
            Console.WriteLine("Recursive method: The min time taken is {0}. Expected:9", GetMinTimeTaken(ref arr, k, 0, ref memoization));
            Console.WriteLine("DP method: The min time taken is {0}. Expected:9", GetMinTimeTakenDP(arr, k));

            arr = new int[] { 1, 1,1,1,1,1,1,1,1, 4, 5, 9 };
            k = 3;
            memoization = new int[k + 1, arr.Length];
            memoization.Populate(-1);
            Console.WriteLine("Recursive method: The min time taken is {0}. Expected:9", GetMinTimeTaken(ref arr, k, 0, ref memoization));
            Console.WriteLine("DP method: The min time taken is {0}. Expected:9", GetMinTimeTakenDP(arr, k));

            arr = new int[] { };
            k = 3;
            memoization = new int[k + 1, arr.Length];
            memoization.Populate(-1);
            Console.WriteLine("Recursive method: The min time taken is {0}. Expected:0", GetMinTimeTaken(ref arr, k, 0, ref memoization));
            Console.WriteLine("DP method: The min time taken is {0}. Expected:0", GetMinTimeTakenDP(arr, k));

            arr = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 5, 9 };
            k = 0;
            memoization = new int[k + 1, arr.Length];
            memoization.Populate(-1);
            Console.WriteLine("Recursive method: The min time taken is {0}. Expected:int.Max", GetMinTimeTaken(ref arr, k, 0, ref memoization));
            Console.WriteLine("DP method: The min time taken is {0}. Expected:int.Max", GetMinTimeTakenDP(arr, k));

            arr = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 5, 9 };
            k = 100;
            memoization = new int[k + 1, arr.Length];
            memoization.Populate(-1);
            Console.WriteLine("Recursive method: The min time taken is {0}. Expected:9", GetMinTimeTaken(ref arr, k, 0, ref memoization));
            Console.WriteLine("DP method: The min time taken is {0}. Expected:9", GetMinTimeTakenDP(arr, k));
        }
    }
}
