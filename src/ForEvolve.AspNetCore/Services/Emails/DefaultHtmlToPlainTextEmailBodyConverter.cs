using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ForEvolve.AspNetCore.Services
{
    public class DefaultHtmlToPlainTextEmailBodyConverter : IHtmlToPlainTextEmailBodyConverter
    {
        public string ConvertToPlainText(string body)
        {
            var regexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant;
            var regexList = new[]
            {
                new { Expression = @"^[\s\S\n.]*\<body([^>]*)?>", ReplaceBy = "" }, // start to <body>
                new { Expression = @"</body>[\s\S\n.]*$[\r\n]*", ReplaceBy ="" },   // </body> to end
                new {                                                               // Replace A[HREF]
                    Expression = @"[<a[^>]*[\s\S]?href=""(?<href>[^""]*)""(?:[^>]*)>(?<text>([\s\S](?!<\/a>))*[\s\S]?)<\/a>",
                    ReplaceBy = "${text} [${href}]"
                },
                new { Expression = "<[^>]+>", ReplaceBy = "" },                     // all tags
                new { Expression = @"^\s*$[\r\n]*", ReplaceBy = "" }                // trim empty lines
            };
            var bodyText = body;
            foreach (var regex in regexList)
            {
                bodyText = Regex.Replace(bodyText, regex.Expression, regex.ReplaceBy, regexOptions);
            }
            return TrimLines(bodyText);
        }

        private string TrimLines(string input)
        {
            var sb = new StringBuilder();
            var lines = input.Split(
                new string[] { "\r\n", "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries
            );
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    sb.AppendLine(line.Trim(' ', '\t'));
                }
            }
            return sb.ToString();
        }
    }
}
