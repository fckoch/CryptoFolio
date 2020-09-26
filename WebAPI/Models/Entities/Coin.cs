using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolioAPI.Models.Entities
{
    public class Coin
    {
        public int CoinId { get; set; }
        public string CoinName { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal CurrentValue { get; set; }
        public string Symbol { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PriceChange { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PriceChangePct { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal MarketCap { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal AllTimeHigh { get; set; }
    }
}
