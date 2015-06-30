using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems
{
    class StringRotation
    {
        public bool IsThisRotatedString(string str1, string str2)
        {
            if(str1.Length != str2.Length)
            {
                return false;
            }
            string sx = str1 + str1;
            return (sx.Contains(str2));
        }

        public static void TestIsThisRotatedString()
        {
            StringRotation sr = new StringRotation();
            // positive test case
            string inputStr1 = "jacob";
            string inputStr2 = "objac";
            Console.WriteLine("the 2 strings are:" + inputStr1 + " and " + inputStr2 + ". And is one string formed by the rotation of other: " + sr.IsThisRotatedString(inputStr1, inputStr2));

            // negative test case
            inputStr1 = "jacob";
            inputStr2 = "casjo";
            Console.WriteLine("the 2 strings are:" + inputStr1 + " and " + inputStr2 + ". And is one string formed by the rotation of other: " + sr.IsThisRotatedString(inputStr1, inputStr2));            

        }
    }
}
