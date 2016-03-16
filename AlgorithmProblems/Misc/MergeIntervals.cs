using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Misc
{
    public class MergeIntervals
    {
        /// <summary>
        /// Each interval will be represented by object of this class
        /// </summary>
        public class Interval : IComparable
        {
            public int Start { get; set; }
            public int End { get; set; }
            public Interval(int start, int end)
            {
                if (end < start)
                {
                    throw new ArgumentException();
                }
                this.Start = start;
                this.End = end;
            }

            /// <summary>
            /// This is used by algo2 to override the default compareTo method used during sorting
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public int CompareTo(object obj)
            {
                Interval interval = (Interval)obj;
                return this.Start.CompareTo(interval.Start);
            }
        }

        # region algorithm1 - sorting and overlapping are done together
        public List<Interval> MergeMain(List<Interval> mainList, int start, int end)
        {

            if(start>end)
            {
                return new List<Interval>();
            }
            else if (start==end)
            {
                return new List<Interval>() {mainList[start]};
            }
            int mid = start + ((end - start) / 2); // prevents overflow
            List<Interval> left = MergeMain(mainList, start, mid);
            List<Interval> right = MergeMain(mainList, mid + 1, end);
            return MergeListOfIntervals(left, right);
        }

        List<Interval> MergeListOfIntervals(List<Interval> listA, List<Interval> listB)
        {
            List<Interval> mergedListOfIntervals = new List<Interval>();

            int listAIndex = 0;
            int listBIndex = 0;
            Interval currentIntervalA = null;
            Interval currentIntervalB = null;

            while(listAIndex < listA.Count && listBIndex<listB.Count)
            {
                currentIntervalA = listA[listAIndex];
                currentIntervalB = listB[listBIndex];

                if(currentIntervalA.End < currentIntervalB.Start)
                {
                    //The currentIntervalB is on the right of currentIntervalA and there is no overlap
                    //(	----A------)
                    //					(-----B-----)
                    mergedListOfIntervals.Add(currentIntervalA);
                    listAIndex++;
                }
                else if(currentIntervalB.End < currentIntervalA.Start)
                {
                    //The currentIntervalA is on the right of currentIntervalB and there is no overlap
                    //(	----B------)
                    //					(-----A-----)
                    mergedListOfIntervals.Add(currentIntervalB);
                    listBIndex++;
                }
                else
                {
                    // there is an overlap between currentIntervalB and currentIntervalA

                    // Now we need to take care of 4 cases:
                    if(currentIntervalA.End>=currentIntervalB.Start && currentIntervalA.Start <= currentIntervalB.Start && currentIntervalA.End <= currentIntervalB.End)
                    {
                        //Case1: 
                        //(------A----------)
                        //			(--------B------)
                        mergedListOfIntervals.Add(new Interval(currentIntervalA.Start, currentIntervalB.End));
                    }
                    else if (currentIntervalB.End>=currentIntervalA.Start && currentIntervalB.Start <= currentIntervalA.Start && currentIntervalB.End <= currentIntervalA.End)
                    {
                        //case2:
                        //(------B----------)
                        //			(--------A------)
                        mergedListOfIntervals.Add(new Interval(currentIntervalB.Start, currentIntervalA.End));
                    }
                    else if(currentIntervalB.Start<=currentIntervalA.Start && currentIntervalB.End>=currentIntervalA.End)
                    {
                        //case3:
                        //(------B--------------------------)
                        //			(--------A------)
                        mergedListOfIntervals.Add(currentIntervalB);
                    }
                    else
                    {
                        //case4:
                        //(------A--------------------------)
                        //			(--------B------)
                        mergedListOfIntervals.Add(currentIntervalA);
                    }
                    listAIndex++;
                    listBIndex++;
                }
                
            }
            while (listAIndex < listA.Count)
            {
                currentIntervalB = listA[listAIndex];
                AddToEndOfIntervalsList(mergedListOfIntervals, currentIntervalB);
                listAIndex++;

            }
            while (listBIndex < listB.Count)
            {
                currentIntervalB = listB[listBIndex];
                AddToEndOfIntervalsList(mergedListOfIntervals, currentIntervalB);
                listBIndex++;
            }

            return mergedListOfIntervals;
        }

        private void AddToEndOfIntervalsList(List<Interval> mergedListOfIntervals, Interval currentIntervalB)
        {
            Interval currentIntervalA;
            try
            {
                currentIntervalA = mergedListOfIntervals.Last();
            }
            catch (InvalidOperationException)
            {
                // the merged list is empty
                mergedListOfIntervals.Add(currentIntervalB);
                return;
            }


            //We will have 3 cases here:
            if (currentIntervalA.End < currentIntervalB.Start)
            {
                //(	----A------)
                //					(-----B-----)
                // we need to add currentIntervalB into the merged list
                mergedListOfIntervals.Add(currentIntervalB);
            }
            else if (currentIntervalA.End >= currentIntervalA.Start && currentIntervalA.Start <= currentIntervalB.Start && currentIntervalA.End <= currentIntervalB.End)
            {
                //(------A----------)
                //			(--------B------)
                // we need to remove the last element from merged list and combine the interval
                mergedListOfIntervals.RemoveAt(mergedListOfIntervals.Count - 1);
                mergedListOfIntervals.Add(new Interval(currentIntervalA.Start, currentIntervalB.End));
            }
            else
            {
                //(------A--------------------------)
                //			(--------B------)
                //we need to do nothing in this case as we dont need to add currentIntervalB to the mergedlist
            }
        }

        #endregion

        # region algorithm2 - We do sorting first and then overlapping

        /// <summary>
        /// Algo: 1. Sort the intervals based on their start in ascending order
        /// 2. Merge the overlapping intervals while traversing through the sorted list
        /// 
        /// The running time here will be O(nlogn)
        /// </summary>
        /// <param name="mainList"></param>
        /// <returns></returns>
        public List<Interval> MergeAndOverlapAlog2(List<Interval> mainList)
        {
            List<Interval> mergedListOfIntervals = new List<Interval>();
            if(mainList.Count==0)
            {
                return mergedListOfIntervals;
            }
            mainList.Sort();

            // initialization
            mergedListOfIntervals.Add(mainList[0]);

            for(int i=1; i<mainList.Count; i++)
            {
                Interval currentIntervalA = mergedListOfIntervals.Last(); ;
                Interval currentIntervalB = mainList[i];
                //We will have 3 cases here:
                if (currentIntervalA.End < currentIntervalB.Start)
                {
                    //Case1:
                    //(	----A------)
                    //					(-----B-----)
                    // we need to add currentIntervalB into the merged list
                    mergedListOfIntervals.Add(currentIntervalB);
                }
                else if (currentIntervalA.End >= currentIntervalA.Start && currentIntervalA.Start <= currentIntervalB.Start && currentIntervalA.End <= currentIntervalB.End)
                {
                    //Case2:
                    //(------A----------)
                    //			(--------B------)
                    // we need to remove the last element from merged list and combine the interval
                    mergedListOfIntervals.RemoveAt(mergedListOfIntervals.Count - 1);
                    mergedListOfIntervals.Add(new Interval(currentIntervalA.Start, currentIntervalB.End));
                }
                else
                {
                    //Case3:
                    //(------A--------------------------)
                    //			(--------B------)
                    //we need to do nothing in this case as we dont need to add currentIntervalB to the mergedlist

                }
            }
            return mergedListOfIntervals;
        }
        #endregion

        public static void TestMergeIntervals()
        {
            List<Interval> listOfIntervals = new List<Interval>();
            listOfIntervals.Add(new Interval(1, 3));
            listOfIntervals.Add(new Interval(3, 4));
            listOfIntervals.Add(new Interval(7, 10));
            listOfIntervals.Add(new Interval(8, 9));
            listOfIntervals.Add(new Interval(1, 11));

            MergeIntervals mi = new MergeIntervals();
            List<Interval> listOfIntervalsReturned = mi.MergeMain(listOfIntervals, 0, listOfIntervals.Count - 1);

            Console.WriteLine("Algo1: The overlapped and sorted intervals are as follows:");
            for(int i=0; i<listOfIntervalsReturned.Count; i++)
            {
                Console.WriteLine("{0} -> {1}", listOfIntervalsReturned[i].Start, listOfIntervalsReturned[i].End);
            }
            listOfIntervalsReturned = mi.MergeAndOverlapAlog2(listOfIntervals);

            Console.WriteLine("Algo2: The overlapped and sorted intervals are as follows:");
            for (int i = 0; i < listOfIntervalsReturned.Count; i++)
            {
                Console.WriteLine("{0} -> {1}", listOfIntervalsReturned[i].Start, listOfIntervalsReturned[i].End);
            }
            Console.WriteLine("The expected result is (1-11)");

            //------------Test2----------------------------------------------
            listOfIntervals = new List<Interval>();
            listOfIntervals.Add(new Interval(40, 50));
            listOfIntervals.Add(new Interval(60, 70));
            listOfIntervals.Add(new Interval(180, 190));
            listOfIntervals.Add(new Interval(160, 170));
            listOfIntervals.Add(new Interval(1, 5));
            listOfIntervals.Add(new Interval(3, 7));
            listOfIntervals.Add(new Interval(4, 5));
            listOfIntervals.Add(new Interval(20, 30));
            listOfIntervals.Add(new Interval(25, 35));

            listOfIntervalsReturned = mi.MergeMain(listOfIntervals, 0, listOfIntervals.Count - 1);

            Console.WriteLine("Algo1: The overlapped and sorted intervals are as follows:");
            for (int i = 0; i < listOfIntervalsReturned.Count; i++)
            {
                Console.WriteLine("{0} -> {1}", listOfIntervalsReturned[i].Start, listOfIntervalsReturned[i].End);
            }
            listOfIntervalsReturned = mi.MergeAndOverlapAlog2(listOfIntervals);

            Console.WriteLine("Algo2: The overlapped and sorted intervals are as follows:");
            for (int i = 0; i < listOfIntervalsReturned.Count; i++)
            {
                Console.WriteLine("{0} -> {1}", listOfIntervalsReturned[i].Start, listOfIntervalsReturned[i].End);
            }
            Console.WriteLine("The expected result is (1-7) (20-35) (40-50) (60-70) (160-170) (180-190)");
        }
    }
}
