using AlgorithmProblems.Heaps.HeapHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.matrix_problems
{
    /// <summary>
    /// Given a matrix with all the rows sorted. Find the minimum range such that the range contains elements from each row
    /// 
    /// For eg: for the following matrix
    /// [[1 1000 2000]
    /// [20 1001 5000]
    /// [55 1002 222222]]
    /// The min range is 1000 - 1002
    /// </summary>
    class MinRangeInMatrix
    {
        /// <summary>
        /// Keep an index array which will have index to the current pointer for each matrix rows.
        /// If the matrix has k rows, Maintain a minheap of size k to get the minimum. Also keep the maxValue
        /// in a memory location and keep updating it as we add new elements in the minheap.
        /// 
        /// Keep Extracting the min value from minheap and increase the pointer in the row where we get the minValue to point to the next element
        /// Add this element into the minheap and get the new min.
        /// During this operation, keep checking whether current range is less than the minRange. If yes, update minRange.
        /// Keep doing this till one of the rows in indexArr hits the end of the row. And return the minRange.
        /// 
        /// The running time of this operation is O(nlog(k)) 
        /// where n is the total number of elements in the matrix and k is the number of rows of the matrix
        /// Use of minheap reduces the running time to O(nlog(k))
        /// 
        /// The space requirement is O(k) to maintain the arrIndex array.
        /// 
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public Range GetMinRange(int[,] mat)
        {
            int[] indexArr = new int[mat.GetLength(0)];
            int maxVal = int.MinValue;
            Range minRange = null;

            PriorityQueue<int> pq = new PriorityQueue<int>(mat.GetLength(0));
            // Add the least element (at index 0) from each row into the priority queue
            for(int i=0; i<mat.GetLength(0); i++)
            {
                if(mat[i,0]>maxVal)
                {
                    maxVal = mat[i, 0];
                }
                pq.Insert(i, mat[i, 0]);
            }

            while (pq.Count == mat.GetLength(0))
            {
                // Extract the min and check if a min Range is reached.
                int rowIndex = pq.ExtractMin();
                Range currentRange = new Range(mat[rowIndex, indexArr[rowIndex]], maxVal);
                if (minRange == null || minRange.CompareTo(currentRange) > 0)
                {
                    minRange = currentRange;
                }

                indexArr[rowIndex] += 1;
                if(indexArr[rowIndex] >= mat.GetLength(1))
                {
                    // this condition means that we have reached at the end of one of the rows
                    // No need to check more, as we need elements from all the rows
                    break;
                }

                // Add the next min element in the priority queue and update the maxVal
                pq.Insert(rowIndex, mat[rowIndex, indexArr[rowIndex]]);
                if(mat[rowIndex, indexArr[rowIndex]]> maxVal)
                {
                    maxVal = mat[rowIndex, indexArr[rowIndex]];
                }
            }

            return minRange;
        }

        /// <summary>
        /// Represents the range object
        /// </summary>
        public class Range : IComparable
        {
            public int Min { get; set; }
            public int Max { get; set; }
            public Range(int min, int max)
            {
                Min = min;
                Max = max;
            }
            public override string ToString()
            {
                return string.Format("({0}, {1})", Min, Max);
            }

            public int CompareTo(object obj)
            {
                Range newObj = (Range)obj;
                return (Max - Min).CompareTo(newObj.Max - newObj.Min);
            }
        }

        #region TestArea
        public static void TestMinRangeInMatrix()
        {
            int[,] mat = new int[,]
            {
                { 1, 1000, 2000 },
                { 20, 1001, 5000},
                { 55, 1002, 222222}
            };
            MinRangeInMatrix r = new MinRangeInMatrix();
            Range minRange = r.GetMinRange(mat);
            Console.WriteLine("The min range is {0}", minRange);

            mat = new int[,]
            {
                { 1, 1000, 2000 },
                { 20, 1001, 5000},
                { 55, 1100, 222222}
            };
            minRange = r.GetMinRange(mat);
            Console.WriteLine("The min range is {0}", minRange);

            mat = new int[,]
            {
                { 1, 1000, 20001 },
                { 20, 1001, 20000 },
                { 55, 1100, 20002 }
            };
            minRange = r.GetMinRange(mat);
            Console.WriteLine("The min range is {0}", minRange);

            mat = new int[,]
            {
                { 1, 2, 3 },
                { 20, 1001, 20000 },
                { 55, 1100, 20002 }
            };
            minRange = r.GetMinRange(mat);
            Console.WriteLine("The min range is {0}", minRange);
        }
        #endregion
    }
}
