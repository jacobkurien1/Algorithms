﻿using AlgorithmProblems.Arrays;
using AlgorithmProblems.Dynamic_Programming;
using AlgorithmProblems.Graphs;
using AlgorithmProblems.Linked_List;
using AlgorithmProblems.matrix_problems;
using AlgorithmProblems.Misc;
using AlgorithmProblems.PermutationAndCombination;
using AlgorithmProblems.Recursion;
using AlgorithmProblems.Sorting;
using AlgorithmProblems.Stack_and_Queue;
using AlgorithmProblems.Stack_and_Queue.Queue_Helper;
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
            Merge2SortedArrays.TestMergeSortedArrays();
            //Median.TestGetMedianOf2SortedArray();

            // Sorting Problems
            SelectionSort.TestSorting();
            BubbleSort.TestSorting();
            InsertionSort.TestSorting();
            ShellSort.TestSorting();
            MergeSort.TestMergeSort();
            QuickSort.TestQuickSort();

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
            ReverseLinkedList.TestReverseLinkedList();

            // stack and queue problem
            ThreeStackWithOneArray.TestThreeStackWithOneArray();
            StackWithMinElement.TestStackWithMinElement();
            StackOfPlates.TestStackOfPlates();
            SortAStack.TestSortAStackAscending();
            WellFormedExpression.TestWellFormedExpression();
            QueueVia2Stack.TestQueueVia2Stack();
            LRUCache.TestLRUCache();
            EvaluatePrefixNotation.TestGetPrefixNotationResult();
            EvaluateInflixNotation.TestGetInflixNotationResults();
            EvaluatePostfixNotation.TestGetPostfixNotationResult();
            TestCircularQueue.TestCircularQueueWithDifferentCases();

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
            MergeIntervals.TestMergeIntervals();

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
            DepthOfTree.TestGetDepthOfTree();

            // Trie problems
            CreateAndSearchSimpleTrie.TestCreateAndSearchSimpleTrie();
            // ToDo: have a problem of suffix trees
            ShortestPrefix.TestGetShortestPrefixNotPresentInStringSet();

            // Dynamic Programming problems
            LongestCommonSubsequence.TestGetLongestCommonSubsequence();
            LongestPalindromeSubString.TestGetLongestPalindromeSubString();
            LongestPalindromicSubsequence.TestGetLongestPalindromicSubsequence();
            MaximumAs.TestGetMaximumAs();
            MinNumberOfJumps.TestGetMinimumNumberOfJumps();
            LongestCommonSubString.TestGetLongestCommonSubString();
            KnapSackProblem.TestGetTheMaximumValueWithLimitedWeight();
            TreeCuttingProblem.TestGetTreeCuttingToMaximizeProfits();
            WordBreaking.TestBreakTheWords();
            DistanceOfWords.TestDistanceOfWords();
            LongestIncreasingSubSequence.TestLongestIncreasingSubSequence();
            MinCostPath.TestMinCostPath();
            DifferentWaysForCoinChange.TestDifferentWaysForCoinChange();
            MatrixMultiplication.TestMatrixMultiplication();
            BinomialCoefficient.TestBinomialCoefficient();
            BoxStacking.TestBoxStacking();
            WordWrapping.TestWordWrapping();

            // Graph Problems
            ShortestPath.TestGetShortestPathBetween2Vertex();
            CycleInDirectedGraph.TestIsCycleInDirectedGraph();
            CycleInUnDirectedGraph.TestIsCycleInUnDirectedGraph();
            SolveAMaze.TestSolveAMaze();
            AllPathsGivenStartEndVertex.TestGetAllPathsInGraphFromStartVertexToEndVertex();
            AllPaths.TestGetAllPathsInGraphFromStartVertex();
            ColorVertices.TestColorVerticesWithDifferentColor();
            TransformOneWordToAnother.TestGetTransformation();
            ConstraintsVerification.TestConstraintsVerification();
            ExtendedContactsInSocialNetwork.TestComputeExtendedContacts();
            CourseScheduling.TestCourseScheduling();

            Console.ReadLine();
        }
    }
}
