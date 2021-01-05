using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strata.Exchange.ForexService
{
    public class PrefixKeyVaultSecretManager : IKeyVaultSecretManager
    {
        private readonly string prefix;

        public PrefixKeyVaultSecretManager(string prefix)
        {
            this.prefix = string.IsNullOrEmpty(prefix) ? string.Empty : $"{prefix}-";
        }

        public string GetKey(SecretBundle secret)
        {
            return secret.SecretIdentifier.Name
             .Substring(this.prefix.Length)
             .Replace("--", ConfigurationPath.KeyDelimiter);
        }

        public bool Load(SecretItem secret)
        {
            return secret.Identifier.Name.StartsWith(this.prefix);
        }
    }
}
