using System;

namespace Strata.Exchange.ForexService.Client
{
    public class ForexServiceClientOptions
    {
        public Uri ForexServiceUrl { get; set; } = new Uri("fabric:/Strata.Exchange/ForexService");
    }
}