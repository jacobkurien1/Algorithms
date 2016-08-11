using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List
{
    class AddNumbers
    {
        public SingleLinkedListNode<int> AddNumbersRepresentedByLinkedListAlgo1(SingleLinkedListNode<int> num1, SingleLinkedListNode<int> num2)
        {
            int num1Val, num2Val, carry = 0;
            SingleLinkedListNode<int> head = null;
            SingleLinkedListNode<int> currentSum = null;

            while( num1 != null || num2 != null)
            {
                num1Val = num1 != null ? num1.Data : 0;
                num2Val = num2 != null ? num2.Data : 0;

                int sum = num1Val + num2Val + carry;
                if(sum>=10)
                {
                    carry = 1;
                    sum = sum % 10;
                }
                else
                {
                    carry = 0;
                }

                if(head == null)
                {
                    head = new SingleLinkedListNode<int>(sum);
                    currentSum = head;
                }
                else
                {
                    currentSum.NextNode = new SingleLinkedListNode<int>(sum);
                    currentSum  = currentSum.NextNode;
                }
                if (num1 != null)
                {
                    num1 = num1.NextNode;
                }
                if (num2 != null)
                {
                    num2 = num2.NextNode;
                }
            }

            if(carry==1)
            {
                currentSum.NextNode = new SingleLinkedListNode<int>(carry);
            }

            return head;
        }

        #region Algo2: recursion
        public SingleLinkedListNode<int> AddNumbersRepresentedByLinkedListAlgo2(SingleLinkedListNode<int> num1, SingleLinkedListNode<int> num2, int carry)
        {
            // Recursion base condition
            if(num1 == null && num2 == null)
            {
                return null;
            }
            int num1Val = num1 != null ? num1.Data : 0;
            int num2Val = num2 != null ? num2.Data : 0;
            int sum = num1Val + num2Val + carry;
            if (sum >= 10)
            {
                carry = 1;
                sum = sum % 10;
            }
            else
            {
                carry = 0;
            }
            SingleLinkedListNode<int> sumNode = new SingleLinkedListNode<int>(sum);
            sumNode.NextNode = AddNumbersRepresentedByLinkedListAlgo2((num1!=null)?num1.NextNode:null, (num2!=null)?num2.NextNode:null, carry);
            return sumNode;
        }
        #endregion

        #region Algo3: naive approach
        /// <summary>
        /// Given 2 linked list, we should do a sum of them and return the result in a linkedlist
        /// ll1 = 1->5->3
        /// ll2 = 4->7->2
        /// 351+274 = 625
        /// so return 5->2->6
        /// </summary>
        /// <param name="ll1"></param>
        /// <param name="ll2"></param>
        /// <returns></returns>
        public SingleLinkedListNode<int> AddLinkedLists(SingleLinkedListNode<int> ll1, SingleLinkedListNode<int> ll2)
        {
            int num1 = GetNumFromLL(ll1);
            int num2 = GetNumFromLL(ll2);
            int sum = num1 + num2;
            return GetLLFromNum(sum);
        }
        /// <summary>
        /// Gets the number from the linked list
        /// For eg
        /// 2->5->1 must yield a num 152
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private int GetNumFromLL(SingleLinkedListNode<int> node)
        {
            int finalNum = 0;
            int multiplicationFactor = 1;
            while (node != null)
            {
                finalNum += (node.Data * multiplicationFactor);
                node = node.NextNode;
                multiplicationFactor *= 10;
            }
            return finalNum;
        }

        /// <summary>
        /// This will convert a number to a linked list
        /// for eg:
        /// num 152 will be converted to 2->5->1
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private SingleLinkedListNode<int> GetLLFromNum(int num)
        {
            if (num == 0)
            {
                return new SingleLinkedListNode<int>(0);
            }
            SingleLinkedListNode<int> linkedListHead = null;
            SingleLinkedListNode<int> linkedListCurrent = null;

            while (num > 0)
            {
                int currentLLData = num % 10;
                if (linkedListCurrent == null)
                {
                    // Case where we need to set the linkedlist head 
                    linkedListCurrent = new SingleLinkedListNode<int>(currentLLData);
                    linkedListHead = linkedListCurrent;
                }
                else
                {
                    // case where we have the linkedlist head and we need to add the new node at the current's next node
                    linkedListCurrent.NextNode = new SingleLinkedListNode<int>(currentLLData);
                    linkedListCurrent = linkedListCurrent.NextNode;
                }
                num /= 10;
            }
            return linkedListHead;
        }
        #endregion

        public static void TestAddNumbersRepresentedByLinkedList()
        {
            AddNumbers adNums = new AddNumbers();

            SingleLinkedListNode<int> ll1 = LinkedListHelper.CreateSinglyLinkedList(6);
            LinkedListHelper.PrintSinglyLinkedList(ll1);

            SingleLinkedListNode<int> ll2 = LinkedListHelper.CreateSinglyLinkedList(7);
            LinkedListHelper.PrintSinglyLinkedList(ll2);

            Console.WriteLine("The sum with algo 1 is as follows:");
            SingleLinkedListNode<int> sum = adNums.AddNumbersRepresentedByLinkedListAlgo1(ll1, ll2);
            LinkedListHelper.PrintSinglyLinkedList(sum);

            Console.WriteLine("The sum with algo 2 is as follows:");
            sum = adNums.AddNumbersRepresentedByLinkedListAlgo2(ll1, ll2, 0);
            LinkedListHelper.PrintSinglyLinkedList(sum);

            Console.WriteLine("The sum with algo 3 is as follows:");
            sum = adNums.AddLinkedLists(ll1, ll2);
            LinkedListHelper.PrintSinglyLinkedList(sum);

        }
    }
}
