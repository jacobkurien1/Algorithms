using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// Given a string "123" we need to decode all the combinations strings that can be formed by converting
    /// 1->a
    /// 2->b
    /// and so on
    /// 
    /// in this case 123 will get converted to 
    /// abc, lc, aw
    /// </summary>
    class StringDecodeAsAlphabets
    {
        #region Algo1: Recursively

        /// <summary>
        /// We can use recursion to find all the different string combinations.
        /// Here the issue is we are doing the same calculations again and again.
        /// Hence we should use bottoms up approach(Dynamic Programming) to do the calculations once.
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<string> DecodeStrings(string inputStr, int index)
        {
            List<string> ret = new List<string>();
            if(index <inputStr.Length)
            {
                char char1 = GetCharFromValue(int.Parse(inputStr[index].ToString()));

                ret.AddRange(AddCharToAllStrings(DecodeStrings(inputStr, index + 1), char1));
                if (index + 2 <= inputStr.Length)
                {
                    int valWith2Chars = int.Parse(inputStr.Substring(index, 2));
                    if (valWith2Chars <= 26)
                    {
                        char char2 = GetCharFromValue(valWith2Chars);
                        ret.AddRange(AddCharToAllStrings(DecodeStrings(inputStr, index + 2), char2));
                    }
                }
            }
            return ret;
        }

        #endregion

        /// <summary>
        /// Adds the characterToAdd to all the strings in allStrings and return the list of strings
        /// </summary>
        /// <param name="allStrings">list of all the strings</param>
        /// <param name="characterToAdd">character to add to the list of strings</param>
        /// <returns></returns>
        private List<string> AddCharToAllStrings(List<string> allStrings, char characterToAdd)
        {
            if (allStrings == null || allStrings.Count == 0)
            {
                return new List<string> { characterToAdd.ToString() };
            }
            List<string> ret = new List<string>();
            foreach (string str in allStrings)
            {
                ret.Add(characterToAdd + str);
            }
            return ret;
        }

        /// <summary>
        /// Convert the int value to char
        /// 1->a
        /// 2->b
        /// etc
        /// </summary>
        /// <param name="intVal"></param>
        /// <returns></returns>
        private char GetCharFromValue(int intVal)
        {
            return (char)(intVal + 96);
        }

        #region Algo2: Dynamic Programming
        /// <summary>
        /// stringsArr[i] = str[i] + {stringsArr[i+1]}
        ///                 str.substring(i,2) + {stringArr[i+2]} 
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public List<string> DecodeStringsAlgo2(string inputStr)
        {
            List<string>[] stringsArr = new List<string>[inputStr.Length];
            for(int i=inputStr.Length-1; i>=0; i--)
            {
                stringsArr[i] = new List<string>();

                // Case where the first alphabet is considered to be formed by single digit
                List<string> allStringsWhenFirstCharIsTaken = AddCharToAllStrings((i + 1 < inputStr.Length) ? stringsArr[i + 1] : null, GetCharFromValue(int.Parse(inputStr[i].ToString())));
                stringsArr[i].AddRange(allStringsWhenFirstCharIsTaken);

                // Case where the alphabet is considered to be formed by two digits
                if (i + 2 <= inputStr.Length)
                {
                    int valWith2Chars = int.Parse(inputStr.Substring(i, 2));
                    if (valWith2Chars <= 26)
                    {
                        // We need to do this check to make sure that the values are less than z->26
                        List<string> allStringsWhenTwoCharsAreTaken = AddCharToAllStrings((i + 2 < inputStr.Length) ? stringsArr[i + 2] : null, GetCharFromValue(valWith2Chars));
                        stringsArr[i].AddRange(allStringsWhenTwoCharsAreTaken);
                    }
                }
            }
            return stringsArr[0];
        }

        
        #endregion

        public static void TestStringDecodeAsAlphabets()
        {
            StringDecodeAsAlphabets strDecode = new StringDecodeAsAlphabets();
            List<string> allStrings = strDecode.DecodeStrings("123", 0);
            Console.WriteLine("The decoded strings are:");
            PrintAllStrings(allStrings);

            allStrings = strDecode.DecodeStringsAlgo2("123");
            Console.WriteLine("The decoded strings from Algo2 are:");
            PrintAllStrings(allStrings);
        }
        private static void PrintAllStrings(List<string> allStrings)
        {
            foreach(string str in allStrings)
            {
                Console.Write("{0}, ", str);
            }
            Console.WriteLine();
        }
    }
}
