using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Sorting
{
    public class RadixSort
    {
        /// <summary>
        /// Counting sort is a good way to achieve linear running time.
        /// But if the range is n^2 then running time also becomes n^2
        /// We can use Radix Sort to achieve a better running time
        /// 
        /// Steps: 1. Get the maximum value of digits that needs to be considered for all elements from the inputArr
        /// 2. Do the count sort for all digitpositions starting from the LSB
        /// 3. if the array has integers<0 then we need to reverse that part of the array
        /// 
        /// The running time of Radix Sort is O(d*(n+b)), where d is the max number of digits that needs to be considered
        /// and b is the base in which all the numbers in inputArr is defined, and n is the size of inputArr
        /// d = logb(K) where K is the max possible value
        /// so running time is O((n+b)logb(K)) 
        /// K<=n^c where c is const
        /// so running time is O(nlogb(n)). This becomes linear when n==b
        /// so if we set the base as n, we get the time complexity as O(n)
        /// </summary>
        /// <param name="inputArr"></param>
        /// <returns></returns>
        public int[] Sort(int[] inputArr)
        {
            // Get the maximum value of digits that needs to be considered for all elements from the inputArr
            int maxVal = GetMaxDigits(inputArr);

            // Do the count sort for all digitpositions starting from the LSB
            for(int i = 0; i<maxVal; i++)
            {
                inputArr = CountingSortSubroutine(inputArr, i);
            }

            // if the array has integers<0 then we need to reverse that part of the array
            int maxNegIndex = 0;
            for(int i =0; i<inputArr.Length; i++)
            {
                if(inputArr[i]>=0)
                {
                    break;
                }
                maxNegIndex = i;
            }
            for(int i=0; i<=Math.Floor(maxNegIndex/2.0); i++)
            {
                int temp = inputArr[i];
                inputArr[i] = inputArr[maxNegIndex - i];
                inputArr[maxNegIndex - i] = temp;
            }

            return inputArr;
        }

        /// <summary>
        /// This is the CountingSort subroutine
        /// Here we specify the digit position where 0 -> LSB
        /// and all the elements in the array will be sorted based on the digit at this position
        /// </summary>
        /// <param name="inputArr">inputArr which needs to be sorted based on the digit at digitPosition</param>
        /// <param name="digitPosition"></param>
        /// <returns></returns>
        private int[] CountingSortSubroutine(int[] inputArr, int digitPosition)
        {
            if (inputArr == null || inputArr.Length == 0)
            {
                // Error check
                throw new ArgumentException("the input array is empty");
            }
            //The range is from -1 to 9
            int min = -1;
            int max = 9;
            //2. Create an array(countArr) of size equal to the range of the elements in the input array
            int[] countArr = new int[max - min + 1];
            for (int i = 0; i < inputArr.Length; i++)
            {
                //3. Whenever we encounter an element in the inputarray increment the element in countArr at the correct index
                int intVal = GetElementAtPosition(inputArr[i], digitPosition);
                ++countArr[intVal - (min)];
            }

            //4. Do a cummulative addition on the whole countArr
            for (int i = 1; i < countArr.Length; i++)
            {
                countArr[i] += countArr[i - 1];
            }
            //5. After that scan the input array and foreach element find the value in countArr(which will be the index in the sorted arr)
            int[] sortedArr = new int[inputArr.Length];
            for (int i = inputArr.Length-1; i >=0; i--)
            {
                int intVal = GetElementAtPosition(inputArr[i], digitPosition);
                if (countArr[intVal - min] != 0)
                {
                    sortedArr[countArr[intVal - (min)] - 1] = inputArr[i];
                    //6. Decrement the count of the element after adding it to the sorted array
                    --countArr[intVal - (min)];
                }
                else
                {
                    // this is not a valid countArr
                    throw new Exception("this is not a valid countArr");
                }
            }
            return sortedArr;
        }

        /// <summary>
        /// Returns the value in the integer at the position calculated from the LSB
        /// </summary>
        /// <param name="val"></param>
        /// <param name="position">position calculated from the LSB</param>
        /// <returns>Returns the value in the integer at the position calculated from the LSB</returns>
        private int GetElementAtPosition(int val, int position)
        {
            string value = val.ToString();
            char charToReturn;
            if (position <= value.Length-1)
            {
                charToReturn = value[value.Length-1 - position];
            }
            else
            {
                // if the position for which we need to find the character is out of bounds
                // then we should return '-' if the integer is less than 0 or else
                // '0' should be returned of the integer is greater than 0
                if(value[0] == '-')
                {
                    charToReturn = '-';
                }
                else
                {
                    charToReturn = '0';
                }
            }

            return (charToReturn == '-') ? -1 : int.Parse(charToReturn.ToString());
        }

        /// <summary>
        /// Gets the maximum digits which needs to be taken into account
        /// for example if the max value in the array is 598 hence the max digits is 3
        /// we also need to take care of the min value in array
        /// if -598 is the min value in the array then the max digits is 4
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private int GetMaxDigits(int[] arr)
        {
            int maxValue = 0;
            int minValue = 0;
            for(int i=0; i<arr.Length; i++)
            {
                if (maxValue < arr[i])
                {
                    maxValue = arr[i];
                }
                if(minValue > arr[i])
                {
                    minValue = arr[i];
                }
            }
            return (minValue.ToString().Length>maxValue.ToString().Length)? minValue.ToString().Length: maxValue.ToString().Length;
        }
        public static void TestRadixSort()
        {
            RadixSort rs = new RadixSort();
            int[] arr = ArrayHelper.CreateArray(10, 10);
            Console.WriteLine("The unsorted array is as shown below:");
            ArrayHelper.PrintArray(arr);
            arr = rs.Sort(arr);
            Console.WriteLine("The sorted array is as shown below:");
            ArrayHelper.PrintArray(arr);

            arr = ArrayHelper.CreateArray(10, -5, 10);
            Console.WriteLine("The unsorted array is as shown below:");
            ArrayHelper.PrintArray(arr);
            arr = rs.Sort(arr);
            Console.WriteLine("The sorted array is as shown below:");
            ArrayHelper.PrintArray(arr);

            arr = ArrayHelper.CreateArray(10, -50, 100);
            Console.WriteLine("The unsorted array is as shown below:");
            ArrayHelper.PrintArray(arr);
            arr = rs.Sort(arr);
            Console.WriteLine("The sorted array is as shown below:");
            ArrayHelper.PrintArray(arr);
        }
    }
}
