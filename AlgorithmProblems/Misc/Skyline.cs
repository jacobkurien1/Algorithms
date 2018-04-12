using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Misc
{
    class Skyline
    {
        public class Building
        {
            public int Start { get; set; }
            public int End { get; set; }
            public int Height { get; set; }

            public Building(int start, int end, int height)
            {
                this.Start = start;
                this.End = end;
                this.Height = height;
            }
        }

        public class SkylinePoint
        {
            public int Start { get; set; }
            public int Height { get; set; }

            public SkylinePoint(int start, int height)
            {
                this.Start = start;
                this.Height = height;
            }
        }

        public static List<SkylinePoint> MergeSkylines(List<SkylinePoint> skyline1, List<SkylinePoint> skyline2)
        {
            if(skyline1 == null)
            {
                return skyline2;    // Note: this can be null
            }
            if(skyline2 == null)
            {
                return skyline1;    // Note: this can be null
            }
            List<SkylinePoint> combinedSkyline = new List<SkylinePoint>();

            int len1 = 0, len2 = 0;
            // lastSkylinePointFromSkyline1 tracks whether the last skyline point we put in the combinedSkyline list is from the skyline1
            bool lastSkylinePointFromSkyline1 = false;

            while(len1<skyline1.Count && len2 < skyline2.Count)
            {
                if(skyline1[len1].Start < skyline2[len2].Start)
                {
                    if (combinedSkyline.Count == 0 || combinedSkyline.Last().Height < skyline1[len1].Height)
                    {
                        combinedSkyline.Add(skyline1[len1]);
                        lastSkylinePointFromSkyline1 = true;
                    }
                    else if (lastSkylinePointFromSkyline1)
                    {
                        if(len2 - 1>=0 && skyline2[len2-1].Height > skyline1[len1].Height)
                        {
                            /*	Case where x axis is at x	
                            |-------|
                            |       |skyline1
                            |       |
                                |---------| skyline2
	                            |         |
                                    x
                            */
                            combinedSkyline.Add(new SkylinePoint(skyline1[len1].Start, skyline2[len2 - 1].Height));
                            lastSkylinePointFromSkyline1 = false;
                        }
                        else
                        {
                            combinedSkyline.Add(skyline1[len1]);
                            lastSkylinePointFromSkyline1 = true;
                        }
                    }
                    len1++;
                }
                else if (skyline2[len2].Start < skyline1[len1].Start)
                {
                    if (combinedSkyline.Count == 0 || combinedSkyline.Last().Height < skyline2[len2].Height)
                    {
                        combinedSkyline.Add(skyline2[len2]);
                        lastSkylinePointFromSkyline1 = false;
                    }
                    else if(!lastSkylinePointFromSkyline1)
                    {
                        if (len1 - 1 >= 0 && skyline1[len1 - 1].Height > skyline2[len2].Height)
                        {
                            combinedSkyline.Add(new SkylinePoint(skyline2[len2].Start, skyline1[len1 - 1].Height));
                            lastSkylinePointFromSkyline1 = true;
                        }
                        else
                        {
                            combinedSkyline.Add(skyline2[len2]);
                            lastSkylinePointFromSkyline1 = false;
                        }
                    }
                    len2++;
                }
                else // skyline1[len1].Start == skyline2[len2].Start
                {
                    if(skyline1[len1].Height > skyline2[len2].Height)
                    {
                        combinedSkyline.Add(skyline1[len1]);
                    }
                    else
                    {
                        combinedSkyline.Add(skyline2[len2]);
                    }
                    len1++;
                    len2++;
                }
            }

            // Case when skyline2 list is empty, but skyline1 has a few skypoints
            // Add all the points in skyline1
            while (len1 < skyline1.Count)
            {
                combinedSkyline.Add(skyline1[len1]);
                len1++;
            }

            // Case when skyline1 list is empty, but skyline2 has a few skypoints
            // Add all the points in skyline2
            while (len2 < skyline2.Count)
            {
                combinedSkyline.Add(skyline2[len2]);
                len2++;
            }

            return combinedSkyline;
        }

        public static List<SkylinePoint> GetSkyline(List<Building> buildings, int start, int end)
        {
            if(start>end)
            {
                return null;
            }
            else if( start == end)
            {
                List<SkylinePoint> retList = new List<SkylinePoint>();
                retList.Add(new SkylinePoint(buildings[start].Start, buildings[start].Height));
                retList.Add(new SkylinePoint(buildings[start].End, 0));
                return retList;
            }
            else
            {
                int mid = (start + end) / 2;
                List<SkylinePoint> skyline1 = GetSkyline(buildings, start, mid);
                List<SkylinePoint> skyline2 = GetSkyline(buildings, mid+1, end);
                return MergeSkylines(skyline1, skyline2);
            }
        }

        public static void TestSkyline()
        {
            List<Building> buildings = new List<Building>();
            buildings.Add(new Building(1, 3, 1));
            buildings.Add(new Building(2, 3, 2));
            buildings.Add(new Building(2, 3, 3));
            buildings.Add(new Building(4, 5, 2));
            List<SkylinePoint> skyline = GetSkyline(buildings, 0, buildings.Count-1);

            Console.WriteLine("The skyline is as shown below:");
            foreach(SkylinePoint sp in skyline)
            {
                Console.WriteLine("The skyline point start is {0} and the height is {1}", sp.Start, sp.Height);
            }
            //Case:
            /*		
            |-------|
            |       |
            |       |
                |---------|
	            |         |

            */
            buildings = new List<Building>();
            buildings.Add(new Building(0, 2, 5));
            buildings.Add(new Building(1, 3, 2));
            skyline = GetSkyline(buildings, 0, buildings.Count - 1);

            Console.WriteLine("The skyline is as shown below:");
            foreach (SkylinePoint sp in skyline)
            {
                Console.WriteLine("The skyline point start is {0} and the height is {1}", sp.Start, sp.Height);
            }
        }
    }
}
