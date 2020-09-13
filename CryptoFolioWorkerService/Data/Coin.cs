using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolioWorkerService
{
    public class Coin
    {
        public int CoinId { get; set; }
        public string CoinName { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal CurrentValue { get; set; }
        public string Symbol { get; set; }
        public int Rank { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price_change { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price_change_pct { get; set; }
    }
}
