using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Arrays
{
    public class SumOfThreeNumbersInArray
    {

        /// <summary>
        /// Get all tuples from the array which sum to the paramter sum
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="sum"></param>
        public static List<ArrayTuple> GetAllTuples(int[] arr, int sum)
        {
            List<ArrayTuple> AllCombinations = new List<ArrayTuple>();
            for (int i = 0; i < arr.Length - 2; i++)
            {
                var arrSegment = new ArraySegment<int>(arr, i + 1, arr.Length - (i + 1));//ArraySegment is lightweight and does not copy the array
                List<ArrayPair> arrayValuePair = SumOfTwoNumbersInArray.GetIndiciesWhenSumIsFoundAlgo3(arrSegment.ToArray(), sum-arr[i]);
                foreach (ArrayPair pair in arrayValuePair)
                {
                    AllCombinations.Add(new ArrayTuple(pair.Element1, pair.Element2, arr[i]));
                }
            }
            return AllCombinations;

        }

        
        public static void TestSumOfThreeNumbersInArray()
        {
            int[] arr = new int[] { 1,4,0,2,1,3,6,2,0,9,5 };
            Console.WriteLine("The input array is as shown below");
            ArrayHelper.PrintArray(arr);
            List<ArrayTuple> list = GetAllTuples(arr, 5);
            Console.WriteLine("The array tuples which sum upto 5 are as shown below");
            PrintArrayTuple(list);

            arr = new int[] { 1, 4, 0, 2, 1, 3, 6, 2, 0, 9, 5, -1 };
            Console.WriteLine("The input array is as shown below");
            ArrayHelper.PrintArray(arr);
            list = GetAllTuples(arr, 5);
            Console.WriteLine("The array tuples which sum upto 5 are as shown below");
            PrintArrayTuple(list);
        }
        private static void PrintArrayTuple(List<ArrayTuple> list)
        {
            foreach (ArrayTuple tuple in list)
            {
                Console.WriteLine(tuple.ToString());
            }
        }
    }

    public class ArrayTuple
    {
        public int Element1 { get; set; }
        public int Element2 { get; set; }
        public int Element3 { get; set; }
        public ArrayTuple(int element1, int element2, int element3)
        {
            Element1 = element1;
            Element2 = element2;
            Element3 = element3;
        }
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", Element1, Element2, Element3);
        }
    }
}
