using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    /// <summary>
    /// Write an algorithm which takes in a social network and computes the extended contact for each individual.
    /// Extended Contact is all the contacts that can be reached from the neighbouring contacts
    /// </summary>
    class ExtendedContactsInSocialNetwork
    {
        /// <summary>
        /// Graph vertex which represents each person in the social network
        /// </summary>
        private class Vertex
        {
            public int Data { get; set; }
            public int VisitTime { get; set; }
            public List<Vertex> Contacts { get; set; }
            public List<Vertex> ExtendedContacts { get; set; }

            public Vertex (int data)
	        {
                this.Data = data;
                this.Contacts = new List<Vertex>();
                this.ExtendedContacts = new List<Vertex>();
	        }
        }

        /// <summary>
        /// This graph represents the social network
        /// </summary>
        private class Graph
        {
            public Dictionary<int, Vertex> AllVerticesDict { get; set; }
            public Graph()
            {
                this.AllVerticesDict = new Dictionary<int, Vertex>();
            }

            public void AddEdge(int vertex1Data, int vertex2Data)
            {
                if(!AllVerticesDict.ContainsKey(vertex1Data))
                {
                    AllVerticesDict.Add(vertex1Data, new Vertex(vertex1Data));
                }
                if(!AllVerticesDict.ContainsKey(vertex2Data))
                {
                    AllVerticesDict.Add(vertex2Data, new Vertex(vertex2Data));
                }
                Vertex vertex1 = AllVerticesDict[vertex1Data];
                Vertex vertex2 = AllVerticesDict[vertex2Data];
                vertex1.Contacts.Add(vertex2);
                vertex2.Contacts.Add(vertex1);
            }

            public Vertex GetVertex(int vertexData)
            {
                Vertex vertex = null;
                if (AllVerticesDict.ContainsKey(vertexData))
                {
                    vertex = AllVerticesDict[vertexData];
                }
                return vertex;
            }
        }

        private static void ComputeExtendedContacts(Graph socialNetwork)
        {
            List<Vertex> allPeople = socialNetwork.AllVerticesDict.Values.ToList<Vertex>();
            for(int i=1; i<= allPeople.Count; i++)
            {
                // We are doing i-1 to accomodate the fact the int value VisitTime in allPeople[0] will be intialized to 0
                // And the below condition will not execute
                if(allPeople[i-1].VisitTime != i)
                {
                    BFS(allPeople[i-1], i);
                }
            }
        }

        private static void BFS(Vertex person, int visitTime)
        {
            Queue<Vertex> bfsQueue = new Queue<Vertex>();
            bfsQueue.Enqueue(person);
            while (bfsQueue.Count > 0)
            {
                Vertex vertex = bfsQueue.Dequeue();
                if (vertex != person && person.Contacts.Where(contact => contact.Equals(vertex)).Count() == 0)
                {
                    person.ExtendedContacts.Add(vertex);
                }
                vertex.VisitTime = visitTime;
                foreach(Vertex contact in vertex.Contacts)
                {
                    if (contact.VisitTime != visitTime)
                    {
                        bfsQueue.Enqueue(contact);
                    }
                }
                
            }
        }

        public static void TestComputeExtendedContacts()
        {
            Graph g = new Graph();
            g.AddEdge(1, 2);
            g.AddEdge(1, 3);
            g.AddEdge(3, 4);

            ComputeExtendedContacts(g);
            PrintAllExtendedContacts(g);

            g = new Graph();
            g.AddEdge(1, 2);
            g.AddEdge(1, 3);
            g.AddEdge(3, 4);
            g.AddEdge(7, 8);

            ComputeExtendedContacts(g);
            PrintAllExtendedContacts(g);
        }

        private static void PrintAllExtendedContacts(Graph g)
        {
            Console.WriteLine("The extended contacts are as follows:");
            foreach (Vertex v in g.AllVerticesDict.Values)
            {
                Console.Write("The extended contact for {0} is: ", v.Data);
                foreach (Vertex extendedContact in v.ExtendedContacts)
                {
                    Console.Write(extendedContact.Data + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
