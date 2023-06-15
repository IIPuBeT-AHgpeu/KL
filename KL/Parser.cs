using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KL
{
    public class Parser
    {
        private HttpClient _httpClient;
        private string _htmlBody;

        public string Url { get; set; }
        public string HtmlBody { get { return _htmlBody; } }

        public Parser()
        {
            _httpClient = new HttpClient();
        }

        public Parser(string url)
        {
            Url = url;
            _httpClient = new HttpClient();
        }

        public void GetHtmlBody()
        {
            var response = _httpClient.GetStringAsync(Url);

            _htmlBody = response.Result;
        }
    }
}
