using Microsoft.Extensions.Options;
using Strata.Exchange.Abstractions;
using Strata.Exchange.CurrencyLayer.Client;
using Strata.Exchange.ServiceFabric;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strata.Exchange.ForexService
{
    public class ForexServiceInstance : IForexService
    {
        private readonly ICurrencyLayerClient _currencyLayerClient;

        private readonly ForexServiceOptions _options;

        public ForexServiceInstance(
            ICurrencyLayerClient currencyLayerClient,
            IOptions<ForexServiceOptions> options)
        {
            this._currencyLayerClient = currencyLayerClient ?? throw new ArgumentNullException(nameof(currencyLayerClient));
            this._options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<ForexDataResponse> GetLiveForexData(ForexServiceContext ctx, string source)
        {
            var accessKey = this._options.AccessKey;
            var liveData = await this._currencyLayerClient.GetLiveForexData(accessKey, source);
            
            //use Automapper here

            return new ForexDataResponse()
            {
                Quotes = liveData.Quotes,
                Success = liveData.Success,
                ErrorMsg = liveData?.Error?.Info,
                RatesAt = liveData.CurrentDateTime
            };
        }

        public async Task<SupportedCurrenciesResponse> GetSupportedCurrenciesAsync(ForexServiceContext ctx)
        {
            //Get Access Key from vault;
            var accessKey = this._options.AccessKey;

            var supportedCurrencies = await this._currencyLayerClient.GetSupportedCurrenciesAsync(accessKey);
            return new SupportedCurrenciesResponse()
            {
                Currencies = supportedCurrencies.Currencies,
                Success = supportedCurrencies.Success,
                ErrorMsg = supportedCurrencies?.Error?.Info
            };
        }
    }
}
