using Microsoft.ServiceFabric.Services.Remoting;
using Strata.Exchange.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strata.Exchange.ServiceFabric
{
    public interface IForexService : IService
    {
        Task<SupportedCurrenciesResponse> GetSupportedCurrenciesAsync(ForexServiceContext ctx);
        Task<ForexDataResponse> GetLiveForexData(ForexServiceContext ctx, string source);

    }
}
