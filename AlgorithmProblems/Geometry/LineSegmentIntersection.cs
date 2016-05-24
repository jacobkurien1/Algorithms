using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Geometry
{
    /// <summary>
    /// Check if the 2 given line segments interset each other or not
    /// </summary>
    public class LineSegmentIntersection
    {
        /// <summary>
        /// We need to check if the 2 line segments are intersecting. This is the main method for that task
        /// Assumption: linesegment1 has points p1 and p2
        /// and linesegment2 has points p3 and p4
        /// 
        /// So find the orientation of p1 p2 and p3 and orientation of p1 p2 and p4 should be different
        /// also the orientation of p3 p4 and p1 should be different than the orientation of p3 p4 and p2
        /// 
        /// The case where we have both the line segments as collinear we need to see the whether there is projection of 
        /// one linesegment over the other.
        ///
        /// </summary>
        /// <param name="linesegment1"></param>
        /// <param name="linesegment2"></param>
        /// <returns></returns>
        public static bool IsIntersecting(LineSegment linesegment1, LineSegment linesegment2)
        {
            int orientationP1P2P3 = GetOrientation(linesegment1.Point1, linesegment1.Point2, linesegment2.Point1);
            int orientationP1P2P4 = GetOrientation(linesegment1.Point1, linesegment1.Point2, linesegment2.Point2);
            int orientationP3P4P1 = GetOrientation(linesegment2.Point1, linesegment2.Point2, linesegment1.Point1);
            int orientationP3P4P2 = GetOrientation(linesegment2.Point1, linesegment2.Point2, linesegment1.Point2);

            if(orientationP1P2P3!=orientationP1P2P4 && orientationP3P4P1!=orientationP3P4P2)
            {
                // the linesegments are intersecting in this case
                return true;
            }
            else if(orientationP1P2P3 == 0 && orientationP1P2P4 == 0 && orientationP3P4P1 == 0 && orientationP3P4P2 == 0)
            {
                // this is the special case in which all 4 points from 2 linesegments are collinear
                return IsPointPresentOnLine(linesegment1, linesegment2.Point1)
                    || IsPointPresentOnLine(linesegment1, linesegment2.Point2)
                    || IsPointPresentOnLine(linesegment2, linesegment1.Point1)
                    || IsPointPresentOnLine(linesegment2, linesegment1.Point2);
            }
            else
            {
                // there is no intersection between the 2 linesegments
                return false;
            }
        }

        /// <summary>
        /// This method checks whether a point lies on the line or not
        /// </summary>
        /// <param name="linesegment">linesegment on which the point is suspected to lie</param>
        /// <param name="pt">the point underconsideration</param>
        /// <returns>true if pt lies on linesegment</returns>
        private static bool IsPointPresentOnLine(LineSegment linesegment, Point pt)
        {
            return (pt.Xcoordinate >= Math.Min(linesegment.Point1.Xcoordinate, linesegment.Point2.Xcoordinate))
                && (pt.Xcoordinate <= Math.Max(linesegment.Point1.Xcoordinate, linesegment.Point2.Xcoordinate))
                && (pt.Ycoordinate >= Math.Min(linesegment.Point1.Ycoordinate, linesegment.Point2.Ycoordinate))
                && (pt.Ycoordinate <= Math.Max(linesegment.Point1.Ycoordinate, linesegment.Point2.Ycoordinate));
        }

        /// <summary>
        /// We will find the orientation by getting the vector p2 - vector p1 (linesegment1)and vector p3 - vector p2 (linesegment2)
        /// We can do this by doing the cross product of linesegment1 and linesegment2
        /// 
        /// if(orientation>0), its anticlockwise
        /// if(orientation==0), its colinear
        /// if(orientation<0), its clockwise
        /// </summary>
        /// <param name="p1">first point</param>
        /// <param name="p2">second point</param>
        /// <param name="p3">third point</param>
        /// <returns></returns>
        public static int GetOrientation(Point p1, Point p2, Point p3)
        {
            return GetStepFunctionY((p2.Xcoordinate - p1.Xcoordinate) * (p3.Ycoordinate - p2.Ycoordinate) - (p2.Ycoordinate - p1.Ycoordinate) * (p3.Xcoordinate - p2.Xcoordinate));
        }

        private static int GetStepFunctionY(float stepFuntionX)
        {
            if(stepFuntionX == 0)
            {
                return 0;
            }
            else if (stepFuntionX>0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public static void TestLineSegmentIntersection()
        {
            LineSegment ls1 = new LineSegment(new Point(1, 1), new Point(10, 1));
            LineSegment ls2 = new LineSegment(new Point(1, 2), new Point(10, 2));

            Console.WriteLine("The 2 line segments {0} and {1} is {2}", ls1, ls2, IsIntersecting(ls1, ls2));

            ls1 = new LineSegment(new Point(10, 0), new Point(0, 10));
            ls2 = new LineSegment(new Point(-1, -1), new Point(20, 20));

            Console.WriteLine("The 2 line segments {0} and {1} is {2}", ls1, ls2, IsIntersecting(ls1, ls2));

            ls1 = new LineSegment(new Point(-3, -3), new Point(0, 0));
            ls2 = new LineSegment(new Point(1, 1), new Point(20, 20));

            Console.WriteLine("The 2 line segments {0} and {1} is {2}", ls1, ls2, IsIntersecting(ls1, ls2));

            ls1 = new LineSegment(new Point(-3, -3), new Point(0, 0));
            ls2 = new LineSegment(new Point(0, 0), new Point(20, 20));

            Console.WriteLine("The 2 line segments {0} and {1} is {2}", ls1, ls2, IsIntersecting(ls1, ls2));

            ls1 = new LineSegment(new Point(1, 1), new Point(4, 6));
            ls2 = new LineSegment(new Point(2, 3), new Point(5, 2));

            Console.WriteLine("The 2 line segments {0} and {1} is {2}", ls1, ls2, IsIntersecting(ls1, ls2));
        }
    }
}
