using Microsoft.ServiceFabric.Services.Remoting.Client;
using Strata.Exchange.Abstractions;
using Strata.Exchange.ServiceFabric;
using System;
using System.Threading.Tasks;

namespace Strata.Exchange.ForexService.Client
{
    public class ForexServiceClient : IForexServiceClient
    {
        private readonly ForexServiceClientOptions _options;
        
        private readonly IForexService _forexService;

        public ForexServiceClient(ForexServiceClientOptions options)
        {
            this._options = options ?? throw new ArgumentNullException(nameof(options));
            this._forexService = ServiceProxy.Create<IForexService>(options.ForexServiceUrl);
        }

        public async Task<ForexDataResponse> GetLiveForexData(ForexServiceContext ctx, string source)
        {
            return await this._forexService.GetLiveForexData(ctx, source);
        }

        public async Task<SupportedCurrenciesResponse> GetSupportedCurrenciesAsync(ForexServiceContext ctx)
        {
            return await this._forexService.GetSupportedCurrenciesAsync(ctx);
        }
    }
}
