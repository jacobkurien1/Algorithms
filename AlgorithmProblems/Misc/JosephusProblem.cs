using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Misc
{
    /// <summary>
    /// This is a theoretical problem, where n people are standing in a circle. The counting begins at some point and kth person is killed.
    /// The counting continues from the next person and the kth person is killed again. And the killing goes on till 1 person is left.
    /// The last person standing is given his freedom.
    /// 
    /// The objective here is if the num of people are given and the k is known, position yourself in such a way that you will get freedom.
    /// </summary>
    class JosephusProblem
    {
        /// <summary>
        /// 1. Get all the positions in the order in the list.
        /// 2. Find the kth position and remove the position from the list.
        /// 3. Do step 2 till the number of people left in the list is 1.
        /// </summary>
        /// <param name="numOfPeople"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private static int GetSafePosition(int numOfPeople, int k)
        {
            if(numOfPeople<1)
            {
                throw new ArgumentException("the numOfPeople is invalid");
            }
            int startPoint = 0;
            //1. Get all the positions in the order in the list.
            List<int> allPeople = new List<int>(Enumerable.Range(1, numOfPeople));
            while(allPeople.Count>1)
            {
                //2.Find the kth position and remove the position from the list.
                int indexToKill = (startPoint + k - 1) % allPeople.Count;
                allPeople.RemoveAt(indexToKill);
                startPoint = indexToKill;
            }
            // the last person left will get his freedom.
            return allPeople[0];
        }
        public static void TestJosephusProblem()
        {
            int numOfPeople = 7;
            int k = 3;
            Console.WriteLine("When number of people is {0} and {1}th person gets killed, the safe position is {2}", numOfPeople, k, GetSafePosition(numOfPeople, k));

            numOfPeople = 5;
            k = 2;
            Console.WriteLine("When number of people is {0} and {1}th person gets killed, the safe position is {2}", numOfPeople, k, GetSafePosition(numOfPeople, k));

            numOfPeople = 8;
            k = 3;
            Console.WriteLine("When number of people is {0} and {1}th person gets killed, the safe position is {2}", numOfPeople, k, GetSafePosition(numOfPeople, k));
        }
    }
}
