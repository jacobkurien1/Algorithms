using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Geometry
{
    public class PairOfPoints
    {
        public PairOfPoints()
        {
            // we need this constructor to create pair of null points
        }
        public PairOfPoints(Point ptA, Point ptB)
        {
            PointA = ptA;
            PointB = ptB;
        }
        public Point PointA { get; set; }
        public Point PointB { get; set; }
        public double Distance
        {
            get
            {
                if (PointA == null || PointB == null)
                {
                    // pair of null points needs to give a very high distance value
                    return double.MaxValue;
                }
                else
                {
                    return PointA.Distance(PointB);
                }
            }
        }
    }
}
