using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoFolioWorkerService.Data
{
    public class CoinData
    {
        public string symbol { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string rank { get; set; }
        [JsonProperty("1d")]
        public oned oneD { get; set; }
    }

    public class oned
    {
        public string price_change { get; set; }
        public string price_change_pct { get; set; }
    }
}