using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;

namespace SD2API.Startup
{
    
    public static class StartupConfiguration
    {
        private static string _cachedApiDbConnectionString = null;

        public static string DatabaseConnectionString
        {
            get
            {
                if (_cachedApiDbConnectionString != null)
                    return _cachedApiDbConnectionString;

                _cachedApiDbConnectionString = Environment.GetEnvironmentVariable("ApiDbConnectionString");
                if (_cachedApiDbConnectionString != null)
                    return _cachedApiDbConnectionString;

                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                var keyVaultClient = new KeyVaultClient(
                    new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                var secret = keyVaultClient.GetSecretAsync(
                    Environment.GetEnvironmentVariable("ApiDbConnectionStringUri"))
                    .Result;

                _cachedApiDbConnectionString = secret.Value;
                return _cachedApiDbConnectionString;
            }
        }
    }
}
