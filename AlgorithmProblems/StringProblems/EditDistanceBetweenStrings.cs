using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProblems.StringProblems
{
    /// <summary>
    /// Check whether the edit distance between 2 strings is 1
    /// </summary>
    class EditDistanceBetweenStrings
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        private static bool IsEditDistanceBtwStringsOne(string str1, string str2)
        {
            int i = 0;
            int j = 0;
            int distance = 0;
            while (i < str1.Length && j < str2.Length)
            {
                if(str1[i]!=str2[j])
                {
                    distance++;
                    if (distance > 1)
                    {
                        // No need to check more indices in string
                        break;
                    }


                    if (str1.Length == str2.Length)
                    {
                        // This is the case where the replace needs to happen
                    }
                    else if(str1.Length>str2.Length)
                    {
                        // This is a case where insert needs to happen in str2
                        i++;
                        continue;
                    }
                    else
                    {
                        // This is a case where delete needs to happen in str2\
                        j++;
                        continue;
                    }
                }
                i++;
                j++;
            }

            distance += str1.Length -i;
            distance += str2.Length -j;

            return distance == 1;
        }

        public static void TestEditDistanceBetweenStrings()
        {
            string str1 = "jacob";
            string str2 = "jacox";
            Console.WriteLine("Edit distance for {0} and {1} is 1:{2}", str1, str2, IsEditDistanceBtwStringsOne(str1, str2));

            str1 = "jacob";
            str2 = "jaco";
            Console.WriteLine("Edit distance for {0} and {1} is 1:{2}", str1, str2, IsEditDistanceBtwStringsOne(str1, str2));

            str1 = "jacob";
            str2 = "jacobx";
            Console.WriteLine("Edit distance for {0} and {1} is 1:{2}", str1, str2, IsEditDistanceBtwStringsOne(str1, str2));

            str1 = "jacob";
            str2 = "jacob";
            Console.WriteLine("Edit distance for {0} and {1} is 1:{2}", str1, str2, IsEditDistanceBtwStringsOne(str1, str2));

            str1 = "jacob";
            str2 = "jacobdsds";
            Console.WriteLine("Edit distance for {0} and {1} is 1:{2}", str1, str2, IsEditDistanceBtwStringsOne(str1, str2));
        }
    }
}
