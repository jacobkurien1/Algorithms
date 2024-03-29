﻿using AlgorithmProblems.Arrays;
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
using AlgorithmProblems.RandomizedAlgo;
using AlgorithmProblems.Graphs.ShortestPathAlgos.TravelInMatrix;
using AlgorithmProblems.Bit_Algorithms;
using AlgorithmProblems.PatternMatching;
using AlgorithmProblems.StringProblems.PatternMatching;
using AlgorithmProblems.Math_Problems;
using AlgorithmProblems.Graphs.ShortestPathAlgos.AStar;
using AlgorithmProblems.Graphs.ShortestPathAlgos.ShortestPathWhenObstaclesRemoved;
using AlgorithmProblems.Graphs.MaxFlow;
using AlgorithmProblems.Graphs.GraphColoring;
using AlgorithmProblems.Search;
using AlgorithmProblems.Graphs.MinSpanningTree;
using AlgorithmProblems.Heaps.HeapHelper;
using AlgorithmProblems.Distributed_Algorithms;

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
            RegexMatching.TestMatch();
            SumOfTwoNumbersInArray.TestSumOfTwoNumbersInArray();
            SumOfThreeNumbersInArray.TestSumOfThreeNumbersInArray();
            PairInSortedArrayClosestToAParticularValue.TestPairInSortedArrayClosestToAParticularValue();
            PalindromeInStringPermutation.TestPalindromeInStringPermutation();
            EditDistanceBetweenStrings.TestEditDistanceBetweenStrings();
            AnagramIsPalindrome.TestAnagramIsPalindrome();
            GreatestPalindrome.TestGreatestPalindrome();
            ReverseStringWithoutVowels.TestReverseStringWithoutVowels();
            LongestSubstringWithKDistinctChars.TestLongestSubstringWithKDistinctChars();
            // Pattern Matching
            NativePatternMatching.TestNativePatternMatching();
            KMPPatternMatching.TestKMPPatternMatching();
            BoyerMoorePatternMatching.TestPatternMatching();
            RabinKarp.TestRabinKarp();

            //Console.ReadLine();

            //Array Problems
            ArrayOfNumsIncrement.TestIncrementArrayOfNumbers();
            MajorityElement.TestFindMajorityElement();
            Merge2SortedArrays.TestMergeSortedArrays();
            MaxDistanceInArray.TestMaxDistanceInArray();
            MovingAverage.TestMovingAverage();
            TotalAverage.TestTotalAverage();
            ArrayWithGreaterElementToRight.TestArrayWithGreaterElementToRight();
            WaterCollectedInPuddleShownByHistogram.TestWaterCollectedInPuddleShownByHistogram();
            CountOfPairsWhichSumUpToGivenSum.TestCountOfPairsWhichSumUpToGivenSum();
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
            DutchNationalFlag.TestDutchNationalFlag();
            SortedSquares.TestSortedSquares();

            // Matrix Problem
            Rotate_Matrix_90_degree.TestRotateMatrix();
            Matrix_Column_Rows_0.TestMakeRowColZero1();
            Matrix_Column_Rows_0.TestMakeRowColZero2();
            RotateMatrix180.TestRotateMatrix180();
            SumOfMatrixElementsFormedByRectangleWithCoordinates.TestSumOfMatrixElements();
            SortedArrayFromSortedMatrix.TestSortedArrayFromSortedMatrix();
            SearchWordInMatrix.TestSearchWordInMatrix();
            MaxOnesInRow.TestMaxOnesInRow();
            MatrixAsTriangle.TestMatrixAsTriangle();
            MinRangeInMatrix.TestMinRangeInMatrix();
            PrintMatrixAsSnake.TestPrintMatrixAsSnake();
            PrintMatrixInSpiral.TestPrintMatrixInSpiral();
            MaxSqAreaInBinaryMatrix.TestMaxRectAreaInBinaryMatrix();
            TicTacToeWinner.TestTicTacToeWinner();
            WaterfallCreation.TestWaterfallCreation();

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
            CycleInLinkedList.TestStartOfCycleInLinkedList();
            MedianForCircularLinkedList.TestGetMedian();
            ReverseLinkedList.TestReverseLinkedList();
            SortedCircularLinkedList.TestCircularLinkedList();

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
            LargestAreaInHistogram.TestLargestAreaInHistogram();
            TextEditerWithUndo.TestTextEditerWithUndo();

            //Recursion Problem
            TowerOfHanoi.TestTowerOfHanoi();
            MaxSumOfConsecutiveNums.TestMaxSumOfConsecutiveNums();

            // Back tracking problems
            Sudoku.TestSudokuSolver();
            HamiltonianCycle.TestHamiltonianCycle();
            GraphColoringWithMColors.TestGraphColoringWithMColors();
            MakeLargestIsland.TestMakeLargestIsland();

            //Misc Problem
            MinNumOfCoins.TestMinNumOfCoins();
            IsPrime.TestCheckPrime();
            SquareRoot.TestCalculateSquareRoot();
            CreditCardCheck.TestLuhnAlgo();
            ExcelFirstRowConversion.TestCovertExcelColumnToLong();
            Skyline.TestSkyline();
            SumOfSquaresWithoutMultiplication.TestSumOfSquares();
            MergeIntervals.TestMergeIntervals();
            WidthOfCalendar.TestWidthOfCalendar();
            JosephusProblem.TestJosephusProblem();

            // Permutation and Combination problem
            ShuffleAList.TestFisherYatesAlgo();
            CombinationsOfBinaryString.TestCombinationsOfBinaryString();
            AllCombinationsOfString.TestAllCombinationsOfString();
            AllPermutationsOfString.TestAllPermutationsOfString();
            PhoneNumberToWords.TestPhoneNumberToWords();
            AllNumbersInRangeWithDifferentDigits.TestAllNumbersInRangeWithDifferentDigits();
            DivideSetIntoTwoEqualSetsWithMinSumDiff.TestDivideSetIntoTwoEqualSetsWithMinSumDiff();
            PowerSet.TestPowerSet();
            AllCombinationsOfParenthesis.TestAllCombinationsOfParenthesis();
            GoodDiceRoll.TestGoodDiceRoll();
            PermutationsOfValidTime.TestPermutationsOfValidTime();

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
            PrintBinaryTreeNodeAtEachLevel.TestPrintBinaryTreeNodeAtEachLevel();
            PrintBinaryTreeNodeAtEachLevelSpirally.TestPrintBinaryTreeNodeAtEachLevelSpirally();
            TreeSubtreeOfAnother.TestMatchTree();
            AncestorOfTwoNodesInBT.TestGetAncestorOfTwoNodesInBT();
            AncestorOfMultiNodesInBT.TestAncestorOfMultiNodesInBT();
            LinkedListFromLeavesOfBT.TestLinkedListFromLeavesOfBT();
            ExteriorOfBT.TestPrintExteriorOfBT();
            DepthOfTree.TestGetDepthOfTree();
            TreeToColumns.TestTreeToColumns();
            KthSmallestElementFromBST.TestKthSmallestElementFromBST();
            MakeBSTFromPreOrder.TestMakeBSTFromPreOrder();
            MirrorATree.TestMirrorATree();
            CloneABTWithRandPointer.TestCloneABTWithRandPointer();
            TreeWithInorderAndPreorder.TestTreeWithInorderAndPreorder();
            TreeWithInorderAndPostorder.TestTreeWithInorderAndPostorder();
            TreePathSumsToValue.TestTreePathSumsToValue();
            AllPathInNArayTree.TestAllPathInNArayTree();
            SerializeDeserializeBinaryTree.TestSerializeDeserializeBinaryTree();
            SerializeDeserializeAnNaryTree.TestSerializeDeserializeAnNaryTree();
            AncestorOfTwoNodesInNaryTree.TestAncestorOfTwoNodesInNaryTree();
            AbsOfMaxAndSecondMaxDepthInBT.TestGetAbsOfMaxAndSecondMaxDepthInBT();

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
            RegularExpressionMatch.TestRegularExpressionMatch();
            NumRepresentedByPerfectSquares.TestNumRepresentedByPerfectSquares();
            LongestCommonSubsequenceInSameString.TestLongestCommonSubsequenceInSameString();
            StringDecodeAsAlphabets.TestStringDecodeAsAlphabets();
            BalloonBursting.TestBalloonBursting();
            TravellingSalesmanProblem.TestTravellingSalesmanProblem();
            MaxSumWithoutAdjacentElements.TestMaxSumWithoutAdjacentElements();
            MaxPathThroughMatrix.TestMaxPathThroughMatrix();
            BrickLaying.TestBrickLaying();
            JobSchedullingWithConstraints.TestJobSchedullingWithConstraints();
            EggDropMinTrials.TestEggDropMinTrials();

            // Graph Problems
            ShortestPath.TestGetShortestPathBetween2Vertex();
            CycleInDirectedGraph.TestIsCycleInDirectedGraph();
            CycleInUnDirectedGraph.TestIsCycleInUnDirectedGraph();
            SolveAMaze.TestSolveAMaze();
            AllPathsGivenStartEndVertex.TestGetAllPathsInGraphFromStartVertexToEndVertex();
            AllPaths.TestGetAllPathsInGraphFromStartVertex();
            ColorVertices.TestColorVerticesWithDifferentColor();
            CheckBipartiteGraph.TestCheckBipartiteGraph();
            TransformOneWordToAnother.TestGetTransformation();
            ConstraintsVerification.TestConstraintsVerification();
            ExtendedContactsInSocialNetwork.TestComputeExtendedContacts();
            CourseScheduling.TestCourseScheduling();
            SnakeAndLadder.TestSnakeAndLadder();
            IsGraphATree.TestIsGraphATree();
            ReverseGraph.TestReverseGraph();
            StronglyConnectedGraph.TestStronglyConnectedGraph();
            ConnectedComponents.TestConnectedComponents();
            ContinentalDivide.TestContinentalDivide();
            CloneGraph.TestCloneGraph();
            Wordament.TestWordament();
            // ShortestPathAlgo
            FloydWarshall.TestFloydWarshall();
            DijkstraAlgorithm.TestDijkstraAlgorithm();
            BellmanFord.TestBellmanFord();
            TravelFromLeftToRightInMatrix.TestTravelFromLeftToRightInMatrix();
            HeuristicSearch.TestHeuristicSearch();
            AStar.TestAStar();
            ShortestPathWhenObstaclesRemoved.TestShortestPathWhenObstaclesRemoved();
            ShortestDistanceFromRoomsToGates.TestShortestDistanceFromRoomsToGates();
            //MaxFlow
            FordFulkersonEdmondKarp.TestFordFulkersonEdmondKarp();
            MinCut.TestMinCut();
            MaximumBipartiteMatching.TestMaximumBipartiteMatching();
            //Minimum Spanning Tree
            KruskalAlgorithm.TestKruskalAlgorithm();
            PrimsAlgorithm.TestPrimsAlgorithm();


            //Heap problems
            BasicMaxHeap.TestMaxHeap();
            BasicMinHeap.TestMinHeap();
            TestMinHeapMap.DoTest();
            TestPriorityQueue.Run();
            MedianForAStreamOfNumbers.TestMedianForAStreamOfNumbers();

            //DisjointSets
            TestingDisjointSet.Run();
            //TestWeightedDisjointSetsWithPathCompression.Run(); // this runs slow, hence commenting it

            //Geometry
            ClosestPairOfPoints.TestClosestPairOfPoints();
            RectangleIntersection.TestRectangleIntersection();
            LineSegmentIntersection.TestLineSegmentIntersection();
            ConvexHull.TestConvexHull();
            KClosestPointToOrigin.TestKClosestPointToOrigin();

            //Greedy Algorithm
            HuffmanCoding.TestHuffmanCoding();

            //Randomized Algorithm
            RandomGeneration.TestRandomGeneration();

            // Bit Algorithms
            IntHaveOppositeSigns.TestIntHaveOppositeSigns();
            Parity.TestParity();

            //Math Problem
            ZerosInFactorial.TestZerosInFactorial();
            GetAllPrimeFactors.TestGetAllPrimeFactors();
            NumberOfFactors.TestNumberOfFactors();
            AllFactors.TestAllFactors();
            MultiplyLongNumbers.TestMultiplyLongNumbers();
            NextLargestPermutation.TestNextLargestPermutation();
            AllPrimesTillN.TestAllPrimesTillN();
            PascalsTriangle.TestPascalsTriangle();
            SubtractLongNumbers.TestSubtractLongNumbers();

            //Search problems
            SearchInSortedRotatedArray.TestSearchInSortedRotatedArray();
            KClosestElementInArray.TestKClosestElementInArray();
            SearchInSortedMatrix.TestSearchInSortedMatrix();
            BinarySearchUnbounded.TestBinarySearchUnbounded();
            LinkedListSublistSearch.TestLinkedListSublistSearch();
            NoOfOccuranceInSortedArray.TestNoOfOccuranceInSortedArray();
            GetSmallestInSortedRotatedArray.TestGetSmallestInSortedRotatedArray();

            //Distributed algorithms
            KWayMerge.TestKWayMerge();


            Console.ReadLine();
        }
    }
}
