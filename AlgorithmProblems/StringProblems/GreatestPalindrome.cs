using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems
{
    /// <summary>
    /// Given a number as a string, convert it into the greatest palindrome when you can do k replaces.
    /// Note: you cannot add/ remove a digit, but you can replace one digit by another one.
    /// Eg: if k = 1 and num is 4954 then answer is 4994
    /// if k = 3 and num is 3543 then answer is 9559
    /// if k = 6 and num is 333333 then answer is 999999
    /// if number cannot be converted to palindrome then return -1
    /// </summary>
    class GreatestPalindrome
    {
        /// <summary>
        /// Change a given number to the greatest palindrome possible
        /// </summary>
        /// <param name="number">number which needs to be made palindrome</param>
        /// <param name="k">number of replaces that can be done</param>
        /// <returns></returns>
        public static string ChangeToGreatestPalindrome(string number, int k)
        {
            int minNumOfSwaps = MinNumOfSwapsNeeded(number);
            if(minNumOfSwaps>k)
            {
                // making this number a palindrome is not possible
                return "-1";
            }

            int extraSwapsPossible = k - minNumOfSwaps;
            char[] numCharArr = number.ToCharArray();

            for (int index = 0; index < number.Length / 2; index++)
            {
                int indexFromEnd = number.Length - 1 - index;
                if (numCharArr[index] != numCharArr[indexFromEnd])
                {
                    // case where the chars numCharArr[index] and numCharArr[indexFromEnd] are different
                    if (extraSwapsPossible != 0)
                    {
                        if (!(numCharArr[index] == '9' || numCharArr[indexFromEnd] == '9'))
                        {
                            // only decrement once cause the other decrement has been counted in minNumOfSwaps
                            extraSwapsPossible--;
                        }
                        numCharArr[index] = '9';
                        numCharArr[indexFromEnd] = '9';
                    }
                    else
                    {
                        // since extra swaps/replaces are not possible we need to minimum replaces to make number a palindrome
                        if (numCharArr[index] > numCharArr[indexFromEnd])
                        {
                            numCharArr[indexFromEnd] = numCharArr[index];
                        }
                        else
                        {
                            numCharArr[index] = numCharArr[indexFromEnd];
                        }
                    }
                }
                else
                {
                    // case where the chars numCharArr[index] and numCharArr[indexFromEnd] are the same
                    if (extraSwapsPossible >= 2 && numCharArr[index] != '9')
                    {
                        // make sure that the numCharArr[index] is already not 9 and the extraSwapsPossible is greater than 2
                        numCharArr[index] = '9';
                        numCharArr[indexFromEnd] = '9';
                        extraSwapsPossible -= 2;
                    }
                }
            }
            if (number.Length % 2 != 0 && extraSwapsPossible > 0)
            {
                // the case in which the string is of odd length  we need to make the mid element 9
                numCharArr[number.Length / 2] = '9';
            }
            return new string(numCharArr);
        }

        /// <summary>
        /// Returns the minimum number of replaces needed to make the number a palindrome
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static int MinNumOfSwapsNeeded(string number)
        {
            int minNumOfSwaps = 0;
            for(int index = 0; index<number.Length/2; index++)
            {
                int indexFromEnd = number.Length - index - 1;
                if (number[index] != number[indexFromEnd])
                {
                    minNumOfSwaps++;
                }
            }
            return minNumOfSwaps;
        }
        public static void TestGreatestPalindrome()
        {
            string number = "4954";
            int k = 1;
            string palindrome = ChangeToGreatestPalindrome(number, k);
            Console.WriteLine("The greatest palindrome for {0} with {1} replaces is {2}", number, k, palindrome);

            number = "3543";
            k = 3;
            palindrome = ChangeToGreatestPalindrome(number, k);
            Console.WriteLine("The greatest palindrome for {0} with {1} replaces is {2}", number, k, palindrome);

            number = "333333";
            k = 6;
            palindrome = ChangeToGreatestPalindrome(number, k);
            Console.WriteLine("The greatest palindrome for {0} with {1} replaces is {2}", number, k, palindrome);

            number = "555";
            k = 3;
            palindrome = ChangeToGreatestPalindrome(number, k);
            Console.WriteLine("The greatest palindrome for {0} with {1} replaces is {2}", number, k, palindrome);

            number = "555";
            k = 1;
            palindrome = ChangeToGreatestPalindrome(number, k);
            Console.WriteLine("The greatest palindrome for {0} with {1} replaces is {2}", number, k, palindrome);

        }
    }
}
