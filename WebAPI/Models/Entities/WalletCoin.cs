using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolio.Models.Entities
{
    public class WalletCoin
    {
        public Coin Bitcoin { get; set; }
        public Coin Ethereum { get; set; }
        public Coin Litecoin { get; set; }
    }
}
