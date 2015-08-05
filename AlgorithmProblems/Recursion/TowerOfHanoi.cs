using AlgorithmProblems.Linked_List.Linked_List_Helper;
using AlgorithmProblems.Stack_and_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Recursion
{
    class TowerOfHanoi
    {
        StackViaLinkedList<int> towerA; // original sorurce tower
        StackViaLinkedList<int> towerB; // original intermediate tower
        StackViaLinkedList<int> towerC; // original destination tower
        int numberOfPanksOnTower = 0;

        public TowerOfHanoi(int numOfPlanks)
        {
            numberOfPanksOnTower = numOfPlanks;
            towerA = new StackViaLinkedList<int>();
            for (int i = numberOfPanksOnTower; i >= 1; i--)
            {
                towerA.Push(i);
            }
            towerB = new StackViaLinkedList<int>();
            towerC = new StackViaLinkedList<int>();
        }

        // What you get at each level of the recursion tree is a power of 2. Hence, the sum is: 2^0 + 2^1 + 2^2 + ... + 2^{n-1}
        // Let O(n) = 1 + 2 + 4 + ... + 2^{n-1}. Then: O(n) - 2*O(n) = 1 - 2^n
        // O(n) = 2^n - 1
        public void SolveTowerOfHanoi(int n, StackViaLinkedList<int> source, StackViaLinkedList<int> intermediate, StackViaLinkedList<int> destination)
        {
            if(n>0)
            {
                SolveTowerOfHanoi(n - 1, source, destination, intermediate);
                Move(source, destination);
                SolveTowerOfHanoi(n - 1, intermediate, source, destination);
            }
        }

        public void Move(StackViaLinkedList<int> source, StackViaLinkedList<int> destination)
        {
            destination.Push(source.Pop());
        }

        public static void TestTowerOfHanoi()
        {
            TowerOfHanoi th = new TowerOfHanoi(20);

            Console.WriteLine("The towers are as shown below:");
            LinkedListHelper.PrintSinglyLinkedList(th.towerA.Peek());
            LinkedListHelper.PrintSinglyLinkedList(th.towerB.Peek());
            LinkedListHelper.PrintSinglyLinkedList(th.towerC.Peek());

            th.SolveTowerOfHanoi(20, th.towerA, th.towerB, th.towerC);

            Console.WriteLine("The towers after solving are as shown below:");
            LinkedListHelper.PrintSinglyLinkedList(th.towerA.Peek());
            LinkedListHelper.PrintSinglyLinkedList(th.towerB.Peek());
            LinkedListHelper.PrintSinglyLinkedList(th.towerC.Peek());
        }
    }
}
