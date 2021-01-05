using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strata.Exchange.CurrencyLayer.Client.CurrencyLayer
{

    public class ErrorMessage
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        
        [JsonProperty("error")]
        public Error Error { get; set; }
    }

    public class Error
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("type")]
        public string @Type { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }
    }

}
