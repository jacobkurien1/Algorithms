using System;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// Pivot floor: floor from which when the egg is dropped it will break.
    /// We need to return the minimum number of trails in worst case to get this privot floor
    /// We are given the number of floors and the number of eggs for the experiment.
    /// </summary>
    class EggDropMinTrials
    {
        /// <summary>
        /// We can solve this problem using dynamic programming
        /// We need to consider 2 cases: when the egg breaks and when the egg does not breaks
        /// trials(floor, egg) = min(1 + max(trials(floor-f, egg), trials(floor-1, egg-1)));
        /// for f -> 1 to floor as we will be getting the worst case so we do the max 
        /// and we will do min to get the minimum number of trails
        /// 
        /// 
        /// The running time is O((floors^2)*eggs)
        /// The space requirement can be optimized to O(floors) if we dont get the whole matrix
        /// </summary>
        /// <param name="floors">number of floors for which the pivot needs to be found</param>
        /// <param name="eggs">number of eggs which we have to find the pivot floor</param>
        /// <returns></returns>
        public static int GetMinTrials(int floors, int eggs)
        {
            if(floors <= 0)
            {
                throw new ArgumentException("Floors are not valid");
            }
            if(eggs <= 0)
            {
                throw new ArgumentException("number of eggs are not valid");
            }

            int[] prevTrials = new int[floors+1];
            int[] currTrials = new int[floors+1];

            for (int floor = 1; floor <= floors; floor++)
            {
                prevTrials[floor] = floor;
            }
            for (int e = 2; e<=eggs; e++)
            {
                for (int floor = 1; floor<=floors; floor++)
                {
                    currTrials[floor] = int.MaxValue;
                    for (int f = 1; f<=floor; f++)
                    {
                        int currVal = 1 + Math.Max(currTrials[floor - f], prevTrials[f - 1]);
                        if(currTrials[floor]> currVal)
                        {
                            currTrials[floor] = currVal;
                        }
                    }
                }
                prevTrials = currTrials;
                currTrials = new int[floors + 1];
            }

            return prevTrials[floors];
        }

        public static void TestEggDropMinTrials()
        {
            int floors = 36;
            int eggs = 2;
            Console.WriteLine("The min trials for {0} floors and {1} eggs is {2}", floors, eggs, GetMinTrials(floors, eggs));

            floors = 10;
            eggs = 1;
            Console.WriteLine("The min trials for {0} floors and {1} eggs is {2}", floors, eggs, GetMinTrials(floors, eggs));

            floors = 10;
            eggs = 4;
            Console.WriteLine("The min trials for {0} floors and {1} eggs is {2}", floors, eggs, GetMinTrials(floors, eggs));
        }
    }
}
