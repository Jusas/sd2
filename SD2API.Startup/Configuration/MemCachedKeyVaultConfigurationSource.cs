using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SD2API.Startup.Configuration
{
    class MemCachedKeyVaultConfigurationSource : IConfigurationSource
    {
        private Func<string> _getKeyVaultUriAction;
        public MemCachedKeyVaultConfigurationSource(Func<string> getKeyVaultUriAction)
        {
            _getKeyVaultUriAction = getKeyVaultUriAction;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MemCachedKeyVaultConfigurationProvider(_getKeyVaultUriAction);
        }
    }
}
