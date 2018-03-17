using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs.MaxFlow
{
    #region Graph class and vertex class
    /// <summary>
    /// Represents the graph
    /// </summary>
    internal class Graph
    {
        public Graph()
        {
            AllVertices = new Dictionary<string, GraphVertex>();
            EdgeWeights = new Dictionary<string, int>();
        }
        public Dictionary<string, GraphVertex> AllVertices { get; set; }
        public Dictionary<string, int> EdgeWeights { get; set; }

        /// <summary>
        /// Add a weighted directed edge
        /// </summary>
        /// <param name="startId"></param>
        /// <param name="endId"></param>
        /// <param name="weight"></param>
        public void AddEdge(string startId, string endId, int weight)
        {
            if (!AllVertices.ContainsKey(startId))
            {
                AllVertices[startId] = new GraphVertex(startId);
            }
            if (!AllVertices.ContainsKey(endId))
            {
                AllVertices[endId] = new GraphVertex(endId);
            }
            AllVertices[startId].Neighbours.Add(AllVertices[endId]);
            EdgeWeights[startId + "#" + endId] = weight;
        }

        /// <summary>
        /// Change the edge weight of the directed edge
        /// </summary>
        /// <param name="startId"></param>
        /// <param name="endId"></param>
        /// <param name="weight"></param>
        public void ChangeEdgeWeight(string startId, string endId, int weight)
        {
            string edgeId = string.Format("{0}#{1}", startId, endId);
            EdgeWeights[edgeId] = weight;
        }

        /// <summary>
        /// increment the edge weight by the weightfactor
        /// </summary>
        /// <param name="startId"></param>
        /// <param name="endId"></param>
        /// <param name="weightFactor"></param>
        public void IncrementEdgeWeightByFactor(string startId, string endId, int weightFactor)
        {
            string edgeId = string.Format("{0}#{1}", startId, endId);
            EdgeWeights[edgeId] += weightFactor;
        }

        /// <summary>
        /// Decrement the edge weight by the weightfactor
        /// </summary>
        /// <param name="startId"></param>
        /// <param name="endId"></param>
        /// <param name="weightFactor"></param>
        public void DecrementEdgeWeightByFactor(string startId, string endId, int weightFactor)
        {
            string edgeId = string.Format("{0}#{1}", startId, endId);
            EdgeWeights[edgeId] -= weightFactor;
        }
    }

    /// <summary>
    /// Represents the vertex of the graph
    /// </summary>
    internal class GraphVertex
    {
        public string Id { get; set; }
        public List<GraphVertex> Neighbours { get; set; }
        public GraphVertex(string id)
        {
            Id = id;
            Neighbours = new List<GraphVertex>();
        }
    }
    #endregion

    /// <summary>
    /// Graph with the indication whether an edge is a forward edge or backward edge
    /// </summary>
    internal class GraphWithFwdEdges : Graph
    {
        public Dictionary<string, bool> IsEdgeFwd;
        public GraphWithFwdEdges()
        {
            IsEdgeFwd = new Dictionary<string, bool>();
        }

        public void AddEdge(string startId, string endId, int weight, bool IsFwdEdge = true)
        {
            IsEdgeFwd[startId + "#" + endId] = IsFwdEdge;
            base.AddEdge(startId, endId, weight);
        }
    }
}
