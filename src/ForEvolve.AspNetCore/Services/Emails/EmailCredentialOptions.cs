﻿using System.Net;

namespace ForEvolve.AspNetCore.Services
{
    public class EmailCredentialOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }

        public NetworkCredential AsNetworkCredential => 
            HasCredentials() ? 
            new NetworkCredential(UserName, Password, Domain) : 
            new NetworkCredential();

        public bool HasCredentials()
        {
            var hasUserName = !string.IsNullOrWhiteSpace(UserName);
            var hasPassword = !string.IsNullOrWhiteSpace(Password);
            return hasUserName && hasPassword;
        }
    }
}
