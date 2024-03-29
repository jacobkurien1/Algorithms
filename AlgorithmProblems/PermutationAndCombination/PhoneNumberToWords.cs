﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.PermutationAndCombination
{
    /// <summary>
    /// given a phone number we need to find all the different words that can be formed using the number
    /// for example 2 corresponds to a,b,c and 3 to d,e,f
    /// so 23 can have ad,ae,af,bd,be,bf,cd,ce,cf different combinations
    /// </summary>
    public class PhoneNumberToWords
    {
        /// <summary>
        /// this dictionary will contain the mapping between the number to the characters as shown in the pinpad
        /// </summary>
        public Dictionary<int, List<char>> NumToChar { get; set; }
        public Dictionary<char, List<char>> CharPhNumToChar { get; set; }

        public PhoneNumberToWords()
        {
            NumToChar = new Dictionary<int, List<char>>();
            CharPhNumToChar = new Dictionary<char, List<char>>();
            NumToChar.Add(2, new List<char> { 'A', 'B', 'C' });
            NumToChar.Add(3, new List<char> { 'D', 'E', 'F' });
            NumToChar.Add(4, new List<char> { 'G', 'H', 'I' });
            NumToChar.Add(5, new List<char> { 'J', 'K', 'L' });
            NumToChar.Add(6, new List<char> { 'M', 'N', 'O' });
            NumToChar.Add(7, new List<char> { 'P', 'Q', 'R', 'S' });
            NumToChar.Add(8, new List<char> { 'T', 'U', 'V' });
            NumToChar.Add(9, new List<char> { 'W', 'X', 'Y', 'Z' });
            foreach(KeyValuePair<int, List<char>> kvp in NumToChar)
            {
                CharPhNumToChar[Char.Parse(kvp.Key.ToString())] = kvp.Value;
            }
        }

        #region Method1: Recusive Method
        /// <summary>
        /// this is the recursive subroutine which helps in getting the combinations from the phonenumbers
        /// this subroutine will call itself with phonenumber /= 10 and the current subroutine will work 
        /// with the last digit of the phone number (phoneNumber % 10)
        /// 
        /// The running time of this method is O(4^n)
        /// The space requirement is also O(4^n) as we have to save all the combination
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns>list of combinations from the phone number</returns>
        public List<string> ConvertPhoneNumberToWords(long phoneNumber)
        {
            // Note if the phone number has 00<other_valid_number> at the start of the number then we will get the different
            // combinations from <other_valid_numbers> and not throw ill-formed expression exception
            if(phoneNumber == 0)
            {
                // base case which ends the recursion
                return new List<string>() { string.Empty };
            }

            int lastDigit = (int)(phoneNumber % 10);
            phoneNumber = phoneNumber / 10;
            List<string> ret = new List<string>();
            List<string> allCombinations = ConvertPhoneNumberToWords(phoneNumber);
            if(!NumToChar.ContainsKey(lastDigit))
            {
                // if the digits do not lie between 2-9 then we throw the exception
                throw new Exception("ill formed phone number");
            }
            foreach(char c in NumToChar[lastDigit])
            {
                ret.AddRange(AddCurrentCharToAllString(c, allCombinations));
            }
            return ret;
        }

        private List<string> AddCurrentCharToAllString(char currentChar, List<string> allStrings)
        {
            List<string> ret = new List<string>();
            for(int i=0; i<allStrings.Count; i++)
            {
                ret.Add(currentChar + allStrings[i]);
            }
            return ret;
        }

        #endregion

        #region Method2: Iterative Method(Optimal method)

        /// <summary>
        /// This is the iterative method to get all possible combinations when a phone number is given.
        /// Here we will use 2 lists and keep adding the chars to the strings of one list and save it in the second list.
        /// 
        /// The running time of this method is O(4^n)
        /// The space requirement is also O(4^n) as we have to save all the combinations
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public List<string> ConvertPhoneNumberToWords(string phoneNumber)
        {
            if(phoneNumber == null || phoneNumber == string.Empty)
            {
                return null;
            }
            List<string> previous = new List<string>() { "" };
            for(int numIndex = 0; numIndex < phoneNumber.Length; numIndex++)
            {
                List<string> currentList = new List<string>(); 
                if (!CharPhNumToChar.ContainsKey(phoneNumber[numIndex]))
                {
                    throw new Exception("Invalid Phone number");
                }

                foreach (char c in CharPhNumToChar[phoneNumber[numIndex]])
                {
                    foreach(string str in previous)
                    {
                        currentList.Add(str + c.ToString());
                    }
                }
                previous = currentList;
            }
            return previous;
        }

        #endregion

        public static void TestPhoneNumberToWords()
        {
            PhoneNumberToWords PhToWords = new PhoneNumberToWords();
            long phoneNumber = 285;
            Console.WriteLine("All the different combinations of {0} is as shown below", phoneNumber);
            Console.WriteLine("Recursive Solution");
            PrintCombinations(PhToWords.ConvertPhoneNumberToWords(phoneNumber));
            Console.WriteLine("Iterative Solution");
            PrintCombinations(PhToWords.ConvertPhoneNumberToWords(phoneNumber.ToString()));

            phoneNumber = 9876;
            Console.WriteLine("All the different combinations of {0} is as shown below", phoneNumber);
            Console.WriteLine("Recursive Solution");
            PrintCombinations(PhToWords.ConvertPhoneNumberToWords(phoneNumber));
            Console.WriteLine("Iterative Solution");
            PrintCombinations(PhToWords.ConvertPhoneNumberToWords(phoneNumber.ToString()));

            phoneNumber = 052;
            Console.WriteLine("All the different combinations of {0} is as shown below", phoneNumber);
            Console.WriteLine("Recursive Solution");
            PrintCombinations(PhToWords.ConvertPhoneNumberToWords(phoneNumber));
            Console.WriteLine("Iterative Solution");
            PrintCombinations(PhToWords.ConvertPhoneNumberToWords(phoneNumber.ToString()));

            phoneNumber = 201;
            Console.WriteLine("All the different combinations of {0} is as shown below", phoneNumber);
            try
            {
                Console.WriteLine("Recursive Solution");
                PrintCombinations(PhToWords.ConvertPhoneNumberToWords(phoneNumber));
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e.Message);
            }
            try
            {
                Console.WriteLine("Iterative Solution");
                PrintCombinations(PhToWords.ConvertPhoneNumberToWords(phoneNumber.ToString()));
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e.Message);
            }
        }
        private static void PrintCombinations(List<string> combinations)
        {
            Console.WriteLine("The total number of different combinations are {0}", combinations.Count);
            foreach (string combination in combinations)
            {
                Console.Write("{0} ", combination);
            }
            Console.WriteLine();
        }
    }
}
