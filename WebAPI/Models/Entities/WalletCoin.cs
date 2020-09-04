using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolioAPI.Models.Entities
{
    public class WalletCoin
    {
        public int WalletCoinId { get; set; }
        public virtual Coin Coin { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal ValueWhenBought { get; set; }
        public DateTime BuyDate { get; set; }
    }
}
