using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Search
{
    /// <summary>
    /// Given a rowise and columnwise sorted matrix. Search for an element in linear time.
    /// </summary>
    class SearchInSortedMatrix
    {
        /// <summary>
        /// We start from the top-right element.
        /// if the element is same as the element to search, return the cell value
        /// If the element is greater than element to search, go left.
        /// if the element is less than the element to search go bottom.
        /// 
        /// The running time of this algo is O(n+m)
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public Cell Search(int[,] mat, int search)
        {
            int row = 0;
            int col = mat.GetLength(1) - 1;

            while(row<mat.GetLength(0) && col>=0)
            {
                if(mat[row,col] == search)
                {
                    // found the search element
                    return new Cell(row, col);
                }
                else if(mat[row,col] < search)
                {
                    //If the element is greater than element to search, go left.
                    row++;
                }
                else
                {
                    //If the element is greater than element to search, go left.
                    col--;
                }
            }
            return null;
        }

        /// <summary>
        /// Represents a cell location in the matrix
        /// </summary>
        public class Cell
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public Cell(int row, int col)
            {
                Row = row;
                Col = col;
            }
            public override string ToString()
            {
                return string.Format("({0}, {1})", Row, Col);
            }
        }

        #region TestArea
        public static void TestSearchInSortedMatrix()
        {
            SearchInSortedMatrix s = new SearchInSortedMatrix();
            int[,] mat = new int[,]
            {
                {1,2,5,6,10 },
                {2,3,6,7,20 },
                {3,4,8,9,50 }
            };
            int searchElem = 9;
            Console.WriteLine("The element {0} is present at {1}", searchElem, s.Search(mat, searchElem));
            searchElem = -1;
            Console.WriteLine("The element {0} is present at {1}", searchElem, s.Search(mat, searchElem));

        }
        #endregion
    }
}
