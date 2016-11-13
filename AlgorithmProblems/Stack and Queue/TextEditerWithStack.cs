using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue
{
    /// <summary>
    /// Implement a textEditor with undo functionalaity
    /// Append(str) - should append str to the current text
    /// Delete(int k) - should delete the last k characters of the string
    /// Print(int k) - print the k th character of the text
    /// Undo() - should undo the last Append/Delete operation
    /// </summary>
    class TextEditerWithUndo
    {
        /// <summary>
        /// Stack to help us do the undo operation
        /// </summary>
        Stack<string> st;

        public TextEditerWithUndo()
        {
            st = new Stack<string>();
        }

        public void Append(string str)
        {
            if (st.Count == 0)
            {
                st.Push(str);
            }
            else
            {
                string prev = st.Peek();
                st.Push(prev + str);
            }
        }

        public void Delete(int k)
        {
            if (st.Count == 0 || st.Peek().Length<k)
            {
                throw new ArgumentException("The Chars cannot be deleted");
            }
            string prev = st.Peek();
            string newStr = prev.Substring(0, prev.Length - k);
            st.Push(newStr);
        }

        public void Print(int k)
        {
            string str = st.Peek();
            Console.WriteLine(str[k - 1]);
        }
        public void PrintAll()
        {
            Console.WriteLine(st.Peek());
        }

        public void Undo()
        {
            st.Pop();
        }

        #region TestArea
        public static void TestTextEditerWithUndo()
        {
            TextEditerWithUndo txt = new TextEditerWithUndo();
            txt.Append("Stewie");
            txt.Append("isgood");
            txt.Print(6);
            txt.Delete(4);
            txt.Print(8);
            txt.Undo();
            txt.Print(9);
            txt.Undo();
            txt.PrintAll();
        }
        #endregion

    }
}
