using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.DisjointSets
{
    /// <summary>
    /// This data structure is also known as union-find data structure.
    /// We will be using the depth of the tree for each node to keep the tree balanced
    /// 
    /// This algorithm is weighted Quick - union and 
    /// the running time for M union-find ops on a set of N objects is O(N + M log N)
    ///  
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DisjointSets<T> where T : IEquatable<T>
    {
        private Dictionary<T, T> parentMapping;

        /// <summary>
        /// This helps in keeping the set tree balanced
        /// </summary>
        private Dictionary<T, int> depthOfTree;
        public DisjointSets()
        {
            parentMapping = new Dictionary<T, T>();
            depthOfTree = new Dictionary<T, int>();
        }

        /// <summary>
        /// Returns true if both the item1 and item2 belongs to the same set
        /// 
        /// This operation takes 
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
            if(!parentMapping.ContainsKey(item))
            {
                // this is a root node
                return item;
            }
            return GetRoot(parentMapping[item]);
        }

        /// <summary>
        /// Union of 2 disjoint sets when the root of the set is given as the input parameter
        /// The running time of this operation is O(1)
        /// </summary>
        /// <param name="item1">root of the set1</param>
        /// <param name="item2">root of set2</param>
        public void UnionOfRoots(T item1, T item2)
        {
            if(parentMapping.ContainsKey(item1) || parentMapping.ContainsKey(item2))
            {
                //Make sure that the item1 and item2 are root of the trees in the disjoint set forest
                throw new Exception("the 2 sets cannot be merged");
            }
            int depthForItem1 = depthOfTree.ContainsKey(item1) ? depthOfTree[item1] : 0;
            int depthForItem2 = depthOfTree.ContainsKey(item2) ? depthOfTree[item2] : 0;
            if (depthForItem1 > depthForItem2)
            {
                parentMapping[item2] = item1;
            }
            else if (depthForItem1 < depthForItem2)
            {
                parentMapping[item1] = item2;
            }
            else
            {
                // either the depth is equal or the items are not present in the depthOfTree dictionary
                parentMapping[item1] = item2;
                if (!depthOfTree.ContainsKey(item2))
                {
                    depthOfTree[item2] = 1;
                }
                else
                {
                    depthOfTree[item2]++;
                }
            }
        }

        /// <summary>
        /// Union of 2 disjoint sets where item1 belongs to set1 and item2 belongs to set2
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

    public class TestingDisjointSet
    {
        public static void Run()
        {
            DisjointSets<char> ds = new DisjointSets<char>();
            ds.UnionOfRoots('a', 'b');
            ds.UnionOfRoots('c', 'd');
            ds.UnionOfRoots('e', 'd');
            Console.WriteLine(ds.GetRoot('e'));
            Console.WriteLine(ds.GetRoot('a'));
            ds.UnionOfRoots(ds.GetRoot('a'), ds.GetRoot('d'));
            Console.WriteLine(ds.GetRoot('a'));
        }
    }
}
