using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    public class SnakeAndLadder
    {
        public DirectedGraphWithVertexDictionary Board { get; set; }
        public SnakeAndLadder(int sAndLBoardLength, List<GraphEdge> snakesAndLadders)
        {
            Board = new DirectedGraphWithVertexDictionary();
            
            for(int i=1; i<= sAndLBoardLength; i++)
            {
                for(int j=1; j<=6 && i+j<= sAndLBoardLength; j++) // This represents the different outcomes of the dice throw
                {
                    // i+j<= sAndLBoardLength handles the overflow(from the board) corner case
                    Board.AddEdge(i, i + j);
                }
            }
            foreach (GraphEdge edge in snakesAndLadders)
            {
                Board.MergeVertex(edge.StartEdgeId, edge.EndEdgeId);
            }
        }

        public List<GraphVertex> Solve()
        {
            Dictionary<GraphVertex, GraphVertex> Backtrack = new Dictionary<GraphVertex, GraphVertex>();
            List<GraphVertex> ret = new List<GraphVertex>();
            Queue<GraphVertex> queue = new Queue<GraphVertex>();
            GraphVertex initialVertex = Board.AllVertices[1];// Snake and ladder game starts at 1th place
            initialVertex.IsVisited = true;
            GraphVertex finalVertex = Board.AllVertices[Board.AllVertices.Count]; // Snake and ladder game ends at sAndLBoardLength
            queue.Enqueue(initialVertex);

            while (queue.Count>0)
            {
                GraphVertex currentVertex = queue.Dequeue();
                if (currentVertex == finalVertex)
                {
                    // We reached the final point in the s and l game
                    break;
                }
                foreach (GraphVertex neighbour in currentVertex.NeighbourVertices)
                {
                    if (!neighbour.IsVisited)
                    {
                        neighbour.IsVisited = true;
                        Backtrack[neighbour] = currentVertex;
                        queue.Enqueue(neighbour);
                    }
                }
            }

            // Lets back track to get the path for the BFS
            while(finalVertex!=initialVertex)
            {
                ret.Add(finalVertex);
                finalVertex = Backtrack[finalVertex];
            }
            ret.Add(initialVertex);
            ret.Reverse();

            return ret;
        }

        public static void TestSnakeAndLadder()
        {
            List<GraphEdge> allEdges = new List<GraphEdge>();
            allEdges.Add(new GraphEdge(3, 22));
            allEdges.Add(new GraphEdge(5, 8));
            allEdges.Add(new GraphEdge(11, 26));
            allEdges.Add(new GraphEdge(20, 29));
            allEdges.Add(new GraphEdge(27, 1));
            allEdges.Add(new GraphEdge(21, 9));
            allEdges.Add(new GraphEdge(17, 4));
            allEdges.Add(new GraphEdge(19, 7));
            SnakeAndLadder sl = new SnakeAndLadder(30, allEdges);
            PrintPath(sl.Solve());
        }
        private static void PrintPath(List<GraphVertex> path)
        {
            foreach(GraphVertex g in path)
            {
                Console.Write("{0} -> ", g.Data);
            }
            Console.WriteLine();
        }
    }
}
