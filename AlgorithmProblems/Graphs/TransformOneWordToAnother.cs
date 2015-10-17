using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Graphs
{
    /// <summary>
    /// Given 2 words of equal length, we need to transform one word to another
    /// We can only change one letter at a time
    /// and the new word formed after changing one letter should be present in the dictionary
    /// 
    /// Eg Damp, Like
    /// damp, lamp, limp, lime, like
    /// </summary>
    class TransformOneWordToAnother
    {
        private Dictionary<string, bool> DictionaryOfValidWords;

        public TransformOneWordToAnother()
        {
            DictionaryOfValidWords = new Dictionary<string, bool>();
            DictionaryOfValidWords.Add("jump", true);
            DictionaryOfValidWords.Add("pump", true);
            DictionaryOfValidWords.Add("lump", true);
            DictionaryOfValidWords.Add("lass", true);
            DictionaryOfValidWords.Add("damp", true);
            DictionaryOfValidWords.Add("limp", true);
            DictionaryOfValidWords.Add("lane", true);
            DictionaryOfValidWords.Add("lamp", true);
            DictionaryOfValidWords.Add("lime", true);
            DictionaryOfValidWords.Add("late", true);
            DictionaryOfValidWords.Add("like", true);
        }

        /// <summary>
        /// Gets all the valid words present in the DictionaryOfValidWords by replacing one character from the input string str
        /// </summary>
        /// <param name="str">input string</param>
        /// <returns>List of all words that are present in the DictionaryOfValidWords by replacing one charater from str</returns>
        private List<string> GetAllValidWords(string str)
        {
            List<string> allValidWords = new List<string>();
            for(int i=0; i< str.Length; i++)
            {
                for(char charVal = 'a'; charVal<='z'; charVal++)
                {
                    string newStrVal = str.Substring(0, i) + charVal.ToString() + (i + 1 == str.Length ? "" : str.Substring(i + 1, str.Length - i - 1));
                    if(DictionaryOfValidWords.ContainsKey(newStrVal))
                    {
                        allValidWords.Add(newStrVal);
                    }
                }
            }
            return allValidWords;
        }

        /// <summary>
        /// We are going to do the BFS traversal here to get the transformation from the start to the end str.
        /// We need to do the BFS as we need to find the minimum number of transformations from start to end str.
        /// </summary>
        /// <param name="startStr">start string</param>
        /// <param name="endStr">end string</param>
        /// <returns>the transformations that are needed to change the startstr to endstr</returns>
        public List<string> GetTransformation(string startStr, string endStr)
        {
            if(startStr == null || startStr == "" || endStr == null || endStr == "" || startStr.Length != endStr.Length)
            {
                return null;
            }
            Queue<string> queueForBFS = new Queue<string>();
            queueForBFS.Enqueue(startStr);
            Dictionary<string, bool> visitedNodes = new Dictionary<string, bool>();
            visitedNodes.Add(startStr, true);
            Dictionary<string, string> backtrackDict = new Dictionary<string, string>();

            while(queueForBFS.Count>0)
            {
                string currentStr = queueForBFS.Dequeue();
                foreach(string validNeighbour in GetAllValidWords(currentStr))
                {
                    if(!visitedNodes.ContainsKey(validNeighbour))
                    {
                        visitedNodes[validNeighbour] = true;
                        queueForBFS.Enqueue(validNeighbour);
                        backtrackDict[validNeighbour] = currentStr;
                        if(validNeighbour == endStr)
                        {
                            // we need to get the path that led us here and return that path
                            List<string> path = new List<string>();
                            string backTrackPath = endStr;
                            while (backtrackDict.ContainsKey(backTrackPath))
                            {
                                path.Add(backTrackPath);
                                backTrackPath = backtrackDict[backTrackPath];
                            }
                            path.Add(startStr);
                            path.Reverse();
                            return path;
                        }
                        
                    }
                }
            }

            // We were not able to find a valid path from startStr to endStr
            return null;
        }

        public static void TestGetTransformation()
        {
            TransformOneWordToAnother tf = new TransformOneWordToAnother();
            List<string> transformationPath = tf.GetTransformation("damp", "like");
            Console.WriteLine("The transformation path is as follows");
            foreach(string pathVal in transformationPath)
            {
                Console.Write(pathVal + " ");
            }
            Console.WriteLine();
        }
    }
}
