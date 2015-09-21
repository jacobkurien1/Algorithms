using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Arrays
{
    /// <summary>
    /// Majority element in an array is the element that occurs more than n/2 times in the array.
    /// </summary>
    class MajorityElement
    {
        /// <summary>
        /// We can have 2 loops, the inner loop counts the number of occurance of each element in the array
        /// We select the majority element.
        /// This Algo has time complexity of O(n^2)
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int FindMajorityElementAlgo1(int[] array)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sort the Array.
        /// Count the majority element
        /// Running time is O(nlog(n))
        /// But the initial array is also modified
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int FindMajorityElementAlgo2(int[] array)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// We can use the Boyer Moore Vote Algorithm to get the majority element
        /// We will need to do 2 passes on the input array
        /// The first pass gets the probable majority element
        /// In the second pass count the number of occurance of this element and make a decision whether this is majority element
        /// The running time is O(n)
        /// and the space requirement is O(1)
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int? FindMajorityElementBoyerMooreVoteAlgo(int[] array)
        {
            int? majorityElem = null;
            int count = 0;
            // The first pass gets the probable majority element
            for(int i=0; i<array.Length; i++)
            {
                if(count==0)
                {
                    majorityElem = array[i];
                    count++;
                }
                else
                {
                    if(majorityElem == array[i])
                    {
                        count++;
                    }
                    else
                    {
                        count--;
                    }
                }
            }
            count =0;
            // In the second pass count the number of occurance of this element and make a decision whether this is majority element
            for (int i = 0; i < array.Length; i++)
            {
                if(array[i] == majorityElem)
                {
                    count++;
                }
            }

            if(count>array.Length/2)
            {
                return majorityElem;
            }
            else
            {
                return null;
            }
        }

        public static void TestFindMajorityElement()
        {
            Console.WriteLine("Need to find the majority element in the following array:");
            int[] array = new int[6]{4,4,2,4,3,4};
            ArrayHelper.PrintArray(array);
            int? majorityVal = FindMajorityElementBoyerMooreVoteAlgo(array);
            Console.WriteLine("The majority element is {0}", majorityVal);

            Console.WriteLine("Need to find the majority element in the following array:");
            array = new int[6] { 4, 4, 2, 4, 3, 1 };
            ArrayHelper.PrintArray(array);
            majorityVal = FindMajorityElementBoyerMooreVoteAlgo(array);
            Console.WriteLine("The majority element is {0}", majorityVal);
        }
    }
}
