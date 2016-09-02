using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.MaxFlow
{
    /// <summary>
    /// There are total n employees and m jobs and the employees have given their preference to the jobs.
    /// Employees can give more that 1 preference, but they should be given only one job and each job can be done only by one employee.
    /// 
    /// Find the best assignment such that max job is allocated to the employees
    /// 
    /// Solution: We can solve this problem by Maximum bipartite matching. Create a source vertex which has edge with unit weight to all the employees.
    /// Note we are using unit edge weight cause each employee can take up only one job.
    /// Also create a sink vertex, all the jobs will have a directed edge towards the sink and the edge weight is 1. 
    /// The unit edge weight is used cause each job must be done by one employee.
    /// 
    /// Now calculate the max flow using ford fulkersons method and the max flow will give the max number of employees that can be assigned a job.
    /// 
    /// the running time of this algo is O(m`*C)
    /// where m` is the number of edges in graph G`. Number of edges in graph G = m
    /// m` = m+2(n)
    /// C is edges from source to all employee nodes = n
    /// so O((m+2n)*n) = O(m*n)
    /// </summary>
    class MaximumBipartiteMatching
    {
        /// <summary>
        /// Create a source vertex which has edge with unit weight to all the employees.
        /// Note we are using unit edge weight cause each employee can take up only one job.
        /// Also create a sink vertex, all the jobs will have a directed edge towards the sink and the edge weight is 1. 
        /// The unit edge weight is used cause each job must be done by one employee.
        /// </summary>
        /// <param name="employeeJobPreference">dictionary having key as the employeeid and value as the list of job ids which are prefered</param>
        public MaximumBipartiteMatching(Dictionary<string, List<string>> employeeJobPreference)
        {
            // initialize the graph
            Graph = new GraphWithFwdEdges();
            foreach(string employeeId in employeeJobPreference.Keys)
            {
                Graph.AddEdge(StartId, employeeId, 1); // Add fwd edge
                Graph.AddEdge(employeeId, StartId, 0, false); // Add backedge
                foreach(string jobId in employeeJobPreference[employeeId])
                {
                    Graph.AddEdge(employeeId, jobId, 1); // Add fwdEdge
                    Graph.AddEdge(jobId, employeeId, 0, false); // Add back edge

                    // Add the directed edge towards sink from the job id
                    Graph.AddEdge(jobId, SinkId, 1); // Add fwdEdge
                    Graph.AddEdge(SinkId, jobId, 0, false); // Add back edge
                }
            }

        }
        private GraphWithFwdEdges Graph { get; set; }
        private const string StartId = "S";
        private const string SinkId = "T";

        /// <summary>
        /// Gets the employee to job match
        /// </summary>
        /// <returns>a dictionary with employee id as the key and jobid as the value</returns>
        public Dictionary<string, string> GetTheEmployeeJobMatch()
        {
            Dictionary<string, string> employeeJobMatch = new Dictionary<string, string>();
            int maxMatches = FordFulkerson(StartId, SinkId);
            Console.WriteLine("The max of {0} employees were matched to jobs", maxMatches);

            // Now lets get the matches of employees to the job
            List <GraphVertex> allEmployees = Graph.AllVertices[StartId].Neighbours;
            foreach (GraphVertex employee in allEmployees)
            {
                foreach(GraphVertex job in employee.Neighbours)
                {
                    string edgeId = employee.Id + "#" + job.Id;
                    if (Graph.EdgeWeights[edgeId] == 0 && Graph.IsEdgeFwd[edgeId])
                    {
                        //for all fwd edges where the weight is 0 suggest that max flow took this flow path
                        employeeJobMatch[employee.Id] = job.Id;
                        break;
                    }
                }
            }
            return employeeJobMatch;
        }

        /// <summary>
        /// Since this bipartite matching will only have 4 layers and all the layers are unidirectional w/o any back edges.
        /// So we can use DFS to get the max flow.
        /// 
        /// The running time will be O(maxFlow*E)
        /// </summary>
        /// <param name="graph"></param>
        /// <returns>the maxflow in the network</returns>
        private int FordFulkerson(string startId, string endId)
        {
            int maxflow = 0;
            while(DFSWithBacktrack(startId, endId))
            {
                // every time we find a path it will only have a unit flow
                ++maxflow;
            }
            return maxflow;
        }

        /// <summary>
        /// Does DFS and then back tracks in the path and subtracts 1 from residual capacity of the fwd edges
        /// and adds 1 to the residual capacity of the back edges
        /// </summary>
        /// <param name="startId">id representing source of graph</param>
        /// <param name="endId">id representing the sink of the graph</param>
        /// <returns></returns>
        private bool DFSWithBacktrack(string startId, string endId)
        {
            Dictionary<string, string> backtrack = new Dictionary<string, string>();
            backtrack[startId] = null;
            if(DFS(startId, endId, backtrack))
            {
                string currentVertexId = endId;
                while (backtrack[currentVertexId] != null)
                {
                    string parentVertexId = backtrack[currentVertexId];
                    // increment and decrement by unit flow, no need to get the min in the path as the path will only have unit flow
                    Graph.DecrementEdgeWeightByFactor(parentVertexId, currentVertexId, 1);
                    Graph.IncrementEdgeWeightByFactor(currentVertexId, parentVertexId, 1);
                    currentVertexId = parentVertexId;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Recursive DFS subroutine used by Ford fulkerson's algorithm
        /// </summary>
        /// <param name="vertexId"></param>
        /// <param name="endId"></param>
        /// <param name="backtrack"></param>
        /// <returns></returns>
        private bool DFS(string vertexId, string endId, Dictionary<string, string> backtrack)
        {
            if (!string.IsNullOrEmpty(vertexId))
            {
                // Check Error condition

                if (vertexId == endId)
                {
                    //We have found a path
                    return true;
                }
                else
                {
                    GraphVertex currentVertex = Graph.AllVertices[vertexId];
                    foreach (GraphVertex neighbour in currentVertex.Neighbours)
                    {
                        string edgeId = currentVertex.Id + "#" + neighbour.Id;
                        if (!backtrack.ContainsKey(neighbour.Id) && Graph.EdgeWeights[edgeId] > 0)
                        {
                            backtrack[neighbour.Id] = currentVertex.Id;
                            
                            if (DFS(neighbour.Id, endId, backtrack))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        #region TestArea
        public static void TestMaximumBipartiteMatching()
        {
            Dictionary<string, List<string>> employeeJobPreference = new Dictionary<string, List<string>>();
            employeeJobPreference["a"] = new List<string>() { "1", "2" };
            employeeJobPreference["b"] = new List<string>() { "1"};
            employeeJobPreference["c"] = new List<string>() { "3", "2" };
            employeeJobPreference["d"] = new List<string>() { "3", "5", "4" };
            employeeJobPreference["e"] = new List<string>() { "5" };

            MaximumBipartiteMatching mbm = new MaximumBipartiteMatching(employeeJobPreference);
            Dictionary<string, string> allMatches = mbm.GetTheEmployeeJobMatch();
            PrintMatches(allMatches);
        }

        private static void PrintMatches(Dictionary<string, string> allMatches)
        {
            Console.WriteLine("The employee job match is as shown below:");
            foreach(KeyValuePair<string, string> match in allMatches)
            {
                Console.WriteLine("{0} -> {1}", match.Key, match.Value);
            }
        }

        #endregion
    }
}
