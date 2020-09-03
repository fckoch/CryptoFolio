using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolio.Models.Entities
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public WalletCoin Walletcoin { get; set; }
    }
}
