using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// Given an array of values and each index having a ballon
    /// When a balloon is burst at index we get the points arr[index-1] * arr[index] * arr[index+1].
    /// if arr[index-1] or arr[index+1] is already burst we need to find the index where balloon is not burst
    /// and use that value in the point calculation.
    /// if arr[index-1] or arr[index+1] becomes less that 0 or greater or equal to length of the array we need to use 1.
    /// 
    /// Find the way to burst the balloons to get the max points
    /// </summary>
    class BalloonBursting
    {
        /// <summary>
        /// We will solve this problem using dynamic programming approach
        /// intially start with the length considered from 1 to arr.Length
        /// for each [i,j] calculate the maxpoints if only the segment [i,j] is considered and also track the last burst balloon index
        /// 
        /// This can be calculated by considering all the index from i to j  to be the index of the last balloon which is burst.
        /// for each of these indices calculate the points that we will get when the ballon at that point is burst.
        /// Save the max points in the maxpointsMat and the index where the last balloon is burst in the lastBustBalloonIndexMat.
        /// 
        /// Backtrack in the lastBustBalloonIndexMat to get the order in which the balloons needs to be burst
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public List<int> GetMaxPoints(int[] arr)
        {
            int maxPoints = 0;

            if (arr != null && arr.Length >0 )
            {
                int[,] maxPointsMat = new int[arr.Length, arr.Length];
                int[,] lastBustBalloonIndexMat = new int[arr.Length, arr.Length];

                for (int numOfBalloonsConsidered = 1; numOfBalloonsConsidered<=arr.Length; numOfBalloonsConsidered++)
                {
                    for(int i = 0; i<arr.Length; i++)
                    {
                        int j = i + numOfBalloonsConsidered - 1;
                        if (j < arr.Length)
                        {
                            for (int lastBustBalloonIndex = i; lastBustBalloonIndex <= j; lastBustBalloonIndex++)
                            {
                                // here we change value of lastBustBalloonIndex from i to j
                                // lastBustBalloonIndex is the last balloon that is burst in the arr[i] to arr[j] inclusive
                                // We calculate the points for different lastBustBalloonIndex and get the max points and put it in maxPointsMat[i,j]
                                int leftSide = (lastBustBalloonIndex - 1 < i) ? 0 : maxPointsMat[i, lastBustBalloonIndex - 1];
                                int rightSide = (lastBustBalloonIndex + 1 > j) ? 0 : maxPointsMat[lastBustBalloonIndex + 1, j];
                                int currentPointsFromBurstingLastBalloon = ((i - 1 >= 0) ? arr[i - 1] : 1) * arr[lastBustBalloonIndex] * ((j + 1 < arr.Length) ? arr[j + 1] : 1);
                                if (maxPointsMat[i, j] < leftSide + rightSide + currentPointsFromBurstingLastBalloon)
                                {
                                    maxPointsMat[i, j] = leftSide + rightSide + currentPointsFromBurstingLastBalloon;
                                    lastBustBalloonIndexMat[i, j] = lastBustBalloonIndex;
                                }
                            }
                        }
                    }
                }
                maxPoints = maxPointsMat[0, arr.Length - 1];
                Console.WriteLine("The max points that can be achieved is: {0}", maxPoints);
                return OrderOfBalloonBursting(lastBustBalloonIndexMat);
            }
            
            return null;
        }

        /// <summary>
        /// Get the order in which the balloon needs to be burst to get the maximum points by backtracking.
        /// 
        /// Example: for an array from 0->5
        /// if the last balloon burst is 3 we need to get the last balloon burst in 0->2 and 4->5
        /// So we need a tree traversing kind of algo
        /// 
        /// </summary>
        /// <param name="lastBustBalloonIndexMat"></param>
        /// <returns></returns>
        private List<int> OrderOfBalloonBursting(int[,] lastBustBalloonIndexMat)
        {
            List<int> ret = new List<int>();
            Stack<ArraySegment> st = new Stack<ArraySegment>();
            st.Push(new ArraySegment(0, lastBustBalloonIndexMat.GetLength(1) - 1));
            while(st.Count>0)
            {
                ArraySegment currentSegment = st.Pop();
                if(currentSegment.StartIndex <= currentSegment.EndIndex)
                {
                    int lastBalloonBurst = lastBustBalloonIndexMat[currentSegment.StartIndex, currentSegment.EndIndex];
                    ret.Add(lastBalloonBurst);
                    
                    // Add the two subtrees of balloons that needs to be burst
                    st.Push(new ArraySegment(currentSegment.StartIndex, lastBalloonBurst - 1));
                    st.Push(new ArraySegment(lastBalloonBurst + 1, currentSegment.EndIndex));
                }
            }

            ret.Reverse();
            return ret;
        }

        /// <summary>
        /// Represents the array segment using the startIndex and endIndex
        /// </summary>
        internal class ArraySegment
        {
            public int StartIndex { get; set; }
            public int EndIndex { get; set; }
            public ArraySegment(int startIndex, int endIndex)
            {
                StartIndex = startIndex;
                EndIndex = endIndex;
            }
        }

        #region Test Area
        public static void TestBalloonBursting()
        {
            BalloonBursting bb = new BalloonBursting();
            List<int> burstingOrder = bb.GetMaxPoints(new int[] { 3, 1, 5, 8 });
            PrintBurstingOrder(burstingOrder);

            burstingOrder = bb.GetMaxPoints(new int[] { 3,1,2,4,5,9,7,1,2,9,3,9 });
            PrintBurstingOrder(burstingOrder);
        }

        private static void PrintBurstingOrder(List<int> burstingOrder)
        {
            Console.WriteLine("The bursting order is as follows");
            foreach (int index in burstingOrder)
            {
                Console.Write("{0}, ", index);
            }
            Console.WriteLine();
        }
        #endregion
    }
}
