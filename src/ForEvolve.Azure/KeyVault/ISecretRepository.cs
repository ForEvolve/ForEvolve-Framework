using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.KeyVault
{
    public interface ISecretsRepository
    {
        Task<string> GetSecretAsync(string secretName);
        Task<string> GetSecretAsync(string secretName, string secretVersion);
    }

}
