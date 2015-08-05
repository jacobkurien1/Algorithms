using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.PermutationAndCombination
{
    class ShuffleAList
    {
        /// <summary>
        /// This is the fisher yates algorithm
        /// The running time is O(N)
        /// Assumption: The Random number is calculated in O(1)
        /// </summary>
        /// <param name="listToShuffle"></param>
        /// <returns></returns>
        public static List<int> FisherYatesAlgo(List<int> listToShuffle)
        {
            Random rnd = new Random();
            for(int index = 0; index< listToShuffle.Count; index++)
            {
                int randomIndex = rnd.Next(index, listToShuffle.Count);

                // swap listToShuffle[index] and listToShuffle[randomIndex]
                int temp = listToShuffle[index];
                listToShuffle[index] = listToShuffle[randomIndex];
                listToShuffle[randomIndex] = temp;
            }
            return listToShuffle;
        }

        public static void TestFisherYatesAlgo()
        {
            List<int> listToShuffle = new List<int>();
            for(int index = 0; index<=10; index++)
            {
                listToShuffle.Add(index);
            }
            Console.WriteLine("The list to be shuffled is as follows:");
            PrintList(listToShuffle);

            List<int> listShuffled = FisherYatesAlgo(listToShuffle);

            Console.WriteLine("The shuffled list is as follows:");
            PrintList(listShuffled);
        }

        public static void PrintList(List<int> listToPrint)
        {
            for (int index = 0; index < listToPrint.Count; index++)
            {
                Console.Write(listToPrint[index] + " ");
            }
            Console.WriteLine();

        }
    }
}
