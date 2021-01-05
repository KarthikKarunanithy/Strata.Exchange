using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strata.Exchange.Abstractions
{
    public class SupportedCurrenciesResponse : CurrencyLayerResponse
    {
        [JsonProperty("currencies")]
        public Dictionary<string, string> Currencies { get; set; }
    }
}
