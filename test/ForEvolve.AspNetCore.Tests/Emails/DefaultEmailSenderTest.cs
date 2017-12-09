using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace ForEvolve.AspNetCore.Emails
{
    public class DefaultEmailSenderTest
    {
        private readonly DefaultEmailSender _senderUnderTest;
        private readonly EmailOptions _emailOptions;

        public DefaultEmailSenderTest()
        {
            _emailOptions = new EmailOptions
            {
                EmailType = EmailType.PlainText,
                SenderEmailAddress = "some-sender@email.com"
            };

            _senderUnderTest = new DefaultEmailSender(_emailOptions);
        }

        // Integration tests
        public class SendEmailAsync : DefaultEmailSenderTest
        {
            [Fact]
            public async System.Threading.Tasks.Task Should_write_file_when_using_SpecifiedPickupDirectory()
            {
                // Arrange
                var pickupDirectoryLocation = Path.Combine(
                    Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                    nameof(Should_write_file_when_using_SpecifiedPickupDirectory)
                );
                CreateAndCleanDirectory(pickupDirectoryLocation);
                _emailOptions.Smtp = new SmtpClientOptions
                {
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory,
                    PickupDirectoryLocation = pickupDirectoryLocation,
                };
                var email = "some@email.com";
                var subject = "Some email subject";
                var message = "Some message";

                // Act
                await _senderUnderTest.SendEmailAsync(email, subject, message);

                // Assert
                var files = Directory.GetFiles(pickupDirectoryLocation);
                Assert.Collection(files,
                    f => Assert.EndsWith(".eml", f)
                );
            }

            private static void CreateAndCleanDirectory(string pickupDirectoryLocation)
            {
                if (!Directory.Exists(pickupDirectoryLocation))
                {
                    Directory.CreateDirectory(pickupDirectoryLocation);
                }
                var files = Directory.GetFiles(pickupDirectoryLocation);
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
        }
    }
}
