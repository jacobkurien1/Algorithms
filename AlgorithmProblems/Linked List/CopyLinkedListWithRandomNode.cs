using AlgorithmProblems.Linked_List.Linked_List_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List
{
    class CopyLinkedListWithRandomNode
    {
        public static LinkedListNodeWithRandomPointer<int> GetCopiedLinkedListWithRandomNode(LinkedListNodeWithRandomPointer<int> linkedListWithRndNode)
        {
            LinkedListNodeWithRandomPointer<int> currentNode = linkedListWithRndNode;
            LinkedListNodeWithRandomPointer<int> copiedLinkedListHead = null;
            
            // insert the copied nodes in between the original linked list node chain
            while(currentNode != null)
            {
                LinkedListNodeWithRandomPointer<int> copiedNode = new LinkedListNodeWithRandomPointer<int>(currentNode.Data);
                copiedNode.NextNode = currentNode.NextNode;
                if (copiedLinkedListHead == null)
                {
                    copiedLinkedListHead = copiedNode;
                }
                currentNode.NextNode = copiedNode;
                currentNode = copiedNode.NextNode;
            }

            currentNode = linkedListWithRndNode;
            while(currentNode!=null)
            {
                LinkedListNodeWithRandomPointer<int> copiedNode = currentNode.NextNode;
                copiedNode.RandomNode = currentNode.RandomNode;
                currentNode = copiedNode.NextNode;
                copiedNode.NextNode = (copiedNode.NextNode != null) ? copiedNode.NextNode.NextNode : null;
            }

            return copiedLinkedListHead;
        }

        public static void TestGetCopiedLinkedListWithRandomNode()
        {
            LinkedListNodeWithRandomPointer<int> linkedListhead = LinkedListHelper.CreateSinglyLinkedListWithRandomPointer(10);
            Console.WriteLine("The linked list is as shown below:");
            LinkedListHelper.PrintLinkedListWithRandomPointer(linkedListhead);

            LinkedListNodeWithRandomPointer<int> copiedlinkedListhead = GetCopiedLinkedListWithRandomNode(linkedListhead);
            Console.WriteLine("The copied linked list is as shown below:");
            LinkedListHelper.PrintLinkedListWithRandomPointer(copiedlinkedListhead);

        }
    }
}
