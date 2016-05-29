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
        /// 
        /// </summary>
        /// <param name="inputArr"></param>
        /// <returns></returns>
        public int[] Sort(int[] inputArr)
        {

            return inputArr;
        }

        private int[] CountingSortSubroutine(int[] inputArr, int digitPosition)
        {
            if (inputArr == null || inputArr.Length == 0)
            {
                // Error check
                throw new ArgumentException("the input array is empty");
            }
            //The range is from 0 to 9 and -ve sign needs to be treated differently
            int min = 0;
            int max = 10;
            //2. Create an array(countArr) of size equal to the range of the elements in the input array
            int[] countArr = new int[max - min + 1];
            for (int i = 0; i < inputArr.Length; i++)
            {
                //3. Whenever we encounter an element in the inputarray increment the element in countArr at the correct index
                char digitAtPosition = GetElementAtPosition(inputArr[i], digitPosition);
                int intVal = (digitAtPosition == '-') ? 10 : int.Parse(digitAtPosition.ToString());
                ++countArr[intVal - (min)];
            }

            //4. Do a cummulative addition on the whole countArr
            for (int i = 1; i < countArr.Length; i++)
            {
                countArr[i] += countArr[i - 1];
            }
            //5. After that scan the input array and foreach element find the value in countArr(which will be the index in the sorted arr)
            int[] sortedArr = new int[inputArr.Length];
            for (int i = 0; i < inputArr.Length; i++)
            {
                if (countArr[inputArr[i] - min] != 0)
                {
                    sortedArr[countArr[inputArr[i] - (min)] - 1] = inputArr[i];
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

        /// <summary>
        /// Returns the value in the integer at the position calculated from the LSB
        /// </summary>
        /// <param name="val"></param>
        /// <param name="position">position calculated from the LSB</param>
        /// <returns>Returns the value in the integer at the position calculated from the LSB</returns>
        private char GetElementAtPosition(int val, int position)
        {
            string value = val.ToString();
            return value[position];
        }
        public static void TestRadixSort()
        {

        }
    }
}
