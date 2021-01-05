using Newtonsoft.Json;
using Strata.Exchange.CurrencyLayer.Client.CurrencyLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strata.Exchange.CurrencyLayer.Client
{
    public class ForexLiveData
    {

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("terms")]
        public string Terms { get; set; }

        [JsonProperty("privacy")]
        public string Privacy { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("timestamp")]
        public long TimeStamp { get; set; }

        [JsonProperty("currentDate")]
        public DateTime CurrentDateTime
        {
            get
            {
                return DateTimeOffset.FromUnixTimeSeconds(this.TimeStamp).UtcDateTime;
            }
        }

        [JsonProperty("error")]
        public Error Error { get; set; }

        [JsonProperty("quotes")]
        public Dictionary<string, double> Quotes { get; set; }

    }
}
