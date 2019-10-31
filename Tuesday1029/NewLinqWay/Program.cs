using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLinqWay
{
    class Program
    {
        static void Main(string[] args)
        {

            // Define an array of strings
            string[] currentVideoGames = {"Morrowind", "Uncharted 2",
            "Fallout 3", "Dexter", "System shock 2"};

            #region lets try using linq
            QueryOverStrings(currentVideoGames);

            


            static void QueryOverStrings(string[] inputarray)
            {
                //build the query
                //IEnumerable<string> subset = from ...

                var subset = from game in inputarray
                             where game.Contains(" ")
                             orderby game
                             select game;
                // print results 
                ReflectOverQueryResults(subset, "Query Expression");
            }

            static void ReflectOverQueryResults(object resultset, string queryType)
            {
                Console.WriteLine("*** query type: {0}", queryType);
                Console.WriteLine("resultSet is of type: {0}", resultset.GetType().Name);
                Console.WriteLine("result location: {0}", resultset.GetType().Assembly.GetName().Name);
            }

            #endregion




        }
    }
}
