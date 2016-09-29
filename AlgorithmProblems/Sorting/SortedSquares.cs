using AlgorithmProblems.Arrays.ArraysHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Sorting
{
    /// <summary>
    /// Given a sorted integer array, return a sorted array of the squares.
    /// The input sorted array can have negative integers.
    /// </summary>
    class SortedSquares
    {
        /// <summary>
        /// Algo: Take the square of the elements in arr. 
        /// 2. Get the pivot point such that all elements arr[pivot->arr.Length-1] are sorted in ascending order
        ///     and all elements arr[0, pivot-1] are sorted in descending order
        /// 3. if pivot point is not found the array is already sorted
        /// 4. Else do a merge operation in mergesort and the 2 array segment will be in ascending and descending order.
        /// 
        /// The running time for this is O(n)
        /// and the space requirement is O(n)
        /// </summary>
        /// <param name="arr">sorted integer array</param>
        /// <returns>sorted squares array of the input integer array</returns>
        public int[] GetSquaresArray(int[] arr)
        {
            int[] squaresArr = new int[arr.Length];
            int sqArrIndex = 0;
            int pivotIndex = -1;

            // Square the input array and get the pivot point
            for(int i=0; i<arr.Length; i++)
            {
                arr[i] = arr[i] * arr[i];
                if(i-1>=0 && arr[i]<arr[i-1])
                {
                    // this is the pivot point, all elements arr[pivot->arr.Length-1] are sorted in ascending order
                    // and all elements arr[0, pivot-1] are sorted in descending order
                    pivotIndex = i;
                }
            }

            if(pivotIndex == -1)
            {
                // the arr is already sorted in ascending order
                return arr;
            }

            int decSt = pivotIndex - 1;
            int incSt = pivotIndex;
            while (decSt >= 0 && incSt < arr.Length)
            {
                if(arr[decSt] < arr[incSt])
                {
                    squaresArr[sqArrIndex++] = arr[decSt--];
                }
                else
                {
                    squaresArr[sqArrIndex++] = arr[incSt++];
                }
            }

            // now add all the element left in the decreasing array segment
            while(decSt>=0)
            {
                squaresArr[sqArrIndex++] = arr[decSt--];
            }

            // now add all the elements left in the increasing array segment
            while(incSt <arr.Length)
            {
                squaresArr[sqArrIndex++] = arr[incSt++];
            }

            return squaresArr;
        }

        #region TestArea
        public static void TestSortedSquares()
        {
            SortedSquares ss = new SortedSquares();
            int[] arr = new int[] { -9, -8, -5, -1, 1, 3, 4, 6 };
            Console.WriteLine("The input arr is as shown below");
            ArrayHelper.PrintArray(arr);
            Console.WriteLine("The sorted squares arr is as shown below");
            ArrayHelper.PrintArray(ss.GetSquaresArray(arr));

            arr = new int[] { -1, -1, 1, 3, 4, 6 };
            Console.WriteLine("The input arr is as shown below");
            ArrayHelper.PrintArray(arr);
            Console.WriteLine("The sorted squares arr is as shown below");
            ArrayHelper.PrintArray(ss.GetSquaresArray(arr));
        }
        #endregion
    }
}
