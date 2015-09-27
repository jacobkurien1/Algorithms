using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    class MinNumberOfJumps
    {
        /// <summary>
        /// We need to find the minimum number of jumps needed to reach the end of the array
        /// 
        /// </summary>
        /// <param name="arr">array which tells at each index what is the maximum jump that can be done</param>
        /// <returns>the minimum number of jumps needed to reach the end of the array</returns>
        private static int GetMinimumNumberOfJumps(int[] arr)
        {
            int[] minJump = new int[arr.Length];

            //initialization
            minJump[0] = 0;
            for(int i=1; i<arr.Length; i++)
            {
                minJump[i] = -1;
            }

            for(int i = 1; i<arr.Length; i++)
            {
                for(int j=0; j<i; j++)
                {
                    if(arr[j] >= i-j && minJump[j] != -1)  // if this condition is true then only we can reach arr[i] by jumping from arr[j]
                    {
                       if(minJump[i] == -1 || minJump[i] > minJump[j] +1)
                       {
                           minJump[i] = minJump[j] + 1;
                       }
                    }
                }
            }

            // minJump[arr.Length-1] has the min # of jumps needs to reach the end of the array from the index = 0
            // if minJump[arr.Length-1] == -1, it means we cannot reach the end of the array
            return minJump[arr.Length - 1];
        }

        public static void TestGetMinimumNumberOfJumps()
        {
            int[] arr = new int[5] { 1, 3, 1, 0, 3 };
            ArrayHelper.PrintArray(arr);
            Console.WriteLine("The minimum number of jumps needed is {0}", GetMinimumNumberOfJumps(arr));

            arr = new int[5] { 1, 1, 1, 0, 3 };
            ArrayHelper.PrintArray(arr);
            Console.WriteLine("The minimum number of jumps needed is {0}", GetMinimumNumberOfJumps(arr));

            arr = new int[5] { 1, 1, 1, 1, 1 };
            ArrayHelper.PrintArray(arr);
            Console.WriteLine("The minimum number of jumps needed is {0}", GetMinimumNumberOfJumps(arr));

        }
    }
}
