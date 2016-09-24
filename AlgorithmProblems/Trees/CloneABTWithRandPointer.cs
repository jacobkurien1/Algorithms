using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    /// <summary>
    /// Clone a binary tree, where each node has a random pointer to any other node in tree along with the 
    /// left and right child
    /// </summary>
    class CloneABTWithRandPointer
    {
        /// <summary>
        /// We will do a preorder traversal of the tree(same as doing the BFS)
        /// 1. Whenever a new node is encountered, create a clone of the node and copy the original nodes fields
        ///     like data, left, right, random
        /// 2. Make the original Nodes rnd pointer point to the nodeClone
        /// 3. Now traverse from rootClone and make 
        ///     nodeClone.Left = nodeClone.Left.Random
        ///     nodeClone.Right = nodeClone.Right.Random
        ///     nodeClone.Random = nodeClone.Random.Random
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public BTNodeWithRnd GetClone(BTNodeWithRnd root)
        {
            BTNodeWithRnd rootClone = null;

            Queue<BTNodeWithRnd> queue = new Queue<BTNodeWithRnd>();
            queue.Enqueue(root);

            while(queue.Count>0)
            {
                BTNodeWithRnd node = queue.Dequeue();

                // 1.Whenever a new node is encountered, create a clone of the node and copy the original nodes fields
                // like data, left, right, random
                BTNodeWithRnd nodeClone = new BTNodeWithRnd(node);
                if (rootClone == null)
                {
                    // Set the root clone node
                    rootClone = nodeClone;
                }

                if (node.Left!=null)
                {
                    queue.Enqueue(node.Left);
                }
                if(node.Right!=null)
                {
                    queue.Enqueue(node.Right);
                }
                // 2. Make the original Nodes rnd pointer point to the nodeClone
                node.Random = nodeClone;
            }


            // 3. Now traverse the clone and make all the node pointers point to the cloned nodes
            queue.Enqueue(rootClone);

            while (queue.Count > 0)
            {
                BTNodeWithRnd nodeClone = queue.Dequeue();

                if (nodeClone.Left != null)
                {
                    nodeClone.Left = nodeClone.Left.Random;
                    queue.Enqueue(nodeClone.Left);
                }
                if (nodeClone.Right != null)
                {
                    nodeClone.Right = nodeClone.Right.Random;
                    queue.Enqueue(nodeClone.Right);
                }
                if(nodeClone.Random != null)
                {
                    nodeClone.Random = nodeClone.Random.Random;
                }
            }
            return rootClone;
        }

        /// <summary>
        /// Represents a binary tree node with a random node pointer
        /// </summary>
        public class BTNodeWithRnd
        {
            public int Data { get; set; }
            public BTNodeWithRnd Left { get; set; }
            public BTNodeWithRnd Right { get; set; }
            public BTNodeWithRnd Random { get; set; }
            public BTNodeWithRnd(int data, BTNodeWithRnd left, BTNodeWithRnd right, BTNodeWithRnd rnd)
            {
                Data = data;
                Left = left;
                Right = right;
                Random = rnd;
            }

            public BTNodeWithRnd(BTNodeWithRnd nodeToCopy)
            {
                Data = nodeToCopy.Data;
                Left = nodeToCopy.Left;
                Right = nodeToCopy.Right;
                Random = nodeToCopy.Random;
            }

            public BTNodeWithRnd(int data)
            {
                Data = data;
            }

            public override string ToString()
            {
                return Data.ToString();
            }
        }

        public static void TestCloneABTWithRandPointer()
        {
            Dictionary<int, BTNodeWithRnd> dict = new Dictionary<int, BTNodeWithRnd>();
            for(int i=0; i<6; i++)
            {
                dict[i] = new BTNodeWithRnd(i);
            }
            dict[0].Left = dict[1];
            dict[0].Right = dict[2];
            dict[1].Left = dict[3];
            dict[1].Right = dict[4];
            dict[2].Right = dict[5];

            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                dict[i].Random = dict[rnd.Next(0,6)];
            }
            Console.WriteLine("The orginal tree looks like:");
            PrintTree(dict[0], 'O');

            CloneABTWithRandPointer clone = new CloneABTWithRandPointer();

            Console.WriteLine("The cloned tree looks like:");
            PrintTree(clone.GetClone(dict[0]), 'C');
        }

        public static void PrintTree(BTNodeWithRnd root, char position)
        {
            if(root!= null)
            {
                Console.WriteLine("{0} : {1}, rnd{2}", position, root.Data, ((root.Random!=null)?root.Random.ToString() :"null"));
                PrintTree(root.Left, 'L');
                PrintTree(root.Right, 'R');
            }
        }

    }
}
