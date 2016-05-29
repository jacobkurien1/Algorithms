using AlgorithmProblems.Arrays.ArraysHelper;
using AlgorithmProblems.Heaps.HeapHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Greedy_Algorithm
{
    public class HuffmanCoding
    {
        private Dictionary<string, HuffmanNode> HuffmanDataDict { get; set; }
        private Dictionary<string, string> HuffmanCodes { get; set; }
        private MinHeap<HuffmanNode> MinHeapForNodes { get; set; }
        private string Text { get; set; }
        public HuffmanCoding(string text)
        {
            if(string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("text is empty");
            }
            Text = text;
            HuffmanDataDict = new Dictionary<string, HuffmanNode>();
            HuffmanCodes = new Dictionary<string, string>();
            for (int i=0; i< Text.Length; i++)
            {
                if(!HuffmanDataDict.ContainsKey(Text[i].ToString()))
                {
                    HuffmanDataDict[Text[i].ToString()] = new HuffmanNode(Text[i].ToString(), 1);
                }
                else
                {
                    ++HuffmanDataDict[Text[i].ToString()].Frequency;
                }
            }
            MinHeapForNodes = new MinHeap<HuffmanNode>(HuffmanDataDict.Count);

            foreach (KeyValuePair<string, HuffmanNode> dictVal in HuffmanDataDict)
            {
                MinHeapForNodes.Insert(dictVal.Value);
            }
            
            // Create the huffman tree
            while(MinHeapForNodes.HeapSize > 1)
            {
                HuffmanNode node1 = MinHeapForNodes.ExtractMin();
                HuffmanNode node2 = MinHeapForNodes.ExtractMin();
                HuffmanNode combinedNode = new HuffmanNode(node1.Data + node2.Data, node1.Frequency + node2.Frequency);
                combinedNode.Left = node1;
                combinedNode.Right = node2;
                MinHeapForNodes.Insert(combinedNode);
            }

            // Create the huffman codes for all text
            // We can use a postorder travesal subroutine to achieve this
            StringBuilder sb = new StringBuilder();
            Stack<HuffmanNode> st = new Stack<HuffmanNode>();
            HuffmanNode currentNode = MinHeapForNodes.ExtractMin();
            do
            {
                if (currentNode != null)
                {
                    if (currentNode.Right != null)
                    {
                        st.Push(currentNode.Right);
                    }
                    st.Push(currentNode);
                    currentNode = currentNode.Left;
                    // Since we are going left we need to append 0
                    sb.Append('0');
                }
                else
                {
                    sb.Remove(sb.Length - 1, 1);
                    currentNode = st.Pop();
                    if (st.Count > 0 && currentNode.Right == st.Peek())
                    {
                        HuffmanNode temp = st.Pop();
                        st.Push(currentNode);
                        // This suggest we will go right in the next iterations hence append 1
                        sb.Append('1');
                        currentNode = temp;
                    }
                    else
                    {
                        // In the normal postorder subroutine,
                        // we process the currentNode here
                        // We check whether the currentNode is leaf Node
                        // if yes then add the values in the HuffmanCodes 
                        if (currentNode.Left == null && currentNode.Right == null)
                        {
                            HuffmanCodes[currentNode.Data] = sb.ToString();
                        }
                        currentNode = null;
                    }
                }
            } while (st.Count > 0);
            
        }

        public string GetEncodedString()
        {
            StringBuilder sb = new StringBuilder();

            for(int i=0; i<Text.Length; i++)
            {
                sb.Append(HuffmanCodes[Text[i].ToString()]);
            }
            return sb.ToString();
        }
        private string ConvertStackToString(Stack<string> stCode)
        {
            StringBuilder sb = new StringBuilder();
            string[] allElementInStack = stCode.ToArray<string>();
            for(int i = allElementInStack.Length-1; i >=0; i--)
            {
                sb.Append(allElementInStack[i]);
            }
            return sb.ToString();
        }
        public static void TestHuffmanCoding()
        {
            string toEncode = "ccccbba";
            HuffmanCoding hc = new HuffmanCoding(toEncode);
            Console.WriteLine("Actual: the huffman encoded string for {0} is {1}", toEncode, hc.GetEncodedString());
            Console.WriteLine("Expected: the huffman encoded string for {0} is {1}", toEncode, "1111010100");

            toEncode = "this is an example for huffman encoding";
            hc = new HuffmanCoding(toEncode);
            Console.WriteLine("Actual: the huffman encoded string for {0} is {1}", toEncode, hc.GetEncodedString());
        }
    }

    /// <summary>
    /// Represents the individual data node for Huffman algo
    /// </summary>
    public class HuffmanNode : IComparable
    {
        public string Data { get; set; }
        public int Frequency { get; set; }
        public HuffmanNode Left { get; set; }
        public HuffmanNode Right { get; set; }
        public HuffmanNode(string data, int frequency)
        {
            Data = data;
            Frequency = frequency;
        }

        public int CompareTo(object obj)
        {
            return Frequency.CompareTo(((HuffmanNode)obj).Frequency);
        }
    }
}
