using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _491Parser
{
    class Page
    {
        private string m_URL = "";
        private string m_Title = "";
        private string m_HTML = "";
        private List<string> m_LinksToOtherPages = new List<string>();
        private string DownloadHTMLFile(string URL)
        {
            TimeoutWebClient client = new TimeoutWebClient(1);
            try
            {
                return client.DownloadString(URL);
            }
            catch (Exception e)
            {
                Database.NewInvalidURL(URL);

                if (Program.Log_Verbosity >= 1)
                    Console.WriteLine("Error while downloading the webpage: " + e.Message);
                return "";
            }
        }
        private string GetPageTitle(string HTML)
        {
            MatchCollection matches = Regex.Matches(HTML, @"<title>(?<title>.*)</title>");
            if (matches.Count != 0)
                return matches[0].Groups["title"].Value.ToString();
            else
                return null;
        }
        private void FindKeywords()
        {
            string[] keywords = Regex.Split(m_HTML, @"\W+");
            foreach(string keyword in keywords)
            {
                if(keyword.Length > 1)
                {
                    Database.NewKeywordOnPage(this.m_URL, keyword);
                }
            }
        }
        private void FindLinksToOtherPages()
        {
            MatchCollection matches = Regex.Matches(this.m_HTML, @"<a.*?href=(""|')(?<href>.*?)(""|').*?>(?<value>.*?)</a>");
            foreach (Match m in matches)
            {
                string linkURL = m.Groups["href"].Value.ToString();
                linkURL = Utilities.ProcessURL(this.m_URL, linkURL);
                if (Utilities.IsLinkValid(linkURL))
                {
                    string tempHTML = DownloadHTMLFile(linkURL);
                    string tempTitle = GetPageTitle(tempHTML);
                    Database.NewPageDiscovered(linkURL, tempTitle);
                    Database.NewLinkBetweenPages(this.m_URL, linkURL);
                    this.m_LinksToOtherPages.Add(linkURL);
                }
            }

            Environment.m_CrawledPages.Add(this.m_URL);

            if (Program.Log_Verbosity >= 2)
            {
                Console.WriteLine("**************************************");
                Console.WriteLine(this.m_URL + " has the following links <" + m_LinksToOtherPages.Count + ">:\n");
                foreach (string str in this.m_LinksToOtherPages)
                    Console.WriteLine(str);
                Console.WriteLine("**************************************");
            }
        }
        public Page(string URL)
        {
            this.m_URL = URL;
            if (!Utilities.IsLinkValid(URL))
                return;
            m_HTML = DownloadHTMLFile(this.m_URL);
            m_Title = GetPageTitle(this.m_HTML);
            Database.NewPageDiscovered(this.m_URL, this.m_Title);
            Database.SaveHTMLToDatabase(this.m_URL, this.m_HTML);
            if(Environment.m_CrawledPages.Contains(this.m_URL))
            {
                Console.WriteLine("Already crawled: " + this.m_URL);
                return;
            }
            Console.WriteLine("Crawling: " + this.m_URL);
            FindKeywords();
            FindLinksToOtherPages();
        }
        public void CrawlRest()
        {
            foreach (string str in this.m_LinksToOtherPages)
            {
                Page p = new Page(str);
                foreach(string str2 in p.m_LinksToOtherPages)
                {
                    Page p2 = new Page(str2);
                }
            }
        }
    }
}
