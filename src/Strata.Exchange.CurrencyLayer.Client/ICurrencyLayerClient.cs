using System.Threading.Tasks;

namespace Strata.Exchange.CurrencyLayer.Client
{
    public interface ICurrencyLayerClient
    {
        Task<SupportedCurrencies> GetSupportedCurrenciesAsync(string accessKey);

        Task<ForexLiveData> GetLiveForexData(string accessKey, string source);
    }
}