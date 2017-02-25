using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _491Parser
{
    public class TimeoutWebClient : WebClient
    {
        public int Timeout { get; set; }
        //Default timeout is 1 second
        public TimeoutWebClient() : this(1) { }
        public TimeoutWebClient(int seconds)
        {
            this.Encoding = Encoding.UTF8;
            this.Timeout = seconds * 1000;
        }
        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request != null)
            {
                request.Timeout = this.Timeout;
            }
            return request;
        }
    }
}
