using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems
{
    /// <summary>
    /// Reverse a string while keeping the vowels at the same location
    /// for eg: recommend -> denommecr
    /// </summary>
    class ReverseStringWithoutVowels
    {
        /// <summary>
        /// Main function to reverse the string keeping the vowels at the same location
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReverseStringWOVowels(string str)
        {
            char[] charArr = str.ToCharArray();
            int left = 0;
            int right = str.Length - 1;
            while (left < right)
            {
                if (IsVowel(charArr[left]) && IsVowel(charArr[right]))
                {
                    left++;
                    right--;
                }
                else if (!IsVowel(charArr[left]) && IsVowel(charArr[right]))
                {
                    right--;
                }
                else if (IsVowel(charArr[left]) && !IsVowel(charArr[right]))
                {
                    left++;
                }
                else
                {
                    Swap(charArr, left, right);
                    left++;
                    right--;
                }
            }


            return new string(charArr);
        }

        /// <summary>
        /// Checks whether a character is a vowel or not
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private static bool IsVowel(char v)
        {
            return (v == 'a' || v == 'e' || v == 'i' || v == 'o' || v == 'u');
        }

        /// <summary>
        /// Swaps the index1 and index2 in the charArr
        /// </summary>
        /// <param name="charArr"></param>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        private static void Swap(char[] charArr, int index1, int index2)
        {
            char temp = charArr[index1];
            charArr[index1] = charArr[index2];
            charArr[index2] = temp;

        }

        public static void TestReverseStringWithoutVowels()
        {
            string str = "recommend";
            Console.WriteLine("The reversed version of {0} without reversing the vowels is {1}", str, ReverseStringWOVowels(str));
        }
    }
}
