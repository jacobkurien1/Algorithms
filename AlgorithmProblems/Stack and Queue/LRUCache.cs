using AlgorithmProblems.Linked_List.Linked_List_Helper;
using AlgorithmProblems.Stack_and_Queue.Queue_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Stack_and_Queue
{
    /// <summary>
    /// Least Recently used cache
    /// We need to implement such that the Get and Put operation is done in O(1)
    /// We can do the O(1) operation by storing whatever is in the cache in a dictionary<key, node of linked list(data of linked list is the value)>
    /// The LRU operation can be achieved using a Queue created by doubly linked list
    /// </summary>
    class LRUCache
    {
        private int Capacity { get; set; }
        public Dictionary<int, DoublyLinkedListNode<KeyValuePair<int, int>>> CacheDictionary { get; set; }
        public QueueViaDoublyLinkedList<KeyValuePair<int,int>> QueueForLRU { get; set; }
        public LRUCache(int cacheCapacity)
        {
            Capacity = cacheCapacity;
            CacheDictionary = new Dictionary<int, DoublyLinkedListNode<KeyValuePair<int, int>>>();
            QueueForLRU = new QueueViaDoublyLinkedList<KeyValuePair<int, int>>();
        }

        public int? Get(int key)
        {
            int? retValue = null;
            DoublyLinkedListNode<KeyValuePair<int, int>> currentNode = (CacheDictionary.ContainsKey(key))?CacheDictionary[key]:null;
            // Get the return value from the CacheDictionary
            retValue = (currentNode != null) ? (int?)currentNode.Data.Value : null;
            // Move enqueue the value to the queue (and delete it if it is present in the queue)
            if(currentNode!=null)
            {
                // delete the node from the queue
                QueueForLRU.Delete(currentNode);

                // Put the node in the Last of Queue
                QueueForLRU.Enqueue(currentNode);

                LimitQueueToCacheCapacity();
            }
            else    // the page requested is not present in the cache
            {
                // We need to add the page from memory to the cache
                retValue = MimicGettingPageFromMemory(key);
                if (retValue != null)
                {
                    Put(key, (int)retValue);
                }
                else
                {
                    // the page number is not present in cache or memory
                    return null;
                }

            }
            return retValue;
        }

        /// <summary>
        /// Put a page number(key) and the page content/or reference to content as value in the cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Put(int key, int value)
        {
            // Algo: if the key is present in dictionary, do nothing
            // if the key is not present in the dictionary, add to dictionary and also enqueue in queue
            //      while enqueue, make sure that the capacity is not reached, else dequeue the LRU element
            if(!CacheDictionary.ContainsKey(key))
            {
                DoublyLinkedListNode<KeyValuePair<int, int>> nodeToAdd = new DoublyLinkedListNode<KeyValuePair<int, int>>(new KeyValuePair<int, int>(key, value));
                
                // Add to cacheDictionary
                CacheDictionary[key] = nodeToAdd;

                // Put the node in the Last of Queue
                QueueForLRU.Enqueue(nodeToAdd);

                LimitQueueToCacheCapacity();
            }
        }

        private void LimitQueueToCacheCapacity()
        {
            while (Capacity < QueueForLRU.Capacity)
            {
                DoublyLinkedListNode<KeyValuePair<int, int>> dequeueNode = QueueForLRU.Dequeue();
                CacheDictionary.Remove(dequeueNode.Data.Key);
            }
        }

        #region TestMethods and helpers
        /// <summary>
        /// The dictionary that mimics memory
        /// the key is the page number
        /// the value is the content/reference to content of that page
        /// </summary>
        private static Dictionary<int,int> memoryDict;
        /// <summary>
        /// This method mimics the process of getting page from a memory
        /// </summary>
        /// <param name="key">the key represents the page number which uniquely identifies a page in the memory</param>
        /// <returns>the content or reference to the content on the page</returns>
        private static int? MimicGettingPageFromMemory(int key)
        {
            if(memoryDict == null)
            {
                PopulateMemoryDictionary();
            }
            if(memoryDict.ContainsKey(key))
            {
                return memoryDict[key];
            }
            else
            {
                return null;    // page number not present in memory
            }

        }

        private static void PopulateMemoryDictionary()
        {
            memoryDict = new Dictionary<int, int>();
            for (int index = 0; index < 40; index++)
            {
                memoryDict[index] = index + 2;
            }
        }

        public static void TestLRUCache()
        {
            Console.WriteLine("Test the LRU cache");
            LRUCache cache = new LRUCache(3);
            Console.WriteLine("Get the pagenumber =2: " + cache.Get(2));
            Console.WriteLine("Get the pagenumber =3: " + cache.Get(3));
            Console.WriteLine("Get the pagenumber =4: " + cache.Get(4));
            cache.QueueForLRU.PrintQueue();
            Console.WriteLine("Get the pagenumber =1: " + cache.Get(1));
            cache.QueueForLRU.PrintQueue();
            Console.WriteLine("Get the pagenumber =4: " + cache.Get(4));
            cache.QueueForLRU.PrintQueue();
            Console.WriteLine("Get the pagenumber =4: " + cache.Get(4));
            cache.QueueForLRU.PrintQueue();
            Console.WriteLine("Get the pagenumber =3: " + cache.Get(3));
            cache.QueueForLRU.PrintQueue();
            Console.WriteLine("Get the pagenumber =50: " + cache.Get(50));
            cache.QueueForLRU.PrintQueue();
            Console.WriteLine("Get the pagenumber =32: " + cache.Get(32));
            cache.QueueForLRU.PrintQueue();

        }
        #endregion
    }
}
