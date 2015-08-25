using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Misc
{
    class ExcelFirstRowConversion
    {
        /// <summary>
        /// We have 26 unique alphabets from A to Z
        /// </summary>
        const int AlphabetSize = 26;

        /// <summary>
        /// Convert the A,B,...Z,AA, AB,...AZ to its int equivalent
        /// </summary>
        /// <param name="columnName">column name of the excel cell</param>
        /// <returns>int value of this column</returns>
        public static long CovertExcelColumnToLong(char[] columnName)
        {
            long finalNumericValue = 0;
            for(int charIndex=0; charIndex<columnName.Length; charIndex++)
            {
                int individualNumericVal = ConvertAlphabetToNumeric(columnName[charIndex]);
                if(individualNumericVal==-1)
                {
                    return -1;  // invalid value
                }
                finalNumericValue = (finalNumericValue * AlphabetSize) + (individualNumericVal);

            }
            return finalNumericValue;
        }

        private static int ConvertAlphabetToNumeric(char alphabet)
        {
            int numericVal = alphabet - 65 + 1; // ascii value of A is 65, we do +1 cause A=1, B=2, and so on
            if(numericVal>=0 && numericVal<26)
            {
                return numericVal;
            }
            else
            {
                return -1;  // invalid value
            }
        }

        #region TestMethods
        public static void TestCovertExcelColumnToLong()
        {
            Console.WriteLine("Test the conversion from the excel column string to the numeral");
            Console.WriteLine("The converted excel column name {0} is {1}", "DKY", CovertExcelColumnToLong("DKY".ToCharArray()));
            Console.WriteLine("The converted excel column name {0} is {1}", "KHO", CovertExcelColumnToLong("KHO".ToCharArray()));
        }
        #endregion
    }
}
