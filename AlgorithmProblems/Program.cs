using AlgorithmProblems.Arrays;
using AlgorithmProblems.Dynamic_Programming;
using AlgorithmProblems.Graphs;
using AlgorithmProblems.Graphs.ShortestPathAlgos;
using AlgorithmProblems.Heaps;
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
using AlgorithmProblems.DisjointSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmProblems.Geometry;
using AlgorithmProblems.Greedy_Algorithm;
using AlgorithmProblems.BackTracking;

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
            SumOfTwoNumbersInArray.TestSumOfTwoNumbersInArray();
            SumOfThreeNumbersInArray.TestSumOfThreeNumbersInArray();

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
            HeapSortTester.TestHeapSort();
            CountingSort.TestSorting();
            RadixSort.TestRadixSort();

            // Matrix Problem
            Rotate_Matrix_90_degree.TestRotateMatrix();
            Matrix_Column_Rows_0.TestMakeRowColZero1();
            Matrix_Column_Rows_0.TestMakeRowColZero2();
            RotateMatrix180.TestRotateMatrix180();
            SumOfMatrixElementsFormedByRectangleWithCoordinates.TestSumOfMatrixElements();
            SortedArrayFromSortedMatrix.TestSortedArrayFromSortedMatrix();

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
            MaxSumOfConsecutiveNums.TestMaxSumOfConsecutiveNums();

            // Back tracking problems
            Sudoku.TestSudokuSolver();
            HamiltonianCycle.TestHamiltonianCycle();

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
            CombinationsOfBinaryString.TestCombinationsOfBinaryString();
            AllCombinationsOfString.TestAllCombinationsOfString();
            AllPermutationsOfString.TestAllPermutationsOfString();
            PhoneNumberToWords.TestPhoneNumberToWords();
            AllNumbersInRangeWithDifferentDigits.TestAllNumbersInRangeWithDifferentDigits();
            DivideSetIntoTwoEqualSetsWithMinSumDiff.TestDivideSetIntoTwoEqualSetsWithMinSumDiff();

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
            TreeToColumns.TestTreeToColumns();
            KthSmallestElementFromBST.TestKthSmallestElementFromBST();

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
            MaxSubMatrixWithAllOnes.TestMaxSubMatrixWithAllOnes();
            LongestSubStringWithEqualSum.TestLongestSubStringWithEqualSum();
            PartitionArrayIntoEqualSumSets.TestPartitionArrayIntoEqualSumSets();
            MaxSumRectangle.TestMaxSumRectangle();

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
            SnakeAndLadder.TestSnakeAndLadder();
            // ShortestPathAlgo
            FloydWarshall.TestFloydWarshall();
            DijkstraAlgorithm.TestDijkstraAlgorithm();
            BellmanFord.TestBellmanFord();
            //Minimum Spanning Tree


            //Heap problems
            BasicMaxHeap.TestMaxHeap();
            BasicMinHeap.TestMinHeap();
            TestMinHeapMap.DoTest();

            //DisjointSets
            TestingDisjointSet.Run();

            //Geometry
            ClosestPairOfPoints.TestClosestPairOfPoints();
            RectangleIntersection.TestRectangleIntersection();
            LineSegmentIntersection.TestLineSegmentIntersection();
            ConvexHull.TestConvexHull();

            //Greedy Algorithm
            HuffmanCoding.TestHuffmanCoding();

            Console.ReadLine();
        }
    }
}
