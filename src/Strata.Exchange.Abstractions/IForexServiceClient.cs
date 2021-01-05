using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strata.Exchange.Abstractions
{
    public interface IForexServiceClient
    {
        Task<SupportedCurrenciesResponse> GetSupportedCurrenciesAsync(ForexServiceContext ctx);

        Task<ForexDataResponse> GetLiveForexData(ForexServiceContext ctx, string source);

    }
}
