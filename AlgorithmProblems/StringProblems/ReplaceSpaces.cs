using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems
{
    /// <summary>
    /// Replace the spaces in the string with '%20'
    /// </summary>
    class ReplaceSpaces
    {
        public StringBuilder ReplaceSpacesInplace(StringBuilder sb)
        {
            int oldLength = sb.Length;
            int numOfSpaces = 0;
            for (int i = oldLength - 1; i >= 0; i--)
            {
                if(string.Compare(sb[i].ToString() , " ") == 0)
                {
                    numOfSpaces++;
                    sb.Append("  ");
                }
            }
            int newLength = oldLength + 2 * numOfSpaces;
            int newIndex = newLength-1;
            for (int i = oldLength - 1; i >= 0; i-- )
            {
                if (string.Compare(sb[i].ToString(), " ") == 0)
                {
                    sb[newIndex--] = '0';
                    sb[newIndex--] = '2';
                    sb[newIndex--] = '%';
                }
                else 
                {
                    sb[newIndex--] = sb[i];
                }
            }
            return sb;
        }

        public static void TestReplaceSpacesInplace()
        {
            ReplaceSpaces rs = new ReplaceSpaces();
            Console.WriteLine(rs.ReplaceSpacesInplace(new StringBuilder("Jacob went up the hill")).ToString());
        }
    }
}
