using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// Also known as the typesetting problem used in text editors like ms word , latex, etc
    /// We are given a page with a length of one line as M
    /// and we will be given a list of words to fit on that line, we need to fit all the 
    /// given words in the page with minimum space between the words and the margin.
    /// 
    /// Note: for simplicity you dont have to worry about breaking the words and can assume that 
    /// all the words are less than the length of line on the page.
    /// 
    /// </summary>
    public class WordWrapping
    {
        /// <summary>
        /// We can solve this problem using dynamic programming
        /// Lets get the formulae for the dynamic programming approach
        /// 
        /// Assuming kth word is the (first word of the last line)
        /// lowestSlack[n] - means the lowest slack when we have all words from 0 to n on the page
        /// slack[k,n] - means the extra spaces in line where the first word is words[k] and the last word is words[n]\
        /// we will square the penalty so that no one line has a lot of spaces, we need to distribute the extra spaces
        /// 
        /// lowestSlack[n] = min {lowestSlack[k-1] + (slack[k,n])^2}
        ///                 k = 1->n
        /// 
        /// We will have to precompute slack matrix
        /// slack[i,j] = maxLengthOfLineOnPage - Sum(words[k].Length) + (j-i) only when this value is greater or equal to zero
        ///                                      k:i->j
        ///             INT_MAX , otherwise
        /// 
        /// the above calculation of slack also has overlapping subproblems
        /// slack[i,j] = slack[i,j-1] - words[j].Length - 1;
        /// </summary>
        /// <param name="words">array of all the words that can be fitted on the line</param>
        /// <param name="maxLengthOfLineOnPage">length of the line on the page</param>
        /// <returns></returns>
        public List<string> GetTypeSettedLines(string[] words, int maxLengthOfLineOnPage)
        {
            List<string> lines = new List<string>();
            int[,] slack = new int[words.Length, words.Length];
            int[] lowestSlack = new int[words.Length];
            int[] backtrack = new int[words.Length];

            // Populate the slack matrix
            for (int i= 0; i<words.Length; i++)
            {
                for (int j = i; j < words.Length; j++)
                {
                    if (i == j)
                    {
                        // we have only one word (words[j]) on the line and the assumption is
                        // we dont have anywords greater than maxLengthOfLineOnPage
                        slack[i, j] = maxLengthOfLineOnPage - words[j].Length;
                    }
                    else
                    {
                        if (slack[i, j - 1] != int.MaxValue)
                        {
                            int slackVal = slack[i, j - 1] - words[j].Length - 1;
                            slack[i, j] = (slackVal < 0) ? int.MaxValue : slackVal;
                        }
                        else
                        {
                            slack[i, j] = int.MaxValue;
                        }
                    }
                }
            }

            lowestSlack[0] = slack[0,0];
            for (int i =1; i<words.Length; i++)
            {
                lowestSlack[i] = int.MaxValue;
                int minSlack = int.MaxValue;
                for(int k=0; k<= i;k++)
                {
                    if (slack[k,i] != int.MaxValue && k - 1 >= 0 && lowestSlack[k-1] != int.MaxValue )
                    {
                        minSlack = lowestSlack[k-1] +  (int)Math.Pow(slack[k,i], 2);
                    }
                    if(minSlack < lowestSlack[i])
                    {
                        lowestSlack[i] = minSlack;
                        backtrack[i] = k;
                    }
                }
            }
            Console.WriteLine("The minimimum slack for printing the words is {0}", lowestSlack[words.Length - 1]);
            //Back track and get the listofStrings
            int index = words.Length - 1;
            while(index>=0)
            {
                lines.Add(GetStringOnLine(words, backtrack[index], index));
                index = backtrack[index] - 1;
            }
            lines.Reverse();
            return lines;
        }
        
        private string GetStringOnLine(string[] allWords, int firstWordIndex, int lastWordIndex)
        {
            StringBuilder sb = new StringBuilder();
            for(int index = firstWordIndex; index<=lastWordIndex; index++)
            {
                sb.Append(allWords[index]);
                if (index !=lastWordIndex)
                {
                    sb.Append(" ");
                }
            }
            return sb.ToString();
        }

        public static void TestWordWrapping()
        {
            WordWrapping ww = new WordWrapping();

            string[] words = new string[] { "it", "was", "the", "best", "of", "times", "and", "it", "was", "the", "worst", "of", "times." };
            PrintAllLines(ww.GetTypeSettedLines(words, 10));

            words = new string[] { "perseverence","it", "was", "the", "best", "of", "times", "and", "it", "was", "the", "worst", "of", "times.", "It", "was", "the", "time", "of", "great", "prosperity", "for", "some", "and", "crazy", "time", "for", "many", "others.", "Some", "people", "were", "persecuted", "and", "some", "were", "not."};
            PrintAllLines(ww.GetTypeSettedLines(words, 15));
        }

        private static void PrintAllLines(List<string> allLines)
        {
            foreach(string line in allLines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
