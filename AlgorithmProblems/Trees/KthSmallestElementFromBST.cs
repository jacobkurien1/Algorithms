using AlgorithmProblems.Trees.TreeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Trees
{
    public class KthSmallestElementFromBST
    {
        public BinarySearchTreeNode<int> KthSmallestElement { get; set; }
        public int GetKthSmallestElement(BinarySearchTreeNode<int> node, int k, int currentOrder)
        {
            if(k == 0)
            {
                // Error condition: K should start from 1
                return -1;
            }
            if(node !=null)
            {
                if (node.Left != null)
                {
                    currentOrder = GetKthSmallestElement(node.Left, k, currentOrder);
                }
                if(++currentOrder == k)
                {
                    //We have found the kth element
                    KthSmallestElement = node;
                }
                if(node.Right != null)
                {
                    currentOrder = GetKthSmallestElement(node.Right, k, currentOrder);
                }
            }
            return currentOrder;

        }

        public static void TestKthSmallestElementFromBST()
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>();
            bst.Insert(6);
            bst.Insert(3);
            bst.Insert(2);
            bst.Insert(1);
            bst.Insert(0);
            bst.Insert(8);
            bst.Insert(7);
            bst.Insert(9);
            WalkTheTree.InOrderTraversal(bst.Head);
            Console.WriteLine();

            KthSmallestElementFromBST kObj = new KthSmallestElementFromBST();
            kObj.GetKthSmallestElement(bst.Head, 5, 0);
            Console.WriteLine("The {0}th element in the bst is {1}", 5, kObj.KthSmallestElement.Data);

            kObj = new KthSmallestElementFromBST();
            kObj.GetKthSmallestElement(bst.Head, 10, 0);
            Console.WriteLine("The {0}th element in the bst is {1}", 10, (kObj.KthSmallestElement==null) ? -1 : kObj.KthSmallestElement.Data);

            kObj = new KthSmallestElementFromBST();
            kObj.GetKthSmallestElement(bst.Head, 0, 0);
            Console.WriteLine("The {0}th element in the bst is {1}", 0, (kObj.KthSmallestElement == null) ? -1 : kObj.KthSmallestElement.Data);

            kObj = new KthSmallestElementFromBST();
            kObj.GetKthSmallestElement(bst.Head, 7, 0);
            Console.WriteLine("The {0}th element in the bst is {1}", 7, (kObj.KthSmallestElement == null) ? -1 : kObj.KthSmallestElement.Data);

            kObj = new KthSmallestElementFromBST();
            kObj.GetKthSmallestElement(bst.Head, 1, 0);
            Console.WriteLine("The {0}th element in the bst is {1}", 1, (kObj.KthSmallestElement == null) ? -1 : kObj.KthSmallestElement.Data);
        }
    }
}
