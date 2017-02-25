using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _491Parser
{
    class Program
    {
        //0 prints only necessary information
        //>=1 prints errors
        //>=2 prints errors and details
        public static int Log_Verbosity = 0;
        private static bool DELETE_ALL = false;
        private static bool CRAWL = false;
        private static bool CALCULATE_IMPORTANCE = false;
        static void Main(string[] args)
        {
            if (DELETE_ALL)
            {
                Console.WriteLine("Are you sure? (YES/NO)");
                if (Console.ReadLine() == "YES")
                {
                    Database.PURGEALLTABLES(DELETE_ALL);
                }
            }
            if (CRAWL)
            {
                DateTime startT = DateTime.Now;
                Page startPage = new Page("http://my.ku.edu.tr");
                startPage.CrawlRest();
                DateTime endT = DateTime.Now;
                TimeSpan ts = endT - startT;
                Console.WriteLine("Offline parsing completed in " + ts.TotalSeconds + " seconds!");
            }

            if (CALCULATE_IMPORTANCE)
            {
                Database.InitializeImportance();
                int n = Database.GetNumberOfPages();
                //Do many iterations
                for (int t = 0; t < 10; t++)
                {
                    //Iterate through all pages
                    for (int i = 1; i <= n; i++)
                    {
                        float newRank = Database.GetImportanceOfPage(i);
                        List<int> hasLinksFrom = Database.PageHasLinksFrom(i);
                        for (int j = 0; j < hasLinksFrom.Count; j++)
                        {
                            float PR = Database.GetImportanceOfPage(hasLinksFrom[j]);
                            int count = Database.GetNumberOfLinksPageHas(hasLinksFrom[j]);
                            if (count > 0)
                            {
                                float temp = PR / count;
                                newRank += temp;
                            }
                        }
                        if (newRank == 0)
                        {
                            newRank = 1.0f / n;
                        }
                        Database.UpdateImportanceValue(i, newRank);
                    }
                }
            }

            Console.WriteLine("Press any button to quit...");
            System.Console.ReadLine();
        }
    }
}