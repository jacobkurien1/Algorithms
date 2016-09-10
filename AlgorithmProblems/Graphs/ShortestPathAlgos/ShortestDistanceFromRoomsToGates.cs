using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.ShortestPathAlgos
{
    /// <summary>
    /// Given a matrix which can have the following values:
    /// -1 - A wall
    /// 0 - A gate
    /// INF - an empty room
    /// 
    /// Fill each room cell in the matrix with the distance to its nearest gate.
    /// For eg: 
    /// INF  -1 0    INF
    /// INF INF INF   -1
    /// INF  -1 INF   -1
    /// 0    -1 INF INF
    /// 
    /// The result should be:
    /// 3 -1 0  1
    /// 2  2 1 -1
    /// 1 -1 2 -1
    /// 0 -1 3  4 
    /// </summary>
    class ShortestDistanceFromRoomsToGates
    {
        /// <summary>
        /// For all the cells having value 0 we need to do BFS and update the 
        /// matrix cells which has the empty rooms with the distance from that cell having 0 value
        /// </summary>
        /// <param name="rooms"></param>
        private void GetShortestDistance(int[,] rooms)
        {
            for (int row = 0; row < rooms.GetLength(0); row++)
            {
                for (int col = 0; col < rooms.GetLength(1); col++)
                {
                    if (rooms[row, col] == 0)
                    {
                        // We need to do BFS from the gates to all the rooms
                        BFS(rooms, row, col);
                    }
                }
            }
        }

        /// <summary>
        /// This is the BFS subroutine
        /// </summary>
        /// <param name="rooms"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void BFS(int[,] rooms, int row, int col)
        {
            Queue<Cell> q = new Queue<Cell>();
            Dictionary<Cell, int> level = new Dictionary<Cell, int>();
            Cell start = new Cell(row, col);
            q.Enqueue(start);
            level[start] = 0;

            while (q.Count > 0)
            {
                Cell c = q.Dequeue();
                if (rooms[c.Row, c.Col] > level[c])
                {
                    // We need to get the min distance from each cell with 0 
                    rooms[c.Row, c.Col] = level[c];
                }
                foreach (Cell neighbour in GetNeighbours(c, rooms))
                {
                    if (!level.ContainsKey(neighbour))
                    {
                        // neighbour is not visited
                        q.Enqueue(neighbour);
                        level[neighbour] = level[c] + 1;

                    }
                }
            }
        }

        /// <summary>
        /// Get all neighbours of the cell c
        /// </summary>
        /// <param name="c"></param>
        /// <param name="rooms"></param>
        /// <returns></returns>
        private List<Cell> GetNeighbours(Cell c, int[,] rooms)
        {
            List<Cell> ret = new List<Cell>();
            if (c.Row - 1 >= 0 && rooms[c.Row - 1, c.Col] != 0 && rooms[c.Row - 1, c.Col] != -1)
            {
                // Add the top cell
                ret.Add(new Cell(c.Row - 1, c.Col));
            }
            if (c.Col - 1 >= 0 && rooms[c.Row, c.Col - 1] != 0 && rooms[c.Row, c.Col - 1] != -1)
            {
                // Add the left cell
                ret.Add(new Cell(c.Row, c.Col - 1));
            }

            if (c.Col + 1 < rooms.GetLength(1) && rooms[c.Row, c.Col + 1] != 0 && rooms[c.Row, c.Col + 1] != -1)
            {
                // Add the right cell
                ret.Add(new Cell(c.Row, c.Col + 1));
            }
            if (c.Row + 1 < rooms.GetLength(0) && rooms[c.Row + 1, c.Col] != 0 && rooms[c.Row + 1, c.Col] != -1)
            {
                // Add the bottom cell
                ret.Add(new Cell(c.Row + 1, c.Col));
            }

            return ret;
        }

        /// <summary>
        /// Represents the cell in the matrix
        /// </summary>
        class Cell
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public Cell(int row, int col)
            {
                Row = row;
                Col = col;
            }

            public override int GetHashCode()
            {
                return Row.GetHashCode() ^ Col.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                Cell newCell = (Cell)obj;
                return Row.Equals(newCell.Row) && Col.Equals(newCell.Col);
            }
        }
        public static void TestShortestDistanceFromRoomsToGates()
        {
            int[,] rooms = new int[,]
            {
                {int.MaxValue, -1, 0, int.MaxValue },
                {int.MaxValue, int.MaxValue, int.MaxValue, -1 },
                {int.MaxValue, -1, int.MaxValue, -1 },
                {0, -1, int.MaxValue, int.MaxValue },
            };
            Console.WriteLine("Rooms matrix before getting the shortest distances");
            PrintMat(rooms);
            ShortestDistanceFromRoomsToGates sd = new ShortestDistanceFromRoomsToGates();
            sd.GetShortestDistance(rooms);
            Console.WriteLine("Rooms matrix after getting the shortest distances");
            PrintMat(rooms);

            rooms = new int[,]
            {
                {int.MaxValue, -1, 0, int.MaxValue },
                {int.MaxValue, int.MaxValue, int.MaxValue, -1 },
                {int.MaxValue, -1, -1, -1 },
                {0, -1, int.MaxValue, int.MaxValue },
            };
            Console.WriteLine("Rooms matrix before getting the shortest distances");
            PrintMat(rooms);
            sd.GetShortestDistance(rooms);
            Console.WriteLine("Rooms matrix after getting the shortest distances");
            PrintMat(rooms);
        }

        private static void PrintMat(int[,] rooms)
        {
            for (int row = 0; row < rooms.GetLength(0); row++)
            {
                for (int col = 0; col < rooms.GetLength(1); col++)
                {
                    if (rooms[row, col] == int.MaxValue)
                    {
                        Console.Write("{0}, ", "inf");
                    }
                    else
                    {
                        Console.Write("{0}, ", rooms[row, col]);
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
