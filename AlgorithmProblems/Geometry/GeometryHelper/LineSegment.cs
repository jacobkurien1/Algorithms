using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Geometry
{
    public class LineSegment
    {
        public LineSegment(Point pt1, Point pt2)
        {
            Point1 = pt1;
            Point2 = pt2;
        }
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
        public override string ToString()
        {
            return string.Format("{0} -> {1}", Point1.ToString(), Point2.ToString());
        }
    }
}
