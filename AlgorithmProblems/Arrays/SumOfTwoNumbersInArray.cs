using AlgorithmProblems.Arrays.ArraysHelper;
using AlgorithmProblems.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Arrays
{
    /// <summary>
    /// Given an array, find all the elements which sum up to a particular sum
    /// </summary>
    public class SumOfTwoNumbersInArray
    {
        /// <summary>
        /// We will sort the array
        /// Have a startPointer and endPointer.
        /// if(arr[startPointer]+arr[endPointer])>sum, decrement endPointer
        /// if(arr[startPointer]+arr[endPointer])<sum, increment startPointer
        /// if(arr[startPointer]+arr[endPointer])==sum, add this in list and decrement endPtr and increment startPtr
        ///     
        /// Running time  = O(nlogn) for sorting
        /// Space = O(1)
        /// </summary>
        /// <param name="arr">array from which the sum needs to be calculated</param>
        /// <param name="sum">sum value</param>
        /// <returns></returns>
        public static List<ArrayPair> GetIndiciesWhenSumIsFoundAlgo1(int[] arr, int sum)
        {
            List<ArrayPair> ret = new List<ArrayPair>();

            // Sort the array
            HeapSort<int> hs = new HeapSort<int>(arr);
            arr = hs.HeapArray;

            int startPointer = 0;
            int endPointer = arr.Length-1;
            while(startPointer<endPointer)
            {
                if(arr[startPointer]+arr[endPointer]>sum)
                {
                    endPointer--;
                }
                else if (arr[startPointer]+arr[endPointer]<sum)
                {
                    startPointer++;
                }
                else
                {
                    //arr[startPointer]+arr[endPointer]==sum
                    ret.Add(new ArrayPair(arr[startPointer], arr[endPointer]));
                    startPointer++;
                    endPointer--;
                }
            }

            return ret;
        }

        /// <summary>
        /// We can use a dictionary to keep track of all the sum - arr[i]
        /// and do a O(1) lookup to check whether that value is present.
        /// 
        /// This makes the algo run at O(n)
        /// The space complexity is O(n)
        /// 
        /// </summary>
        /// <param name="arr">array from which the sum needs to be calculated</param>
        /// <param name="sum">sum value</param>
        /// <returns>list of array value pairs, where addition of each pair yields sum</returns>
        public static List<ArrayPair> GetIndiciesWhenSumIsFoundAlgo2(int[] arr, int sum)
        {
            List<ArrayPair> ret = new List<ArrayPair>();

            Dictionary<int,bool> dict = new Dictionary<int,bool>();
            for (int i = 0; i < arr.Length;i++ )
            {
                if(dict.ContainsKey(sum - arr[i]))
                {
                    ret.Add(new ArrayPair(sum - arr[i], arr[i]));
                }
                else
                {
                    dict[arr[i]] = true;
                }
            }

            return ret;
        }

        public static void TestSumOfTwoNumbersInArray()
        {
            int[] arr = new int[]{2,5,2,6,3,5,6,2,5,1,0,9,3};
            Console.WriteLine("The input array is as shown below");
            ArrayHelper.PrintArray(arr);
            List<ArrayPair> list = GetIndiciesWhenSumIsFoundAlgo1(arr, 5);
            Console.WriteLine("Algo1: The array value pairs which sum upto 5 are as shown below");
            PrintArrayValuePairs(list);

            list = GetIndiciesWhenSumIsFoundAlgo2(arr, 5);
            Console.WriteLine("Algo2: The array value pairs which sum upto 5 are as shown below");
            PrintArrayValuePairs(list);
        }
        private static void PrintArrayValuePairs(List<ArrayPair> list)
        {
            foreach(ArrayPair arrIndexPair in list)
            {
                Console.WriteLine(arrIndexPair.ToString());
            }
        }
    }

    /// <summary>
    /// this class object will be used to represent a pair of 2 element in the array
    /// </summary>
    public class ArrayPair
    {
        public ArrayPair (int element1, int element2)
	    {
            Element1 = element1;
            Element2 = element2;
	    }
        public int Element1 { get; set; }
        public int Element2 { get; set; }
        public override string ToString()
        {
            return string.Format("{0},{1}",Element1,Element2);
        }
    }
}
