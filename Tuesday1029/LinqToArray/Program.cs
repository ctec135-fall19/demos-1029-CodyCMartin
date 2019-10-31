using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToArray
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with LINQ to Objects *****\n");

            // 3. define an array of strings
            string[] currentVideoGames = { "Morrowind", "Uncharted 2",
                "Fallout 3", "Daxter", "System Shock 2" };

            // desired query: Games that have a space in the title

            #region First let's try it the old fashioned way
            string[] results = QueryOverStringsLongHand(currentVideoGames);

            Console.WriteLine("Returned results from longhand version");
            foreach (string s in results)
            {
                Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();
            #endregion

            #region Let's try the same thing using a LINQ query
            List<string> listResults = QueryOverStrings(currentVideoGames);

            Console.WriteLine("Returned results from LINQ query method");
            foreach (string s in listResults)
            {
                Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();
            #endregion
        }

        #region old fashioned way
        static string[] QueryOverStringsLongHand(string[] inputArray)
        {
            // create array to store results
            // not sure how many results so make is same size as input array
            string[] ResultsWithSpaces = new string[inputArray.Length];

            // find results
            for (int i = 0; i < inputArray.Length; i++)
            {
                // results may be interspersed with nulls
                if (inputArray[i].Contains(" "))
                    ResultsWithSpaces[i] = inputArray[i];
            }

            // sort results array
            Array.Sort(ResultsWithSpaces);

            // print results and ignore nulls
            Console.WriteLine("Immediate results from longhand version.");
            foreach (string s in ResultsWithSpaces)
            {
                if (s != null)
                    Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();

            // generate return array
            // figure out size
            int count = 0;
            foreach (string s in ResultsWithSpaces)
            {
                if (s != null)
                    count++;
            }
            // create array
            string[] outputArray = new string[count];

            // populate array - cod is similar to print above
            count = 0;
            foreach (string s in ResultsWithSpaces)
            {
                if (s != null)
                {
                    outputArray[count] = s;
                    count++;
                }
            }

            // return result
            return outputArray;
        }
        #endregion

        #region use LINQ quer
        static List<string> QueryOverStrings(string[] inputArray)
        {
            // Build a query that find items with embedded space
            /* Troelsen's code
            IEnumerable<string> subset = from game in currentVideoGames 
                                         where game.Contains(" ") 
                                         orderby game 
                                         select game;
            */
            // using implicit typing - define query - query NOT EXECUTED HERE
            var subset = from game in inputArray
                         where game.Contains(" ")
                         orderby game
                         select game;

            // print result info
            ReflectOverQueryResults(subset, "Query Expression");

            // print results
            // LINQ query is executed here not above - deferred execution
            Console.WriteLine("\nImmediate results using LINQ query");
            foreach (var s in subset)
                Console.WriteLine("Item: {0}", s);
            Console.WriteLine();

            // demonstrate reuse of query
            inputArray[0] = "some string";
            Console.WriteLine("Immediate results using LINQ query after change to data");
            foreach (var s in subset)
                Console.WriteLine("Item: {0}", s);
            Console.WriteLine();

            // deomonstrate returning results - immediate execution - create snapshot
            List<string> outputList = (from game in inputArray
                                       where game.Contains(" ")
                                       orderby game
                                       select game).ToList<string>();
            return outputList;
        }
        #endregion

        static void ReflectOverQueryResults(object resultSet, string queryType)
        {
            Console.WriteLine("*** query type: {0}", queryType);
            Console.WriteLine("resultSet is of type: {0}", resultSet.GetType().Name);
            Console.WriteLine("resultSet location: {0}", resultSet.GetType().Assembly.GetName().Name);
        }
    }
}