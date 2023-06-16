using System.Text.RegularExpressions;

namespace KL
{
    public class Parser
    {
        private HttpClient _httpClient;
        private string _htmlBody;
        private Patent _patent;

        //public static Regex AbstractReg = new Regex("<div( num=\"\\d+\"){0,1} class=\"abstract\">(.||\\s)+?<\\/div>");
        //public static Regex DescriptionReg = new Regex("<div num=\"\\d+\" class=\"description-paragraph\">.+<\\/div>");
        //public static Regex ClaimsReg = new Regex("<div class=\"claim-text\">.+\n{0,1}<\\/div>");
        public static Regex AbstractReg = new Regex("<section itemprop=\"abstract\" itemscope>(.||\\s)+?</section>");
        public static Regex DescriptionReg = new Regex("<section itemprop=\"description\" itemscope>(.||\\s)+?</section>");
        public static Regex ClaimsReg = new Regex("<section itemprop=\"claims\" itemscope>(.||\\s)+?</section>");

        public string Url { get; set; }
        public string HtmlBody { get { return _htmlBody; } }
        public Patent Patent { get { return _patent; } }

        public Parser()
        {
            _httpClient = new HttpClient();
            _patent = new Patent();
        }

        public Parser(string url)
        {
            Url = url;
            _httpClient = new HttpClient();
            _patent = new Patent();
        }

        public void GetHtmlBody()
        {
            var response = _httpClient.GetStringAsync(Url);

            _htmlBody = response.Result;
        }

        //public void ParseHtmlBody()
        //{
        //    var abs = AbstractReg.Match(HtmlBody);
        //    var descriptions = DescriptionReg.Matches(HtmlBody);
        //    var claims = ClaimsReg.Matches(HtmlBody);

        //    _patent.Abstract = Regex.Replace(Regex.Replace(abs.Value, "<div( num=\"\\d+\"){0,1} class=\"abstract\">", ""), "<\\/div>", "");

        //    foreach (var desc in descriptions)
        //    {
        //        _patent.Description += Regex.Replace(Regex.Replace(desc.ToString(), "<div num=\"\\d+\" class=\"description-paragraph\">", ""), "<\\/div>", "");
        //        _patent.Description += " ";
        //    }

        //    foreach (var cl in claims)
        //    {
        //        _patent.Claims += Regex.Replace(Regex.Replace(cl.ToString(), "<div class=\"claim-text\">", ""), "<\\/div>", "");
        //        _patent.Claims += " ";
        //    }
        //}

        public void ParseHtmlBody()
        {
            var abs = AbstractReg.Match(HtmlBody);
            var description = DescriptionReg.Match(HtmlBody);
            var claim = ClaimsReg.Match(HtmlBody);

            _patent.Abstract = Regex.Replace(Regex.Replace(abs.Value, "<(.||\\s)+?>", ""), "\n ", "");
            _patent.Abstract = Regex.Replace(_patent.Abstract, "\\s*Abstract\\s*", "");

            _patent.Description = Regex.Replace(Regex.Replace(description.Value, "<(.||\\s)+?>", ""), "\n ", "");
            _patent.Description = Regex.Replace(_patent.Description, "\\s*Description\\s*", "");        

            _patent.Claims = Regex.Replace(Regex.Replace(claim.Value, "<(.||\\s)+?>", ""), "\n ", "");
            _patent.Claims = Regex.Replace(_patent.Claims, "\\s*Claims \\(\\d+\\)\\s*", "");
        }
    }
}
