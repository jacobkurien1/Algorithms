using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Arrays.ArraysHelper
{
    class ArrayHelper
    {
        public static int[] CreateArray(int size)
        {
            return CreateArray(size,0, 100);
        }
        public static int[] CreateArray(int size, int maxVal)
        {
            return CreateArray(size,0,maxVal);
        }

        public static int[] CreateArray(int size, int minVal, int maxVal)
        {
            Random rnd = new Random();
            int[] retArr = new int[size];
            for (int i = 0; i < size; i++)
            {
                retArr[i] = rnd.Next(minVal, maxVal);
            }
            return retArr;
        }

        public static void PrintArray(int[] arr)
        {
            for(int i=0; i < arr.Length; i++)
            {
                Console.Write(arr[i]+" ");
            }
            Console.WriteLine();
        }
    }

    
}
