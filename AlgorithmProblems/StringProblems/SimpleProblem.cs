using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems
{
    class SimpleProblem
    {
        //Reverse a string (inplace operation)
        public static Char[] ReverseString(Char[] str)
        {
            int len = str.Length;
            for (int i = 0; i < Math.Floor(len / 2.0); i++)
            {
                char temp = str[i];
                str[i] = str[len - 1 - i];
                str[len - 1 - i] = temp;
 
            }
            return str;
        }
    }
}
