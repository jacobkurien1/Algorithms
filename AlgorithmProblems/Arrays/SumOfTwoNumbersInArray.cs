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

        /// <summary>
        /// Instead of using a dictionary(as shown in algo2), we can use an int[] if we know the range of numbers in the arr.
        /// each bit in the int[] will tell us whether this number was present in the array or not.
        /// So the space requirement will drastically reduce to Range/32 ints in the array, since each int can have 32 bits.
        /// 
        /// This will get us the run time of O(n) and almost constant space requirement
        /// </summary>
        /// <param name="arr">array from which the sum needs to be calculated</param>
        /// <param name="sum">sum value</param>
        /// <returns>list of array value pairs, where addition of each pair yields sum</returns>
        public static List<ArrayPair> GetIndiciesWhenSumIsFoundAlgo3(int[] arr, int sum)
        {
            List<ArrayPair> ret = new List<ArrayPair>();

            // We need to find the Range. to do that we need to find the max and min
            int minVal = int.MaxValue;
            int maxVal = int.MinValue;
            for (int i = 0; i < arr.Length; i++ )
            {
                if(arr[i]>maxVal)
                {
                    // get the max value
                    maxVal = arr[i];
                }
                if (arr[i] < minVal)
                {
                    // get the min Value
                    minVal = arr[i];
                }
            }
            //So the number of bits needed are equal to Range
            int Range = maxVal - minVal + 1;//overflow can happen here
            int[] bitDictionary = new int[(int)Math.Ceiling((double)Range / (sizeof(int)*8))];

            for (int i = 0; i < arr.Length; i++)
            {
                if (IsValuePresent(bitDictionary, sum - arr[i]))
                {
                    ret.Add(new ArrayPair(sum - arr[i], arr[i]));
                }
                else
                {
                    SetABit(bitDictionary,arr[i]);
                }
            }
            return ret;
        }

        /// <summary>
        /// This method will find the bit corresponding to value and return whether the bit is set or not
        /// </summary>
        /// <param name="bitDictionary"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsValuePresent(int[] bitDictionary, int value)
        {
            int arrayIndex = value / (sizeof(int) * 8);
            int bitIndex = value % (sizeof(int) * 8);
            int flag = bitDictionary[arrayIndex] & (1 << bitIndex);
            return !(flag == 0);
        }

        /// <summary>
        /// This method sets the bit corresponding to value as 1 in the bitDictionary
        /// </summary>
        /// <param name="bitDictionary"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static void SetABit(int[] bitDictionary, int value)
        {
            int arrayIndex = value / (sizeof(int) * 8);
            int bitIndex = value % (sizeof(int) * 8);
            bitDictionary[arrayIndex] |= (1 << bitIndex);
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

            list = GetIndiciesWhenSumIsFoundAlgo3(arr, 5);
            Console.WriteLine("Algo3: The array value pairs which sum upto 5 are as shown below");
            PrintArrayValuePairs(list);

            list = GetIndiciesWhenSumIsFoundAlgo1(arr, 3);
            Console.WriteLine("Algo1: The array value pairs which sum upto 3 are as shown below");
            PrintArrayValuePairs(list);

            list = GetIndiciesWhenSumIsFoundAlgo2(arr, 3);
            Console.WriteLine("Algo2: The array value pairs which sum upto 3 are as shown below");
            PrintArrayValuePairs(list);

            list = GetIndiciesWhenSumIsFoundAlgo3(arr, 3);
            Console.WriteLine("Algo3: The array value pairs which sum upto 3 are as shown below");
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
