using AlgorithmProblems.Arrays;
using AlgorithmProblems.Dynamic_Programming;
using AlgorithmProblems.Linked_List;
using AlgorithmProblems.matrix_problems;
using AlgorithmProblems.Misc;
using AlgorithmProblems.PermutationAndCombination;
using AlgorithmProblems.Recursion;
using AlgorithmProblems.Stack_and_Queue;
using AlgorithmProblems.StringProblems;
using AlgorithmProblems.Trees;
using AlgorithmProblems.Trees.TreeHelper;
using AlgorithmProblems.Trie;
using AlgorithmProblems.Trie.TrieHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems
{
    class Program
    {
        static void Main(string[] args)
        {
            // StringProblems
            //Test calls for Reverse string
            string input = "jacob";
            Console.WriteLine(input + "<=>" +new string(SimpleProblem.ReverseString(input.ToCharArray())));
            input = "jake";
            Console.WriteLine(input + "<=>" + new string(SimpleProblem.ReverseString(input.ToCharArray())));
            input = "";
            Console.WriteLine(input + "<=>" + new string(SimpleProblem.ReverseString(input.ToCharArray())));
            input = "jdshjdh@#$%^&)";
            Console.WriteLine(input + "<=>" + new string(SimpleProblem.ReverseString(input.ToCharArray())));

            ReplaceSpaces.TestReplaceSpacesInplace();
            Anagrams.TestIsAnagramAlgo();
            StringRotation.TestIsThisRotatedString();
            RemoveDuplicates.TestRemoveDuplicatesFromString();
            StringToLongConverter.TestStringToLong();
            PatternMatching.TestPatternMatching();
            RegexMatching.TestMatch();

            //Console.ReadLine();

            //Array Problems
            ArrayOfNumsIncrement.TestIncrementArrayOfNumbers();
            MajorityElement.TestFindMajorityElement();


            // Matrix Problem
            Rotate_Matrix_90_degree.TestRotateMatrix();
            Matrix_Column_Rows_0.TestMakeRowColZero1();
            Matrix_Column_Rows_0.TestMakeRowColZero2();
            RotateMatrix180.TestRotateMatrix180();
            SumOfMatrixElementsFormedByRectangleWithCoordinates.TestSumOfMatrixElements();

            // Linked list Problems
            DeleteLinkedListNode.TestDeleteFirstNode();
            DeleteDuplicatesFromLinkedList.TestDeleteDuplicates();
            NthLastElementOfLinkedList.TestNthLastNodeOfLinkedList();
            DeleteNodeWithDirectReference.TestDeleteNode();
            AddNumbers.TestAddNumbersRepresentedByLinkedList();
            CopyLinkedListWithRandomNode.TestGetCopiedLinkedListWithRandomNode();
            CommonElementInTwoLinkedList.TestCommonElement();
            ReverseAdjacentNodesInLinkedList.TestReverseAdjacentNodes();
            MergeSortedLinkedList.TestMerge();
            CircularLinkedList.TestStartOfCircularLinkedList();
            MedianForCircularLinkedList.TestGetMedian();

            // stack and queue problem
            ThreeStackWithOneArray.TestThreeStackWithOneArray();
            StackWithMinElement.TestStackWithMinElement();
            StackOfPlates.TestStackOfPlates();
            SortAStack.TestSortAStackAscending();
            QueueVia2Stack.TestQueueVia2Stack();
            LRUCache.TestLRUCache();

            //Recursion Problem
            TowerOfHanoi.TestTowerOfHanoi();
            Sudoku.TestSudokuSolver();
            MaxSumOfConsecutiveNums.TestMaxSumOfConsecutiveNums();

            //Misc Problem
            MinNumOfCoins.TestMinNumOfCoins();
            IsPrime.TestCheckPrime();
            SquareRoot.TestCalculateSquareRoot();
            CreditCardCheck.TestLuhnAlgo();
            ExcelFirstRowConversion.TestCovertExcelColumnToLong();
            Skyline.TestSkyline();
            SumOfSquaresWithoutMultiplication.TestSumOfSquares();

            // Permutation and Combination problem
            ShuffleAList.TestFisherYatesAlgo();

            // Tree Problems
            TreeFromExpression.TestCreateTreeFromExpression();
            TestBinarySearchTree.TestDifferentOperationsOnBST();
            AncestorOfTwoNodesInBST.TestAncestorOfTwoNodesInBST();
            CheckBTisBST.TestCheckBTisBST();
            MaxSumOnTreeBranch.TestMaxSum();
            WalkTheTree.TestWalkTheTree();
            SkewedBSTToCompleteBST.TestConvertSkewedBSTToCompleteBST();
            CheckIfTheTreeIsBalanced.TestIsTreeBalanced();
            LinkedListOfTreeNodesAtEachDepth.TestCreateLinkedListOfTreeNodesAtEachDepth();
            TreeSubtreeOfAnother.TestMatchTree();
            AncestorOfTwoNodesInBT.TestGetAncestorOfTwoNodesInBT();
            LinkedListFromLeavesOfBT.TestLinkedListFromLeavesOfBT();
            ExteriorOfBT.TestPrintExteriorOfBT();

            // Trie problems
            CreateAndSearchSimpleTrie.TestCreateAndSearchSimpleTrie();
            // ToDo: have a problem of suffix trees
            ShortestPrefix.TestGetShortestPrefixNotPresentInStringSet();

            // Dynamic Programming problems
            LongestCommonSubsequence.TestGetLongestCommonSubsequence();
            LongestPalindromeSubString.TestGetLongestPalindromeSubString();
            LongestPalindromicSubsequence.TestGetLongestPalindromicSubsequence();

            Console.ReadLine();
        }
    }
}
