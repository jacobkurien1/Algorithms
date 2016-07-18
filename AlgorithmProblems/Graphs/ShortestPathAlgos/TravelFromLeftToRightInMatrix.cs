using AlgorithmProblems.Graphs.GraphHelper;
using AlgorithmProblems.Heaps.HeapHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.ShortestPathAlgos.TravelInMatrix
{
    /// <summary>
    /// Given a matrix which has mines denoted by 1 and 0 denotes that we can traverse to that cell.
    /// We need to traverse from left side of the matrix to right side of the matrix
    /// |------*---*-------*|
    /// |--*---*-------*----|
    /// |--*------*--------*|
    /// |------*-----*--*---|
    /// |---*-------*----*--|
    /// |---*---*----------*|
    /// |-----*---*-------*-|
    /// |---*----------*---*|
    /// </summary>
    class TravelFromLeftToRightInMatrix
    {

        /// <summary>
        /// To solve this since we can start from any cell on the left axis and we can end at any cell on the right axis
        /// Lets define a source cell which has edges which connect to all the cells on the left axis ie mat[i,0]
        /// Also define a destination cell which has all edges connected to all the cells on the right axis ie mat[i,mat.Length-1]
        /// 
        /// Then we can do a djkstra's shortest path algorithm to figure out the 
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public List<Cell> GetPath(int[,] mat)
        {
            UndirectedGraphWithVertexDictionary udg = new UndirectedGraphWithVertexDictionary();

            // Fix the source and destination cells.
            GraphVertexWithDistance source = new GraphVertexWithDistance(new Cell(-1, -1));
            GraphVertexWithDistance destination = new GraphVertexWithDistance(new Cell(mat.Length, mat.Length));

            // Create the graph vertices and put it in a matrix
            GraphVertexWithDistance[,] allVertex = CreateGraphFromMatrix(mat, source, destination);

            // Now we need to do dijkstras shortest path algorithm. Each edge will have a weight of 1.
            return DijkstrasShortestPath(source, destination, allVertex);
        }

        /// <summary>
        /// Create graph from the matrix and add all the edges in the format shown below
        ///     |------*---*-------*|
        ///     |--*---*-------*----|
        ///     |--*------*--------*|
        ///s ---|------*-----*--*---| --- d
        ///     |---*-------*----*--|
        ///     |---*---*----------*|
        ///     |-----*---*-------*-|
        ///     |---*----------*---*|
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        private GraphVertexWithDistance[,] CreateGraphFromMatrix(int[,] mat, GraphVertexWithDistance source, GraphVertexWithDistance destination)
        {
            // Create the graph from the matrix
            GraphVertexWithDistance[,] allVertex = new GraphVertexWithDistance[mat.GetLength(0), mat.GetLength(1)];
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    allVertex[i, j] = new GraphVertexWithDistance(new Cell(i, j));
                }

            }

            // Add all the edges to the graph vertices
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    if (j == 0 && mat[i,j] !=1)
                    {
                        // This is the left side of the matrix
                        source.AddEdge(allVertex[i, j]);
                        allVertex[i, j].AddEdge(source);
                    }
                    if (j == mat.GetLength(1) - 1 && mat[i, j] != 1)
                    {
                        // This is the right side of the matrix
                        destination.AddEdge(allVertex[i, j]);
                        allVertex[i, j].AddEdge(destination);
                    }
                    if (j + 1 < mat.GetLength(1) && mat[i, j+1] != 1 && mat[i, j] != 1)
                    {
                        // Add the edge to the right of the graphvertex allVertex[i,j]
                        allVertex[i, j].AddEdge(allVertex[i, j + 1]);
                        allVertex[i, j + 1].AddEdge(allVertex[i, j]);
                    }
                    if (i + 1 < mat.GetLength(0) && mat[i+1, j] != 1 && mat[i, j] != 1)
                    {
                        // Add the edge below graphVertex allVertex[i,j]
                        allVertex[i, j].AddEdge(allVertex[i + 1, j]);
                        allVertex[i + 1, j].AddEdge(allVertex[i, j]);
                    }

                }
            }
            return allVertex;
        }

        private List<Cell> DijkstrasShortestPath(GraphVertexWithDistance source, GraphVertexWithDistance destination, GraphVertexWithDistance[,] allVertex)
        {
            // Initialization
            MinHeap<GraphVertexWithDistance> mh = new MinHeap<GraphVertexWithDistance>(allVertex.GetLength(0)*allVertex.GetLength(1)+2);
            Dictionary<GraphVertexWithDistance, GraphVertexWithDistance> backtrack = new Dictionary<GraphVertexWithDistance, GraphVertexWithDistance>();
            Dictionary<GraphVertexWithDistance, int> FinalDistance = new Dictionary<GraphVertexWithDistance, int>();

            // Add all the graphvertices to the minheap
            source.Distance = 0;
            mh.Insert(source);
            mh.Insert(destination);
            for(int i=0; i<allVertex.GetLength(0); i++)
            {
                for(int j=0; j<allVertex.GetLength(1); j++)
                {
                    mh.Insert(allVertex[i, j]);
                }
            }

            while(mh.HeapSize >0)
            {
                GraphVertexWithDistance vertex = mh.ExtractMin();
                FinalDistance[vertex] = vertex.Distance;
                if(vertex == destination)
                {
                    // We have reached the destination. No need to calculate final distance for all the other graph vertex
                    break;
                }
                foreach(GraphEdge neighbourEdge in vertex.NeighbourEdges)
                {
                    if(!FinalDistance.ContainsKey(neighbourEdge.Destination))
                    {
                        if (vertex.Distance + 1 < neighbourEdge.Destination.Distance)
                        {
                            GraphVertexWithDistance oldVertex = neighbourEdge.Destination;
                            neighbourEdge.Destination.Distance = vertex.Distance + 1;
                            GraphVertexWithDistance newVertex = neighbourEdge.Destination;
                            mh.Replace(newVertex, oldVertex);
                            backtrack[neighbourEdge.Destination] = vertex;
                        }

                    }
                }
            }

            // Back track to get the path 
            List<Cell> path = new List<Cell>();
            GraphVertexWithDistance currentVertex = destination;
            while(currentVertex != source)
            {
                if(backtrack.ContainsKey(currentVertex))
                {
                    if (currentVertex != destination)
                    {
                        path.Add(currentVertex.Id);
                    }
                    currentVertex = backtrack[currentVertex];
                }
                else
                {
                    break;
                }
            }
            path.Reverse();
            return path;
        }

        public static void TestTravelFromLeftToRightInMatrix()
        {
            int[,] mat = new int[,]
            {
                { 1,1,1,1 },
                {1,0,0,0 },
                {0,0,1,1 },
                {1,1,1,1 }

            };
            TravelFromLeftToRightInMatrix travel = new TravelFromLeftToRightInMatrix();
            List<Cell> path = travel.GetPath(mat);
            PrintPath(path);

            mat = new int[,]
            {
                { 1,1,1,1 },
                {1,0,0,0 },
                {0,0,1,1 },
                {0,0,0,0 }

            };
            travel = new TravelFromLeftToRightInMatrix();
            path = travel.GetPath(mat);
            PrintPath(path);
        }

        private static void PrintPath(List<Cell> path)
        {
            Console.WriteLine("The path is as shown below:");
            foreach(Cell c in path)
            {
                Console.Write("{0} ->", c.ToString());
            }
            Console.WriteLine();
        }
    }

    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Cell(int row, int col)
        {
            Row = row;
            Column = col;
        }

        public override int GetHashCode()
        {
            return Row.GetHashCode() + Column.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ((Cell)obj).Row == Row && ((Cell)obj).Column == Column;
        }

        public override string ToString()
        {
            return string.Format("({0},{1})",Row,Column);
        }
    }

    public class GraphEdge
    {
        public GraphVertexWithDistance Source { get; set; }
        public GraphVertexWithDistance Destination { get; set; }
        public int Distance { get; set; }

        public GraphEdge(GraphVertexWithDistance source, GraphVertexWithDistance destination)
        {
            Source = source;
            Destination = destination;
            Distance = 1;
        }
    }

    public class GraphVertexWithDistance : IComparable
    {
        public Cell Id { get; set; }
        public List<GraphEdge> NeighbourEdges { get; set; }
        public int Distance { get; set; }
        public GraphVertexWithDistance(Cell cell)
        {
            Id = cell;
            NeighbourEdges = new List<GraphEdge>();
            Distance = int.MaxValue;
        }


        public void AddEdge(GraphVertexWithDistance neighbour)
        {
            NeighbourEdges.Add(new GraphEdge(this, neighbour));
        }

        public int CompareTo(object obj)
        {
            return Distance.CompareTo(((GraphVertexWithDistance)obj).Distance);
        }
    }
}
