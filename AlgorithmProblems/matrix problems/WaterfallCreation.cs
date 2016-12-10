using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.matrix_problems
{
    /// <summary>
    /// A matrix is given which depicts boulders in the path of a waterfall.
    /// When ever the water hits a boulder it splits into 1/2 of the total volume.
    /// Find the volume of the water in the different areas.
    /// </summary>
    class WaterfallCreation
    {
        /// <summary>
        /// We have 3 different cases here:
        /// Case1: the water gets divided on the boulder
        /// 0,0,50,0,0
        /// 25======25
        /// 
        /// Case2: There is no left passage for water
        /// 0,0,50,0,0
        /// ========50
        /// 
        /// Case3: There is no right passage for water
        /// 0,0,50,0,0
        /// 50========
        /// 
        /// The running time is O(n*m)
        /// where n is the number of rows and m is the number of columns in matrix.
        /// The space requirement is O(1)
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static float[,] GetWaterFallVolumes(float[,] area)
        {
            for(int i=1; i<area.GetLength(0); i++)
            {
                int boulderStart = -1;
                float boulderAccumulatedVol = 0; 
                for(int j = 0; j<area.GetLength(1); j++)
                {
                    if(area[i,j] == -1)
                    {
                        if(j-1>=0 && area[i,j-1] != -1)
                        {
                            // this is the first boulder in i-1th row
                            boulderStart = j - 1;
                            boulderAccumulatedVol = 0;
                        }
                        boulderAccumulatedVol += area[i - 1, j] != -1 ? area[i - 1, j] : 0;

                    }
                    else
                    {
                        // Take care of water falling vertically downwards
                        area[i, j] += area[i - 1, j] != -1 ? area[i - 1, j] : 0;
                        //Take care of water falling after hitting the boulder
                        if (boulderStart == -1)
                        {
                            //Case2: There is no left passage for water
                            // All water flows through the right of the boulder
                            area[i, j] += boulderAccumulatedVol;
                            boulderAccumulatedVol = 0;
                        }
                        else
                        {
                            //Case1: The water volume gets divided into halfs
                            area[i, boulderStart] += boulderAccumulatedVol / 2;
                            area[i, j] += boulderAccumulatedVol / 2;
                            boulderAccumulatedVol = 0;
                        }
                    }
                }
                if (boulderStart != -1 && boulderAccumulatedVol != 0)
                {
                    //Case3: There is no right passage for the water
                    // All water flows through left of the boulder
                    area[i, boulderStart] += boulderAccumulatedVol;
                }
            }

            return area;
        }

        public static void TestWaterfallCreation()
        {
            float[,] area = new float[,]
                                {
                                    { 0,0,50,0,0 },
                                    { 0,-1,-1,-1,0},
                                    { -1,0,0,0,-1},
                                    { 0,-1,0,-1,0},
                            };
            MatrixProblemHelper.PrintMatrix(area);
            area = GetWaterFallVolumes(area);
            Console.WriteLine("The water fall volume is as shown below:");
            MatrixProblemHelper.PrintMatrix(area);

            area = new float[,]
                                {
                                    { 0,0,50,0,0 },
                                    { 0,-1,-1,-1,-1},
                                    { -1,-1,-1,-1,0},
                                    { 0,-1,0,-1,0},
                            };
            MatrixProblemHelper.PrintMatrix(area);
            area = GetWaterFallVolumes(area);
            Console.WriteLine("The water fall volume is as shown below:");
            MatrixProblemHelper.PrintMatrix(area);

            area = new float[,]
                                {
                                    { 0,0,50,0,0 },
                                    { 0,-1,-1,-1,-1},
                                    { -1,-1,-1,-1,0},
                                    { -1,-1,0,-1,-1},
                            };
            MatrixProblemHelper.PrintMatrix(area);
            area = GetWaterFallVolumes(area);
            Console.WriteLine("The water fall volume is as shown below:");
            MatrixProblemHelper.PrintMatrix(area);

            area = new float[,]
                                {
                                    { 0,0,50,0,0 },
                                    { 0,-1,-1,-1,-1},
                                    { -1,-1,-1,-1,0},
                                    { -1,-1,-1,-1,-1},
                                    { -1,0,0,-1,-1},
                            };
            MatrixProblemHelper.PrintMatrix(area);
            area = GetWaterFallVolumes(area);
            Console.WriteLine("The water fall volume is as shown below:");
            MatrixProblemHelper.PrintMatrix(area);

        }
    }
}
