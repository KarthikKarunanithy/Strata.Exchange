using Newtonsoft.Json;
using Strata.Exchange.CurrencyLayer.Client.CurrencyLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strata.Exchange.CurrencyLayer.Client
{

    public class SupportedCurrencies
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("terms")]
        public string Terms { get; set; }

        [JsonProperty("privacy")]
        public string Privacy { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }

        [JsonProperty("currencies")]
        public Dictionary<string, string> Currencies { get; set; }
    }
}
