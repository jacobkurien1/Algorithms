using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Geometry
{
    public class Point
    {
        public Point(float xcoordinate, float ycoordinate)
        {
            Xcoordinate = xcoordinate;
            Ycoordinate = ycoordinate;
        }
        public float Xcoordinate { get; set; }
        public float Ycoordinate { get; set; }

        public double Distance(Point p)
        {
            return Math.Sqrt(Math.Pow(p.Xcoordinate - Xcoordinate, 2) + Math.Pow(p.Ycoordinate - Ycoordinate, 2));
        }
        public override string ToString()
        {
            return string.Format("({0}, {1})", Xcoordinate, Ycoordinate);
        }
    }
}
