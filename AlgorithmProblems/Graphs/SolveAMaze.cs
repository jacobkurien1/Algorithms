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
        #region Algo1: Using recursion
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
        #endregion

        #region Algo2: Iteratively

        /// <summary>
        /// Solves a maze iteratively
        /// </summary>
        /// <param name="start">start vertex</param>
        /// <param name="end">end vertex</param>
        /// <returns></returns>
        public static List<GraphVertex> SolveMazeIter(GraphVertex start, GraphVertex end)
        {
            // parent will also be used to check whether a vertex is visited or not
            Dictionary<GraphVertex, GraphVertex> parent = new Dictionary<GraphVertex, GraphVertex>();
            Stack<GraphVertex> st = new Stack<GraphVertex>();
            st.Push(start);
            parent[start] = null;

            while (st.Count > 0)
            {
                GraphVertex vertex = st.Pop();
                if(vertex == end)
                {
                    // we have found the path
                    // backtrack to get the path
                    return BackTrackToGetPath(parent, start, end);
                }
                foreach (GraphVertex neighbour in vertex.NeighbourVertices)
                {
                    if(!parent.ContainsKey(neighbour))
                    {
                        // this vertex is not visited
                        parent[neighbour] = vertex;
                        st.Push(neighbour);
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Backtrack to get the path from start to end vertex
        /// </summary>
        /// <param name="parent">stores a vertex as key and its parent in the path as value of the dictionary</param>
        /// <param name="start">start vertex</param>
        /// <param name="end">end vertex</param>
        /// <returns></returns>
        private static List<GraphVertex> BackTrackToGetPath(Dictionary<GraphVertex, GraphVertex> parent, GraphVertex start, GraphVertex end)
        {
            List<GraphVertex> path = new List<GraphVertex>();

            if (parent != null && start != null)
            {
                while (end != null)
                {
                    path.Add(end);
                    end = parent[end];
                }
                path.Reverse();
            }

            return path;
        }


        #endregion

        public static void TestSolveAMaze()
        {
            UndirectedGraph udg = GraphProbHelper.CreateUndirectedGraph();
            if (IsMazeSolved(udg.AllVertices[0], udg.AllVertices[5]))
            {
                MazePath.Reverse();
                PrintMazePath(MazePath);
            }

            MazePath = new List<GraphVertex>();
            DirectedGraph dg = GraphProbHelper.CreatedirectedGraphWithoutCycle();
            if (IsMazeSolved(dg.AllVertices[0], dg.AllVertices[5]))
            {
                MazePath.Reverse();
                PrintMazePath(MazePath);
            }

            MazePath = new List<GraphVertex>();
            DirectedGraph dgc = GraphProbHelper.CreatedirectedGraphWithCycle();
            if (IsMazeSolved(dgc.AllVertices[0], dgc.AllVertices[5]))
            {
                MazePath.Reverse();
                PrintMazePath(MazePath);
            }

            // test the iterative method
            List<GraphVertex> path = SolveMazeIter(udg.AllVertices[0], udg.AllVertices[5]);
            if (path != null)
            {
                PrintMazePath(path);
            }

            path = SolveMazeIter(dg.AllVertices[0], dg.AllVertices[5]);
            if(path!=null)
            {
                PrintMazePath(path);
            }

            path = SolveMazeIter(dgc.AllVertices[0], dgc.AllVertices[5]);
            if (path != null)
            {
                PrintMazePath(path);
            }
        }

        private static void PrintMazePath(List<GraphVertex> path)
        {
            Console.WriteLine("The maze is solved and the path is");

            for (int i = 0; i<path.Count; i++)
            {
                Console.Write(path[i].Data + " -> ");
            }
            Console.WriteLine();
        }
    }
}
