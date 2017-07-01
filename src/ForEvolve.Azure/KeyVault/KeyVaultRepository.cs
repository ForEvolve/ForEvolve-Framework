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
    public class KeyVaultRepository : ISecretsRepository, IKeysRepository
    {
        private readonly IKeyVaultSettings _keyVaultSettings;
        public KeyVaultRepository(IKeyVaultSettings keyVaultSettings)
        {
            _keyVaultSettings = keyVaultSettings ?? throw new ArgumentNullException(nameof(keyVaultSettings));
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            var secretUri = CreateUri("secrets", secretName);
            var secret = await InternalGetSecretAsync(secretUri);
            return secret.Value;
        }

        public async Task<string> GetSecretAsync(string secretName, string secretVersion)
        {
            var secretUri = CreateUri("secrets", secretName, secretVersion);
            var secret = await InternalGetSecretAsync(secretUri);
            return secret.Value;
        }

        public async Task<string> GetKeyAsync(string keyName)
        {
            var key = await InternalGetKeyAsync(keyName);
            return key.Key.ToString();
        }

        public async Task<string> GetKeyAsync(string keyName, string keyVersion)
        {
            var key = await InternalGetKeyAsync(keyName, keyVersion);
            return key.Key.ToString();
        }

        private async Task<KeyBundle> InternalGetKeyAsync(string keyName, string keyVersion = null)
        {
            var client = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(AuthenticationCallback));
            if (keyVersion == null)
            {
                return await client.GetKeyAsync(_keyVaultSettings.BaseVaultUrl, keyName);
            }
            return await client.GetKeyAsync(_keyVaultSettings.BaseVaultUrl, keyName, keyVersion);
        }

        private async Task<SecretBundle> InternalGetSecretAsync(string secretUri)
        {
            var client = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(AuthenticationCallback));
            return await client.GetSecretAsync(secretUri);
        }

        private string CreateUri(string prefix, string secretName, string secretVersion = null)
        {
            var secretUri = $"{_keyVaultSettings.BaseVaultUrl}/{prefix}/{secretName}";
            if (secretVersion != null)
            {
                secretUri += $"/{secretVersion}";
            }
            return secretUri;
        }

        private async Task<string> AuthenticationCallback(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            var clientCred = new ClientCredential(
                _keyVaultSettings.ClientId,
                _keyVaultSettings.ClientSecret
            );
            var result = await authContext.AcquireTokenAsync(resource, clientCred);
            if (result == null)
            {
                throw new InvalidOperationException("Failed to obtain the JWT token");
            }

            return result.AccessToken;
        }
    }
}