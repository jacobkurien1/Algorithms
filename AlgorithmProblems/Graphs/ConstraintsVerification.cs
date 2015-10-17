using AlgorithmProblems.Graphs.GraphHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    /// <summary>
    /// Question: You will be given a list of constraints like
    /// a == b , b== c a == d
    /// and you need to make sure that these constraints can be satisfied
    /// For eg a==b, b==c and a!=c cannot be completed.
    /// </summary>
    class ConstraintsVerification
    {
        /// <summary>
        /// This is the constraint class which defines whether 
        /// constraintVal1 == contraintVal2
        /// or
        /// contraintVal1 != constraintVal2
        /// </summary>
        private class Constraint
        {
            public int ConstraintVal1 { get; set; }
            public int ConstraintVal2 { get; set; }
            public bool IsEqual { get; set; }

            public Constraint (int constraintVal1, int constraintVal2, bool isEqual)
	        {
                this.ConstraintVal1 = constraintVal1;
                this.ConstraintVal2 = constraintVal2;
                this.IsEqual = isEqual;
	        }
        }

        private class Graph
        {
            public Dictionary<int, GraphVertex> AllVerticesDict { get; set; }
            public Graph()
            {
                this.AllVerticesDict = new Dictionary<int, GraphVertex>();
            }

            public void AddEdge(int vertex1Data, int vertex2Data)
            {
                if(!AllVerticesDict.ContainsKey(vertex1Data))
                {
                    AllVerticesDict.Add(vertex1Data, new GraphVertex(vertex1Data));
                }
                if(!AllVerticesDict.ContainsKey(vertex2Data))
                {
                    AllVerticesDict.Add(vertex2Data, new GraphVertex(vertex2Data));
                }
                GraphVertex vertex1 = AllVerticesDict[vertex1Data];
                GraphVertex vertex2 = AllVerticesDict[vertex2Data];
                vertex1.NeighbourVertices.Add(vertex2);
                vertex2.NeighbourVertices.Add(vertex1);
            }

            public GraphVertex GetVertex(int vertexData)
            {
                GraphVertex vertex = null;
                if (AllVerticesDict.ContainsKey(vertexData))
                {
                    vertex = AllVerticesDict[vertexData];
                }
                return vertex;
            }
        }

        private static Dictionary<GraphVertex,int> MappingGraphVertexAndGroup = new Dictionary<GraphVertex,int>();

        /// <summary>
        /// Step1: seperate the constraints into equal constraints and unequal ones.
        /// Step2: With equal constraints, form a graph where a=b will represent an edge between vertices a and b
        /// Step3: Do a DFS add all nodes to a dictionary as a key and add the group number as the value.
        /// Step4: For all the unequal constraints, check whether the group number is different.
        /// if the group number is same, the constraint list cannot be met
        /// if the group number is different, the constraint can be met.
        /// </summary>
        /// <param name="allConstraints"></param>
        /// <returns></returns>
        private static bool IsConstraintsValid(List<Constraint> allConstraints)
        {
            //Step1: seperate the constraints into equal constraints and unequal ones.
            IEnumerable<Constraint> equalConstraint = allConstraints.Where(constraint => constraint.IsEqual);
            IEnumerable<Constraint> unequalConstraint = allConstraints.Where(constraint => !constraint.IsEqual);

            //Step2: With equal constraints, form a graph where a=b will represent an edge between vertices a and b
            Graph grph = new Graph();

            foreach(Constraint cnst in equalConstraint)
            {
                grph.AddEdge(cnst.ConstraintVal1, cnst.ConstraintVal2);
            }

            //Step3: Do a DFS add all nodes to a dictionary as a key and add the group number as the value.
            int groupNum = 0;
            foreach(GraphVertex vertex in grph.AllVerticesDict.Values)
            {
                if(!vertex.IsVisited)
                {
                    groupNum++;
                    DFS(vertex, groupNum);
                }
            }

            //Step4: For all the unequal constraints, check whether the group number is different.
            foreach (Constraint cnst in unequalConstraint)
            {
                GraphVertex vertex1 = grph.GetVertex(cnst.ConstraintVal1);
                GraphVertex vertex2 = grph.GetVertex(cnst.ConstraintVal2);
                if(vertex1!=null && vertex2!=null)
                {
                    if(MappingGraphVertexAndGroup[vertex1] == MappingGraphVertexAndGroup[vertex2])
                    {
                        //if the group number is same, the constraint list cannot be met
                        return false;
                    }
                }
            }
            return true;

        }

        private static void DFS(GraphVertex vertex, int groupNum)
        {
            vertex.IsVisited = true;
            MappingGraphVertexAndGroup[vertex] = groupNum;
            foreach(GraphVertex neighbour in vertex.NeighbourVertices)
            {
                if(!neighbour.IsVisited)
                {
                    DFS(neighbour, groupNum);
                }
            }
        }

        public static void TestConstraintsVerification()
        {
            List<Constraint> allConstraints = new List<Constraint>();
            allConstraints.Add(new Constraint(1, 2, true));
            allConstraints.Add(new Constraint(2, 3, true));
            allConstraints.Add(new Constraint(3, 4, true));
            allConstraints.Add(new Constraint(1, 4, true));
            allConstraints.Add(new Constraint(5, 6, true));
            allConstraints.Add(new Constraint(1, 6, false));

            Console.WriteLine("The constraints can be met: {0}", IsConstraintsValid(allConstraints));

            allConstraints.Add(new Constraint(2, 4, false));

            Console.WriteLine("The constraints can be met: {0}", IsConstraintsValid(allConstraints));
        }
    }
}
