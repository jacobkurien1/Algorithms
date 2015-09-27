using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    class SolveAMaze
    {
        /// <summary>
        /// This will have the MazePath in the reverse order
        /// </summary>
        private static List<GraphVertex> MazePath = new List<GraphVertex>();

        /// <summary>
        /// This function uses DFS to solve a maze
        /// Once the Maze is solved we store the path in the reverse order in MazePath
        /// Note: We can also use a BFS if we want to find the shortest path
        /// </summary>
        /// <param name="start">start node of the maze</param>
        /// <param name="end">end node of the maze</param>
        /// <returns>whether the maze is solved or not</returns>
        private static bool IsMazeSolved(GraphVertex start, GraphVertex end)
        {
            if (start == null || end == null)
            {
                return false;
            }
            else if (start == end)
            {
                return true;
            }
            start.IsVisited = true;
            foreach(GraphVertex neighbour in start.NeighbourVertices)
            {
                if (!neighbour.IsVisited && IsMazeSolved(neighbour, end))
                {
                    MazePath.Add(neighbour);
                    return true;
                }
            }
            return false;

        }

        public static void TestSolveAMaze()
        {
            UndirectedGraph udg = GraphProbHelper.CreateUndirectedGraph();
            if (IsMazeSolved(udg.AllVertices[0], udg.AllVertices[5]))
            {
                Console.WriteLine("The maze is solved and the path is");
                for(int i=MazePath.Count-1; i>=0; i--)
                {
                    Console.Write(MazePath[i].Data + " -> ");
                }
                Console.WriteLine();
            }

            MazePath = new List<GraphVertex>();
            DirectedGraph dg = GraphProbHelper.CreatedirectedGraphWithoutCycle();
            if (IsMazeSolved(dg.AllVertices[0], dg.AllVertices[5]))
            {
                Console.WriteLine("The maze is solved and the path is");
                for (int i = MazePath.Count - 1; i >= 0; i--)
                {
                    Console.Write(MazePath[i].Data + " -> ");
                }
                Console.WriteLine();
            }
        }
    }
}
