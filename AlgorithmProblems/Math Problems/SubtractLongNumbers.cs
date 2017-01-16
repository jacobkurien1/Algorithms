using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Math_Problems
{
    /// <summary>
    /// Subtract 2 very long numbers represented in form of string.
    /// Note these numbers are always positive integers
    /// </summary>
    class SubtractLongNumbers
    {
        /// <summary>
        /// Subtract the number in str1 from str2.
        /// Str1 will always have the larger number of 2,
        /// and this is made sure by the SubtractMain method.
        /// 
        /// The running time is O(n) where n is the max(str1.Length, str2.Length)
        /// </summary>
        /// <param name="str1">larger string</param>
        /// <param name="str2">smaller string</param>
        /// <returns></returns>
        private static string Subtract(string str1, string str2)
        {
            StringBuilder sb = new StringBuilder();
            str1 = Reverse(str1);
            str2 = Reverse(str2);
            int index = 0;
            int carry = 0;
            while(carry == 1 || index < str1.Length)
            {
                int val1 = int.Parse(str1[index].ToString());
                int val2 = (index >= str2.Length)?0:int.Parse(str2[index].ToString());
                val2 += carry;
                carry = 0;
                if(val2>val1)
                {
                    val1 += 10;
                    carry = 1;
                }
                sb.Insert(0, val1 - val2);
                index++;
            }
            return sb.ToString();
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// Find the larger number and call Subtract method with large num as the 1st argument and small num with 2nd argument
        /// Put - at the end if str2 is larger number.
        /// 
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static string SubtractMain(string str1, string str2)
        {
            bool isStr1Large = (str1.Length > str2.Length);
            if(str1.Length == str2.Length)
            {
                for(int index = 0; index<str1.Length; index++)
                {
                    if(str1[index] != str2[index])
                    {
                        isStr1Large = (str1[index] > str2[index]);
                    }
                    
                }
                isStr1Large = (str1[0] > str2[0]);
            }

            string result;
            if(isStr1Large)
            {
                result = Subtract(str1, str2);
            }
            else
            {
                result = Subtract(str2, str1);
                result = "-" + result;
            }

            return result;
        }

        public static void TestSubtractLongNumbers()
        {
            string str1 = "1255853577";
            string str2 = "9985985";
            Console.WriteLine("The difference is {0} and the expected answer is: 1245867592", SubtractMain(str1, str2));

            str2 = "1255853577";
            str1 = "9985985";
            Console.WriteLine("The difference is {0} and the expected answer is: -1245867592", SubtractMain(str1, str2));
        }
    }
}
