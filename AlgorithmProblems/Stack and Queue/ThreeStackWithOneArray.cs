using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue
{
    class ThreeStackWithOneArray
    {
        private int[] stackTopIndex = new int[3];
        private int indiStackSize;
        private int[] storage;

        public ThreeStackWithOneArray(int singleStackSize)
        {
            if(singleStackSize <= 0)
            {
                throw new Exception("capacity is invalid");
            }
            indiStackSize = singleStackSize;
            storage = new int[singleStackSize * 3];
            stackTopIndex[0] = 0;
            stackTopIndex[1] = singleStackSize;
            stackTopIndex[2] = 2 * singleStackSize;
        }

        public int Pop(int stackNum)
        {
            if(stackNum >=0 && stackNum < 3)
            {
                if(stackTopIndex[stackNum]> stackNum*indiStackSize)
                {
                    int indexAffected = --stackTopIndex[stackNum];
                    int retVal = storage[indexAffected];
                    storage[indexAffected] = 0;
                    return retVal;
                }
                else
                {
                    throw new Exception("stack is empty");
                }
            }
            else
            {
                throw new Exception("invalid stack number");
            }
        }

        public void Push(int stackNum, int data)
        {
            if (stackNum >= 0 && stackNum < 3)
            {
                if (stackTopIndex[stackNum] < ((stackNum+1) * indiStackSize))
                {
                    storage[stackTopIndex[stackNum]++] = data;
                }
                else
                {
                    throw new Exception("stack is full");
                }
            }
            else
            {
                throw new Exception("invalid stack number");
            }
        }

        #region Testing
        public static void TestThreeStackWithOneArray()
        {
            Console.WriteLine("test the three stack array");
            ThreeStackWithOneArray threeStack = new ThreeStackWithOneArray(3);

            try
            {
                threeStack.Push(1, 3);
                threeStack.Push(1, 3);
                threeStack.Push(1, 3);
                threeStack.Push(1, 3);

                threeStack.Push(1, 3);

                threeStack.Push(1, 3);
                threeStack.Push(1, 3);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            threeStack.PrintStorage();

            try
            {
                threeStack.Pop(1);
                threeStack.Pop(1);
                threeStack.Pop(1);
                threeStack.Pop(1);

                threeStack.Pop(1);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            threeStack.PrintStorage();
        }

        public void PrintStorage()
        {
            for (int i = 0; i<storage.Length; i++)
            {
                Console.Write(storage[i] + " ");
            }
            Console.WriteLine();
        }
        #endregion

    }
}
