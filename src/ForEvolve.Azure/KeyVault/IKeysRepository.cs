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
    public interface IKeysRepository
    {
        Task<string> GetKeyAsync(string keyName);
        Task<string> GetKeyAsync(string keyName, string keyVersion);
    }
}