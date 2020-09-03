using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolio.Models.Entities
{
    public class Wallet
    {
        public int Id { get; set; }
        public ICollection<WalletCoin> Walletcoins { get; set; }
    }
}
