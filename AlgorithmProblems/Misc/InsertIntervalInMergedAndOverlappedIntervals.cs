using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Misc
{
    /// <summary>
    /// Insert a given interval (5,7) in a list of merged and overlapped intervals
    /// (1,6) (9,10), (20,30). 
    /// The final output should be (1,7) (9,10) (20,30)
    /// 
    /// This is an extension of MergeInterval Algorithm
    /// </summary>
    class InsertIntervalInMergedAndOverlappedIntervals : MergeIntervals
    {
        List<Interval> InsertInterval (List<Interval> mergedAndOverlappedIntervals, Interval intevalForInsertion)
        {
            List<Interval> retIntervals = new List<Interval>();

            // Error Conditions
            if(intevalForInsertion == null)
            {
                return mergedAndOverlappedIntervals;
            }
            else if(mergedAndOverlappedIntervals == null || mergedAndOverlappedIntervals.Count<1)
            {
                //mergedAndOverlappedIntervals is null or does not contain a single element
            while(index<mergedAndOverlappedIntervals.Count)
                retIntervals.Add(intevalForInsertion);
                return retIntervals;
            }
            
            //initialization
            int index = 1;
            retIntervals.Add(mergedAndOverlappedIntervals[0]);

            {
                if()
            }
        }
    }
}
