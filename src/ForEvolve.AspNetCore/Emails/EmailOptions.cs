using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ForEvolve.AspNetCore.Emails
{
    public class EmailOptions
    {
        public string SenderEmailAddress { get; set; }
        public EmailType EmailType { get; set; }
        public SmtpClientOptions Smtp { get; set; }

        public void SetupSmtpClient(SmtpClient client)
        {
            if (Smtp == null) { return; }
            client.Credentials = Smtp.Credentials;
            client.UseDefaultCredentials = Smtp.UseDefaultCredentials.GetValueOrDefault(client.UseDefaultCredentials);
            client.Timeout = Smtp.Timeout.GetValueOrDefault(client.Timeout);
            client.TargetName = Smtp.TargetName ?? client.TargetName;
            client.Port = Smtp.Port.GetValueOrDefault(client.Port);
            client.PickupDirectoryLocation = Smtp.PickupDirectoryLocation ?? client.PickupDirectoryLocation;
            if (!string.IsNullOrWhiteSpace(Smtp.Host)) { client.Host = Smtp.Host; }
            client.EnableSsl = Smtp.EnableSsl.GetValueOrDefault(client.EnableSsl);
            client.DeliveryMethod = Smtp.DeliveryMethod.GetValueOrDefault(client.DeliveryMethod);
            client.DeliveryFormat = Smtp.DeliveryFormat.GetValueOrDefault(client.DeliveryFormat);
        }
    }


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

    public class EmailCredentialOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public NetworkCredential AsNetworkCredential => 
            HasCredentials() ? 
            new NetworkCredential(UserName, Password) : 
            new NetworkCredential();

        public bool HasCredentials()
        {
            var hasUserName = !string.IsNullOrWhiteSpace(UserName);
            var hasPassword = !string.IsNullOrWhiteSpace(Password);
            return hasUserName && hasPassword;
        }
    }

    public enum EmailType
    {
        PlainText,
        Html,
        Both
    }
}
