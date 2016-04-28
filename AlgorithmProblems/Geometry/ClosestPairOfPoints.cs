using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Geometry
{
    /// <summary>
    /// Given an array of points in space. Find the pair of points which are closest to each other
    /// </summary>
    public class ClosestPairOfPoints
    {

        /// <summary>
        /// We will use divide and conquer approach to solve this as the native approach is to choose 2 points from n points and find the points with minimum distance
        /// The native approach will take O(n^2) approach
        /// 
        /// The divide and conquer approach will take O(nlogn) running time
        /// Steps: 
        /// 1. Sort all the points wrt the xaxis and get the mid element. Our space will be divided with a line perpendicular to the x axis and passing through this point
        /// 2. Get the point pair with the mininimum distance in the left side of the space and the right side of the space
        /// 3. During merging the space divided in the step1 we need to consider points where one point is present in the left and another on the right
        /// But we only have to consider a strip arround the midIndex element with width of dmin = min(leftMinDistance, rightMinDistance) on either side
        /// We can prove using geometry principles that each element in this strip needs to be compared with atmost 6 other points in the strip.
        /// Hence the running time of finding closest pair in the strip becomes O(6n)
        /// 4. Sort all the points in the strip w.r.t the y axis
        /// 5. for each sorted point compare with other points untill currentPt.Y - newPoint.Y less than dmin.(atmost 6 points)
        /// 
        /// Note the running time of the above algorithm is O(n log^2(n)) because we are doing sorting wrt y axis in the step4.
        /// We can prevent this by having a set of points sorted wrt to x and y axis seperately and directly sending that to the combine algorithm and eliminating step4
        /// That will make the running time of the algorithm to be O(nlogn)
        /// T(n) = 2T(n/2) + O(n) + O(n) + O(n)
        /// T(n) = 2T(n/2) + O(n)
        /// T(n) = T(nLogn)
        /// </summary>
        /// <param name="allPoints"></param>
        /// <returns></returns>
        public PairOfPoints GetTheClosestPoint(Point[] allPoints)
        {
            Array.Sort(allPoints, ComparerUsingXCoordinate.GetComparer());
            Point[] allPointsSortedByX = allPoints;
            Point[] allPointsSortedByY = (Point[])allPoints.Clone();
            Array.Sort(allPointsSortedByY, ComparerUsingYCoordinate.GetComparer());
            return GetClosestPointsSubroutine(allPointsSortedByX, allPointsSortedByY);
        }

        /// <summary>
        /// This is the divide subroutine in divide and conquer approach
        /// Here we divide the space into left and right and get the pair of points with minimum distance
        /// and then call the combine subroutine and return the pair of points with minimum distance
        /// </summary>
        /// <param name="ptsSortedByX">points sorted wrt x axis</param>
        /// <param name="ptsSortedByY">points sorted wrt y axis</param>
        /// <returns>pair of points with minimum distance</returns>
        private PairOfPoints GetClosestPointsSubroutine(Point[] ptsSortedByX, Point[] ptsSortedByY)
        {
            //Base case
            if(ptsSortedByX.Length < 2)
            {
                return new PairOfPoints();
            }
            else if(ptsSortedByX.Length == 2)
            {
                return new PairOfPoints(ptsSortedByX[0], ptsSortedByX[1]);
            }

            int midIndex = ptsSortedByX.Length / 2;
            Point[] pointsOnLeftSideSortedByY = new Point[midIndex];
            Point[] pointsOnRightSideSortedByY = new Point[ptsSortedByX.Length -(midIndex)]; // Element at the midIndex will lie on the right side

            Point[] pointsOnLeftSideSortedByX = new Point[midIndex];
            Point[] pointsOnRightSideSortedByX = new Point[ptsSortedByX.Length - (midIndex)]; // Element at the midIndex will lie on the right side

            int leftIndex = 0;
            int rightIndex = 0;

            // Divide the ptsSortedByY into left and right of the line perpendicular to the x-axis and passing from the midIndex
            for(int i=0;i<ptsSortedByY.Length;i++)
            {
                if(ptsSortedByY[i].Xcoordinate <= ptsSortedByX[midIndex].Xcoordinate && leftIndex<midIndex) 
                {
                    // this condition is used in case where there are duplicate Points with same Xcoordinates
                    // and one point is the one in midIndex
                    pointsOnLeftSideSortedByY[leftIndex++] = ptsSortedByY[i];
                }
                else
                {
                    pointsOnRightSideSortedByY[rightIndex++] = ptsSortedByY[i];
                }

                if(i<midIndex)
                {
                    pointsOnLeftSideSortedByX[i] = ptsSortedByX[i];
                }
                else
                {
                    pointsOnRightSideSortedByX[i-midIndex] = ptsSortedByX[i];
                }
            }

            // Now solve the subproblem
            PairOfPoints leftMinPair = GetClosestPointsSubroutine(pointsOnLeftSideSortedByX, pointsOnLeftSideSortedByY);
            PairOfPoints rightMinPair = GetClosestPointsSubroutine(pointsOnRightSideSortedByX, pointsOnRightSideSortedByY);

            //get the min
            PairOfPoints min = leftMinPair;
            if(min.Distance>rightMinPair.Distance)
            {
                min = rightMinPair;
            }

            //Create the list with all the points in the strip
            List<Point> pointsInStrip = new List<Point>();
            for(int i=0;i<ptsSortedByY.Length; i++)
            {
                if(Math.Abs(ptsSortedByX[midIndex].Xcoordinate - ptsSortedByY[i].Xcoordinate)<=min.Distance)
                {
                    pointsInStrip.Add(ptsSortedByY[i]);
                }
            }
            //Combine the left and right
            return Combine(pointsInStrip, min);
        }

        /// <summary>
        /// this the merge subroutine of the divide and conquer approach
        /// </summary>
        /// <param name="pointsInStrip">this is the points present in the strip. These points are sorted wrt y axis</param>
        /// <param name="min">the current pair of points with minimum distance</param>
        /// <returns>pair of points with minimum distance after merging the 2 space</returns>
        private PairOfPoints Combine(List<Point> pointsInStrip, PairOfPoints min)
        {
            // Note this operations should take O(n) running time
            // it can be geometrically proven that each point in the strip needs to be compared with atmost 6 other points
            for(int i=0;i<pointsInStrip.Count;i++)
            {
                for(int j=i+1;j< pointsInStrip.Count;j++)
                {
                    if(pointsInStrip[j].Ycoordinate - pointsInStrip[i].Ycoordinate>min.Distance)
                    {
                        // there is no point in checking more than j cause the distance will always ne greater than min.Distance
                        break;
                    }
                    PairOfPoints currentPair = new PairOfPoints(pointsInStrip[i], pointsInStrip[j]);
                    if(currentPair.Distance<min.Distance)
                    {
                        min = currentPair;
                    }
                }
            }
            return min;
        }

        public static void TestClosestPairOfPoints()
        {
            Point[] allPoints = new Point[] { new Point(2, 3), new Point(12, 30), new Point(40, 50), new Point(5, 1), new Point(12, 10), new Point(3, 4) };
            ClosestPairOfPoints cp = new ClosestPairOfPoints();
            PairOfPoints pair = cp.GetTheClosestPoint(allPoints);
            Console.WriteLine("The closest pair of points are {0} and {1} and the minDistance is {2}", pair.PointA.ToString(), pair.PointB.ToString(), pair.Distance);
        }
    }

}
