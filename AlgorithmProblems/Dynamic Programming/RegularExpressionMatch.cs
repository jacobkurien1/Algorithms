using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// Given a text string and a regular expression. Do a pattern match of the regex in the text string
    /// For sake of convienience we will use the following special charaters
    /// * - 0 or 1 preceeding character before *
    /// . - matches any single character
    /// 
    /// </summary>
    public class RegularExpressionMatch
    {
        /// <summary>
        /// This is the main subroutine which calls the recursive subroutine after setting the startIndex
        /// </summary>
        /// <param name="text">text inside which the pattern needs to be seached</param>
        /// <param name="pattern">The pattern which needs to be searched</param>
        /// <returns></returns>
        public List<RegExMatchPosition> GetAllMatches(string text, string pattern)
        {
            List<RegExMatchPosition> allMatches = new List<RegExMatchPosition>();
            for (int i = 0; i < text.Length; i++)
            {
                allMatches.AddRange(GetAllMatchesWithTextIndex(text, pattern, i));
            }
            return allMatches;
        }

        /// <summary>
        /// We can solve this using the Dynamic Programming approach.
        /// Text is present along the row axis and the pattern is present along the column axis
        /// Match is a boolean matrix where Match[i,j] means whether Text[0,i] is matching pattern[0,j]
        /// 
        /// Match[i,j] = if text[i] == pattern[j] || pattern[j] == "."  -> Match[i-1, j-1] 
        ///             if pattern[j] == "*"  -> Match[i, j-2] case where 0 occurance of the character before *
        ///                                    -> Match[i-1, j] if text[i] == pattern[j-1] || pattern[j-1]== "." 
        ///             else -> False
        /// 
        /// All the Trues in last column of the boolean matrix will means the match
        ///             
        /// </summary>
        /// <param name="text">text string</param>
        /// <param name="pattern">pattern string</param>
        /// <param name="startIndex">Index from which the start of the text should be considered</param>
        /// <returns></returns>
        public List<RegExMatchPosition> GetAllMatchesWithTextIndex(string text, string pattern, int startIndex)
        {
            // initialization
            List<RegExMatchPosition> ret = new List<RegExMatchPosition>();
            bool[,] match = new bool[text.Length - startIndex + 1, pattern.Length + 1];
            for(int i=1; i<=text.Length - startIndex; i++)
            {
                match[i, 0] = false;
            }
            for (int j = 1; j <= pattern.Length; j++)
            {
                match[0,j] = false;
            }
            match[0, 0] = true;

            // fill the match matrix
            for(int i=1; i<=text.Length - startIndex; i++)
            {
                for(int j=1; j<=pattern.Length; j++)
                {
                    if (text[i - 1 + startIndex] == pattern[j-1] || pattern[j-1] == '.')
                    {
                        // The case where the characters match
                        match[i, j] = match[i - 1, j - 1];
                    }
                    else if (pattern[j-1] == '*')
                    {
                        match[i, j] = match[i, j - 2]; // The case where 0 characters before the * is present
                        if(text[i - 1 + startIndex] == pattern[j-2] || pattern[j-2] == '.')
                        {
                            match[i, j] = match[i, j] || match[i - 1, j];
                        }
                    }
                    else
                    {
                        //This is the case where match is not present
                        match[i, j] = false;
                    }

                }
            }

            //Get all the matches
            for (int i = 1; i <= text.Length - startIndex; i++)
            {
                if(match[i,pattern.Length])
                {
                    //We have a match here
                    ret.Add(new RegExMatchPosition(startIndex, i, text.Substring(startIndex, i)));
                }
            }

            return ret;
        }

        public static void TestRegularExpressionMatch()
        {
            
            RegularExpressionMatch regex = new RegularExpressionMatch();
            string text = "abaaxyzby";
            string pattern = "aba*.*b";
            Console.WriteLine("The matches in text:{0} with pattern:{1} are as shown below", text, pattern);
            List<RegExMatchPosition> allMatches = regex.GetAllMatches(text, pattern);
            PrintAllMatches(allMatches);

            text = "jacob is a cool boy";
            pattern = "o.*l";
            Console.WriteLine("The matches in text:{0} with pattern:{1} are as shown below", text, pattern);
            allMatches = regex.GetAllMatches(text, pattern);
            PrintAllMatches(allMatches);

            text = "zzzabaaaaaay";
            pattern = "aba*.";
            Console.WriteLine("The matches in text:{0} with pattern:{1} are as shown below", text, pattern);
            allMatches = regex.GetAllMatches(text, pattern);
            PrintAllMatches(allMatches);

        }

        public static void PrintAllMatches(List<RegExMatchPosition> allMatches)
        {
            foreach(RegExMatchPosition pos in allMatches)
            {
                Console.WriteLine(pos.ToString());
            }
        }
    }
    public class RegExMatchPosition
    {
        public int Offset { get; set; }
        public int StartIndex { get; set; }
        public string MatchText { get; set; }

        public RegExMatchPosition(int startIndex, int offset, string matchText)
        {
            StartIndex = startIndex;
            Offset = offset;
            MatchText = matchText;
        }

        public override string ToString()
        {
            return string.Format("{0} at {1}", MatchText, StartIndex);
        }
    }
}
