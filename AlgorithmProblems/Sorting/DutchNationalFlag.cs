using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Sorting
{
    /// <summary>
    /// This is a variation of the dutch national flag problem.
    /// Given an array having values 0, 1, 2 sort the array.
    /// </summary>
    class DutchNationalFlag
    {
        /// <summary>
        /// We need to sort the array arr which contains only 0,1, and 2's
        /// We can do this using 3 pointer
        /// 
        /// 000000111111<0,1,2>2222222
        ///      |     |	   |
        /// 	left    mid     right
        /// 
        /// arr[0,left-1] - all 0's
        /// arr[left, mid-1] - all 1's
        /// arr[mid, right] - unknowns
        /// arr[right+1, arr.Length-1] - 2's 
        /// 
        /// 
        /// The running time for this algo is O(n)
        /// The space requirement is O(1)
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private int[] Sort(int[] arr)
        {
            int left = 0;
            int mid = 0;
            int right = arr.Length - 1;

            while(right > mid)
            {
                if(arr[mid] == 0)
                {
                    Swap(arr, mid, left);
                    left++;
                    mid++;
                }
                else if (arr[mid] == 1)
                {
                    mid++;
                }
                else
                {
                    //arr[mid] == 2
                    Swap(arr, mid, right);
                    right--;
                }
            }
            return arr;
        }

        private void Swap(int[] arr, int index1, int index2)
        {
            int temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;
        }

        public static void TestDutchNationalFlag()
        {
            DutchNationalFlag dnf = new DutchNationalFlag();
            int[] arr = ArrayHelper.CreateArray(10, 0, 3);
            Console.WriteLine("The unsorted array is as shown below:");
            ArrayHelper.PrintArray(arr);
            arr = dnf.Sort(arr);
            Console.WriteLine("The sorted array is as shown below:");
            ArrayHelper.PrintArray(arr);

            arr = ArrayHelper.CreateArray(10, 0, 3);
            Console.WriteLine("The unsorted array is as shown below:");
            ArrayHelper.PrintArray(arr);
            arr = dnf.Sort(arr);
            Console.WriteLine("The sorted array is as shown below:");
            ArrayHelper.PrintArray(arr);

            arr = ArrayHelper.CreateArray(10, 0, 3);
            Console.WriteLine("The unsorted array is as shown below:");
            ArrayHelper.PrintArray(arr);
            arr = dnf.Sort(arr);
            Console.WriteLine("The sorted array is as shown below:");
            ArrayHelper.PrintArray(arr);
        }

        
    }
}
