using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Geometry
{
    public class ComparerUsingXCoordinate : IComparer<Point>
    {
        public static IComparer<Point> GetComparer()
        {
            return new ComparerUsingXCoordinate();
        }

        public int Compare(Point pt1, Point pt2)
        {
            return pt1.Xcoordinate.CompareTo(pt2.Xcoordinate);
        }
    }

    public class ComparerUsingYCoordinate : IComparer<Point>
    {
        public static IComparer<Point> GetComparer()
        {
            return new ComparerUsingYCoordinate();
        }
        public int Compare(Point pt1, Point pt2)
        {
            return pt1.Ycoordinate.CompareTo(pt2.Ycoordinate);
        }
    }
}
