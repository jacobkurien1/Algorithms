using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue
{
    public class WellFormedExpression
    {
        private Dictionary<char,char> MatchingParenthesis { get; set; }

        public WellFormedExpression()
        {
            MatchingParenthesis = new Dictionary<char, char>();
            MatchingParenthesis.Add('}', '{');
            MatchingParenthesis.Add(')', '(');
            MatchingParenthesis.Add(']', '[');
        }

        /// <summary>
        /// Checks whether an expression is well formed or not
        /// </summary>
        /// <param name="expression">string expression with parenthesis which needs to be tested</param>
        /// <returns>whether the expression is well formed or not</returns>
        public bool MatchParenthesisInAnExpression(string expression)
        {
            Stack<char> st = new Stack<char>();
            for (int index = 0; index < expression.Length; index++)
            {
                if(MatchingParenthesis.ContainsKey(expression[index]))
                {
                    // The expression[index] is a closing parenthesis
                    try
                    {
                        if (st.Pop() != MatchingParenthesis[expression[index]])
                        {
                            return false;
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        // The stack is empty and we are trying to pop out the element
                        // The expression is not well formed
                        return false;
                    }
                }
                else if(MatchingParenthesis.Values.Contains(expression[index]))
                {
                    // The expression[index] is an opening parenthesis
                    st.Push(expression[index]);
                }
                // Note: we only care for parenthesis in the expression, 
                // hence we can leave out the else part in the code
            }
            // If the expression is well formed we should have an empty stack at the end of for loop.
            return st.Count == 0;
        }

        public static void TestWellFormedExpression()
        {
            WellFormedExpression wfe = new WellFormedExpression();
            string expression = "(2121(3232{2121}8989{2312}3232{3232{3232}}32[]323))";
            Console.WriteLine("The expression {0} is well formed: {1} : Expected: true", expression, wfe.MatchParenthesisInAnExpression(expression));

            expression = "(2121(3232{2121}8989{2312}3232{3232{3232}}32[]323))[";
            Console.WriteLine("The expression {0} is well formed: {1} : Expected: false", expression, wfe.MatchParenthesisInAnExpression(expression));

            expression = "(2121(3232{2121}8989{2312}3232{3232{3232}}32[]323))}";
            Console.WriteLine("The expression {0} is well formed: {1} : Expected: false", expression, wfe.MatchParenthesisInAnExpression(expression));

            expression = "(2((121(3232{2121}8989{2312}3232{3232{3232}}32[]323))}";
            Console.WriteLine("The expression {0} is well formed: {1} : Expected: false", expression, wfe.MatchParenthesisInAnExpression(expression));

        }
    }
}
