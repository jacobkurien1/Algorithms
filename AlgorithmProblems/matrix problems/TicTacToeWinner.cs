using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.matrix_problems
{
    /// <summary>
    /// Check whether the tic tac toe board has a winner and return the winner.
    /// 
    /// Note: you should ask clarifying questions as to whether the last cell  where the player moved is known.
    /// if its known then just get that column and row and check whether its a diagonal/antidiagonal element and check which player won.
    /// 
    /// This problem needs to be solved when the last move is not known.
    /// 
    /// </summary>
    class TicTacToeWinner
    {
        /// <summary>
        /// Gets the winner on the board.
        /// The assumption here is that the board is a valid board.
        /// 
        /// Player1 is denoted by 1, player2 by -1 and empty space using 0
        /// 
        /// We need to take cummulative sum along rows, columns and diagonal and whenever we get 3, means player1 has won
        /// and a -3 suggest player 2 has won
        /// 
        /// We will store all the cummulative sum in a single array of length Row*2 + 2.
        /// first Row  elements are cummulative sum of rows,
        /// next Row elements are cummulative sum of columns
        /// and then we have diagonal sum
        /// and the last element is anti diagonal sum.
        /// 
        /// The running time is O(n).
        /// if we maintain this cummSum array during the match and keep updating it with the last move
        /// we can get the running time to be O(1).
        /// 
        /// The space requirement is O(2*Rows + 2)
        /// </summary>
        /// <param name="board"></param>
        /// <returns>
        /// 1 - player1
        /// -1 - player2
        /// 0 - no one won
        /// </returns>
        public int GetWinner(int[,] board)
        {
            int[] cummSum = new int[board.GetLength(0) * 2 + 2];
            for(int i=0; i<board.GetLength(0); i++)
            {
                for(int j=0; j<board.GetLength(1); j++)
                {
                    cummSum[i] += board[i, j]; // sum for rows
                    cummSum[board.GetLength(0) + j] += board[i, j]; // sum for columns
                    if(i==j)
                    {
                        // this is a diagonal element
                        cummSum[2 * board.GetLength(0)] += board[i, j];
                    }
                    if(i+j == board.GetLength(0) -1)
                    {
                        // this is an anti-diagonal element
                        cummSum[2 * board.GetLength(0) + 1] += board[i, j];
                    }
                }
            }

            for(int i=0; i< cummSum.Length; i++)
            {
                if(Math.Abs(cummSum[i]) == board.GetLength(0))
                {
                    // in a 3x3 board we need to check whether the cummulative sum is 3 or -3
                    return cummSum[i] / board.GetLength(0);
                }
            }
            // the match is a draw, we dont have a winner
            return 0;
        }

        public static void TestTicTacToeWinner()
        {
            TicTacToeWinner ttt = new TicTacToeWinner();
            int[,] board = new int[,]
            {
                {1,-1,1 },
                {-1,0,1 },
                {0,-1,1 }
            };
            Console.WriteLine("The winner is {0}", ttt.GetWinner(board));

            board = new int[,]
            {
                {-1,1,1 },
                {1,-1,0 },
                {-1,1,-1 }
            };
            Console.WriteLine("The winner is {0}", ttt.GetWinner(board));

            board = new int[,]
            {
                {1,1,1 },
                {1,-1,0 },
                {-1,1,-1 }
            };
            Console.WriteLine("The winner is {0}", ttt.GetWinner(board));

            board = new int[,]
            {
                {1,1,-1 },
                {1,-1,0 },
                {-1,0,-1 }
            };
            Console.WriteLine("The winner is {0}", ttt.GetWinner(board));

            board = new int[,]
            {
                {1,1,-1 },
                {1,1,0 },
                {-1,0,-1 }
            };
            Console.WriteLine("The winner is {0}", ttt.GetWinner(board));
        }
    }
}
