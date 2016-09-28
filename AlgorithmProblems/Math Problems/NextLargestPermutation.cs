using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Math_Problems
{
    /// <summary>
    /// Find the next largest permutation of a number
    /// for eg: 35421 -> 41235
    /// 4153 - > 4315
    /// 3154 -> 3415
    /// </summary>
    class NextLargestPermutation
    {
        /// <summary>
        /// Algo: 1. traverse from the end and any digit which is less than the previous digit is the pivot point
        /// 2. Get the index of the digit greater than the pivot point digit (use binary search)
        /// 3. Swap the pivot digit and the digit greater than the pivot point.
        /// 4. Reverse all the char Array from pivot Index +1 to the end (as they are present in descending order)
        /// 
        /// The running time of the algo is O(n), as we take O(n) time to do the reverse.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int GetNextLargestPermutationOfNum(int num)
        {
            if (num > 0)
            {
                char[] number = num.ToString().ToCharArray();
                for (int i = number.Length - 2; i >= 0; i--)
                {
                    if (number[i] < number[i + 1])
                    {
                        // we have found the pivot point
                        // now find the element in sb which is greater than 
                        int greaterIndex = BinarySearch(number, i, number[i]);
                        if (greaterIndex == -1)
                        {
                            // we dont have any digit greater than the currentNum
                            // hence we cannot get a greater permutation
                            break;
                        }

                        // swap the character at greaterIndex and i
                        Swap(number, i, greaterIndex);

                        // reverse the char array from index i+1 to number.Length-1
                        Reverse(number, i + 1, number.Length - 1);

                        return int.Parse(new string(number));
                    }
                }
            }
            return -1;

        }

        /// <summary>
        /// Reverses the characters in a char array from startIndex to endIndex
        /// </summary>
        /// <param name="number"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        private void Reverse(char[] number, int startIndex, int endIndex)
        {
            while ( startIndex<endIndex)
            {
                Swap(number, startIndex, endIndex);
                startIndex++;
                endIndex--;
            }
        }

        /// <summary>
        /// Swaps the char at index1 and index2
        /// </summary>
        /// <param name="number"></param>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        private void Swap(char[] number, int index1, int index2)
        {
            char temp = number[index1];
            number[index1] = number[index2];
            number[index2] = temp;
        }

        /// <summary>
        /// does a binary search to get the index of the digit greater than search.
        /// the str is present in the descending order from the st index
        /// </summary>
        /// <param name="str">the ints in the sb is in descending order</param>
        /// <param name="search">the number whose greater value needs to be searched</param>
        /// <returns>index at which the greater value number is present</returns>
        private int BinarySearch(char[] str, int st, char search)
        {
            int end = str.Length - 1;
            int index = -1;
            while(st<=end)
            {
                int mid = end - ((end - st) / 2);
                if(str[mid] > search)
                {
                    index = mid;
                    st = mid + 1;
                }
                else
                {
                    // str[mid] <= search
                    end = mid - 1;
                }
            }
            return index;
        }

        public static void TestNextLargestPermutation()
        {
            NextLargestPermutation nextLargest = new NextLargestPermutation();
            int num = 234966321;
            Console.WriteLine("the next largest permutation of the number {0} is {1}. Expected: 236123469", num, nextLargest.GetNextLargestPermutationOfNum(num));

            num = 111;
            Console.WriteLine("the next largest permutation of the number {0} is {1}. Expected: -1", num, nextLargest.GetNextLargestPermutationOfNum(num));

            num = 511;
            Console.WriteLine("the next largest permutation of the number {0} is {1}. Expected: -1", num, nextLargest.GetNextLargestPermutationOfNum(num));

            num = 0;
            Console.WriteLine("the next largest permutation of the number {0} is {1}. Expected: -1", num, nextLargest.GetNextLargestPermutationOfNum(num));

            num = 27;
            Console.WriteLine("the next largest permutation of the number {0} is {1}. Expected: 72", num, nextLargest.GetNextLargestPermutationOfNum(num));

            num = -1;
            Console.WriteLine("the next largest permutation of the number {0} is {1}. Expected: -1", num, nextLargest.GetNextLargestPermutationOfNum(num));

        }
    }
}
