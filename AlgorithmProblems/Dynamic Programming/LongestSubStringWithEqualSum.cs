using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// Find the longest substring of even size with the sum of digits on the right and left as equal
    /// </summary>
    public class LongestSubStringWithEqualSum
    {
        /// <summary>
        /// We can solve this problem using the dynamic programming approach
        /// M[i,j] -> stores the (sum of LHS from i to i+j/2) - (sum of RHS from i+j/2+1 to j)
        /// 
        /// M[i,j] = arr[i] - arr[j] + M[i+1,j-1] for even number of digits from i to j
        ///        = infinity for odd number of digits from i to j
        /// 
        /// We need to find the place where M[i,j] = 0 for j-i is the max
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string GetLongestSubStringWithEqualSum(string inputStr)
        {
            int startIndex = -1;
            int endIndex = -1;
            int[,] longestSubStringMat = new int[inputStr.Length, inputStr.Length];
            for(int k=1; k<inputStr.Length; k++)
            {
                for(int i = 0; i<inputStr.Length; i++)
                {
                    int j = i + k;
                    if(j>i && i<inputStr.Length && j<inputStr.Length) // as longestSubStringMat[i,j] gives the sum of LHS - Sum of RHS, hence j should be > i
                    {
                        if((j-i+1) %2 == 0)
                        {
                            // for even number of digits between i and j
                            longestSubStringMat[i, j] = GetDigit(inputStr, i) - GetDigit(inputStr, j) +( (i + 1 < j - 1 && i+1<inputStr.Length && j-1>=0) ? longestSubStringMat[i + 1, j - 1] : 0);
                            if(longestSubStringMat[i,j] == 0)
                            {
                                // we have found a case where the sum on LHS and RHS is same
                                if(startIndex ==-1 ||endIndex == -1 || endIndex-startIndex<j-i)
                                {
                                    startIndex = i;
                                    endIndex = j;
                                }
                            }
                        }
                    }
                }
            }
            return (startIndex == -1 || endIndex == -1) ? "" : inputStr.Substring(startIndex, endIndex - startIndex + 1);
        }


        public static int GetDigit(string inputStr, int index)
        {
            if(index<0 || index>=inputStr.Length)
            {
                // error condition
                throw new ArgumentException("index value is out of bounds");
            }
            return int.Parse(inputStr[index].ToString());
        }

        public static void TestLongestSubStringWithEqualSum()
        {
            Console.WriteLine("The longest substring with equal sum for {0} is {1}", "80532", GetLongestSubStringWithEqualSum("80532"));
            Console.WriteLine("The longest substring with equal sum for {0} is {1}", "6680532", GetLongestSubStringWithEqualSum("6680532"));
            Console.WriteLine("The longest substring with equal sum for {0} is {1}", "802882532", GetLongestSubStringWithEqualSum("802882532"));
            Console.WriteLine("The longest substring with equal sum for {0} is {1}", "371532", GetLongestSubStringWithEqualSum("371532"));
            Console.WriteLine("The longest substring with equal sum for {0} is {1}", "4480532", GetLongestSubStringWithEqualSum("4480532"));
            Console.WriteLine("The longest substring with equal sum for {0} is {1}", "213456789", GetLongestSubStringWithEqualSum("213456789"));
        }
    }
}
