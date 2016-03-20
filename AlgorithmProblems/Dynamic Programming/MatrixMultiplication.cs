using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// We need to find the order in which multiplication between a set of matrix needs to be performed
    /// </summary>
    public class MatrixMultiplication
    {
        public class MatrixDimensions
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public MatrixDimensions(int row, int column)
            {
                Row = row;
                Column = column;
            }
        }

        /// <summary>
        /// We can solve this using dynamic Programming
        /// 
        /// when we multiply 2 matrix m1 and m2, the total number of operation is r1 * c1* c2
        /// 
        /// Since we will be doing a bottoms up approach we need to create a 2-D costMatrix
        /// costMatrix[i,j] is the least number of operations needed to multiply matrices from i to j in mats array
        /// So if have an array of matrices to multiply and get the min number of operations we need to do the following
        /// 
        /// costMatrix[i,j] = min       {costMatrix[i,i+k] + costMatrix[i+k+1,j] + mats[i].row*mats[i+k].col*mats[j].col}
        ///                   (k=0->j-1)
        /// 
        /// Also costMatrix will be filled in the upper right portion from  the diagonal.
        /// We need to first fill the elements closer to the diagonals and move up.
        /// 
        /// the running time here is O(n^3)
        /// and the space requirement is O(n^2)
        /// </summary>
        /// <param name="mats"></param>
        /// <returns></returns>
        public int LeastNumOfOperationsInMatrixChainMultiplication(MatrixDimensions[] mats)
        {
            int[,] costMatrix = new int[mats.Length, mats.Length];
            int[,] backtrackMat = new int[mats.Length, mats.Length];
            for (int increment=1; increment<mats.Length; increment++)
            {
                for(int index= 0; index<mats.Length;index++)
                {
                    int i = index;
                    int j = index + increment;
                    if (j<mats.Length)
                    {
                        costMatrix[i, j] = int.MaxValue;
                        for(int k=0; k<j- i; k++)
                        {
                            int currentVal = costMatrix[i, i + k] + costMatrix[i + k + 1, j] + (mats[i].Row * mats[i+k].Column * mats[j].Column);
                            if(currentVal < costMatrix[i, j])
                            {
                                backtrackMat[i, j] = i + k;
                                costMatrix[i, j] = currentVal;
                            }
                        }
                    }
                }
            }
            Queue<MatrixDimensions> queue = new Queue<MatrixDimensions>();
            // We will use the MatrixDimensions class to store the location of the cell in the matrix
            queue.Enqueue(new MatrixDimensions(0,mats.Length-1));
            while(queue.Count!=0)
            {
                var location = queue.Dequeue();
                int k = backtrackMat[location.Row, location.Column];
                int i = location.Row;
                int j = location.Column;
                if (k - i == 1)
                {
                    Console.WriteLine("[{0}, {1}]", i, k);
                }
                else if (k - i == 0)
                {
                    Console.WriteLine("[{0}]", i);
                }
                else
                {
                    queue.Enqueue(new MatrixDimensions(i, k));
                }
                if (j - (k+1) == 1)
                {
                    Console.WriteLine("[{0}, {1}]", k+1, j);
                }
                else if (j - (k + 1) == 0)
                {
                    Console.WriteLine("[{0}]", k+1);
                }
                else
                {
                    queue.Enqueue(new MatrixDimensions(k + 1, j));
                }
            }
            return costMatrix[0, mats.Length - 1];
        }

        public static void TestMatrixMultiplication()
        {
            MatrixMultiplication matMul = new MatrixMultiplication();
            MatrixDimensions[] mats = new MatrixDimensions[] { new MatrixDimensions(1, 2), new MatrixDimensions(2, 3), new MatrixDimensions(3, 4) };
            Console.WriteLine("the minimum number of multiplications needed is {0}", matMul.LeastNumOfOperationsInMatrixChainMultiplication(mats));

            mats = new MatrixDimensions[] { new MatrixDimensions(10, 30), new MatrixDimensions(30, 5), new MatrixDimensions(5, 60) };
            Console.WriteLine("the minimum number of multiplications needed is {0}", matMul.LeastNumOfOperationsInMatrixChainMultiplication(mats));

            mats = new MatrixDimensions[] { new MatrixDimensions(40, 20), new MatrixDimensions(20, 30), new MatrixDimensions(30, 10), new MatrixDimensions(10, 30) };
            Console.WriteLine("the minimum number of multiplications needed is {0}", matMul.LeastNumOfOperationsInMatrixChainMultiplication(mats));

        }
    }
}
