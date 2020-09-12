using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoFolioWorkerService.Data
{
    public class CoinData
    {
        public string symbol { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string rank { get; set; }
        [JsonPropertyName("1d")]
        public oned oneD { get; set; }
    }

    public class oned
    {
        public string volume { get; set; }
        public string price_change { get; set; }
        public string price_change_pct { get; set; }
        public string volume_change { get; set; }
        public string volume_change_pct { get; set; }
        public string market_cap_change { get; set; }
        public string market_cap_change_pct { get; set; }
    }
}