using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strata.Exchange.Abstractions
{
    public class ForexDataResponse : CurrencyLayerResponse
    {      
        [JsonProperty("ratesAt")]
        public DateTime RatesAt { get; set; }

        [JsonProperty("quotes")]
        public Dictionary<string, double> Quotes { get; set; }

    }
}
