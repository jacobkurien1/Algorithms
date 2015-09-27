using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue
{
    class EvaluatePrefixNotation
    {
        /// <summary>
        /// Algo for the evaluation of a prefix notation
        /// 1. Traverse from right to left
        /// 2. Whenever you encounter a number add it to stack
        /// 3. When ever you encounter an operatoe. Do the operation on
        /// operands found by popping stack twice. 
        /// 4. Push the result back into the stack.
        /// 
        /// Check for illformed expression and also at the end stack should have one number
        /// </summary>
        /// <param name="prefixStr"></param>
        /// <returns></returns>
        private static double GetPrefixNotationResult(string prefixStr)
        {
            string[] individualVals = prefixStr.Split(' ');

            // Use the stack to hold the operands
            Stack<double> operandSt = new Stack<double>();

            for(int i=individualVals.Length-1; i>=0; i--)
            {
                double operand;
                if (Double.TryParse(individualVals[i], out operand))
                {
                    operandSt.Push(operand);
                }
                else
                {
                    operandSt.Push(DoOperation(individualVals[i], operandSt.Pop(), operandSt.Pop()));
                }
            }

            if(operandSt.Count != 1)
            {
                throw new Exception("ill- formed prefix expression");
            }
            return operandSt.Pop();
        }

        private static double DoOperation(string operation, double operand1, double operand2)
        {
            switch(operation)
            {
                case "+":
                    return operand1 + operand2;
                case "-":
                    return operand1 - operand2;
                case "*":
                    return operand1 * operand2;
                case "/":
                    return operand1 / operand2;
                default:
                    throw new ArgumentException("Invalid operation");
            }
        }

        public static void TestGetPrefixNotationResult()
        {
            string prefixNotation = "- * / 15 - 7 + 1 1 3 + 2 + 1 1";
            Console.WriteLine("The result for prefix notation {0} is {1}", prefixNotation, GetPrefixNotationResult(prefixNotation));

            prefixNotation = "- * / 15 - 7 + 1 1 3 + 2 + uu 1 1";
            try
            {
                Console.WriteLine("The result for prefix notation {0} is {1}", prefixNotation, GetPrefixNotationResult(prefixNotation));
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
    }
}
