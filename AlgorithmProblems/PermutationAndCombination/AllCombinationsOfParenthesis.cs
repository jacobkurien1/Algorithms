using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.PermutationAndCombination
{
    /// <summary>
    /// Print all the combinations of balanced parenthesis
    /// for eg PrintAllCombinations(2) should give the following results
    /// {{}}
    /// {}{}
    /// </summary>
    class AllCombinationsOfParenthesis
    {
        /// <summary>
        /// Main function which runs the recursive subroutine
        /// </summary>
        /// <param name="num">num is the total number of open parenthesis that we can have.</param>
        public static void PrintAllCombinations(int num)
        {
            if(num<0)
            {
                throw new ArgumentException();
            }
            StringBuilder sb = new StringBuilder();
            PrintCombinationsRecursive(ref sb, 0, 0, num);

        }

        /// <summary>
        /// The recursive subroutine which prints all the combinations of parenthesis.
        /// 1.The idea is to have the total number of open parenthesis and closed parenthesis in the variable open and close.
        /// 2. When close hits num, we print the string.
        /// close needs to be always less than or equal to open
        /// 3. As long as open < num, add the "{" to the stringbuilder and recurse with open+1,
        /// Remove "{" at the end of the recursion.
        /// 4. If close<open, means we can add "}".
        /// 
        /// The total number of valid expressions follow the nth Catalan number
        /// Catalan(n) = {1/(n+1) } 2nCn 
        /// or Catalan(n) = 2nCn - 2nC(n+1)
        /// The catalan numbers grow as 4^n/(n*Sqrt(n)) or 4^n/(n^(3/2))
        /// Since each valid sequence has n steps to backtrack
        /// So running time is 4^n/sqrt(n) and space requirement is also 4^n/sqrt(n)
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="open"></param>
        /// <param name="close"></param>
        /// <param name="num"></param>
        public static void PrintCombinationsRecursive(ref StringBuilder sb, int open, int close, int num)
        {
            if(open > num || close > num)
            {
                return;
            }
            if (close == num)
            {
                //Print the stringbuilder cause it has open braces = closed braces = num
                Console.WriteLine(sb.ToString());
            }

            if(open < num)
            {
                sb.Append("{");
                PrintCombinationsRecursive(ref sb, open + 1, close, num);
                sb.Remove(sb.Length - 1, 1); // for backtracking
            }
            if(open > close)
            {
                sb.Append("}");
                PrintCombinationsRecursive(ref sb, open, close + 1, num);
                sb.Remove(sb.Length - 1, 1); // for backtracking
            }

        }

        public static void TestAllCombinationsOfParenthesis()
        {
            int num = 2;
            Console.WriteLine("All parenthesis of count {0} are as shown below", num);
            PrintAllCombinations(num);

            num = 3;
            Console.WriteLine("All parenthesis of count {0} are as shown below", num);
            PrintAllCombinations(num);
        }
    }
}
