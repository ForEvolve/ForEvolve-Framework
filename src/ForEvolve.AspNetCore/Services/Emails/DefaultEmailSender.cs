﻿using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace ForEvolve.AspNetCore.Services
{
    public class DefaultEmailSenderService : IEmailSenderService
    {
        private readonly EmailOptions _emailOptions;
        private readonly IHtmlToPlainTextEmailBodyConverter _htmlToPlainTextEmailBodyConverter;

        public DefaultEmailSenderService(IHtmlToPlainTextEmailBodyConverter htmlToPlainTextEmailBodyConverter, EmailOptions emailOptions)
        {
            _htmlToPlainTextEmailBodyConverter = htmlToPlainTextEmailBodyConverter ?? throw new ArgumentNullException(nameof(htmlToPlainTextEmailBodyConverter));
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
                            var convertedBody = _htmlToPlainTextEmailBodyConverter.ConvertToPlainText(message);
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
    }
}
