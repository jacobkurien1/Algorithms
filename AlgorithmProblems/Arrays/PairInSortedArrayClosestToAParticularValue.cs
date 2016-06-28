using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Arrays
{
    /// <summary>
    /// Given a sorted array and a ParticularValue x, find a pair of elements in the array 
    /// whose sum is closest to x
    /// </summary>
    class PairInSortedArrayClosestToAParticularValue
    {
        /// <summary>
        /// if(Math.Abs(arr[leftIndex]+arr[rightIndex] - x) < diff) we have the new pair closer to x
        /// Check whether arr[leftIndex] + arr[rightIndex] >x , if true decrement the rightIndex
        /// and if false increment the leftIndex
        /// </summary>
        /// <param name="arr">sorted array</param>
        /// <param name="x"></param>
        /// <returns></returns>
        public Pair GetPairClosestToX(int[] arr, int x)
        {
            Pair retVal = null;
            int leftIndex = 0;
            int rightIndex = arr.Length - 1;
            int diff = int.MaxValue;
            while(leftIndex<rightIndex)
            {
                if(Math.Abs(arr[leftIndex]+arr[rightIndex] - x) < diff)
                {
                    diff = Math.Abs(arr[leftIndex] + arr[rightIndex] - x);
                    retVal = new Pair(arr[leftIndex], arr[rightIndex]);
                }
                if(arr[leftIndex] + arr[rightIndex] >x)
                {
                    // move to a lower value
                    rightIndex--;
                }
                else
                {
                    //move to a larger value
                    leftIndex++;
                }
            }
            return retVal;
        }

        /// <summary>
        /// Class to represent the pair of 2 numbers from the array
        /// </summary>
        public class Pair
        {
            public int FirstElement { get; set; }
            public int SecondElement { get; set; }
            public Pair(int firstElem, int secondElem)
            {
                FirstElement = firstElem;
                SecondElement = secondElem;
            }
            public override string ToString()
            {
                return FirstElement.ToString() + " " + SecondElement.ToString();
            }

        }
        public static void TestPairInSortedArrayClosestToAParticularValue()
        {
            int[] arr = new int[] { 1,2,5,6,9,10,21 };
            Pair pair = new PairInSortedArrayClosestToAParticularValue().GetPairClosestToX(arr,20);
            Console.WriteLine("The input array is as shown below");
            ArrayHelper.PrintArray(arr);
            Console.WriteLine("The pair of element closest to {0} are {1}", 20, pair.ToString());

            pair = new PairInSortedArrayClosestToAParticularValue().GetPairClosestToX(arr, 60);
            Console.WriteLine("The input array is as shown below");
            ArrayHelper.PrintArray(arr);
            Console.WriteLine("The pair of element closest to {0} are {1}", 60, pair.ToString());

            pair = new PairInSortedArrayClosestToAParticularValue().GetPairClosestToX(arr, -1);
            Console.WriteLine("The input array is as shown below");
            ArrayHelper.PrintArray(arr);
            Console.WriteLine("The pair of element closest to {0} are {1}", -1, pair.ToString());

            arr = new int[] { -51,-21,-9,-6,-3,-1 };
            pair = new PairInSortedArrayClosestToAParticularValue().GetPairClosestToX(arr, -10);
            Console.WriteLine("The input array is as shown below");
            ArrayHelper.PrintArray(arr);
            Console.WriteLine("The pair of element closest to {0} are {1}", -10, pair.ToString());

        }
    }
}
