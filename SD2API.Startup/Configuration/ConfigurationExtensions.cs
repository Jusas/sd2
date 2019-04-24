using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SD2API.Startup.Configuration
{
    static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddMemCachedKeyVaultConfiguration(this IConfigurationBuilder builder,
            Func<string> keyVaultUriAction)
        {
            return builder.Add(new MemCachedKeyVaultConfigurationSource(keyVaultUriAction));
        }
    }
}
