using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.DisjointSets
{
    /// <summary>
    /// This data structure is also known as weighted union-find with path compression data structure.
    /// We will be using the depth of the tree for each node to keep the tree balanced
    /// 
    /// The running time for M union-find ops on a set of N objects is O((N + M)*lg(N))
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class WeightedDisjointSetsWithPathCompression<T> where T : IEquatable<T>
    {
        private Dictionary<T, T> parentMapping;

        /// <summary>
        /// This helps in keeping the set tree balanced
        /// Key of this dictionary will be the node and the value will be all the nodes below this node +1(for the node itself)
        /// </summary>
        private Dictionary<T, int> weightOfTree;
        public WeightedDisjointSetsWithPathCompression()
        {
            parentMapping = new Dictionary<T, T>();
            weightOfTree = new Dictionary<T, int>();
        }

        /// <summary>
        /// Returns true if both the item1 and item2 belongs to the same set
        /// 
        /// This operation takes O(log(n)) time
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns></returns>
        public bool Find(T item1, T item2)
        {
            T root1 = GetRoot(item1);
            T root2 = GetRoot(item2);
            return root1.Equals(root2);
        }

        /// <summary>
        /// We need to find the root of the tree where item is present.
        /// The running time for the subroutine is O(depth_of_tree)
        /// </summary>
        /// <param name="item">item whose root needs to be found</param>
        /// <returns>the root of the tree in the set</returns>
        public T GetRoot(T item)
        {
            T root = item;

            // Find the root
            while (parentMapping.ContainsKey(root))
            {
                //if(parentMapping.ContainsKey(parentMapping[root]))
                //{
                //    //if the grandparent exist
                //    parentMapping[root] = parentMapping[parentMapping[root]];
                //}
                root = parentMapping[root];
            }

            // We will do path compression here
            T currentNode = item;
            while (parentMapping.ContainsKey(currentNode))
            {
                T nextNode = parentMapping[currentNode];
                parentMapping[currentNode] = root;
                weightOfTree[currentNode] = 1;
                currentNode = nextNode;
            }

            return root;
        }

        /// <summary>
        /// Union of 2 disjoint sets when the root of the set is given as the input parameter
        /// The running time of this operation is O(1)
        /// </summary>
        /// <param name="item1">root of the set1</param>
        /// <param name="item2">root of set2</param>
        public void UnionOfRoots(T item1, T item2)
        {
            if (parentMapping.ContainsKey(item1) || parentMapping.ContainsKey(item2))
            {
                //Make sure that the item1 and item2 are root of the trees in the disjoint set forest
                throw new Exception("the 2 sets cannot be merged");
            }
            int weightForItem1 = weightOfTree.ContainsKey(item1) ? weightOfTree[item1] : 1;
            int weightForItem2 = weightOfTree.ContainsKey(item2) ? weightOfTree[item2] : 1;
            if (weightForItem1 > weightForItem2)
            {
                parentMapping[item2] = item1;
                weightOfTree[item1] = weightForItem1 + weightForItem2;
            }
            else //(weightForItem1 <= weightForItem2)
            {
                parentMapping[item1] = item2;
                weightOfTree[item2] = weightForItem1 + weightForItem2;
            }

        }

        /// <summary>
        /// Union of 2 disjoint sets where item1 belongs to set1 and item2 belongs to set2
        /// The running time is O(depth_of_the_tree) = O(log(n))
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        public void Union(T item1, T item2)
        {
            T root1 = GetRoot(item1);
            T root2 = GetRoot(item2);
            UnionOfRoots(root1, root2);
        }
    }
    public class TestWeightedDisjointSetsWithPathCompression
    {
        public static void Run()
        {
            DisjointSets<int> ds = new DisjointSets<int>();
            WeightedDisjointSetsWithPathCompression<int> wdspc = new WeightedDisjointSetsWithPathCompression<int>();

            var sw = Stopwatch.StartNew();
            for (int j = 10001; j < 20000; j++)
            {
                for (int i = 1; i < 10000; i++)
                {
                    if (!ds.Find(i, j))
                    {
                        ds.Union(i, j);
                    }

                }
            }

            for (int j = 10001; j < 20000; j++)
            {
                for (int i = 1; i < 10000; i++)
                {
                    if (!ds.Find(i, j))
                    {
                        ds.Union(i, j);
                    }

                }
            }
            sw.Stop();
            Console.WriteLine("RunTime without path compression" + sw.Elapsed);

            sw = Stopwatch.StartNew();
            for (int j = 10001; j < 20000; j++)
            {
                for (int i = 1; i < 10000; i++)
                {
                    if (!wdspc.Find(i, j))
                    {
                        wdspc.Union(i, j);
                    }
                }
            }

            for (int j = 10001; j < 20000; j++)
            {
                for (int i = 1; i < 10000; i++)
                {
                    if (!wdspc.Find(i, j))
                    {
                        wdspc.Union(i, j);
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("RunTime path compression" + sw.Elapsed);

        }
    }
}
