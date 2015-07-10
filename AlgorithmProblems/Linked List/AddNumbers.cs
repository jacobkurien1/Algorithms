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

            while( num1 != null && num2 != null)
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
                num1 = num1.NextNode;
                num2 = num2.NextNode;
            }

            if(carry==1)
            {
                currentSum.NextNode = new SingleLinkedListNode<int>(carry);
            }

            return head;
        }

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
            sumNode.NextNode = AddNumbersRepresentedByLinkedListAlgo2(num1.NextNode, num2.NextNode, carry);
            return sumNode;
        }

        public static void TestAddNumbersRepresentedByLinkedList()
        {
            AddNumbers adNums = new AddNumbers();

            SingleLinkedListNode<int> ll1 = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(ll1);

            SingleLinkedListNode<int> ll2 = LinkedListHelper.CreateSinglyLinkedList(10);
            LinkedListHelper.PrintSinglyLinkedList(ll2);

            Console.WriteLine("The sum with algo 1 is as follows:");
            SingleLinkedListNode<int> sum = adNums.AddNumbersRepresentedByLinkedListAlgo1(ll1, ll2);
            LinkedListHelper.PrintSinglyLinkedList(sum);

            Console.WriteLine("The sum with algo 2 is as follows:");
            sum = adNums.AddNumbersRepresentedByLinkedListAlgo2(ll1, ll2, 0);
            LinkedListHelper.PrintSinglyLinkedList(sum);

        }
    }
}
