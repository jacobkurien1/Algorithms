using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.PermutationAndCombination
{
    /// <summary>
    /// Get the powerset of a given set
    /// </summary>
    class PowerSet
    {
        #region Algo1: Iterative solution
        public List<List<string>> GetThePowerSetsIterative(List<string> inputSet)
        {
            List<List<string>> powerSet = new List<List<string>>();

            //Add the empty set
            powerSet.Add(new List<string>());

            for(int index =0; index<inputSet.Count; index++)
            {
                List<List<string>> clonedPowerSetWithElementAddedAtIndex = AddCurrentValToAllSet(powerSet, inputSet[index]);
                powerSet.AddRange(clonedPowerSetWithElementAddedAtIndex);
            }

            return powerSet;
        }
        #endregion


        #region Algo2: Recursive solution
        /// <summary>
        /// We can recursively get a set without the present of element at currentIndex
        /// and clone it and add the element at current index to all the sets in the clone.
        /// 
        /// The running time is O(2^n)
        /// 
        /// T(n) = T(n-1) +1
        /// T(n-1) = T(n-2) + 2
        /// T(n-2) = T(n-3) + 4
        /// T(n-3) = T(n-4) + 8
        /// 
        /// T(1) = 1
        /// 
        /// T(n) = 2^0+2^1 + 2^2 + .. 2^k + T(n-k-1)
        /// k = n-2
        /// so
        /// T(n) = 2^0 + 2^1 + 2^2 + .. 2^(n-2)
        ///      = 2^((n-2)+1)
        ///      = O(2^n)
        /// </summary>
        /// <param name="inputSet"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<List<string>> GetThePowerSetsRecursive(List<string> inputSet, int index)
        {
            List<List<string>> powerSet = new List<List<string>>();

            if(index<inputSet.Count)
            {
                List<List<string>> setWithoutCurrentIndex = GetThePowerSetsRecursive(inputSet, index + 1);
                List<List<string>> setWithCurrentIndex = AddCurrentValToAllSet(setWithoutCurrentIndex, inputSet[index]);
                powerSet.AddRange(setWithoutCurrentIndex);
                powerSet.AddRange(setWithCurrentIndex);
            }
            else
            {
                powerSet.Add(new List<string>() {});
            }

            return powerSet;
        }

        /// <summary>
        /// Adds valToAdd to the clone of all the sets in InputSet
        /// </summary>
        /// <param name="setWithoutCurrentIndex"></param>
        /// <param name="valToAdd"></param>
        /// <returns></returns>
        private List<List<string>> AddCurrentValToAllSet(List<List<string>> inputSet, string valToAdd)
        {
            List<List<string>> ret = new List<List<string>>();
            foreach(List<string> set in inputSet)
            {
                // clone the current set
                List<string> cloneSet = new List<string>(set);

                // Add the string to this new set
                cloneSet.Add(valToAdd);

                ret.Add(cloneSet);
            }
            return ret;
        }
        #endregion
        public static void TestPowerSet()
        {
            PowerSet ps = new PowerSet();
            List<string> set = new List<string>() { "1", "2", "3" };
            Console.WriteLine("The powerset output from the recursive algo is as shown below:");
            PrintPowerset(ps.GetThePowerSetsRecursive(set, 0));

            Console.WriteLine("The powerset output from the iterative algo is as shown below:");
            PrintPowerset(ps.GetThePowerSetsIterative(set));
        }

        private static void PrintPowerset(List<List<string>> powerSet)
        {
            foreach(List<string> set in powerSet)
            {
                foreach(string setElem in set)
                {
                    Console.Write("{0}, ", setElem);
                }
                Console.WriteLine();
            }
        }
    }
}
