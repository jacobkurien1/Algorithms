using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue
{
    class EvaluatePostfixNotation
    {
        /// <summary>
        /// Algo for the evaluation of a postfix notation
        /// 1. Traverse from left to right
        /// 2. Whenever you encounter a number add it to stack
        /// 3. When ever you encounter an operator. Do the operation on
        /// operands found by popping stack twice. 
        /// 4. Push the result back into the stack.
        /// 
        /// Check for illformed expression and also at the end stack should have one number
        /// </summary>
        /// <param name="prefixStr"></param>
        /// <returns></returns>
        private static double GetPostfixNotationResult(string postfixStr)
        {
            string[] individualVals = postfixStr.Split(' ');

            // Use the stack to hold the operands
            Stack<double> operandSt = new Stack<double>();

            for(int i=0; i < individualVals.Length; i++)
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
                throw new Exception("ill- formed postfix expression");
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

        public static void TestGetPostfixNotationResult()
        {
            string postfixNotation = "5 5 + 5 5 + *";
            Console.WriteLine("The result for postfix notation {0} is {1}", postfixNotation, GetPostfixNotationResult(postfixNotation));

            postfixNotation = "- * / 15 - 7 + 1 1 3 + 2 + uu 1 1";
            try
            {
                Console.WriteLine("The result for postfix notation {0} is {1}", postfixNotation, GetPostfixNotationResult(postfixNotation));
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
    }
}
