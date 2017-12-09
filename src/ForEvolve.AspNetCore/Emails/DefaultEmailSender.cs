using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ForEvolve.AspNetCore.Emails
{
    public class DefaultEmailSender : IEmailSender
    {
        private readonly EmailOptions _emailOptions;
        public DefaultEmailSender(EmailOptions emailOptions)
        {
            _emailOptions = emailOptions ?? throw new ArgumentNullException(nameof(emailOptions));
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                _emailOptions.SetupSmtpClient(smtp);

                using (MailMessage msg = new MailMessage(_emailOptions.SenderEmailAddress, email))
                {
                    msg.Subject = subject;
                    switch (_emailOptions.EmailType)
                    {
                        case EmailType.Both:
                            var convertedBody = ConvertToPlainText(message);
                            if (!string.IsNullOrWhiteSpace(convertedBody))
                            {
                                // HTML version
                                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(
                                    message,
                                    null,
                                    "text/html"
                                ));
                                // Plain text version
                                var decodedBody = HttpUtility.HtmlDecode(convertedBody); // Fix: &amp; in urls (anyway its plain-text)
                                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(
                                    decodedBody,
                                    null,
                                    "text/plain"
                                ));
                            }
                            else
                            {
                                msg.Body = message;
                                msg.IsBodyHtml = true;
                            }
                            break;
                        case EmailType.PlainText:
                        case EmailType.Html:
                            msg.Body = message;
                            msg.IsBodyHtml = _emailOptions.EmailType == EmailType.Html;
                            break;
                    }
                    await smtp.SendMailAsync(msg);
                }
            }
        }

        private string ConvertToPlainText(string body)
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

    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
