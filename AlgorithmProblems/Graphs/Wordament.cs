using AlgorithmProblems.Graphs.GraphHelper;
using AlgorithmProblems.Trie.TrieHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    /// <summary>
    /// Create a wordament/boggle solver which given a grid will get all the valid words from a dictionary
    /// </summary>
    class Wordament
    {
        private HashSet<string> AllWordsPresentInBoard { get; set; }
        private Trie<char> AllKnownWordsTrie { get; set; }
        public Wordament(List<string> allKnownWords)
        {
            AllWordsPresentInBoard = new HashSet<string>();
            AllKnownWordsTrie = new Trie<char>();
            foreach(string word in allKnownWords)
            {
                AllKnownWordsTrie.Insert(word.ToCharArray());
            }
        }

        /// <summary>
        /// Get all the words present on the board
        /// 
        /// if total cells tc = rows*cols
        /// The running time will be O(tc^3)
        /// The space required will be the space used by stringbuilder which can be O(tc)
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public HashSet<string> GetAllWords(char[,] board)
        {
            for(int i=0; i<board.GetLength(0); i++)
            {
                for(int j=0; j<board.GetLength(1); j++)
                {
                    StringBuilder sb = new StringBuilder();
                    Dictionary<Cell, bool> visitedCells = new Dictionary<Cell, bool>();
                    Cell current = new Cell(i, j);
                    FindAllWordsDFS(board, current, ref sb, ref visitedCells);
                }
            }
            return AllWordsPresentInBoard;
        }

        /// <summary>
        /// This is a dfs subroutine which will help us to get the different words that can be formed by starting at board[i,j]
        /// </summary>
        /// <param name="board"></param>
        private void FindAllWordsDFS(char[,] board, Cell current, ref StringBuilder sb, ref Dictionary<Cell,bool> visitedCells)
        {
            if (!IsCellValid(current, board.GetLength(0), board.GetLength(1)) || !AllKnownWordsTrie.Search(sb.ToString().ToCharArray(), true))
            {
                // if the cell is invalid or the trie does not have a partial match of the word
                return;
            }
            sb.Append(board[current.XCoordinate, current.YCoordinate]);
            visitedCells[current] = true;
            if(AllKnownWordsTrie.Search(sb.ToString().ToCharArray(), false))
            {
                // Found a word
                AllWordsPresentInBoard.Add(sb.ToString());
            }

            foreach(Cell direction in GetAllDirections())
            {
                Cell newCell = new Cell(current.XCoordinate + direction.XCoordinate, current.YCoordinate + direction.YCoordinate);
                if(!visitedCells.ContainsKey(newCell))
                {
                    FindAllWordsDFS(board, newCell, ref sb, ref visitedCells);
                }
            }

            // backtracking
            sb.Remove(sb.Length - 1, 1);
            visitedCells.Remove(current);
        }

        /// <summary>
        /// Checks whether the given cell is a valid cell on the board
        /// </summary>
        /// <param name="c"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <returns></returns>
        private bool IsCellValid(Cell c, int maxX, int maxY)
        {
            return (c.XCoordinate >= 0 && c.YCoordinate >= 0 && c.XCoordinate < maxX && c.YCoordinate < maxY);
        }

        /// <summary>
        /// Gets all the direction in which we need to traverse
        /// </summary>
        /// <returns></returns>
        private List<Cell> GetAllDirections()
        {
            return new List<Cell> {
                new Cell(-1,0),
                new Cell(0,-1),
                new Cell(1,0),
                new Cell(0,1),
                new Cell(1,1),
                new Cell(-1,1),
                new Cell(1,-1),
                new Cell(-1,-1)
            };
        }


        #region TestArea
        public static void TestWordament()
        {
            List<string> allWords = new List<string>()
            {
                "jacob",
                "boy",
                "bat",
                "taste",
                "bye",
                "ball",
                "cat",
                "rat"
            };

            Wordament wordament = new Wordament(allWords);
            char[,] board = new char[,]
            {
                { 'a','j', 'y', 'e'},
                { 'c','o','b','t'},
                { 'y','t','a','s'},
                { 'b','a','l','l'}
            };
            PrintAllElements(wordament.GetAllWords(board));
        }

        private static void PrintAllElements(HashSet<string> hs)
        {
            Console.WriteLine("All the words present on the board is as shown below:");
            foreach(string s in hs)
            {
                Console.WriteLine("{0}", s);
            }
        }

        #endregion
    }
}
