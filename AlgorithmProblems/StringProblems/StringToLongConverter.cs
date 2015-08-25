using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems
{
    class StringToLongConverter
    {
        private static long StringToLong(char[] numStr)
        {
            int placeVal = 1;
            long convertedNum = 0;
            // start from the end of the string
            for (int index = numStr.Length-1; index >=0; index--)
            {
                // keep adding the int number at the "index" to our output
                convertedNum += placeVal * ConvertCharToInt(numStr[index]);

                // We need to do this so that as to shift the position of the next value to the left
                placeVal *= 10;

            }
            return convertedNum;
        }

        private static int ConvertCharToInt(char charVal)
        {
            int intVal = charVal - 48;
            if(intVal>=0 && intVal<=9)
            {
                return intVal;
            }
            else
            {
                throw new Exception("invalid input");
            }
        }

        public static void TestStringToLong()
        {
            Console.WriteLine("The string val{0} is converted to the following num {1}", "23434", StringToLong("23434".ToCharArray()));

            string valToDisplay;
            try
            {
                long val = StringToLong("234*348".ToCharArray());
                valToDisplay = val.ToString();
            }
            catch (Exception exp)
            {
                valToDisplay = exp.Message;
            }
            
            Console.WriteLine("The string val{0} is converted to the following num {1}", "234*348", valToDisplay);
        }
    }
}
