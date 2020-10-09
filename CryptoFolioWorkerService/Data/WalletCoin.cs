using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolioWorkerService
{
    public class WalletCoin
    {
        [Required]
        public int WalletCoinId { get; set; }
        [Required]
        public virtual Coin Coin { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal ValueWhenBought { get; set; }
        public DateTime BuyDate { get; set; }
        public int WalletId { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
    }
}
