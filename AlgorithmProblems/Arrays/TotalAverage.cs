using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Arrays
{
    /// <summary>
    /// When we are streaming numbers, find the total average of the numbers encountered
    /// </summary>
    class TotalAverage
    {
        #region Mimics streaming of digits
        public TotalAverage(int[] arr)
        {
            this.arr = arr;
            currentIndex = 0;
        }
        private int currentIndex;
        private int[] arr;

        private int? GetNext()
        {
            if (arr != null && arr.Length > currentIndex)
            {
                return arr[currentIndex++];
            }
            return null;
        }
        #endregion

        /// <summary>
        /// get the average while streaming int values
        /// </summary>
        public void GetAverageWhileStreaming()
        {
            float previousAvg = 0;
            int countOfNumTillNow = 0;
            int? currentNum = GetNext();
            while (currentNum != null)
            {
                previousAvg = (previousAvg * countOfNumTillNow + (int)currentNum) / (++countOfNumTillNow);
                Console.WriteLine("The total avg till {0} elements is: {1}", countOfNumTillNow, previousAvg);
                currentNum = GetNext();
            }
        }

        public static void TestTotalAverage()
        {
            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            TotalAverage ta = new TotalAverage(arr);
            ta.GetAverageWhileStreaming();
        }
    }
}
