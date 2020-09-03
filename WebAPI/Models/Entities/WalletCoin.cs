using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolio.Models.Entities
{
    public class WalletCoin
    {
        public int Id { get; set; }
        public virtual Coin Coin { get; set; }
        public decimal ValueWhenBought { get; set; }
        public DateTime BuyDate { get; set; }
    }
}
