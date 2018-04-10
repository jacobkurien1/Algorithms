using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.PermutationAndCombination
{
    /// <summary>
    /// Given a time 12:32 find all the permutations using the individual digits and return the valid times.
    /// 
    /// For eg 12:32
    /// 1. 23:12
    /// 2. 22:31
    /// 3. 22:13
    /// 4. 21:31 etc
    /// </summary>
    class PermutationsOfValidTime
    {
        /// <summary>
        /// This is a recursive subroutine which returns list of all times in int format.
        /// 23:12 will be represented as array 2,3,1,2. Hashset is used to eliminate duplicates
        /// 
        /// The running time is O(4!) and space requirement is also O(4!)
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private HashSet<int> GetAllValidTimes(int[] time, int index)
        {
            if(index >= 4)
            {
                return null;
            }
            else
            {
                int currentDigit = time[index];
                HashSet<int> nextPermutations = GetAllValidTimes(time, index+1);
                if(nextPermutations == null)
                {
                    return new HashSet<int>() { currentDigit }; 
                }
                else
                {
                    HashSet<int> currentPermutations = new HashSet<int>();
                    foreach(int partialTime in nextPermutations)
                    {
                        int power = 1;
                        int q = partialTime/power;
                        while (power <= 10000)
                        {
                            int r = partialTime % power;
                            int newNum = q * (power * 10) + currentDigit * power + r;
                            currentPermutations.Add(newNum);
                            power *= 10;
                            q = partialTime/power;
                        }
                    }
                    return currentPermutations;
                }
            }
        }

        /// <summary>
        /// This is the main method which takes in a time as input and prints all the 
        /// valid time permutations of the input
        /// </summary>
        /// <param name="time"></param>
        public void PrintAllValidTimes(int time)
        {
            if(!IsValidTime(time))
            {
                throw new ArgumentException("The time is not in correct format");
            }

            // Create the time Array
            int[] timeArr = new int[4];
            for (int i = 0; i < 4; i++)
            {
                timeArr[i] = time % 10;
                time /= 10;
            }
            
            //Run Algo
            HashSet<int> alltimes = GetAllValidTimes(timeArr, 0);

            // Print time permutations
            foreach(int individualtime in alltimes)
            {
                if (IsValidTime(individualtime))
                {
                    Console.Write("{0}, ", individualtime);
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Checks wether a given time is a valid time
        /// </summary>
        /// <param name="time">time is represented in int format. Eg 12:34 is 1234</param>
        /// <returns></returns>
        private bool IsValidTime(int time)
        {

            int hours = time / 100;
            int mins = time % 100;
            return hours >= 0 && hours < 24 && mins >= 0 && mins < 60;
        }

        public static void TestPermutationsOfValidTime()
        {
            PermutationsOfValidTime pvt = new PermutationsOfValidTime();
            pvt.PrintAllValidTimes(1234);

            // with duplicates
            pvt.PrintAllValidTimes(2234);

            //with zeros
            pvt.PrintAllValidTimes(0001);
        }
    }
}
