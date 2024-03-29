﻿using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Sorting
{
    class CountingSort
    {
        /// <summary>
        /// 1. we first need to get the range of elements in the input array
        /// 2. Create an array(countArr) of size equal to the range of the elements in the input array
        /// 3. Whenever we encounter an element in the inputarray increment the element in countArr at the correct index
        /// 4. Do a cummulative addition on the whole countArr
        /// 5. After that scan the input array and foreach element find the value in countArr(which will be the index in the sorted arr)
        /// 6. Decrement the count of the element after adding it to the sorted array
        /// 
        /// The running time for this algo is O(n+k) where k is the range.
        /// The space requirement is also O(n+k)
        /// We should be using this algorithm when k<<n
        /// 
        /// This is not a comparison based sorting, hence its running time is O(n) with range proportional to n
        /// This algo is not stable and online
        /// </summary>
        /// <param name="inputArr"></param>
        /// <returns></returns>
        public static int[] Sort(int[] inputArr)
        {
            if(inputArr == null || inputArr.Length == 0)
            {
                // Error check
                throw new ArgumentException("the input array is empty");
            }
            //1. we first need to get the range of elements in the input array
            int min = inputArr[0];
            int max = inputArr[0];
            for(int i = 1; i<inputArr.Length; i++)
            {
                if(inputArr[i]<min)
                {
                    min = inputArr[i];
                }
                if(inputArr[i]>max)
                {
                    max = inputArr[i];
                }
            }

            //2. Create an array(countArr) of size equal to the range of the elements in the input array
            int[] countArr = new int[max - min + 1];
            for(int i = 0; i<inputArr.Length; i++)
            {
                //3. Whenever we encounter an element in the inputarray increment the element in countArr at the correct index
                ++countArr[inputArr[i] - (min)];
            }

            //4. Do a cummulative addition on the whole countArr
            for(int i=1; i<countArr.Length; i++)
            {
                countArr[i] += countArr[i - 1];
            }
            //5. After that scan the input array and foreach element find the value in countArr(which will be the index in the sorted arr)
            int[] sortedArr = new int[inputArr.Length];
            for(int i= 0; i<inputArr.Length; i++)
            {
                if (countArr[inputArr[i] - min] != 0)
                {
                    sortedArr[countArr[inputArr[i] - (min)]-1] = inputArr[i];
                    //6. Decrement the count of the element after adding it to the sorted array
                    --countArr[inputArr[i] - (min)];
                }
                else
                {
                    // this is not a valid countArr
                    throw new Exception("this is not a valid countArr");
                }
            }
            return sortedArr;
        }
        public static void TestSorting()
        {
            int[] arr = ArrayHelper.CreateArray(10, 10);
            Console.WriteLine("The unsorted array is as shown below:");
            ArrayHelper.PrintArray(arr);
            arr = Sort(arr);
            Console.WriteLine("The sorted array is as shown below:");
            ArrayHelper.PrintArray(arr);

            arr = ArrayHelper.CreateArray(10, -5, 10);
            Console.WriteLine("The unsorted array is as shown below:");
            ArrayHelper.PrintArray(arr);
            arr = Sort(arr);
            Console.WriteLine("The sorted array is as shown below:");
            ArrayHelper.PrintArray(arr);
        }
    }
}
