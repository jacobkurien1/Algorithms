using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Geometry
{
    /// <summary>
    /// Find the convex hull of all the points on the space
    /// </summary>
    public class ConvexHull
    {
        /// <summary>
        /// We can solve this using the Jarvis Algorithm.
        /// Here calculate the left most point.
        /// Now find the point p for which GetOrientation(hull.lastpoint, p, q) for all q is counterclockwise.
        /// Add this point p into the hull.
        /// Keep doing this till the point p is equal to the leftmost point
        /// 
        /// The running time of this algorithm is O(m*n) where m is the number of points in hull 
        /// and n is the number of points in the space
        /// In worst case m=n and running time is O(n^2)
        /// </summary>
        /// <param name="allPoints">all the points in the space/param>
        /// <returns>All the points in the hull</returns>
        public List<Point> GetConvexHullAlgo1(List<Point> allPoints)
        {
            if(allPoints.Count<3)
            {
                // error condition: we atleast need 3 points
                return null;
            }
            List<Point> hull = new List<Point>();

            Point leftmostPoint = allPoints[0];
            // Get the leftmost point wrt the x-axis
            for (int i = 1; i < allPoints.Count; i++)
            {
                if (leftmostPoint.Xcoordinate > allPoints[i].Xcoordinate)
                {
                    leftmostPoint = allPoints[i];
                }
            }
            hull.Add(leftmostPoint);
            Point currentPoint = null;
            int currentIndex = 0;

            do
            {
                // We need to keep coming back to the same points to check whether they are in hull
                // as the points initially encountered might not be in order in which the hull points occur
                currentIndex = (currentIndex+1)% allPoints.Count;
                for(int i=0; i<allPoints.Count; i++)
                {
                    if(LineSegmentIntersection.GetOrientation(hull.Last(), allPoints[currentIndex], allPoints[i])<0)
                    {
                        //clockwise rotation is detected
                        currentIndex = i;
                    }
                }
                currentPoint = allPoints[currentIndex];
                hull.Add(currentPoint);
            } while (currentPoint != leftmostPoint);
            
            return hull;
        }

        public static void TestConvexHull()
        {
            ConvexHull ch = new ConvexHull();
            List<Point> allPoints = new List<Point>();
            //allPoints.Add(new Point(0, 3));
            //allPoints.Add(new Point(2, 2));
            //allPoints.Add(new Point(1, 1));
            //allPoints.Add(new Point(2, 1));
            //allPoints.Add(new Point(3, 0));
            //allPoints.Add(new Point(0, 0));
            //allPoints.Add(new Point(3, 3));
            allPoints.Add(new Point(0, 0));
            allPoints.Add(new Point(8,0));
            allPoints.Add(new Point(0, 10));
            allPoints.Add(new Point(10, 0));
            allPoints.Add(new Point(5, 2));
            allPoints.Add(new Point(4, 7));
            allPoints.Add(new Point(8, 0));
            allPoints.Add(new Point(0, 9));
            allPoints.Add(new Point(9, 9));
            allPoints.Add(new Point(3, 1));
            allPoints.Add(new Point(9, 8));
            allPoints.Add(new Point(10, 10));

            PrintHull(ch.GetConvexHullAlgo1(allPoints));
        }

        private static void PrintHull(List<Point> hull)
        {
            Console.WriteLine("The hull is as shown below");
            foreach(Point p in hull)
            {
                Console.Write("{0} -> ", p.ToString());
            }
            Console.WriteLine();
        }
    }
}
