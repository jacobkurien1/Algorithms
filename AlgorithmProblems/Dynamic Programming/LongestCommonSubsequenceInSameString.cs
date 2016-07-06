using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// Given a string check whether there is any subsequence which repeats itself
    /// Here subsequence is the non contiguous pattern with the same relative order
    /// </summary>
    public class LongestCommonSubsequenceInSameString
    {
        /// <summary>
        /// Represents the move which will help us back track the steps to calculate the LCS string
        /// </summary>
        enum Move
        {
            Left,
            Top,
            Diagonal
        }

        /// <summary>
        /// We can use the dynamic programming approach to solve this problem
        /// 
        /// This is similar to the LCS problem with the same string in both row axis and column axis 
        /// except we will not use the i==j case cause that means we are checking the LCS with the same string
        /// 
        /// We will have LCS 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GetLCSInSameString(string input)
        {
            int[,] LCS = new int[input.Length, input.Length];
            Move[,] backtrack = new Move[input.Length, input.Length];
            
            // Dynamic programming  execution
            for(int i=0; i<input.Length; i++)
            {
                for (int j = i+1; j < input.Length; j++)
                {
                    if(input[i] == input[j])
                    {
                        LCS[i, j] = 1 + ((i - 1 >= 0 && j - 1 >= 0) ? LCS[i - 1, j - 1] : 0);
                        backtrack[i, j] = Move.Diagonal;
                    }
                    if(i - 1>=0 && LCS[i-1,j]> LCS[i,j])
                    {
                        LCS[i, j] = LCS[i - 1, j];
                        backtrack[i, j] = Move.Top;
                    }
                    if (j-1>0 && LCS[i,j-1]>LCS[i,j])
                    {
                        LCS[i, j] = LCS[i, j - 1];
                        backtrack[i, j] = Move.Left;
                    }
                }
            }

            Console.WriteLine("The length longest common subsequence in :{0} is:{1}", input, LCS[input.Length - 2, input.Length - 1]);

            //Back track to get the value of the LCS of repeating subsequence in the string
            int row = input.Length - 2;
            int col = input.Length - 1;
            StringBuilder sb = new StringBuilder();
            while(row>=0 && col >=0)
            {
                if(backtrack[row,col] == Move.Diagonal)
                {
                    sb.Append(input[col]);
                    row--;
                    col--;
                }
                else if (backtrack[row,col] == Move.Top)
                {
                    row--;
                }
                else if (backtrack[row,col] == Move.Left)
                {
                    col--;
                }
            }
            char[] sbCharArray = sb.ToString().ToCharArray();
            Array.Reverse(sbCharArray);
            return new string(sbCharArray);
        }

        public static void TestLongestCommonSubsequenceInSameString()
        {
            LongestCommonSubsequenceInSameString lcs = new LongestCommonSubsequenceInSameString();
            string inputStr = "abab";
            Console.WriteLine("the LCS in the string {0} is {1}", inputStr, lcs.GetLCSInSameString(inputStr));

            inputStr = "abba";
            Console.WriteLine("the LCS in the string {0} is {1}", inputStr, lcs.GetLCSInSameString(inputStr));

            inputStr = "acbdaghfb";
            Console.WriteLine("the LCS in the string {0} is {1}", inputStr, lcs.GetLCSInSameString(inputStr));

            inputStr = "abcdacb";
            Console.WriteLine("the LCS in the string {0} is {1}", inputStr, lcs.GetLCSInSameString(inputStr));
        }
    }
}
