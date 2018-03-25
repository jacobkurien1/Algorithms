using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List
{
    class MergeSortedLinkedList
    {
        /// <summary>
        /// Merge two sorted linked list w/o using extra space in such a way that the final linked list is sorted
        /// </summary>
        /// <param name="list1">linked list to be merged</param>
        /// <param name="list2">linked list to be merged</param>
        /// <returns>Merged and sorted linked list</returns>
        private static SingleLinkedListNode<int> Merge(SingleLinkedListNode<int> list1, SingleLinkedListNode<int> list2)
        {
            SingleLinkedListNode<int> mergedListHead = null;
            SingleLinkedListNode<int> mergedListCurrent = null;
            while(list1 != null && list2 != null)
            {
                if(list1.Data< list2.Data)
                {
                    AddToMergedList(ref mergedListHead, ref mergedListCurrent, ref list1);
                }
                else
                {
                    AddToMergedList(ref mergedListHead, ref mergedListCurrent, ref list2);
                }
            }

            // Add all list1 items if any of them are left
            if(list1 != null)
            {
                AddToMergedList(ref mergedListHead, ref mergedListCurrent, ref list1);
            }

            // Add all the list2 items if any of them are left
            if(list2 !=null)
            {
                AddToMergedList(ref mergedListHead, ref mergedListCurrent, ref list2);
            }

            return mergedListHead;
        }

        private static void AddToMergedList(ref SingleLinkedListNode<int> mergedListHead, ref SingleLinkedListNode<int> mergedListCurrent, ref SingleLinkedListNode<int> list)
        {
            if (mergedListHead == null)
            {
                mergedListHead = list;
                mergedListCurrent = mergedListHead;
            }
            else
            {
                mergedListCurrent.NextNode = list;
                mergedListCurrent = mergedListCurrent.NextNode;
            }
            list = list.NextNode;
        }

        public static void TestMerge()
        {
            Console.WriteLine("Test merging of 2 sorted linked list");
            SingleLinkedListNode<int> list1 = LinkedListHelper.SortedLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(list1);

            SingleLinkedListNode<int> list2 = LinkedListHelper.SortedLinkedList(10, 5);
            LinkedListHelper.PrintSinglyLinkedList(list2);

            Console.WriteLine("The merged linked list is as follows:");
            LinkedListHelper.PrintSinglyLinkedList(Merge(list1, list2));

            //Test2
            Console.WriteLine("Test2:Test merging of 2 sorted linked list");
            list1 = LinkedListHelper.SortedLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(list1);

            list2 = LinkedListHelper.SortedLinkedList(1, 5);
            LinkedListHelper.PrintSinglyLinkedList(list2);

            Console.WriteLine("The merged linked list is as follows:");
            LinkedListHelper.PrintSinglyLinkedList(Merge(list1, list2));
        }
    }
}
