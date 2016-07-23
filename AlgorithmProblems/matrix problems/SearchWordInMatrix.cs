using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.matrix_problems
{
    /// <summary>
    /// Given a matrix search a word in a matrix. 
    /// From any node we can go in any of the 8 directions to get the match
    /// </summary>
    class SearchWordInMatrix
    {
        /// <summary>
        /// Traverse the complete matrix and whenever the text[0] matches with the mat[i,j]
        /// go in all the 8 directions to see whether there is a match.
        /// </summary>
        /// <param name="mat">matrix from which we need to search the string</param>
        /// <param name="text">string to search for</param>
        /// <returns></returns>
        public List<Match> GetAllMatches(char[,] mat, string text)
        {
            List<Match> ret = new List<Match>();
            
            // Traverse all the element in the matrix
            for (int i=0;i<mat.GetLength(0); i++)
            {
                for(int j=0; j<mat.GetLength(1); j++)
                {
                    if (mat[i, j] == text[0])
                    {
                        // Check if the matrix element has a match with the first char of the text
                        ret.AddRange(GetAllMatchesInAllDirection(mat, i, j, text));
                    }

                }
            }
            return ret;
        }

        /// <summary>
        /// Get the match of text in any of the 8 directions in the matrix with the starting point as (
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        List<Match> GetAllMatchesInAllDirection(char[,] mat, int row, int col, string text)
        {
            List<Match> ret = new List<Match>();
            Direction[] allDirection = GetAllDirections();
            for (int directionIndex = 0; directionIndex < allDirection.Length; directionIndex++)
            {
                // Traverse in all the different direction for a match
                int currentRow = row;
                int currentCol = col;
                int textIndex = 0;
                
                while(currentCol>=0 && currentRow>=0
                    && currentRow<mat.GetLength(0) && currentCol<mat.GetLength(1)
                    && textIndex<text.Length)
                {
                    if(mat[currentRow,currentCol] != text[textIndex])
                    {
                        break;
                    }
                    else
                    {
                        if(textIndex == text.Length-1)
                        {
                            // We have a match
                            ret.Add(new Match(row, col, allDirection[directionIndex]));
                        }
                    }
                    currentRow += allDirection[directionIndex].NextRow;
                    currentCol += allDirection[directionIndex].NextCol;
                    textIndex++;
                }
            }
            return ret;
        }

        /// <summary>
        /// We need to return 8 different directions where we can go from a particulat cell in the matrix
        /// (i-1,j-1)	(i-1,j)		(i-1,j+1)
        /// (i,j-1)		(i,j)		(i,j+1)
        /// (i+1,j-1)	(i+1,j)		(i+1,j+1)
        /// </summary>
        /// <returns></returns>
        internal Direction[] GetAllDirections()
        {
            return new Direction[]
            {
                new Direction(-1,-1),
                new Direction(-1,0),
                new Direction(-1,1),
                new Direction(0,-1),
                new Direction(0,1),
                new Direction(1,-1),
                new Direction(1,0),
                new Direction(1,1)
            };
        }

        public static void TestSearchWordInMatrix()
        {
            SearchWordInMatrix sw = new SearchWordInMatrix();
            char[,] mat = new char[,]
            {
                { 'a', 'j', 'a','c','b','k'},
                {'j','a','c', 'o', 'b', 'b' },
                {'k', 'a', 'x', 'x','o','x' },
                {'y','x','c','c','x','x' },
                {'x','x','a','o','x','x' },
                {'x','j','x','x','b','x' },
                {'j','x','x','x','x','x' },
                {'a','x','x','x','x','x' },
                {'c','x','x','b','x','x' },
                {'o','x','x','o','x','x' },
                {'b','x','x','c','j','x' },
                {'x','x','x','a','x','x' },
                {'x','x','c','j','x','x' },
                {'b','o','c','a','j','x' },
                {'b','x','x','x','x','x' }

            };
            List<Match> matches = sw.GetAllMatches(mat, "jacob");
            PrintMatches(matches);
        }

        private static void PrintMatches(List<Match> matches)
        {
            Console.WriteLine("All the matches are as follows:");
            foreach(Match m in matches)
            {
                Console.WriteLine(m.ToString());
            }
        }

        /// <summary>
        /// Class which denotes a match by giving the start row and column index and the direction where the match is present
        /// </summary>
        internal class Match
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public Direction MatchDirection { get; set; }
            public Match(int row, int col, Direction direction)
            {
                Row = row;
                Col = col;
                MatchDirection = direction;
            }

            public override string ToString()
            {
                return string.Format("the match is present at ({0},{1} in the direction {2})", Row, Col, MatchDirection);
            }
        }

        /// <summary>
        /// This class denotes the direction in which the match has occured.
        /// NextRow=-1 and NextCol = 0 denotes the North direction, and so on
        /// </summary>
        internal class Direction
        {
            public int NextRow { get; set; }
            public int NextCol { get; set; }
            public Direction(int nextRow, int nextCol)
            {
                NextRow = nextRow;
                NextCol = nextCol;
            }

            public override string ToString()
            {
                return string.Format("({0}, {1})", NextRow, NextCol);
            }
        }
    }
}
