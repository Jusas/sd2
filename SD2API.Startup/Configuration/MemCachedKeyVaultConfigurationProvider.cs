using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;

namespace SD2API.Startup.Configuration
{
    class MemCachedKeyVaultConfigurationProvider : ConfigurationProvider
    {
        private Func<string> _getKeyVaultUriAction;

        public MemCachedKeyVaultConfigurationProvider(Func<string> getKeyVaultUriAction)
        {
            _getKeyVaultUriAction = getKeyVaultUriAction;
        }

        public override void Load()
        {
            var keyVaultUri = _getKeyVaultUriAction();
            if (string.IsNullOrEmpty(keyVaultUri))
            {
                Data = new Dictionary<string, string>();
                return;
            }

            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            var secretList = keyVaultClient.GetSecretsAsync(keyVaultUri).Result.ToList();
            Data = new Dictionary<string, string>();
            secretList.ForEach(s => Data.Add(Grouped(s.Identifier.Name), keyVaultClient.GetSecretAsync(s.Id).Result.Value));
        }

        private string Grouped(string identifier)
        {
            return identifier.Replace("--", ConfigurationPath.KeyDelimiter);
        }
    }
}
