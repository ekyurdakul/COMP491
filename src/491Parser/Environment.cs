using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _491Parser
{
    static class Environment
    {
        public static List<string> m_CrawledPages = new List<string>();
        public static void DisplayCrawledPages()
        {
            Console.WriteLine("**************************************");
            Console.WriteLine("Crawled Pages: ");
            foreach (string str in m_CrawledPages)
                Console.WriteLine(str);
            Console.WriteLine("**************************************");
        }
    }
}
