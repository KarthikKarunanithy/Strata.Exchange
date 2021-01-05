using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strata.Exchange.Abstractions
{
    public class CurrencyLayerResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error")]
        public string ErrorMsg { get; set; }
    }
}
