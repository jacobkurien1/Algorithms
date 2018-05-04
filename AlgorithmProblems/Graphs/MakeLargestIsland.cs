using System;
using System.Collections;
using System.Collections.Generic;
using AlgorithmProblems.Graphs.GraphHelper;

namespace AlgorithmProblems.Graphs
{
    /// <summary>
    /// An island is represented by a matrix having 1s and 0s, where 1 -> land and 0 ->water.
    /// You can change a 0 -> 1 to get the greatest island and need to return the total number of elements forming land
    /// </summary>
    public class MakeLargestIsland
    {
        #region Naive approach
        /// <summary>
        /// This is the main method.
        /// Algo: 1. Iterate all the cells with 0 and make it 1 
        /// 2. Do BFS starting at the cell which was switched and calculate the island size
        /// 3. update whether we found the largest island
        /// 4. Backtrack by switching back the cell to 0
        /// 
        /// The time complexity is O(n^4)
        /// The space complexity is O(n^2)
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public int LargestIsland(int[][] grid)
        {
            bool isAnyCellWater = false;
            int largestIsland = 0;
            if (grid != null)
            {
                for (int i = 0; i < grid.Length; i++)
                {
                    for (int j = 0; j < grid[i].Length; j++)
                    {
                        if (grid[i][j] == 0)
                        {
                            isAnyCellWater = true;
                            // Make this cell as land and check the max land
                            grid[i][j] = 1;
                            int largeIsland = GetMaxIsland(new Cell(i, j), grid, InitializeVisitedArray(grid));
                            if(largeIsland > largestIsland)
                            {
                                largestIsland = largeIsland;
                            }
                            grid[i][j] = 0; // backtrack
                        }
                    }
                }
            }
            return (isAnyCellWater) ? largestIsland: GetTotalCells(grid);
        }

        
        #endregion

        #region Performant approach
        /// <summary>
        /// 
        /// The running time for this approach is O(n^2)
        /// The space requirement is O(n^2)
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public int LargestIslandAlgo2(int[][] grid)
        {
            int[][] visitedArr = InitializeVisitedArray(grid);
            Dictionary<int, int> groupSize = new Dictionary<int, int>();
            int groupId = 1;
            int largestIslandSize = 0;

            // First pass is to traverse all closely connected components and mark them with one groupId
            // Also have the groupsize saved in the dictionary
            for(int i = 0; i < visitedArr.Length; i++)
            {
                for(int j = 0; j < visitedArr[i].Length; j++)
                {
                    if(visitedArr[i][j] == 0 && grid[i][j] == 1)
                    {
                        int islandSize = GetMaxIsland(new Cell(i, j), grid, visitedArr, groupId);
                        groupSize[groupId++] = islandSize;
                    }
                }
            }

            // Second pass will mock convert 0->1 and check whether we can form the biggest island
            for (int i = 0; i < visitedArr.Length; i++)
            {
                for (int j = 0; j < visitedArr[i].Length; j++)
                {
                    if (grid[i][j] == 0)
                    {
                        int islandSize = 1;
                        HashSet<int> positions = new HashSet<int>();
                        foreach(Cell direction in GetAllDirections())
                        {
                            int newRow = i + direction.XCoordinate;
                            int newCol = j + direction.YCoordinate;
                            if (newRow >= 0 &&
                                newCol >= 0 &&
                                newRow < grid.Length &&
                                newCol < grid[newRow].Length &&
                                !positions.Contains(visitedArr[newRow][newCol]) &&
                                grid[newRow][newCol] == 1
                                )
                            {
                                positions.Add(visitedArr[newRow][newCol]);
                                islandSize += groupSize[visitedArr[newRow][newCol]];
                            }
                        }
                        if(islandSize > largestIslandSize)
                        {
                            largestIslandSize = islandSize;
                        }
                    }
                }
            }

            return largestIslandSize == 0 ? GetTotalCells(grid) : largestIslandSize;
        }

        #endregion

        #region common methods

        private int[][] InitializeVisitedArray(int[][] grid)
        {
            int[][] ret = new int[grid.Length][];
            for (int i = 0; i < grid.Length; i++)
            {
                ret[i] = new int[grid[i].Length];
            }
            return ret;
        }

