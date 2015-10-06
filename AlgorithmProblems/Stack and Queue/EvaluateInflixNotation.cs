using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue
{
    class EvaluateInflixNotation
    {
        /// <summary>
        /// We will use Dijkstra's shunting algorithm to solve this.
        /// 
        /// Algo:
        /// We will need 2 stack one to store the operators and one to store the operands.
        /// 
        /// 1. We will keep adding the numbers in the operandStack and keep adding the operators in the operatorSt.
        /// When we are adding a operator of lower priority on top of operator of higher priority, we will pop
        /// the operator and 2 operands do the operation and push the results back in the operand stack.
        /// We do the above step till the operator stack is empty or the stack top has an operator of lower priority
        /// 
        /// We will push operator with the lower priority in the operator stack
        /// 
        /// 2. Once all the elements from the expression is in the stack, keep popping 2 operands and 1 operator 
        /// Get the result and push it back to the operand stack.
        /// 
        /// The last element in the operand stack is the result of inflix expression.
        /// 
        /// </summary>
        /// <param name="inflixStr"></param>
        /// <returns></returns>
        private static double GetInflixNotationResults(string inflixStr)
        {
            string[] individualVals = inflixStr.Split(' ');

            Stack<double> operandSt = new Stack<double>();
            Stack<string> operatorSt = new Stack<string>();

            for(int i=0; i< individualVals.Length; i++)
            {
                double operand;
                if (Double.TryParse(individualVals[i], out operand))
                {
                    operandSt.Push(operand);
                }
                else
                {
                    while (operatorSt.Count!=0 && GetPriority(operatorSt.Peek()) > GetPriority(individualVals[i]))
                    {
                        double operand2 = operandSt.Pop();
                        double operand1 = operandSt.Pop();
                        operandSt.Push(DoOperation(operatorSt.Pop(), operand1, operand2));
                    }
                    operatorSt.Push(individualVals[i]);
                }
            }

            // 2. Once all the elements from the expression is in the stack, keep popping 2 operands and 1 operator 
            // Get the result and push it back to the operand stack.
            while(operatorSt.Count > 0)
            {
                double operand2 = operandSt.Pop();
                double operand1 = operandSt.Pop();
                operandSt.Push(DoOperation(operatorSt.Pop(), operand1, operand2));
            }

            if (operandSt.Count != 1)
            {
                throw new Exception("ill- formed inflix expression");
            }
            return operandSt.Pop();
        }

        private static double DoOperation(string operation, double operand1, double operand2)
        {
            switch (operation)
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

        private static int GetPriority(string operation)
        {
            switch (operation)
            {
                case "+":
                    return 0;
                case "-":
                    return 1;
                case "*":
                    return 2;
                case "/":
                    return 3;
                default:
                    throw new ArgumentException("Invalid operation");
            }
        }

        public static void TestGetInflixNotationResults()
        {
            string inflixStr = "2 + 5 * 3 - 4";
            Console.WriteLine("The inflix expression {0} gets evaluated to {1}", inflixStr, GetInflixNotationResults(inflixStr));

            inflixStr = "5 * 5 / 5 - 3";
            Console.WriteLine("The inflix expression {0} gets evaluated to {1}", inflixStr, GetInflixNotationResults(inflixStr));

            inflixStr = "5 * 5 / 5 - 3 + 9 * 9 - 7 * 1";
            Console.WriteLine("The inflix expression {0} gets evaluated to {1}", inflixStr, GetInflixNotationResults(inflixStr));

            inflixStr = "5 * 5 / 5 - 3 + 9 * 9 - 7 * 1cxc";
            try
            {
                Console.WriteLine("The inflix expression {0} gets evaluated to {1}", inflixStr, GetInflixNotationResults(inflixStr));
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
    }
}
