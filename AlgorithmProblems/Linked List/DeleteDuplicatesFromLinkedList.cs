using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List
{
    class DeleteDuplicatesFromLinkedList
    {
        /// <summary>
        /// Use a Dictionary to decide if the item is a duplicate or not
        /// Running time = O(n)
        /// Running Space = O(n)
        /// </summary>
        /// <param name="head"> head reference to the linked list</param>
        /// <returns> the head reference to the linked list w/o the duplicates</returns>
        public SingleLinkedListNode<int> DeleteDuplicatesAlgo1(SingleLinkedListNode<int> head)
        {
            Dictionary<int, bool> dict = new Dictionary<int, bool>();
            SingleLinkedListNode<int> currentNode = head;
            dict[head.Data] = true;
            while (currentNode != null && currentNode.NextNode != null)
            {
                
                if (dict.ContainsKey(currentNode.NextNode.Data))
                {
                    currentNode.NextNode = currentNode.NextNode.NextNode;
                }
                else
                {
                    dict[currentNode.NextNode.Data] = true;
                }
                
                currentNode = currentNode.NextNode;
            }
            return head;
        }

        /// <summary>
        /// We can delete the Linked list duplicates with o(1) space but we will need O(n^2) time
        /// </summary>
        /// <param name="head"> head reference to the linked list</param>
        /// <returns> the head reference to the linked list w/o the duplicates</returns>
        public SingleLinkedListNode<int> DeleteDuplicatesAlgo2(SingleLinkedListNode<int> head)
        {
            SingleLinkedListNode<int> currentNode = head;

            while(currentNode != null)
            {
                bool isDuplicate = false;
                SingleLinkedListNode<int> iterateNode = head;
                if (currentNode.NextNode != null)
                {
                    while (currentNode.NextNode != iterateNode)
                    {
                        if (currentNode.NextNode.Data == iterateNode.Data)
                        {
                            isDuplicate = true;
                            break;
                        }
                        iterateNode = iterateNode.NextNode;
                    }
                }

                if(isDuplicate)
                {
                    currentNode.NextNode = currentNode.NextNode.NextNode;
                }
                currentNode = currentNode.NextNode;
            }

            return head;
        }

        public static void TestDeleteDuplicates()
        {
            Console.WriteLine("Delete all duplicates from a linked list");
            DeleteDuplicatesFromLinkedList dlt = new DeleteDuplicatesFromLinkedList();

            SingleLinkedListNode<int> ll = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(ll);
            dlt.DeleteDuplicatesAlgo1(ll);
            LinkedListHelper.PrintSinglyLinkedList(ll);

            ll = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(ll);
            dlt.DeleteDuplicatesAlgo2(ll);
            LinkedListHelper.PrintSinglyLinkedList(ll);
        }
    }
}
