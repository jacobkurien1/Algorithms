using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Misc
{
    /// <summary>
    /// Tasks are arranged in a calendar timeline. Find the maximum width of the calendar timeline
    /// </summary>
    class WidthOfCalendar
    {
        /// <summary>
        /// Steps are as follows:
        /// 1. Convert all the intervals into the Endpoints
        /// 2. Sort all the endpoint
        /// 3. Traverse all endpoints, whenever a start endpoint is seen increase the count
        /// and decrease the count whenever a finish endpoint is encountered.
        /// 4. Track the max count of count at any time using another variable (maxCount) in the below code
        /// </summary>
        /// <param name="allIntervals">list of intervals are sent as input to the function</param>
        /// <returns>the maximum unit width of the calendar timeline</returns>
        public int GetWidth(List<Interval> allIntervals)
        {
            // Convert all the intervals into the Endpoints
            List<Endpoint> allEndpoints = new List<Endpoint>();
            foreach (Interval interval in allIntervals)
            {
                allEndpoints.Add(new Endpoint(interval.Start, true));
                allEndpoints.Add(new Endpoint(interval.Finish, false));
            }

            // Sort all the endpoints
            allEndpoints.Sort();

            //3. Traverse all endpoints, whenever a start endpoint is seen increase the count
            // and decrease the count whenever a finish endpoint is encountered.
            // 4. Track the max count of count at any time using another variable (maxCount) in the below code
            int maxCount = 0;
            int count = 0;
            foreach (Endpoint endpoint in allEndpoints)
            {
                if(endpoint.IsStart)
                {
                    ++count;
                    if(count> maxCount)
                    {
                        maxCount = count;
                    }
                }
                else
                {
                    count--;
                }
            }

            return maxCount;
        }

        public static void TestWidthOfCalendar()
        {
            List<Interval> allIntervals = new List<Interval>()
            {
                new Interval(3,8),
                new Interval(2,5),
                new Interval(4,7),
                new Interval(6,9),
                new Interval(1,10)
            };

            WidthOfCalendar wdth = new WidthOfCalendar();
            int maxWidth = wdth.GetWidth(allIntervals);
            Console.WriteLine("The maxwidth for the intervals are{0}", maxWidth);
        }

        internal class Interval
        {
            public int Start { get; set; }
            public int Finish { get; set; }
            public Interval(int start, int finish)
            {
                Start = start;
                Finish = finish;
            }
        }

        internal class Endpoint : IComparable<Endpoint>
        {
            public int Time { get; set; }
            public bool IsStart { get; set; }
            public Endpoint(int time, bool isStart)
            {
                Time = time;
                IsStart = isStart;
            }

            public int CompareTo(Endpoint other)
            {
                return Time.CompareTo(other.Time);
            }
        }
    }
}
