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
    public interface IKeyVaultSettings
    {
        string ClientId { get; }
        string ClientSecret { get; }
        string BaseVaultUrl { get; }
    }
}