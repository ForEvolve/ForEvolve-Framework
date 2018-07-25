using System.Net;
using System.Net.Mail;

namespace ForEvolve.AspNetCore.Services
{
    public class SmtpClientOptions
    {
        public SmtpClientOptions()
        {
            CredentialOptions = new EmailCredentialOptions();
        }
        public EmailCredentialOptions CredentialOptions { get; set; }
        public bool? UseDefaultCredentials { get; set; }
        public int? Timeout { get; set; }
        public string TargetName { get; set; }
        public int? Port { get; set; }
        public string PickupDirectoryLocation { get; set; }
        public string Host { get; set; }
        public bool? EnableSsl { get; set; }
        public SmtpDeliveryMethod? DeliveryMethod { get; set; }
        public SmtpDeliveryFormat? DeliveryFormat { get; set; }

        public ICredentialsByHost Credentials => CredentialOptions.AsNetworkCredential;
    }
}
