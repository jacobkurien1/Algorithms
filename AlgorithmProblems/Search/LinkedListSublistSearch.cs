using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Search
{
    /// <summary>
    /// Find whether the linked list is a subset of another linked list.
    /// Eg 1: list1 = 1->2->3->5->6
    /// list2 = 3->5
    /// Ans: yes
    /// 
    /// Eg2: list1 = 1->2->3->5->6
    /// list2 = 7->8
    /// Ans: no
    /// </summary>
    public class LinkedListSublistSearch
    {
        #region Algo1

        /// <summary>
        /// We need to check whether list2 is present in list1.
        /// Naive Algo: travese list1 and keep checking for match with list2.
        /// 
        /// The running time is O(m*n) where m is the length of list1 and n is the length of list2
        /// The space requirement is O(1)
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns>true if list2 is a subset of list1</returns>
        public bool Search1(SingleLinkedListNode<int> list1, SingleLinkedListNode<int> list2)
        {
            if(list1 == null || list2 == null)
            {
                // Argument check
                throw new ArgumentNullException("the input linked list cannot be null");
            }

            SingleLinkedListNode<int> head2 = list2;
            while(list1 != null && list2 != null)
            {
                if(list1.Data == list2.Data)
                {
                    list1 = list1.NextNode;
                    list2 = list2.NextNode;
                    if(list2 == null)
                    {
                        // we have a match
                        return true;
                    }
                }
                else
                {
                    list2 = head2;
                    if(list2.Data != list1.Data)
                    {
                        list1 = list1.NextNode;
                    }
                }
            }
            return false; // no match
        }

        #endregion

        #region Algo2
        /// <summary>
        /// if extra space is permitted, we can convert list1 and list2 to string
        /// and then use KMP to figure out whether list2Str is a substring of list1Str.
        /// 
        /// Running time is O(m+n)
        /// Space is O(max(m,n))
        /// </summary>
        #endregion

        #region TestArea
        public static void TestLinkedListSublistSearch()
        {
            LinkedListSublistSearch llss = new LinkedListSublistSearch();
            SingleLinkedListNode<int> list1 = LinkedListHelper.CreateSinglyLinkedList(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            SingleLinkedListNode<int> list2 = LinkedListHelper.CreateSinglyLinkedList(new int[] { 4, 5, 6, 7 });
            SingleLinkedListNode<int> list3 = LinkedListHelper.CreateSinglyLinkedList(new int[] { 4, 5, 6, 8 });

            Console.WriteLine("The list2 is present in list1. Actual: {0} Expected:{1}", llss.Search1(list1, list2), true);
            Console.WriteLine("The list3 is present in list1. Actual: {0} Expected:{1}", llss.Search1(list1, list3), false);
            Console.WriteLine("The list1 is present in list2. Actual: {0} Expected:{1}", llss.Search1(list2, list1), false);
        }
        #endregion
    }
}
