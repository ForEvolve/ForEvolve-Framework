using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.KeyVault
{
    public class KeyVaultSettings : IKeyVaultSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string BaseVaultUrl { get; set; }
    }
}