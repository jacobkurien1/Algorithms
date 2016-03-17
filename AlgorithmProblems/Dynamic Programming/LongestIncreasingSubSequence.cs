using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// The longest increasing subsequence problem is to get the longest subsequence of a given
    /// sequence such that all the elements in the LIS is in ascending order.
    /// For eg LIS of {21,11,3,4,6,32,6,4,43,91} is {3,4,6,32,43,91}
    /// </summary>
    class LongestIncreasingSubSequence
    {

        #region Algo1: Dynamic Programming - Running time O(n^2)
        /// <summary>
        /// We can use the dynamic programming approach to solve the LIS problem
        /// Let LIS[i] = 1+Max(LIS[j]) where i>j and inputArr[i]> inputArr[j]
        ///              1, otherwise
        /// To get the longest increasing subsequence we can store the index j where we get Max(LIS[j])
        /// in a different array in the index i. So Backtrack[i] = j where j satisfies Max(LIS[j])
        /// 
        /// The running time for this algorithm is O(n^2) and the space requirement is O(n)
        /// </summary>
        /// <param name="inputList"></param>
        /// <returns></returns>
        public List<int> GetLIS(int[] inputArr)
        {
            // initialization
            List<int> ret = new List<int>();
            int[] lis = new int[inputArr.Length];
            int[] backtrack = new int[inputArr.Length];

            // get the LIS
            for(int i=0; i<inputArr.Length; i++)
            {
                lis[i] = 1;
                backtrack[i] = -1;
                for(int j=0; j< i;j++)
                {
                    if(inputArr[i] > inputArr[j] && lis[i] < 1+lis[j])
                    {
                        lis[i] = 1 + lis[j];
                        backtrack[i] = j;
                    }
                }
            }

            Console.WriteLine("the longest increasing subsequence is {0}", ((lis.Length > 0)? lis[inputArr.Length - 1] : 0));

            // lets get the longest increasing subsequence by backtracking
            int index = backtrack.Length-1;
            while (index >= 0)
            {
                ret.Add(inputArr[index]);
                index = backtrack[index];
            }

            return ret;
        }

        #endregion

        #region Algo2: Running time O(nlog(n))

        /// <summary>
        /// We will keep track of all the last element of the subsequence and whenever while traversing the inputArr
        /// we get an element greater than the last element(greatest element) of the list we append the new element to the list.
        /// If while traversing the inputArr we find an element smaller than the last element, we do a binary search of the list 
        /// and get the element which is just greater than the current element. We will replace this element in the list with the 
        /// element from inputArr.
        /// 
        /// Also maintian a trackParent dictionary which will be used to backtrack and get the LIS.
        /// 
        /// Since we are employing binary seach while iterating inputArr the running time is O(nlog(n))
        /// The space requirement is O(n)
        /// </summary>
        /// <param name="inputArr"></param>
        /// <returns></returns>
        public List<int> GetLISAlgo2(int[] inputArr)
        {
            // initialization
            List<int> lis = new List<int>();
            List<int> trackSubSequenceIndices = new List<int>();
            Dictionary<int, int> trackParent = new Dictionary<int, int>();
            
            for(int i=0;i<inputArr.Length; i++)
            {
                // if inputArr[i] > last element of trackSubSequence then add it to the list trackSubSequence
                // Else we need to find the element which is greater than inputArr[i] in  trackSubSequence and replace it
                if(trackSubSequenceIndices.Count==0)
                {
                    trackSubSequenceIndices.Add(i);
                    trackParent[i] = -1;
                }
                else
                {
                    if(inputArr[i] > inputArr[trackSubSequenceIndices.Last()])
                    {
                        // if inputArr[i] > last element of trackSubSequence then add it to the list trackSubSequence
                        trackParent[i] = trackSubSequenceIndices.Last();
                        trackSubSequenceIndices.Add(i);
                    }
                    else
                    {
                        // Else we need to find the element which is greater than inputArr[i] in  trackSubSequence and replace it
                        int indexToReplace = GetIndexToReplace(trackSubSequenceIndices, inputArr, 0, trackSubSequenceIndices.Count, inputArr[i]);
                        trackSubSequenceIndices[indexToReplace] = i;
                        trackParent[i] = (indexToReplace>1)?trackSubSequenceIndices[indexToReplace - 1]:-1;
                    }
                }
            }
            Console.WriteLine("The number of elements in longest increasing subsequence is : {0}", trackSubSequenceIndices.Count);
            //Now lets return the LIS by backtracking
            if (trackSubSequenceIndices.Count > 0)
            {
                int index = trackSubSequenceIndices.Last();
                while (index != -1)
                {
                    lis.Add(inputArr[index]);
                    index = trackParent[index];
                }
            }
            return lis;
        }

        /// <summary>
        /// We will do a binary search to get the index of the element which is greater than refVal
        /// </summary>
        /// <param name="listToSearchIndices">this contains a list of indices for the valueForIndices array.</param>
        /// <param name="valueForIndices">When we put all the indices from listToSearchIndices to the valueForIndices array we get the sorted list on which binary search can be performed</param>
        /// <param name="start">start index from which the list needs to be considered</param>
        /// <param name="end">end index till which the list needs to be considered</param>
        /// <param name="refVal">value which needs to be searched in the list so that we get the index in which the element value is just greater than the refVal</param>
        /// <returns></returns>
        private int GetIndexToReplace(List<int> listToSearchIndices, int[] valueForIndices, int start, int end, int refVal)
        {
            if (end < start)
            {
                // error case
                throw new InvalidOperationException();
            }
            else if( end == start)
            {
                return end;
            }
            int mid = start + ((end - start) / 2); // this is done to prevent overflow
            if(valueForIndices[listToSearchIndices[mid]] < refVal)
            {
                return GetIndexToReplace(listToSearchIndices, valueForIndices, mid + 1, end, refVal);
            }
            else
            {
                return GetIndexToReplace(listToSearchIndices, valueForIndices, start, mid, refVal);
            }

        }

        #endregion

        #region Algo3: dynamic programming 
        /// <summary>
        /// Steps are as follows:
        /// 1. Sort the given array arr and create sortedArr O(nlog(n))
        /// 2. Get the longest common subsequence between arr and sortedArr O(n^2)
        /// </summary>
        #endregion

        public static void TestLongestIncreasingSubSequence()
        {
            LongestIncreasingSubSequence lis = new LongestIncreasingSubSequence();
            int[] arr = new int[] { 21, 11, 3, 4, 6, 32, 6, 4, 43, 91 };
            List<int> lisArr = lis.GetLIS(arr);
            lisArr.Reverse();
            Console.WriteLine("LIS from algo1");
            PrintList(lisArr);

            lisArr = lis.GetLISAlgo2(arr);
            lisArr.Reverse();
            Console.WriteLine("LIS from algo2");
            PrintList(lisArr);

            arr = new int[] { 21};
            lisArr = lis.GetLIS(arr);
            lisArr.Reverse();
            Console.WriteLine("LIS from algo1");
            PrintList(lisArr);

            lisArr = lis.GetLISAlgo2(arr);
            lisArr.Reverse();
            Console.WriteLine("LIS from algo2");
            PrintList(lisArr);

            arr = new int[] { };
            lisArr = lis.GetLIS(arr);
            lisArr.Reverse();
            Console.WriteLine("LIS from algo1");
            PrintList(lisArr);

            lisArr = lis.GetLISAlgo2(arr);
            lisArr.Reverse();
            Console.WriteLine("LIS from algo2");
            PrintList(lisArr);

            arr = new int[] { 21,20,19,18,17,16,15,14,13,12,11,10 };
            lisArr = lis.GetLIS(arr);
            lisArr.Reverse();
            Console.WriteLine("LIS from algo1");
            PrintList(lisArr);

            lisArr = lis.GetLISAlgo2(arr);
            lisArr.Reverse();
            Console.WriteLine("LIS from algo2");
            PrintList(lisArr);

            arr = new int[] {1,2,3,4,5,6,7,78,91,103 };
            lisArr = lis.GetLIS(arr);
            lisArr.Reverse();
            Console.WriteLine("LIS from algo1");
            PrintList(lisArr);

            lisArr = lis.GetLISAlgo2(arr);
            lisArr.Reverse();
            Console.WriteLine("LIS from algo2");
            PrintList(lisArr);
        }

        private static void PrintList(List<int> arr)
        {
            Console.Write("{");
            foreach (int i in arr)
            {
                Console.Write(" {0} ,", i);
            }
            Console.Write("}");
            Console.WriteLine();
        }
    }
}
