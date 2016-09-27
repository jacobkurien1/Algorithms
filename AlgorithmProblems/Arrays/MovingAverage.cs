using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Arrays
{
    /// <summary>
    /// Get the moving average of the last n numbers in an array.
    /// This question can also be framed as the inputs are streaming and we need to get the
    /// average of the last n numbers at any point in time.
    /// </summary>
    class MovingAverage
    {
        #region Mimics streaming of digits
        public MovingAverage(int[] arr)
        {
            this.arr = arr;
            currentIndex = 0;
        }
        private int currentIndex;
        private int[] arr;

        private int? GetNext()
        {
            if(arr!=null && arr.Length>currentIndex)
            {
                return arr[currentIndex++];
            }
            return null;
        }
        #endregion

        /// <summary>
        /// We keep the last n elements in a circular buffer/array and as the new elements come
        /// we remove the first element and add the new elements in that index.
        /// 
        /// We can also use a queue for this operation. The circular buffer is used to replace a queue.
        /// 
        /// </summary>
        /// <param name="n">size of the sliding window</param>
        public void GetMovingAvg(int n)
        {
            
            int[] arrForCurrentAvg = new int[n]; // this is a circular buffer
            int arrIndex = 0; // index of the buffer where the new elements needs to be added
            int numOfElementSeen = 0; // keeps track of the number of elements seen till now
            float currentSum = 0; // keeps track of the sum of all elements in the buffer

            int? currentNum = GetNext();
            while (currentNum != null)
            {
                if (numOfElementSeen > n)
                {
                    // we need to remove the element before added the new value here
                    currentSum -= arrForCurrentAvg[arrIndex];
                }
                numOfElementSeen++;
                arrForCurrentAvg[arrIndex] = (int)currentNum;
                arrIndex = (arrIndex + 1) % n;
                currentSum += (int)currentNum;

                if (numOfElementSeen >= n)
                {
                    // we can print the average of last n numbers only when we have seen atleast n numbers
                    Console.WriteLine("The moving avg is: {0}", currentSum / n);
                }
                currentNum = GetNext();
            }
        }
        public static void TestMovingAverage()
        {
            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            MovingAverage ma = new MovingAverage(arr);
            ma.GetMovingAvg(3);
        }
    }
}
