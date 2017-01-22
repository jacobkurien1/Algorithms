using AlgorithmProblems.Geometry;
using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    /// <summary>
    /// The matrix is given with positive integers representing the elevation/height of the land mass.
    /// The pacific ocean is present in the top and left side and Atlantic ocean is present in the bottom and right side.
    /// Water can only flow from the land at higher or equal elevation.
    /// Find the continental divide (which is all the points in the grid from where water can flow both to Pacific and Atlantic ocean.)?
    /// 
    /// For eg:
    /// Pacific------------
    /// | { 1,2,2,3,(5)}  |
    /// | { 3,2,3,(4),(4)}|
    /// | { 2,4,(5),3,1}  |
    /// | { (6),(7),1,4,5}|
    /// | { (5),1,1,2,4}  |
    /// -----------Atlantic
    /// 
    /// The points of continental divide is as shown below:
    /// (0, 4) , (1, 4) , (1, 3) , (2, 2) , (4, 0) , (3, 0) , (3, 1)
    /// 
    /// </summary>
    class ContinentalDivide
    {
        /// <summary>
        /// Algo: instead of going from the land to Pacific and Atlantic. We can come back from the oceans to the continent.
        /// We fist come from the top and left edges and keep setting the points in flowToPacificOrAtlantic 1 when we see an elevation at higher or equal level.
        /// Now we go from right and bottom and as soon as we hit any point whose flowToPacificOrAtlantic is 1, we add it to the List of cells,
        /// and keep updating the flowToPacificOrAtlantic value at the point to 2.
        /// 
        /// 
        /// The running time is O(rows*col)
        /// The space requirement is O(rows*col)
        /// </summary>
        /// <param name="landElevation">land elevation matrix</param>
        /// <returns>points of continental divide</returns>
        public static List<Cell> GetContinentalDivide(int[,] landElevation)
        {
            List<Cell> ret = new List<Cell>();

            // flowToPacificOrAtlantic[row, col] == 1 denotes water from Pacific
            // flowToPacificOrAtlantic[row, col] == 2 denotes water from Atlantic
            int[,] flowToPacificOrAtlantic = new int[landElevation.GetLength(0), landElevation.GetLength(1)];
            

            //Run the DFS subroutine from all the cells from which water falls to pacific(left and top border)
            for(int i = 0; i<landElevation.GetLength(0); i++)
            {
                // left border
                DFS(landElevation, flowToPacificOrAtlantic, true, i, 0, ret);
            }

            for(int j=0; j<landElevation.GetLength(1); j++)
            {
                // top border
                DFS(landElevation, flowToPacificOrAtlantic, true, 0, j, ret);

            }

            //Run the DFS subroutine from all the cells from which water falls to atlantic(right and bottom border)
            for (int i = 0; i < landElevation.GetLength(0); i++)
            {
                // right border
                DFS(landElevation, flowToPacificOrAtlantic, false, i, landElevation.GetLength(1)-1, ret);
            }

            for (int j = 0; j < landElevation.GetLength(1); j++)
            {
                // bottom border
                DFS(landElevation, flowToPacificOrAtlantic, false, landElevation.GetLength(0)-1, j, ret);

            }

            return ret;
        }

        /// <summary>
        /// DFS subroutine for both points from Pacific and Atlantic
        /// </summary>
        /// <param name="landElevation">landElevation matrix</param>
        /// <param name="flowToPacificOrAtlantic">tracks where the </param>
        /// <param name="IsPacific">bit to tell whether the points have water flowing to Pacific</param>
        /// <param name="row">starting cell row</param>
        /// <param name="col">starting cell column</param>
        /// <param name="ret">points of continental divide</param>
        public static void DFS(int[,] landElevation, int[,] flowToPacificOrAtlantic, bool IsPacific, int row, int col, List<Cell> ret)
        {
            if(IsPacific)
            {
                if (flowToPacificOrAtlantic[row, col] == 1)
                {
                    //stop DFS when we are coming from the pacific side
                    return;
                }
                flowToPacificOrAtlantic[row, col] = 1;
            }
            else
            {
                if (flowToPacificOrAtlantic[row, col] == 2)
                {
                    //we have visited this cell
                    return;
                }
                else if (flowToPacificOrAtlantic[row, col] == 1)
                {
                    // we have found a point which is a continental divide
                    Cell currentCell = new Cell(row, col);
                    ret.Add(currentCell);
                }
                flowToPacificOrAtlantic[row, col] = 2;

            }

            foreach (Cell directions in GetAllDirections())
            {
                int newRow = row + directions.XCoordinate;
                int newCol = col + directions.YCoordinate;
                if (newRow>=0 && newRow < landElevation.GetLength(0) 
                    && newCol >=0 && newCol < landElevation.GetLength(1) 
                    && landElevation[newRow, newCol] >= landElevation[row, col])
                {
                    // Make sure that water can flow from landElevation[newRow, newCol] to landElevation[row, col]
                    DFS(landElevation, flowToPacificOrAtlantic, IsPacific, newRow, newCol, ret);
                }
            }
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

        public static void TestContinentalDivide()
        {
            int[,] landElevation = new int[,]
            {
                { 1,2,2,3,5},
                { 3,2,3,4,4},
                { 2,4,5,3,1},
                { 6,7,1,4,5},
                { 5,1,1,2,4}
            };
            List<Cell> continentalDivide = GetContinentalDivide(landElevation);
            Console.WriteLine("The points of continental divide is as shown below:");
            foreach(Cell c in continentalDivide)
            {
                Console.Write("{0} , ", c.ToString());
            }
            Console.WriteLine();
        }
    }

    
}
