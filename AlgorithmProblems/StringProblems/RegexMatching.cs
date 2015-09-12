using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems
{
    /// <summary>
    /// ^_________ means match at the start of the text string
    /// _________$ means match at the end of the text string
    /// _? means the previous element is present 0 or 1 times
    /// . any one character
    /// _* preceding character occurs 0 or many times
    /// _+ preceding character occurs 1 or many times(not used in this problem)
    /// </summary>
    class RegexMatching
    {
        private static bool MatchHere(string text, string pattern)
        {
            if (text.Length == 0 && (pattern.Length == 0 || pattern == "$"))
            {
                //match has been found here
                return true;
            }
            else if (text.Length== 0 && pattern.Length!=0)
            {
                //match was not found
                return false;
            }
            else if (text.Length !=0 && pattern.Length == 0)
            {
                return true;
            }

            if (pattern.Length >=2 && pattern[0] == '.' && pattern[1] == '*')
            {
                // Traverse all over the text to find the match
                for(int i=1; i<text.Length;i++)
                {
                    if(MatchHere(text.Substring(i,text.Length-i), pattern.Substring(2, pattern.Length-2)))
                    {
                        return true;
                    }
                }
            }
            else if (pattern.Length >= 2 && pattern[0] == '.' && pattern[1] == '?')
            {
                return MatchHere(text.Substring(1, text.Length - 1), pattern.Substring(2, pattern.Length - 2)) ||
                    MatchHere(text, pattern.Substring(2, pattern.Length - 2));
            }
            else if (pattern.Length >= 2 && pattern[1] == '?') // pattern[0] will be any charater
            {
                // we will need to match for the 0 or 1 occurance of this character
                return (text[0] == pattern[0] && MatchHere(text.Substring(1, text.Length-1), pattern.Substring(2, pattern.Length - 2))) ||
                    MatchHere(text, pattern.Substring(2, pattern.Length - 2));
            }
            else if (pattern.Length >= 2 && pattern[1] == '*')
            {
                if (MatchHere(text, pattern.Substring(2, pattern.Length - 2)))
                {
                    return true;
                }
                for (int i = 1; i < text.Length; i++)
                {
                    if (text[i] != pattern[0])
                    {
                        return false;
                    }
                    else
                    {
                        if (MatchHere(text.Substring(i, text.Length - i), pattern.Substring(2, pattern.Length - 2)))
                        {
                            return true;
                        }
                    }
                    
                }
            }
            else
            {
                if(text[0] == pattern[0] || pattern[0] == '.')
                {
                    return MatchHere(text.Substring(1, text.Length - 1), pattern.Substring(1, pattern.Length - 1));
                }
                else
                {
                    // match is not found
                    return false;
                }
            }
            return false;
        }

        private static bool Match(string text, string pattern)
        {
            if(pattern[0] == '^')
            {
                return MatchHere(text, pattern.Substring(1, pattern.Length - 1));
            }
            else
            {
                for(int i=0; i<text.Length; i++)
                {
                    if(MatchHere(text.Substring(i,text.Length-i), pattern))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public static void TestMatch()
        {
            string text = "Joker said: but the joke was on me.";
            string pattern = "said";

            Console.WriteLine("The text is :{0} and the pattern is :{1}. Is the pattern present in the text:{2}", text, pattern, Match(text, pattern));

            pattern = "on me.$";
            Console.WriteLine("The text is :{0} and the pattern is :{1}. Is the pattern present in the text:{2}", text, pattern, Match(text, pattern));

            pattern = "^Joker";
            Console.WriteLine("The text is :{0} and the pattern is :{1}. Is the pattern present in the text:{2}", text, pattern, Match(text, pattern));

            pattern = "but.*joke";
            Console.WriteLine("The text is :{0} and the pattern is :{1}. Is the pattern present in the text:{2}", text, pattern, Match(text, pattern));

            pattern = "j.?ke";
            Console.WriteLine("The text is :{0} and the pattern is :{1}. Is the pattern present in the text:{2}", text, pattern, Match(text, pattern));

            pattern = "j.ke";
            Console.WriteLine("The text is :{0} and the pattern is :{1}. Is the pattern present in the text:{2}", text, pattern, Match(text, pattern));

            pattern = "jo?ke";
            Console.WriteLine("The text is :{0} and the pattern is :{1}. Is the pattern present in the text:{2}", text, pattern, Match(text, pattern));

        }
    }
}