        private int GetTotalCells(int[][] grid)
        {
            int totalCells = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                totalCells += grid[i].Length;
            }
            return totalCells;
        }

        private List<Cell> GetCellNeighbours(Cell c, int[][] grid, int[][] visited, int groupId = 1)
        {
            List<Cell> cells = new List<Cell>();
            if (c.XCoordinate + 1 < grid.Length && visited[c.XCoordinate + 1][c.YCoordinate] != groupId && grid[c.XCoordinate + 1][c.YCoordinate] == 1)
            {
                cells.Add(new Cell(c.XCoordinate + 1, c.YCoordinate));
            }
            if (c.XCoordinate - 1 >= 0 && visited[c.XCoordinate - 1][c.YCoordinate] != groupId && grid[c.XCoordinate - 1][c.YCoordinate] == 1)
            {
                cells.Add(new Cell(c.XCoordinate - 1, c.YCoordinate));
            }
            if (c.YCoordinate + 1 < grid[c.XCoordinate].Length && visited[c.XCoordinate][c.YCoordinate + 1] != groupId && grid[c.XCoordinate][c.YCoordinate + 1] == 1)
            {
                cells.Add(new Cell(c.XCoordinate, c.YCoordinate + 1));
            }
            if (c.YCoordinate - 1 >= 0 && visited[c.XCoordinate][c.YCoordinate - 1] != groupId && grid[c.XCoordinate][c.YCoordinate - 1] == 1)
            {
                cells.Add(new Cell(c.XCoordinate, c.YCoordinate - 1));
            }
            return cells;
        }

        /// <summary>
        /// We do BFS from the input cell and keep track of the number of cells which are 1
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private int GetMaxIsland(Cell c, int[][] grid, int[][] visited, int groupId = 1)
        {
            Queue<Cell> q = new Queue<Cell>();
            q.Enqueue(c);
            int maxIsland = 0;
            visited[c.XCoordinate][c.YCoordinate] = groupId;

            while (q.Count > 0)
            {
                Cell currentCell = q.Dequeue();
                maxIsland++;

                foreach (Cell neighbour in GetCellNeighbours(currentCell, grid, visited, groupId))
                {
                    visited[neighbour.XCoordinate][neighbour.YCoordinate] = groupId;
                    q.Enqueue(neighbour);
                }
            }
            return maxIsland;
        }

        private static List<Cell> GetAllDirections()
        {
            return new List<Cell> {
                new Cell(-1,0),
                new Cell(0,-1),
                new Cell(1,0),
                new Cell(0,1)
            };
        }


        #endregion

        public static void TestMakeLargestIsland()
        {
            int[][] island = new int[2][];
            island[0] = new int[2] { 1, 1 };
            island[1] = new int[2] { 1, 0 };

            MakeLargestIsland largeIsland = new MakeLargestIsland();
            Console. WriteLine("The size of largest island is {0}, Expected: 4",largeIsland.LargestIsland(island));
            Console.WriteLine("Algo2: The size of largest island is {0}, Expected: 4", largeIsland.LargestIslandAlgo2(island));

            island = new int[2][];
            island[0] = new int[2] { 1, 1 };
            island[1] = new int[2] { 1, 1 };

            Console.WriteLine("The size of largest island is {0}, Expected: 4", largeIsland.LargestIsland(island));
            Console.WriteLine("Algo2: The size of largest island is {0}, Expected: 4", largeIsland.LargestIslandAlgo2(island));

            island = new int[5][];
            island[0] = new int[5] { 1, 1, 1,0,0 };
            island[1] = new int[5] { 1, 1, 1,0,0 };
            island[2] = new int[5] { 0, 0, 0, 0, 0 };
            island[3] = new int[5] { 0, 0, 1, 1, 1 };
            island[4] = new int[5] { 0, 0, 1, 1, 1 };


            Console.WriteLine("The size of largest island is {0}, Expected: 13", largeIsland.LargestIsland(island));
            Console.WriteLine("Algo2: The size of largest island is {0}, Expected: 13", largeIsland.LargestIslandAlgo2(island));
        }
    }
}
