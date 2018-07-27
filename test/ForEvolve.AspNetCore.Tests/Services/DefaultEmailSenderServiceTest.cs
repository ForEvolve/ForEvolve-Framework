using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace ForEvolve.AspNetCore.Services
{
    public class DefaultEmailSenderServiceTest
    {
        private readonly DefaultEmailSenderService _senderUnderTest;
        private readonly EmailOptions _emailOptions;
        private readonly Mock<IHtmlToPlainTextEmailBodyConverter> _htmlToPlainTextEmailBodyConverterMock;

        public DefaultEmailSenderServiceTest()
        {
            _htmlToPlainTextEmailBodyConverterMock = new Mock<IHtmlToPlainTextEmailBodyConverter>();
            _emailOptions = new EmailOptions
            {
                EmailType = EmailType.PlainText,
                SenderEmailAddress = "some-sender@email.com"
            };

            _senderUnderTest = new DefaultEmailSenderService(
                _htmlToPlainTextEmailBodyConverterMock.Object, 
                _emailOptions
            );
        }

        // Integration tests
        public class SendEmailAsync : DefaultEmailSenderServiceTest
        {
            [Fact]
            public async System.Threading.Tasks.Task Should_write_file_when_using_SpecifiedPickupDirectory()
            {
                // Arrange
                var pickupDirectoryLocation = Path.Combine(
                    Directory.GetCurrentDirectory(),
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
