using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Strata.Exchange.Abstractions;
using Strata.Exchange.CurrencyLayer.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strata.Exchange.ForexService
{
    public static class ForexServiceDependencyCollection
    {
        public static void AddForexService(this IServiceCollection collection, IConfigurationRoot configuration)
        {
            collection.Configure<ForexServiceOptions>(configuration.GetSection("Strata:Exchange:ForexService"));
            collection.AddTransient<ForexServiceInstance>();
            collection.AddTransient<ICurrencyLayerClient, CurrencyLayerClient>();
        }
    }
}
