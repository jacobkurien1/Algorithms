using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.DisjointSets
{
    public class DisjointSets<T>
    {
        private Dictionary<T, T> parentMapping;
        private Dictionary<T, int> depthOfTree;
        public DisjointSets()
        {
            parentMapping = new Dictionary<T, T>();
            depthOfTree = new Dictionary<T, int>();
        }

        /// <summary>
        /// We need to find the root of the tree where item is present.
        /// The running time for the subroutine is O(depth_of_tree)
        /// </summary>
        /// <param name="item">item whose root needs to be found</param>
        /// <returns>the root of the tree in the set</returns>
        public T Find(T item)
        {
            if(!parentMapping.ContainsKey(item))
            {
                // this is a root node
                return item;
            }
            return Find(parentMapping[item]);
        }

        /// <summary>
        /// Union of 2 disjoint sets
        /// The running time of this operation is O(1)
        /// </summary>
        /// <param name="item1">root of the set1</param>
        /// <param name="item2">root of set2</param>
        public void Union(T item1, T item2)
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
    }

    public class TestingDisjointSet
    {
        public static void Run()
        {
            DisjointSets<char> ds = new DisjointSets<char>();
            ds.Union('a', 'b');
            ds.Union('c', 'd');
            ds.Union('e', 'd');
            Console.WriteLine(ds.Find('e'));
            Console.WriteLine(ds.Find('a'));
            ds.Union('a', 'd');
            Console.WriteLine(ds.Find('a'));
        }
    }
}
