using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{

    /// <summary>
    /// Calculate the distance between 2 words
    /// The operations that can be done are insert, delete, edit.
    /// We will use dynamic programming approach here.
    /// This algorithm is also known as Levenshtein algorithm.
    /// </summary>
    class DistanceOfWords
    {
        /// <summary>
        /// We need to check all the possibility and since we are going to solve the subproblems again and again
        /// we will use the dynamic programming approach for optimization.
        /// We will have a matrix of size word1.Length X word2.Length to calculate the minimum distance.
        /// We also need another matrix(operationsMat) to store the operations that are done so that we can backtrack.
        /// 
        /// Step1: Initialization step: set all minDistance[i,0] =i and minDistance[0,j] = 0
        /// Set all the other elements in the minDistance matrix to max value of int.
        /// 
        /// Step2:
        /// minDistance[i,j] = min { 1+ minDistance[i,j-1]  for insertion
        ///                        { 1+ minDistance[i-1,j]  for deletion
        ///                        { 1+ minDistance[i-1,j-1] if the word1[i] needs to be replaced by word2[j]
        ///                        { minDistance[i-1,j-1]   if the word1[i] == word2[j]
        /// 
        /// The different values in the operationsMat are as follows:
        /// 0 - do nothing, we have a match here
        /// 1 - insert in word1 , the value word2[j]
        /// 2 - delete in word1, the value word1[i]
        /// 3 - replace in word1 with the value word2[j]
        /// 
        /// Step3: We need to backtrack to see how the actual transformation from word1 to word2 happens.
        /// </summary>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        private static void GetDistanceBtw2Words(string word1 , string word2)
        {
            //Step1:  intialization
            int[,] minDistance = new int[word1.Length + 1, word2.Length + 1];
            int[,] operationsMat = new int[word1.Length + 1, word2.Length + 1];

            for(int i = 1; i<word1.Length+1; i++)
            {
                for(int j=1; j<word2.Length+1; j++)
                {
                    // initilize the minDistance to max value of int
                    minDistance[i,j] = int.MaxValue;
                }
            }

            for (int i = 1; i < word1.Length + 1; i++)
            {
                minDistance[i, 0] = i;
            }
            for (int j = 1; j < word2.Length + 1; j++)
            {
                minDistance[0, j] = j;
            }


            //Step2: lets find the minimum distance
            for(int i = 1; i < word1.Length + word2.Length; i++)
            {
                int rowIndex = i;
                for(int colIndex = 1; colIndex <= word2.Length; colIndex++ )
                {
                    if(rowIndex > word1.Length)
                    {
                        rowIndex--;
                        continue;
                    }
                    if(rowIndex<=0)
                    {
                        break;
                    }
                    if (minDistance[rowIndex, colIndex] > 1 + minDistance[rowIndex, colIndex - 1])
                    {
                        // insertion will give minimum distance
                        minDistance[rowIndex, colIndex] = 1 + minDistance[rowIndex, colIndex - 1];
                        operationsMat[rowIndex, colIndex] = 1;
                    }
                    if (minDistance[rowIndex, colIndex] > 1 + minDistance[rowIndex - 1, colIndex])
                    {
                        // deletion will give minimum distance
                        minDistance[rowIndex, colIndex] = 1 + minDistance[rowIndex - 1, colIndex];
                        operationsMat[rowIndex, colIndex] = 2;
                    }
                    if (word1[rowIndex-1] == word2[colIndex-1] && minDistance[rowIndex, colIndex] > minDistance[rowIndex - 1, colIndex - 1])
                    {
                        // We have a match here
                        minDistance[rowIndex, colIndex] = minDistance[rowIndex - 1, colIndex - 1];
                        operationsMat[rowIndex, colIndex] = 0;
                    }
                    if (word1[rowIndex-1] != word2[colIndex-1] && minDistance[rowIndex, colIndex] > 1 + minDistance[rowIndex - 1, colIndex - 1])
                    {
                        /// replace will give minimum distance
                        minDistance[rowIndex, colIndex] = 1 + minDistance[rowIndex - 1, colIndex - 1];
                        operationsMat[rowIndex, colIndex] = 3;
                    }
                    rowIndex--;
                }
            }

            //Step3: Backtracking to get the operations to make word1 equal to word2
            Console.WriteLine("The minimum distance between word1: {0} and word2 {1} is {2}", word1, word2, minDistance[word1.Length, word2.Length]);

            Stack<string> backtrackedOperations = new Stack<string>();
            int rowIdx = word1.Length;
            int colIdx = word2.Length;
            while(rowIdx>0 && colIdx>0)
            {
                switch (operationsMat[rowIdx,colIdx] )
                {
                    case 0:
                        backtrackedOperations.Push("match");
                        rowIdx--;
                        colIdx--;
                        break;
                    case 1:
                        backtrackedOperations.Push("insert");
                        colIdx--;
                        break;
                    case 2:
                        backtrackedOperations.Push("delete");
                        rowIdx--;
                        break;
                    case 3:
                        backtrackedOperations.Push("replace");
                        colIdx--;
                        rowIdx--;
                        break;
                    default:
                        throw new Exception("Illegal value");
                }
            }

            while(backtrackedOperations.Count>0)
            {
                Console.Write(backtrackedOperations.Pop() + " ");
            }
            Console.WriteLine();
        }

        public static void TestDistanceOfWords()
        {
            GetDistanceBtw2Words("jacob", "jax");
            GetDistanceBtw2Words("jax", "jacob");
            GetDistanceBtw2Words("jacob", "jacob");
            GetDistanceBtw2Words("intention", "execution");
        }
    }
}
