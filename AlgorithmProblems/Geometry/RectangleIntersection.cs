using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Geometry
{
    /// <summary>
    /// Given 2 rectangles, check whether they intersect or not
    /// </summary>
    public class RectangleIntersection
    {
        /// <summary>
        /// We need to find whether rectangles a and b are intersecting
        /// </summary>
        /// <param name="a">one of the rectangles</param>
        /// <param name="b">one of the rectangles</param>
        /// <returns></returns>
        public static bool IsIntersecting(Rectangle a, Rectangle b)
        {
            return (a.TopLeft.Xcoordinate < b.BottomRight.Xcoordinate)
                && (b.TopLeft.Xcoordinate < a.BottomRight.Xcoordinate)
                && (a.TopLeft.Ycoordinate > b.BottomRight.Ycoordinate)
                && (a.BottomRight.Ycoordinate < b.TopLeft.Ycoordinate);
        }
        public static void TestRectangleIntersection()
        {
            Rectangle rect1 = new Rectangle(new Point(1, 5), new Point(7, 2));
            Rectangle rect2 = new Rectangle(new Point(5, 3), new Point(10, 1));
            Console.WriteLine("The intersection is : {0}", RectangleIntersection.IsIntersecting(rect1, rect2));
        }
    }

    public class Rectangle
    {
        public Point TopLeft { get; set; }
        public Point BottomRight { get; set; }
        public Rectangle(Point topLeft, Point bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }
    }
}
