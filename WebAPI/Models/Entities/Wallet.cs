using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolioAPI.Models.Entities
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public virtual ICollection<WalletCoin> Walletcoins { get; set; }
    }
}
