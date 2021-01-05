using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Strata.Exchange.ServiceFabric;
using Strata.Exchange.Abstractions;
using Strata.Exchange.CurrencyLayer.Client;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace Strata.Exchange.ForexService
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class ForexService : StatelessService, IForexService
    {
        
        public ForexService(StatelessServiceContext context)
            : base(context)
        {
        }

        internal IServiceProvider ServiceProvider { get; set; }

        internal IConfigurationRoot Configuration { get; set; }

        internal ForexServiceInstance Instance
        {
            get
            {
                return this.ServiceProvider.GetRequiredService<ForexServiceInstance>();
            }
        }

        public async Task<ForexDataResponse> GetLiveForexData(ForexServiceContext ctx, string source)
        {
            return await this.Instance.GetLiveForexData(ctx, source);
        }

        public async Task<SupportedCurrenciesResponse> GetSupportedCurrenciesAsync(ForexServiceContext ctx)
        {
            return await this.Instance.GetSupportedCurrenciesAsync(ctx);
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            this.Initialise();
            return this.CreateServiceRemotingInstanceListeners();
        }

        private void Initialise()
        {
            var service = new ServiceCollection();
            var builder = new ConfigurationBuilder();
            var appTokenProvider = new AzureServiceTokenProvider();

            var keyvaultEndpoint = "KEYVAULT_NAME"; //Read from environment

            var appAuthCallback = new KeyVaultClient.AuthenticationCallback(appTokenProvider.KeyVaultTokenCallback);
            var keyVaultClient = new KeyVaultClient(appAuthCallback);
            builder.AddAzureKeyVault(keyvaultEndpoint, keyVaultClient, new PrefixKeyVaultSecretManager(string.Empty));
            this.Configuration = builder.Build();

            service.AddSingleton(this.Configuration);
            service.AddHttpClient<ICurrencyLayerClient, CurrencyLayerClient>();
            service.AddOptions();
            service.AddForexService(this.Configuration);
            this.ServiceProvider = service.BuildServiceProvider();
        }
    }
}
