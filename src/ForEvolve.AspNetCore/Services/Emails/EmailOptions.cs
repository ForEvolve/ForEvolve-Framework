using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ForEvolve.AspNetCore.Services
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
}
