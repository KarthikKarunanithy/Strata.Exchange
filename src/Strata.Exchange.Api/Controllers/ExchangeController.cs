using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strata.Exchange.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strata.Exchange.Api.Controllers
{
    [Route("v1.0/exchange")]
    [Authorize]
    public class ExchangeController : Controller
    {
        private readonly IForexServiceClient _forexClient;
        public ExchangeController(IForexServiceClient forexClient)
        {
            this._forexClient = forexClient ?? throw new ArgumentNullException(nameof(forexClient));
        }

        [HttpGet("{tenantId}/supported-currency")]
        public async Task<SupportedCurrenciesResponse> GetSupportedCurrenciesAsync(string tenantId)
        {
            var ctx = new ForexServiceContext()
            {
                CorrelationId = HttpContext.TraceIdentifier,
                TenantId = tenantId
            };

            return await this._forexClient.GetSupportedCurrenciesAsync(ctx);
        }

        [HttpGet("{tenantId}/rates")]
        public async Task<ForexDataResponse> GetLiveForexRates(string tenantId, [FromQuery]string source)
        {
            var ctx = new ForexServiceContext()
            {
                CorrelationId = HttpContext.TraceIdentifier,
                TenantId = tenantId
            };

            return await this._forexClient.GetLiveForexData(ctx, source);
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public string TestMethod()
        {
            return "Test method";
        }

   }
}
