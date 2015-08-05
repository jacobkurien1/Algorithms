using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Linked_List.Linked_List_Helper
{
    class LinkedListNodeWithRandomPointer<T>
    {
        public LinkedListNodeWithRandomPointer<T> RandomNode { get; set; }
        public LinkedListNodeWithRandomPointer<T> NextNode { get; set; }
        public T Data { get; set; }

        public LinkedListNodeWithRandomPointer(T data)
        {
            Data = data;
        }
    }
}
