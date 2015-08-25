using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Misc
{
    class CreditCardCheck
    {
        private static bool LuhnAlgo(char[] creditCardNum)
        {
            long sumVal = 0;
            // start from the end of the string
            for (int index = creditCardNum.Length - 1; index >= 0; index--)
            {
                int intValAtIndex = 0;
                try
                {
                    intValAtIndex = ConvertCharToInt(creditCardNum[index]);
                }
                catch (Exception)
                {
                    return false;   // The credit card number contains invalid characters
                }
                int position = 0;
                if(creditCardNum.Length%2==0)
                {
                    // This is the case for visa, discover, mastercard
                    position = index;
                }
                else
                {
                    // condition for american express
                    position = index + 1;
                }
                if(position % 2 == 0)
                {
                    // Process the even positioned value
                    int evenIndexVal = intValAtIndex * 2;
                    if(evenIndexVal >=10)
                    {
                        // We need to add the 2 digits individually
                        sumVal += evenIndexVal % 10;
                        sumVal += 1;
                    }
                    else
                    {
                        // add the doubled value
                        sumVal += evenIndexVal;
                    }
                }
                else
                {
                    // add the odd positioned value directly
                    sumVal += intValAtIndex;
                }
            }

            if(sumVal%10==0)
            {
                return true;    // This is a credit card number
            }
            else
            {
                return false;   //Not a credit card number
            }
        }

        private static int ConvertCharToInt(char charVal)
        {
            int intVal = charVal - 48;
            if (intVal >= 0 && intVal <= 9)
            {
                return intVal;
            }
            else
            {
                throw new Exception("invalid input");
            }
        }

        public static void TestLuhnAlgo()
        {
            Console.WriteLine("The number {0} is a visa credit card number {1}", "4539638645089136", LuhnAlgo("4539638645089136".ToCharArray()));
            Console.WriteLine("The number {0} is a mastercard credit card number {1}", "5484723447330961", LuhnAlgo("5484723447330961".ToCharArray()));
            Console.WriteLine("The number {0} is a discover credit card number {1}", "6011738424345378", LuhnAlgo("6011738424345378".ToCharArray()));
            Console.WriteLine("The number {0} is a american express credit card number {1}", "349737169114557", LuhnAlgo("349737169114557".ToCharArray()));
            Console.WriteLine("The number {0} is a credit card number {1}", "3497371&69114557", LuhnAlgo("3497371&69114557".ToCharArray()));
            Console.WriteLine("The number {0} is a credit card number {1}", "6011738224345378", LuhnAlgo("6011738224345378".ToCharArray()));
        }
    }
}
