using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.Dynamic_Programming
{
    /// <summary>
    /// Given a set of cities and distance between them, find the shortest possible route which will visit all the cities
    /// once and return to the start city.
    /// 
    /// In other words, this problem is equivalent to finding the minimum weight hamiltonian distance cycle.
    /// </summary>
    class TravellingSalesmanProblem
    {
        /// <summary>
        /// Stores all the distances from each city to each other city.
        /// foreg dictanceMat[2,3] is the distance from city 2 to 3.
        /// 
        /// Note distance[2,3] maynot be equal to distanceMat[3,2]
        /// This can also be considered as the cost matrix. 
        /// The cost to fly from A to B may not be same as the cost to fly from B to A
        /// </summary>
        public int[,] DistanceMat { get; set; }

        /// <summary>
        /// This is the starting city of the salesman
        /// </summary>
        public int StartPoint { get; set; }
        public int TotalNumOfCities { get; set; }
        public HashSet<int> AllCitiesExceptStartSet { get; set; }

        /// <summary>
        /// We will be storing the subset of different size in this dictioanry
        /// The key will be the size of the subset and the value will be all subsets of that size from the AllCitiesSet
        /// We are saving this as a part of preprocessing when we get all the subsets of AllCitiesSet
        /// </summary>
        private Dictionary<int, List<HashSet<int>>> SubSetOfDifferentSize { get; set; }
        public TravellingSalesmanProblem(int[,] distanceMat, int startPoint)
        {
            DistanceMat = distanceMat;
            StartPoint = startPoint;
            TotalNumOfCities = distanceMat.GetLength(0);

            AllCitiesExceptStartSet = new HashSet<int>(Enumerable.Range(0, TotalNumOfCities));
            AllCitiesExceptStartSet.Remove(StartPoint);


        }

        /// <summary>
        /// The dynamic programming approach will involve storing the cost for a city subset and endpoint in a Dictionary.
        /// if C(S,j) is the cost of going from StartPoint to j when we have a set of cities belong to S.
        /// C(S,j) = min{C(S-{j}, i) + DistanceMat[i,j]} where i belongs to S-{j}
        /// 
        /// The running time of this algo is O((2^n)(n^2))
        /// the total number of subproblems are O((2^n)n) and each subproblem takes n time.
        /// </summary>
        /// <returns>the shortest route for the salesman</returns>
        public List<int> GetShortestRoute()
        {
            // initialization
            Dictionary<CitySetWithEndPoint, int> costOfPath = new Dictionary<CitySetWithEndPoint, int>();
            Dictionary<CitySetWithEndPoint, int> parentDict = new Dictionary<CitySetWithEndPoint, int>();
            CitySetWithEndPoint startSetWithStartPoint = new CitySetWithEndPoint(new HashSet<int>() { StartPoint }, StartPoint);
            costOfPath[startSetWithStartPoint] = 0;
            parentDict[startSetWithStartPoint] = -1;
            int minPathDistance = int.MaxValue;
            int finalDestinationBeforeComingToStart = -1;

            for (int sizeOfSet = 2; sizeOfSet <= TotalNumOfCities; sizeOfSet++)
            {
                // for each size of the set get all the possible subsets that can be formed
                // the subset should contain the startCity
                List<HashSet<int>> allSubsets = GetAllSubSetOfSize(sizeOfSet);
                foreach (HashSet<int> subSet in allSubsets)
                {
                    foreach (int city in subSet)
                    {
                        // city represents j
                        if (city != StartPoint)
                        {
                            // this represents S
                            CitySetWithEndPoint currentSetAndEndpoint = new CitySetWithEndPoint(subSet, city);

                            foreach (int intermediateCity in subSet)
                            {
                                // intermediateCity represents i
                                if (intermediateCity != city)
                                {
                                    // This will make a clone of the currentSetAndEndpoint object
                                    // this represents S-{j}
                                    CitySetWithEndPoint intermediateSetAndEndpoint = new CitySetWithEndPoint(currentSetAndEndpoint);
                                    intermediateSetAndEndpoint.SetOfCities.Remove(city);
                                    intermediateSetAndEndpoint.EndPoint = intermediateCity;

                                    // get the min for costOfPath[currentSetAndEndpoint]
                                    int initPathCost = int.MaxValue;
                                    if (GetDistanceFromCostPathDict(costOfPath, intermediateSetAndEndpoint) != int.MaxValue)
                                    {
                                        initPathCost = GetDistanceFromCostPathDict(costOfPath, intermediateSetAndEndpoint) + DistanceMat[intermediateCity, city];
                                    }
                                    if (GetDistanceFromCostPathDict(costOfPath, currentSetAndEndpoint) > initPathCost)
                                    {
                                        costOfPath[currentSetAndEndpoint] = initPathCost;
                                        parentDict[currentSetAndEndpoint] = intermediateCity;

                                        // this will keep track of the min path cost and distance from
                                        if (sizeOfSet == TotalNumOfCities && DistanceMat[city, StartPoint] != int.MaxValue && initPathCost + DistanceMat[city, StartPoint] < minPathDistance)
                                        {
                                            minPathDistance = initPathCost + DistanceMat[city, StartPoint];
                                            finalDestinationBeforeComingToStart = city;
                                        }
                                    }


                                }
                            }
                        }
                    }
                }
            }

            if (finalDestinationBeforeComingToStart == -1)
            {
                // there is no travelling salesman route
                return null;
            }
            else
            {
                // backtrack and return the path
                return (BackTrackToGetPath(parentDict, finalDestinationBeforeComingToStart));
            }

        }

        /// <summary>
        /// Backtrack to get the path of the travelling salesman route
        /// </summary>
        /// <param name="parentDict">dictionary with the parent of the city info.
        /// the back tracking will be done as shown below:
        /// [[0,1,2,3], 1] = 3
        /// [[0,2,3], 3] =  2
        /// [[0,2], 2] = 1
        /// [[0], 0] = 0
        /// 
        /// </param>
        /// <param name="finalDestinationBeforeComingToStart">desitnation before coming back to the city from which TS started</param>
        /// <returns></returns>
        public List<int> BackTrackToGetPath(Dictionary<CitySetWithEndPoint, int> parentDict, int finalDestinationBeforeComingToStart)
        {
            List<int> travellingSalesmanRoute = new List<int> { StartPoint };
            CitySetWithEndPoint lastSet = new CitySetWithEndPoint(new HashSet<int>(AllCitiesExceptStartSet), finalDestinationBeforeComingToStart);
            lastSet.SetOfCities.Add(StartPoint);

            int currentCity = finalDestinationBeforeComingToStart;
            while (currentCity != -1)
            {
                travellingSalesmanRoute.Add(currentCity);

                int previousCity = parentDict[lastSet];
                lastSet.SetOfCities.Remove(currentCity);
                lastSet.EndPoint = previousCity;

                currentCity = previousCity;
            }
            travellingSalesmanRoute.Reverse();
            return travellingSalesmanRoute;
        }

        /// <summary>
        /// If the key c is present in the dictionary costOfPath then return the cost of the path,
        /// else return infinty, denoted by int.MaxValue
        /// </summary>
        /// <param name="costOfPath"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public int GetDistanceFromCostPathDict(Dictionary<CitySetWithEndPoint, int> costOfPath, CitySetWithEndPoint c)
        {
            int cost = int.MaxValue;
            if (costOfPath.ContainsKey(c))
            {
                cost = costOfPath[c];
            }
            return cost;
        }

        /// <summary>
        /// Gets all the subset of a particular size. All these subsets have start point as a mandatory set element
        /// </summary>
        /// <param name="sizeOfSet"></param>
        /// <returns></returns>
        private List<HashSet<int>> GetAllSubSetOfSize(int sizeOfSet)
        {
            if (SubSetOfDifferentSize == null)
            {
                PopulateSubSetOfDifferentSize();
            }
            return SubSetOfDifferentSize[sizeOfSet];
        }

        /// <summary>
        /// Populate the Ditionary SubSetOfDifferentSize by gettting all the subset and bucketing it into its corresponding size bucket.
        /// This is a preprocessing step and we will do this only once.
        /// </summary>
        private void PopulateSubSetOfDifferentSize()
        {
            // initialization
            SubSetOfDifferentSize = new Dictionary<int, List<HashSet<int>>>();
            List<HashSet<int>> subsets = new List<HashSet<int>>() { new HashSet<int>() };

            // get all subsets
            for (int i = 0; i < AllCitiesExceptStartSet.Count; i++)
            {
                // make a clone of subsets
                List<HashSet<int>> subsetsClone = new List<HashSet<int>>();
                foreach (HashSet<int> set in subsets)
                {
                    subsetsClone.Add(new HashSet<int>(set));
                }

                foreach (HashSet<int> set in subsetsClone)
                {
                    set.Add(AllCitiesExceptStartSet.ElementAt(i));

                    // Add the set in the dictionary SubSetOfDifferentSize whose key is the set size
                    // We need to do set.Count+1 cause we will add the start city later and the +1 takes that into account
                    if (SubSetOfDifferentSize.ContainsKey(set.Count + 1))
                    {
                        SubSetOfDifferentSize[set.Count + 1].Add(set);
                    }
                    else
                    {
                        SubSetOfDifferentSize[set.Count + 1] = new List<HashSet<int>>() { set };
                    }
                }
                subsets.AddRange(subsetsClone);
            }

            // Now add the start point to all the subsets
            foreach (KeyValuePair<int, List<HashSet<int>>> subset in SubSetOfDifferentSize)
            {
                foreach (HashSet<int> set in subset.Value)
                {
                    set.Add(StartPoint);
                }
            }

        }

        /// <summary>
        /// Represents the Set of cities and the final finish point.
        /// We need to override the Equals and GetHashCode() method so that we can make object of this class as Dictionary key
        /// </summary>
        public class CitySetWithEndPoint
        {
            public HashSet<int> SetOfCities { get; set; }

            public int EndPoint { get; set; }

            public CitySetWithEndPoint(HashSet<int> setOfCities, int endPoint)
            {
                SetOfCities = setOfCities;
                EndPoint = endPoint;
            }

            public CitySetWithEndPoint(CitySetWithEndPoint objToClone)
            {
                SetOfCities = new HashSet<int>(objToClone.SetOfCities);
                EndPoint = objToClone.EndPoint;
            }

            /// <summary>
            /// Needed to make sure that dictionary keys with same set and endpoints gets matched.
            /// The hashset comparison should not check for order in the hashset
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                int hashCode = 0;
                foreach (int i in SetOfCities)
                {
                    hashCode ^= i.GetHashCode();
                }

                return hashCode ^ EndPoint.GetHashCode();
            }

            /// <summary>
            /// Needed to make sure that dictionary keys with same set and endpoints gets matched.
            /// The hashset comparison should not check for order in the hashset
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object obj)
            {
                CitySetWithEndPoint compObj = (CitySetWithEndPoint)obj;
                return EndPoint == compObj.EndPoint && SetOfCities.SetEquals(compObj.SetOfCities);
            }
        }

        #region TestArea
        public static void TestTravellingSalesmanProblem()
        {
            // positive test case
            int[,] distanceMat = new int[,]
            {
                {0,80, 69, 50 },
                {80, 0,140,100 },
                {69, 140, 0,90 },
                {50, 100,90,0 }
            };
            TravellingSalesmanProblem tsp = new TravellingSalesmanProblem(distanceMat, 0);
            PrintPath(tsp.GetShortestRoute());

            // negative test case
            distanceMat = new int[,]
            {
                {0,2, 1, int.MaxValue },
                {int.MaxValue, 0,int.MaxValue,3 },
                {int.MaxValue, int.MaxValue, 0,2 },
                {int.MaxValue, int.MaxValue, int.MaxValue, 0 }
            };
            tsp = new TravellingSalesmanProblem(distanceMat, 0);
            PrintPath(tsp.GetShortestRoute());
        }
        private static void PrintPath(List<int> path)
        {
            Console.WriteLine("The tsp route is as shown below:");
            if (path != null)
            {
                foreach (int city in path)
                {
                    Console.Write("{0} -> ", city);
                }
            }
            Console.WriteLine();
        }
        #endregion

    }
}
