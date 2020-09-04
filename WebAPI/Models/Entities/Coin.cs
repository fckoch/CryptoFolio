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
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal CurrentValue { get; set; }
    }
}
