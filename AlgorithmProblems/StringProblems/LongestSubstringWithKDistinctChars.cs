using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems
{
    /// <summary>
    /// Find the longest substring with k distinct chars
    /// </summary>
    class LongestSubstringWithKDistinctChars
    {
        /// <summary>
        /// Algo:
        /// 1.Keep a start index and current index.
        /// 2.Keep incrementing the current index and add the new chars to the dict.
        /// 3.Once the dict size becomes equal to k, for all subsequent adds remove the chars and increment start index till the dict size == k
        /// 4. Keep track of the string of max length.
        /// 
        /// The running time is O(n)
        /// The space requirement is O(k)
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static string GetLongestSubstringWithKDistinctChars(string str, int k)
        {
            if(str == null || str.Length == 0 || k<=0)
            {
                return string.Empty;
            }
            int st = 0;
            string ret = str[st].ToString();
            int current = 1;
            Dictionary<char, int> dict = new Dictionary<char, int>();
            dict[str[st]] = 1;
            int len = 1;

            while(current <str.Length)
            {
                if(dict.ContainsKey(str[current]))
                {
                    dict[str[current]] += 1;
                    len++;
                    
                }
                else
                {
                    if(dict.Keys.Count <k) // dict.Keys.Count == distinct charaters
                    {
                        dict[str[current]] = 1;
                        len++;
                    }
                    else
                    {
                        // distinctChars ==k
                        while (dict.Keys.Count == k)
                        {
                            dict[str[st]] -= 1;
                            if (dict[str[st]] == 0)
                            {
                                dict.Remove(str[st]);
                            }
                            st++;
                            len--;
                        }
                        dict[str[current]] = 1;
                        len++;
                    }
                }
                current++;
                if (len > ret.Length)
                {
                    ret = str.Substring(st, len);
                }
            }

            return ret;
        }

        public static void TestLongestSubstringWithKDistinctChars()
        {
            string str = "xxaaabbbbcccccklsjakhfksjdhf";
            int k = 3;
            Console.WriteLine("The longest substring with {0} distinct chars is {1}. Expected: aaabbbbccccc", k, GetLongestSubstringWithKDistinctChars(str, k));

            str = "xxaaabbbbcccccklsjakhfksjdhf";
            k = 4;
            Console.WriteLine("The longest substring with {0} distinct chars is {1}. Expected: xxaaabbbbccccc", k, GetLongestSubstringWithKDistinctChars(str, k));

            str = "xxaaabbbbcccccklsjakhfksjdhfaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            k = 3;
            Console.WriteLine("The longest substring with {0} distinct chars is {1}. Expected: hfaaaaaaaaaaaaaaaaaaaaaaaaaaa", k, GetLongestSubstringWithKDistinctChars(str, k));
        }
    }
}
