using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Arrays
{
    /// <summary>
    /// Given a number in the form of an array. For eg an array 1,2,3,4 means a number 1234.
    /// Increment this number by 1 and send the result back in the array form.
    /// </summary>
    class ArrayOfNumsIncrement
    {
        public static int[] IncrementArrayOfNumbers(int[] arrayOfNums)
        {
            int carry = 0;
            for(int i= arrayOfNums.Length-1; i>=0; i--)
            {
                arrayOfNums[i] += (i == arrayOfNums.Length - 1)?(carry + 1):carry;
                if(arrayOfNums[i] >= 10)
                {
                    carry = 1;
                    arrayOfNums[i] %= 10;
                }
                else
                {
                    carry = 0;
                    break;
                }
            }

            if(carry ==1)
            {
                // We need to create a new array in this case as the result cannot be contained in the initial arry
                // for eg 999+1 = 1000
                int[] retArray = new int[arrayOfNums.Length + 1];
                retArray[0] = carry;
                arrayOfNums.CopyTo(retArray, 1);
                return retArray;
            }
            return arrayOfNums;
        }

        public static void TestIncrementArrayOfNumbers()
        {
            int[] arrayOfNums = new int[4] { 1, 2, 3, 4 };
            ArrayHelper.PrintArray(arrayOfNums);
            Console.WriteLine("the incremented array is:");
            ArrayHelper.PrintArray(IncrementArrayOfNumbers(arrayOfNums));

            arrayOfNums = new int[4] { 9,9,9,9 };
            ArrayHelper.PrintArray(arrayOfNums);
            Console.WriteLine("the incremented array is:");
            ArrayHelper.PrintArray(IncrementArrayOfNumbers(arrayOfNums));
        }
    }
}
