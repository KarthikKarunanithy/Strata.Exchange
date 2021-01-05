using Strata.Exchange.CurrencyLayer.Client.CurrencyLayer;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace Strata.Exchange.CurrencyLayer.Client
{
    public class CurrencyLayerClient : ICurrencyLayerClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly HttpClient _currencyLayerClient;

        public CurrencyLayerClient(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this._currencyLayerClient = this._httpClientFactory.CreateClient();
        }

        public async Task<SupportedCurrencies> GetSupportedCurrenciesAsync(string accessKey)
        {
            
            var url = this.GetSupportedCurrencyUrl(accessKey);

            var reqMessage = new HttpRequestMessage(
                HttpMethod.Get,
                url
                );
            reqMessage.Headers.Add("User-Agent", "Strata-Exchange");

            var response = await this._currencyLayerClient.SendAsync(reqMessage);
            var resp = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                //Even when there is error with accessKey Status code returns with 200

                var supportedCurr = JsonConvert.DeserializeObject<SupportedCurrencies>(resp);
                if(!supportedCurr.Success)
                {
                    supportedCurr.Error = JsonConvert.DeserializeObject<ErrorMessage>(resp)?.Error;
                }

                return supportedCurr;
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorMessage>(resp);
                return new SupportedCurrencies()
                {
                    Error = error.Error,
                    Success = error.Success
                };
            }
        }



        private string GetSupportedCurrencyUrl(string accessKey)
        {
            return $"http://{CurrencyLayerApiEndpoints.SupportedCurrencies}?access_key={accessKey}&format=1";
        }

        private string GetCurrencyLiveRatesUrl(string accessKey, string sourceCurrency)
        {
            return $"http://{CurrencyLayerApiEndpoints.LiveRates}?access_key={accessKey}&format=1&source={sourceCurrency}";
        }

        public async Task<ForexLiveData> GetLiveForexData(string accessKey, string source)
        {
            var url = this.GetCurrencyLiveRatesUrl(accessKey, source);

            var reqMessage = new HttpRequestMessage(
                HttpMethod.Get,
                url
                );
            reqMessage.Headers.Add("User-Agent", "Strata-Exchange");

            var response = await this._currencyLayerClient.SendAsync(reqMessage);
            var resp = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                //Even when there is error with accessKey Status code returns with 200

                var liveData = JsonConvert.DeserializeObject<ForexLiveData>(resp);
                if (!liveData.Success)
                {
                    liveData.Error = JsonConvert.DeserializeObject<ErrorMessage>(resp)?.Error;
                }

                return liveData;
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorMessage>(resp);
                return new ForexLiveData()
                {
                    Error = error.Error,
                    Success = error.Success
                };
            }
        }
    }
}
