using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _491Parser
{
    static class Utilities
    {
        public static bool IsLinkValid(string link)
        {
            //Ignore empty
            if (String.IsNullOrWhiteSpace(link))
                return false;
            //Ignore executables
            if (link.Contains(".exe"))
                return false;
            //Ignore self and javascript
            if (link == "/" || link == "javascript:;")
                return false;
            //Ignore mail
            if (link.StartsWith("mailto"))
                return false;
            //Rest is OK
            return true;
        }
        public static string ProcessURL(string pageURL, string linkURL)
        {
            if (IsLinkValid(linkURL))
            {
                string original = linkURL;
                if (linkURL[0] == '/')
                {
                    linkURL = linkURL.Insert(0, pageURL);
                }
                else if (!linkURL.StartsWith("http"))
                {
                    linkURL = linkURL.Insert(0, pageURL + "/");
                }
                if (Program.Log_Verbosity >= 2)
                    Console.WriteLine("URL processed from " + original + " to " + linkURL);
                return linkURL;
            }
            else
            {
                Database.NewInvalidURL(linkURL);
                if (Program.Log_Verbosity >= 2)
                    Console.WriteLine("Invalid URL: " + linkURL);
                return "";
            }
        }
    }
}
