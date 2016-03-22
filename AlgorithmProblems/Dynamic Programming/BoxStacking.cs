using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// you are given n 3-D boxes. you want to create a stack of boxes with the maximum height
    /// you can rotate the box any way you want and you can use the 3 different instances in the problem.
    /// This is a variation of longest increasing subsequence
    /// </summary>
    public class BoxStacking
    {
        public class Box : IComparable
        {
            public int Height { get; set; }
            public int Width { get; set; }
            public int Length { get; set; }
            public Box(int length, int width, int height)
            {
                Length = length;
                Width = width;
                Height = height;
            }

            public int Area
            {
                get
                {
                    return Length * Width;
                }
            }

            public List<Box> GetAllInstancesOfBox()
            {
                List<Box> boxes = new List<Box>();
                boxes.Add(this);
                boxes.Add(new Box(Width, Height, Length));
                boxes.Add(new Box(Height, Length, Width));
                return boxes;
            }
            /// <summary>
            /// We can use this to do sorting for a List<Box> w.r.t the Area of the Box
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public int CompareTo(object obj)
            {
                return Area.CompareTo(((Box)obj).Area);
            }

            public override string ToString()
            {
                return string.Format("({0} x {1} x {2})", Length, Width, Height);
            }
        }

        /// <summary>
        /// We can solve this by dynamic programming
        /// Steps are as follows:
        /// 1. Get all instances of the box and create a list
        /// 2. Sort all the boxes w.r.t the area.
        /// 3. maxHeight[i] = { max     {maxHeight[j]} + height(i)} when box[j].Length > box[i].Length and box[j].Width > box[i].Width
        ///                     j=0->i-1
        ///                     height(i) , otherwise if we could not find any j which satisfies the above condition
        /// 
        /// The running time is O(n^2)
        /// </summary>
        /// <param name="boxes"></param>
        /// <returns></returns>
        public int GetMaxHeightByStackingBoxes(Box[] boxes)
        {
            //1. Get all instances of the box and create a list
            List<Box> allBoxes = new List<Box>();
            foreach (Box b in boxes)
            {
                allBoxes.AddRange(b.GetAllInstancesOfBox());
            }
            //2.Sort all the boxes w.r.t the area.
            allBoxes.Sort();
            int[] maxHeight = new int[allBoxes.Count()];
            int[] backtrack = new int[allBoxes.Count()];

            for (int i = 0; i < allBoxes.Count; i++)
            {
                maxHeight[i] = allBoxes[i].Height;
                int maxPrevious = 0;
                for (int j = 0; j < i; j++)
                {
                    if (allBoxes[j].Length < allBoxes[i].Length && allBoxes[j].Width < allBoxes[i].Width && maxHeight[j] > maxPrevious)
                    {
                        backtrack[i] = j;
                        maxPrevious = maxHeight[j];
                    }
                }
                maxHeight[i] += maxPrevious;
            }
            // Get all the stack of boxes
            List<Box> theStack = new List<Box>();
            int index = allBoxes.Count()-1;
            while(index > 0)
            {
                theStack.Add(allBoxes[index]);
                index = backtrack[index];
            }
            //Add the last box
            theStack.Add(allBoxes[index]);
            theStack.Reverse();
            // Print all the stack of boxes
            foreach(Box b in theStack)
            {
                Console.WriteLine(b.ToString());
            }
            return maxHeight[allBoxes.Count - 1];
        }

        public static void TestBoxStacking()
        {
            BoxStacking bs = new BoxStacking();
            int maxHeight = bs.GetMaxHeightByStackingBoxes(new Box[] { new Box(4, 6, 7), new Box(1, 2, 3), new Box(4, 5, 6), new Box(10, 12, 32) });
            Console.WriteLine("The max height of the stackable boxes are {0}", maxHeight);
        }
    }
}
