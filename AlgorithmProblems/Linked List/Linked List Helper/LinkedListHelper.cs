﻿using AlgorithmProblems.PermutationAndCombination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List.Linked_List_Helper
{
    class LinkedListHelper
    {
        public static SingleLinkedListNode<int> CreateSinglyLinkedList(int length)
        {
            // Error checking
            if (length <= 0)
            {
                return null;
            }
            SingleLinkedListNode<int> head = null;
            SingleLinkedListNode<int> currentNode = null;
            Random rnd = new Random();
            while(length>0)
            {
                if(head == null)
                {
                    head = new SingleLinkedListNode<int>(rnd.Next(0, 9));
                    currentNode = head;
                }
                else
                {
                    currentNode.NextNode = new SingleLinkedListNode<int>(rnd.Next(0, 9));
                    currentNode = currentNode.NextNode;
                }
                
                length--;
            }

            return head;
        }

        public static void PrintSinglyLinkedList(SingleLinkedListNode<int> head)
        {
            while(head != null)
            {
                Console.Write(head.Data + " -> ");
                head = head.NextNode;
            }
            Console.Write("null");
            Console.WriteLine();
        }

        public static SingleLinkedListNode<int> GetRandomNode(SingleLinkedListNode<int> head, int linkedListLength)
        {
            Random rnd = new Random();
            int index = rnd.Next(0, linkedListLength - 1);
            SingleLinkedListNode<int> randomNode = head;
            while(index != 0)
            {
                index--;
                randomNode = randomNode.NextNode;
            }
            return randomNode;
        }

        #region LinkedListWithRandomPointer
        public static LinkedListNodeWithRandomPointer<int> CreateSinglyLinkedListWithRandomPointer(int length)
        {
            Dictionary<int, LinkedListNodeWithRandomPointer<int>> dict = new Dictionary<int, LinkedListNodeWithRandomPointer<int>>();

            // Error checking
            if (length <= 0)
            {
                return null;
            }
            LinkedListNodeWithRandomPointer<int> head = null;
            LinkedListNodeWithRandomPointer<int> currentNode = null;
            Random rnd = new Random();
            while (length > 0)
            {
                if (head == null)
                {
                    head = new LinkedListNodeWithRandomPointer<int>(rnd.Next(0, 9));
                    currentNode = head;
                }
                else
                {
                    currentNode.NextNode = new LinkedListNodeWithRandomPointer<int>(rnd.Next(0, 9));
                    currentNode = currentNode.NextNode;
                }
                dict[length] = currentNode;
                length--;
            }

            // Randomize the keys
            List<int> listOfRandomDictionaryKeys = ShuffleAList.FisherYatesAlgo(dict.Keys.ToList<int>());

            // Now populate the Random node for each node in the linked list
            currentNode = head;
            int dictIndex = 0;

            while (currentNode != null)
            {
                currentNode.RandomNode = dict[listOfRandomDictionaryKeys[dictIndex]];
                dictIndex++;
                currentNode = currentNode.NextNode;
            }

            return head;
        }

        public static void PrintLinkedListWithRandomPointer(LinkedListNodeWithRandomPointer<int> head)
        {
            while (head != null)
            {
                Console.Write(head.Data + " |^ " + head.RandomNode.Data + " -> ");
                head = (LinkedListNodeWithRandomPointer<int>)head.NextNode;
            }
            Console.Write("null");
            Console.WriteLine();
        }
        #endregion
    }
}
