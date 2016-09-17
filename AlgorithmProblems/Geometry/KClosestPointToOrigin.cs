using AlgorithmProblems.Heaps.HeapHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Geometry
{
    /// <summary>
    /// Find the k closest point to the origin.
    /// 
    /// Method1: Sort all the n points and then return the k points from start
    /// this method will have nlog(n) running time and is not very efficient
    /// 
    /// Method 2:Use a max heap of size K and keep adding elements and when the heap is full
    /// Extract the max element and add the new element. After the first pass, all the remaining 
    /// k elements in the max heap is the k points closest to the origin.
    /// The running time of this approach is O(nlog(k))
    /// We will use method 2 when we have a million points and need 100 closest points to the origin.
    /// 
    /// 
    /// Method3:(optimal) Do a quickselect and get the element at kth position in the sorted array(note: the array is partially sorted)
    /// All the elements to the left of the kth position will be the k points closest to origin
    /// The average running time for quick select is O(n). Note the worst case is O(n^2) and depends on the pivot selection
    /// The space requirement is O(1)
    /// </summary>
    class KClosestPointToOrigin
    {
        #region method3
        /// <summary>
        /// Method3:(optimal) Do a quickselect and get the element at kth position in the sorted array(note: the array is partially sorted)
        /// All the elements to the left of the kth position will be the k points closest to origin
        /// The average running time for quick select is O(n). Note the worst case is O(n^2) and depends on the pivot selection
        /// The space requirement is O(1)
        /// </summary>
        /// <param name="allPoints"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public IEnumerable<Point> GetKClosestPoints(Point[] allPoints, int k)
        {
            if(allPoints.Length <k )
            {
                throw new ArgumentException("k is less than the number of elements in array");
            }
            Random rnd = new Random();
            int startIndex = 0;
            int endIndex = allPoints.Length - 1;
            while (startIndex <= endIndex)
            {
                int pivotIndex = rnd.Next(startIndex, endIndex + 1);
                int finalPivotIndex = QuickSelect(allPoints, startIndex, endIndex, pivotIndex);
                if(finalPivotIndex >k-1)
                {
                    endIndex = finalPivotIndex - 1;
                }
                else if( finalPivotIndex <k-1)
                {
                    startIndex = finalPivotIndex + 1;
                }
                else
                {
                    //finalPivotIndex == k-1
                    return allPoints.Take(k);
                }
            }

            return null;
        }

        /// <summary>
        /// Does the quick select algorithm(also used in quick sorting). We take the pivot at the pivot index
        /// and find the finalPivot index which is the index in array where the pivot element should be if the 
        /// array was sorted. All the elements smaller than the pivot element is in the left side of pivot element
        /// and all the elements greater than the pivot element is on the right.
        /// 
        /// T(n) = T(n/2) + O(n)
        /// The avg running time of this algo is O(n).
        /// </summary>
        /// <param name="allPoints">array having all the points</param>
        /// <param name="startIndex">startindex</param>
        /// <param name="endIndex">endIndex</param>
        /// <param name="pivotIndex">index where the current pivot element is present</param>
        /// <returns>index where the pivot element should be present if the array was sorted</returns>
        private int QuickSelect(Point[] allPoints, int startIndex, int endIndex, int pivotIndex)
        {
            SwapInArray(allPoints, startIndex, pivotIndex);
            int currentIndex = startIndex + 1;
            while(currentIndex <= endIndex)
            {
                if(allPoints[currentIndex].CompareTo(allPoints[startIndex]) < 0)
                {
                    // pivot element is greater
                    currentIndex++;
                }
                else
                {
                    // pivot element is smaller
                    SwapInArray(allPoints, currentIndex, endIndex);
                    endIndex--;
                }
            }

            // the final Pivot Index is at endIndex
            SwapInArray(allPoints, startIndex, endIndex);
            return endIndex;
        }

        /// <summary>
        /// This will help to swap the values at 2 indices(indexToSwap1 and indexToSwap2) in array arr.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="indexToSwap1"></param>
        /// <param name="indexToSwap2"></param>
        private static void SwapInArray(Point[] arr, int indexToSwap1, int indexToSwap2)
        {
            Point temp = arr[indexToSwap1];
            arr[indexToSwap1] = arr[indexToSwap2];
            arr[indexToSwap2] = temp;
        }
        #endregion

        #region Method2
        /// <summary>
        /// Method 2:Use a max heap of size K and keep adding elements and when the heap is full
        /// Extract the max element and add the new element. After the first pass, all the remaining 
        /// k elements in the max heap is the k points closest to the origin.
        /// The running time of this approach is O(nlog(k))
        /// We will use method 2 when we have a million points and need 100 closest points to the origin.
        /// </summary>
        /// <param name="allPoints"></param>
        /// <returns></returns>
        public IEnumerable<Point> GetKClosestPointsMethod2(Point[] allPoints, int k)
        {
            if (allPoints.Length < k)
            {
                // Error condition
                throw new ArgumentException("k is less than the number of elements in array");
            }

            // Create a max heap of size k+1
            MaxHeap<Point> mh = new MaxHeap<Point>(k+1);
            for(int i=0; i<allPoints.Length; i++)
            {
                if(mh.HeapSize == k+1)
                {
                    // once the capacity is hit, we need to extract the max value
                    mh.ExtractMax();
                }
                mh.Insert(allPoints[i]);
            }
            // Extract the last max value, now the max heap has k elements
            if (mh.HeapSize == k + 1)
            {
                mh.ExtractMax();
            }

            return mh.HeapArray;
        }
        #endregion

        public class Point : IComparable
        {
            public int XCordinate { get; set; }
            public int YCordinate { get; set; }
            public int Distance { get; set; }

            public Point(int xCordinate, int yCordinate)
            {
                XCordinate = xCordinate;
                YCordinate = yCordinate;
                Distance = GetDistanceFromOrigin();
            }
            private int GetDistanceFromOrigin()
            {
                // Note: Since the distance will be used only for comparison we dont need to do Math.Sqrt(x^2 + y^2) 
                return XCordinate * XCordinate + YCordinate * YCordinate;
            }

            public int CompareTo(object obj)
            {
                Point other = (Point)obj;
                return Distance.CompareTo(other.Distance);
            }

            public override string ToString()
            {
                return string.Format("({0},{1})", XCordinate, YCordinate);
            }
        }

        #region TestArea
        public static void TestKClosestPointToOrigin()
        {
            Point[] allPoints = new Point[]
            {
                new Point(0,0),
                new Point(1,1),
                new Point(0,1),
                new Point(1,0),
                new Point(-1,-1),
                new Point(-1,0),
                new Point(5,6),
                new Point(9,22),
                new Point(44,2),
                new Point (43,22)
            };
            KClosestPointToOrigin kpts = new KClosestPointToOrigin();
            PrintKClosePoints(kpts.GetKClosestPoints(allPoints, 4), 4);
            allPoints = new Point[]
            {
                new Point(0,0),
                new Point(1,1),
                new Point(0,1),
                new Point(1,0),
                new Point(-1,-1),
                new Point(-1,0),
                new Point(5,6),
                new Point(9,22),
                new Point(44,2),
                new Point (43,22)
            };
            PrintKClosePoints(kpts.GetKClosestPointsMethod2(allPoints, 4), 4);

            allPoints = new Point[]
            {
                new Point(0,0),
                new Point(1,1),
                new Point(0,1),
                new Point(1,0),
                new Point(-1,-1),
                new Point(-1,0),
                new Point(5,6),
                new Point(9,22),
                new Point(44,2),
                new Point (43,22)
            };
            PrintKClosePoints(kpts.GetKClosestPoints(allPoints, 6), 6);
            allPoints = new Point[]
            {
                new Point(0,0),
                new Point(1,1),
                new Point(0,1),
                new Point(1,0),
                new Point(-1,-1),
                new Point(-1,0),
                new Point(5,6),
                new Point(9,22),
                new Point(44,2),
                new Point (43,22)
            };
            PrintKClosePoints(kpts.GetKClosestPointsMethod2(allPoints, 6), 6);

            allPoints = new Point[]
            {
                new Point(0,0),
                new Point(1,1),
                new Point(0,1),
                new Point(1,0),
                new Point(-1,-1),
                new Point(-1,0),
                new Point(5,6),
                new Point(9,22),
                new Point(44,2),
                new Point (43,22)
            };
            PrintKClosePoints(kpts.GetKClosestPoints(allPoints, 7), 7);
            allPoints = new Point[]
            {
                new Point(0,0),
                new Point(1,1),
                new Point(0,1),
                new Point(1,0),
                new Point(-1,-1),
                new Point(-1,0),
                new Point(5,6),
                new Point(9,22),
                new Point(44,2),
                new Point (43,22)
            };
            PrintKClosePoints(kpts.GetKClosestPointsMethod2(allPoints, 7), 7);

            allPoints = new Point[]
            {
                new Point(0,0),
                new Point(1,1),
                new Point(0,1),
                new Point(1,0),
                new Point(-1,-1),
                new Point(-1,0),
                new Point(5,6),
                new Point(9,22),
                new Point(44,2),
                new Point (43,22)
            };
            PrintKClosePoints(kpts.GetKClosestPoints(allPoints, 10), 10);
            allPoints = new Point[]
            {
                new Point(0,0),
                new Point(1,1),
                new Point(0,1),
                new Point(1,0),
                new Point(-1,-1),
                new Point(-1,0),
                new Point(5,6),
                new Point(9,22),
                new Point(44,2),
                new Point (43,22)
            };
            PrintKClosePoints(kpts.GetKClosestPointsMethod2(allPoints, 10), 10);

            allPoints = new Point[]
            {
                new Point(0,0),
                new Point(1,1),
                new Point(0,1),
                new Point(1,0),
                new Point(-1,-1),
                new Point(-1,0),
                new Point(5,6),
                new Point(9,22),
                new Point(44,2),
                new Point (43,22)
            };
            PrintKClosePoints(kpts.GetKClosestPoints(allPoints, 0), 0);
            allPoints = new Point[]
            {
                new Point(0,0),
                new Point(1,1),
                new Point(0,1),
                new Point(1,0),
                new Point(-1,-1),
                new Point(-1,0),
                new Point(5,6),
                new Point(9,22),
                new Point(44,2),
                new Point (43,22)
            };
            PrintKClosePoints(kpts.GetKClosestPointsMethod2(allPoints, 0), 0);
        }
        private static void PrintKClosePoints(IEnumerable<Point> points, int k)
        {
            Console.WriteLine("The {0} close points are shown below", k);
            if (points != null)
            {
                foreach (Point p in points)
                {
                    Console.WriteLine(p.ToString());
                }
            }
        }
        #endregion
    }
}
